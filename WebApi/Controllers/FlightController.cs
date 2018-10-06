﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using WebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi
{
    [Route("api/flight")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly DAL.FlightContext _context;
        private readonly ReserveContext _context2;

        public FlightController(DAL.FlightContext context, ReserveContext context2)
        {
            _context = context;
            _context2 = context2;
        }

        public Flight_DTO Flight_DAL_to_DTO(long fID, long bID, DateTime d, string dep, string dest, long pID, string st)
        {
            var plane = _context.PlaneTypes.Single(i => i.planeTypeID == pID);
            String planeName = String.Copy(plane.planeType);

            Flight_DTO temp = new Flight_DTO(planeName);

            temp.FlightId = fID;
            temp.DatabaseId = bID;
            temp.Departure = dep;
            temp.Date = d;
            temp.Destination = dest;
            temp.Status = st;

            return temp;
        }

        //Ilyen paraméterekkel hívod:
        //                Flight_DTO_to_DAL(item.FlightId, item.DatabaseId, item.Date, item.Departure, item.Destination, item.FreeSeats, item.PlaneType.PlaneTypeName, item.Status);
        public DAL.Flight Flight_DTO_to_DAL(long fID, long bID, DateTime d, string dep, string dest, int frSeats, string ptName, string st)
        {
            DAL.Flight temp = new DAL.Flight();
            temp.flightID = fID; //TODO EZ szerintem FORDÍTVA KELLENE
            temp.businessID = bID;
            temp.departure = dep;
            temp.date = d;
            temp.destination = dest;
            temp.freeSeats = frSeats;
            var plane = _context.PlaneTypes.Single(i => i.planeType.Equals(ptName)); //Néha ez is dob kivételt. Sőt mindíg.
            var seats = _context.Seats.Where(s => s.planeTypeID == plane.planeTypeID);
            temp.numberofSeats = seats.ToList().Count;
            temp.planeType = plane;
            temp.status = st;

            return temp;
        }

        [HttpGet]
        public ActionResult<List<Flight_DTO>> GetAll()
        {
            var DAL_list = _context.Flights.ToList();
            List<Flight_DTO> result = new List<Flight_DTO>();
            for (int i = 0; i < DAL_list.Count; i++)
            {
                Flight_DTO current = Flight_DAL_to_DTO(DAL_list[i].flightID, DAL_list[i].businessID, DAL_list[i].date, DAL_list[i].departure, DAL_list[i].destination,
                    DAL_list[i].planeTypeID,
                    DAL_list[i].status);
                result.Add(current);
            }
            return result;
        }

        [HttpGet("{id}", Name = "GetFlight")]
        public ActionResult<Flight_DTO> GetById(long id)
        {
            //DAL.Flight temp = _context.Flights.Find(id);
            var temp = _context.Flights.Single(p => p.businessID == id);
            if (temp == null)
                return NotFound();
            Flight_DTO result = Flight_DAL_to_DTO(temp.flightID, temp.businessID, temp.date, temp.departure, temp.destination, temp.planeTypeID, temp.status);

            return result;
        }

        [HttpPost]
        public IActionResult Create(Flight_DTO item)
        {
            Debug.WriteLine("1"); //Ez még lefut
            DAL.Flight tempfl = Flight_DTO_to_DAL(item.FlightId, item.DatabaseId, item.Date, item.Departure, item.Destination, item.FreeSeats, item.PlaneType.PlaneTypeName, item.Status);
            Debug.WriteLine("2"); //Ez már nem
            _context.Flights.Add(tempfl);
            Debug.WriteLine("3");
            _context.SaveChanges();
            Debug.WriteLine("4");
            var ret = CreatedAtRoute("GetFlight", new { id = tempfl.flightID }, item);
            Debug.WriteLine("5");
            return ret;
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Flight_DTO item)
        {
            //var todo = _context.Flights.Find(id);
            var todo = _context.Flights.Single(p => p.businessID == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.date = item.Date;
            todo.departure = item.Departure;
            todo.destination = item.Destination;
            todo.status = item.Status;
            todo.freeSeats = item.FreeSeats;
            var plane = _context.PlaneTypes.Single(i => i.planeType.Equals(item.PlaneType));
            todo.planeTypeID = plane.planeTypeID;

            _context.Flights.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            //var todo = _context.Flights.Find(id);
            var todo = _context.Flights.Single(p => p.businessID == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Flights.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
