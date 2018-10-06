﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DTO
{
    public class Flight_DTO
    {
        //Konstruktor
        public Flight_DTO(String type)
        {
            PlaneType = new PlaneType(type);
        }

        //Elsődleges kulcs az adatbázisban
        public long DatabaseId { get; set; }

        //Egyedi azonosító a kliensben (ez alapján lehet törölni és módosítani)
        public long FlightId { get; set; }

        //Dátum
        public DateTime Date { get; set; }

        //Indulás helye
        public string Departure { get; set; }

        //Érkezés helye
        public string Destination { get; set; }

        //Repülő típusa, tartalmazza a székeket
        public PlaneType PlaneType { get; set; }

        //A járat státusza (pl: Cancelled, Sceduled, Delayed)
        public string Status { get; set; }

        //A székek száma
        public int NumberOfSeats
        {
            get{ return PlaneType.GetTotalSeatsCount(); }
        }

        //A szabad székek száma
        public int FreeSeats
        {
            get{ return PlaneType.GetFreeSeatsCount(); }
        }

        //Kiíráshoz ToStirng
        public override string ToString()
        {
            return "id: " + FlightId.ToString() + " from: " + Departure + " to: " + Destination + " type: " + PlaneType.PlaneTypeName;
        }
    }
}