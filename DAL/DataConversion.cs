﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;

namespace DAL
{
    public static class DataConversion
    {
        public static Flight_DTO Flight_DAL_to_DTO(DAL.Flight dalFlight, FlightContext _context)
        {
            var planeType = _context.PlaneTypes.Single(i => i.planeTypeID == dalFlight.planeTypeID);

            Flight_DTO temp = new Flight_DTO(planeType.planeType, planeType.planeTypeID);
            temp.FlightId = dalFlight.flightID;
            temp.Departure = dalFlight.departure;
            temp.Date = dalFlight.date;
            temp.Destination = dalFlight.destination;
            temp.Status = dalFlight.status;
            temp.PlaneTypeName = planeType.planeType;
            //temp.NormalPrice = dalFlight.normalPrice;
            //temp.FirstClassPrice = dalFlight.firstClassPrice;

            return temp;
        }

        public static DAL.Flight Flight_DTO_to_DAL(DTO.Flight_DTO dtoFlight, FlightContext _context)
        {
            DAL.Flight temp = new DAL.Flight();
            temp.departure = dtoFlight.Departure;
            temp.date = dtoFlight.Date;
            temp.destination = dtoFlight.Destination;
            temp.status = dtoFlight.Status;
            temp.planeTypeID = dtoFlight.PlaneTypeID;
            //temp.normalPrice = dtoFlight.NormalPrice;
            //temp.firstClassPrice = dtoFlight.FirstClassPrice;
            temp.isDeleted = false;

            return temp;
        }

        public static DTO.Seat Seat_DAL_to_DTO(DAL.Seat dalSeat)
        {
            DTO.Seat temp = new DTO.Seat(dalSeat.seatID);

            temp.SeatId = dalSeat.seatID;
            temp.Reserved = dalSeat.IsReserved;
            temp.SeatType = dalSeat.seatType;
            temp.PlaneTypeId = dalSeat.planeTypeID;

            Cord coord = new Cord(dalSeat.Xcord, dalSeat.Ycord);
            temp.Coordinates = coord;

            return temp;
        }

        public static DAL.Seat Seat_DTO_to_DAL(DTO.Seat dtoSeat, FlightContext _context)
        {
            DAL.Seat temp = new DAL.Seat();

            temp.planeTypeID = dtoSeat.PlaneTypeId;
            temp.IsReserved = dtoSeat.Reserved;
            temp.seatType = dtoSeat.SeatType;
            temp.Xcord = dtoSeat.Coordinates.X;
            temp.Ycord = dtoSeat.Coordinates.Y;

            return temp;
        }

        public static DTO.PlaneType PlaneType_DAL_to_DTO(DAL.PlaneType dalPlaneType, FlightContext _context)
        {
            DTO.PlaneType temp = new DTO.PlaneType(dalPlaneType.planeType, dalPlaneType.planeTypeID);

            var queriedSeats = _context.Seats.Where(s => s.planeTypeID == dalPlaneType.planeTypeID);

            List<DTO.Seat> dtoSeats = new List<DTO.Seat>();
            foreach (DAL.Seat seat in queriedSeats)
            {
                DTO.Seat current = DataConversion.Seat_DAL_to_DTO(seat);
                dtoSeats.Add(current);
            }

            return temp;
        }

        public static DAL.PlaneType PlaneType_DTO_to_DAL(DTO.PlaneType dtoPlaneType)
        {
            DAL.PlaneType temp = new DAL.PlaneType();
            temp.planeType = dtoPlaneType.PlaneTypeName;
            return temp;
        }
    }
}