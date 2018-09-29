﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Services;
using DTO;

namespace Desktop.ViewModels
{
    public class ReservationViewModel
    {
        public ObservableCollection<Reservation> Source
        {
            get
            {
                Debug.WriteLine("ABC");
                return DataService.GetReservations();
            }
        }
    }
}
