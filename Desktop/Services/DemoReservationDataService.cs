﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using DTO;

namespace Desktop.Services
{
    public static class ReservationsDataService
    {
        private static ObservableCollection<Reservation> reservationList = new ObservableCollection<Reservation>();

        //Foglalás adatbázis
        public static ObservableCollection<Reservation> ReservationList
        {
            get
            {
                if (reservationList == null) { reservationList = new ObservableCollection<Reservation>(); }
                return reservationList;
            }
        }

        //A foglalások letöltése a szerverről
        private static async void ReloadReservationListAsync()
        {
            /*
            List<Reservation> dtoList = await HttpService.ListReservationsAsync();
            reservationList.Clear();
            foreach (Reservation dto in dtoList)
            {
                reservationList.Add(dto);
            }
            */
        }

        //Foglalás hozzáadása
        public static void Reserve(Reservation reserveRequest)
        {
            //Felhasználó beállítása
            reserveRequest.User = SignInService.User.Name;
            //Http kérés kiadása
            //HttpService.ReservationAsync(reserveRequest);
            int flightId = (int) reserveRequest.FlightId;
            foreach (long s in reserveRequest.Seats)
            {
                FlightsDataService.FlightList[flightId].PlaneType.ReserveSeat((int)s);
            }
            //TODO: A nézet frissítése

            reservationList.Add(reserveRequest);
            //ReloadReservationListAsync();
        }
    }
}