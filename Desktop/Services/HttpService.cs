﻿using DTO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Collections.Generic;

namespace Desktop.Services
{
    public static class HttpService
    {
        private static HttpClient client = new HttpClient();
        private static HttpClientHandler handler = new HttpClientHandler();

        //TODO: config fájlból beolvasni
        //private static string baseUri = "https://localhost:5001/api/"; //Kristóf 
        private static string baseUri = "https://localhost:44376/api/"; //Gábor 

        private static string UriFlights;
        private static string UriReservation;
        private static string UriUsers;

        public static void Initialize()
        {
            //Ne változtasd meg, így működik
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
            UriFlights = baseUri + "flight/";
            UriReservation = baseUri + "reservation/";
            UriUsers = baseUri + "users/";
        }

        //Repülő hozzáadása OK
        public static async Task PostAddFlightAsync(Flight_DTO addRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriFlights, addRequest);
            var contents = await response.Content.ReadAsStringAsync();
            //Debug.WriteLine("A hozzáadott repülő típusa: " + addRequest.PlaneType.PlaneTypeName);
        }

        //Járatok listázása OK
        public static async Task<List<Flight_DTO>> PostListAsync(ListFlights_DTO listRequest=null)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.GetAsync(UriFlights);
            List<Flight_DTO> list = await response.Content.ReadAsAsync<List<Flight_DTO>>();
            return list;
        }

        //Járat törlése OK
        public static async Task PostDeleteFlightAsync(DeleteFlight_DTO deleteRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.DeleteAsync(UriFlights + deleteRequest.FlightId);
            var contents = await response.Content.ReadAsStringAsync();
        }

        //Járat módosítása OK
        public static async Task PostUpdateFlightAsync(UpdateFlight_DTO updateRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.PutAsJsonAsync(UriFlights + updateRequest.Flight.FlightId, updateRequest.Flight);
            var contents = await response.Content.ReadAsStringAsync();
            //Debug.WriteLine("A módosított repülő adatai: " + updateRequest.Flight.ToString());
        }

        //Foglalás hozzáadása
        public static async Task PostReservationAsync(ReserveSeat_DTO reserveRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriReservation, reserveRequest);
            var contents = await response.Content.ReadAsStringAsync();
            //Debug.WriteLine(contents);
        }

        //Bejelentkezési kérés
        public static async Task<bool> PostLoginAsync(Login_DTO loginRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriUsers, loginRequest);
            var contents = await response.Content.ReadAsAsync<bool>();
            Debug.WriteLine(contents);
            return contents;
        }

        public static async Task<bool> PostLoginAsync(string name, string pass)
        {
            Login_DTO loginRequest = new Login_DTO(new User_DTO(name, pass));

            bool ret = await PostLoginAsync(loginRequest);

            //TODO: Ha van ilyen felhasználó és megfelelő a jelszó
            if (pass == "Password")
                return true;
            else
                return false;
        }
    }
}
