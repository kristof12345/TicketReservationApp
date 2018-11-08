﻿using Desktop.Models;
using Desktop.ViewModels;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Desktop.Views
{
    public sealed partial class MyFlightsPage : Page
    {
        private MyFlightViewModel ViewModel
        {
            get { return DataContext as MyFlightViewModel; }
        }

        public MyFlightsPage()
        {
            InitializeComponent();
            //A dupla kattintást jelző event
            dataTable.DoubleTapped += doubleTapped;
        }

        //Dupla kattintásnál átváltunk a kiválasztott repülő nézetére
        private void doubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (dataTable.SelectedItem != null)
            {
                Flight param = (Flight)dataTable.SelectedItem;
                this.Frame.Navigate(typeof(PlanePage), param);
            }
        }

        //Ha a keresés gombra kattintunk, újratöltjük az oldalt
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyFlightsPage));
        }
    }
}