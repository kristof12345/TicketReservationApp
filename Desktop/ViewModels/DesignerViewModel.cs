﻿using Desktop.Services;
using DTO;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class DesignerViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private List<Seat> seats = new List<Seat>();
        private String name;

        public String Name { get { return name; } set { name = value; RaisePropertyChanged("Name"); } }

        public String NumberOfSeats
        {
            get{ return seats.Count.ToString(); }
            set { }
        }

        public void AddSeat(double x, double y, int seatType=1)
        {
            Seat s = new Seat(seats.Count);
            seats.Add(s);
            RaisePropertyChanged("NumberOfSeats");
            Debug.WriteLine(NumberOfSeats);
        }

        internal void Save()
        {
            HttpService.PostAddPlaneTypeAsync(Name,seats);
        }
    }
}