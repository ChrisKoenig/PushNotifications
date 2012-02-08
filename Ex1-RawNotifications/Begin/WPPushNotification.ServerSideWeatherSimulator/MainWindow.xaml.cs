// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WPPushNotification.ServerSideWeatherSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private variables
        //TODO - Private variables here...
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            InitializeWeather();
            InitializeLocations();

            //TODO - additional initializations here...
        }

        #region Initializations
        private void InitializeLocations()
        {
          List<string> locations = new List<string>();
          locations.Add("New York");
          locations.Add("London");
          locations.Add("Paris");
          locations.Add("Moscow");
          locations.Add("Redmond");

          cmbLocation.ItemsSource = locations;
          cmbLocation.SelectedIndex = 0;
        }

        private void InitializeWeather()
        {
          Dictionary<string, string> weather = new Dictionary<string, string>();
          weather.Add("Chance_Of_Showers", "Chance Of Showers");
          weather.Add("Clear", "Clear");
          weather.Add("Cloudy", "Cloudy");
          weather.Add("Cloudy_Period", "Cloudy Period");
          weather.Add("Cloudy_With_Drizzle", "Cloudy With Drizzle");
          weather.Add("Few_Flurries", "Few Flurries");
          weather.Add("Few_Flurries_Night", "Few Flurries Night");
          weather.Add("Few_Showers", "Few Showers");
          weather.Add("Flurries", "Flurries");
          weather.Add("Fog", "Fog");
          weather.Add("Freezing_Rain", "Freezing Rain");
          weather.Add("Mostly_Cloudy", "Mostly Cloudy");
          weather.Add("Mostly_Sunny", "Mostly Sunny");
          weather.Add("Rain", "Rain");
          weather.Add("Rain_Or_Snow", "Rain Or Snow");
          weather.Add("Risk_Of_Thundershowers", "Risk Of Thundershowers");
          weather.Add("Snow", "Snow");
          weather.Add("Sunny", "Sunny");
          weather.Add("Thunder_Showers", "Thunder Showers");
          weather.Add("Thunderstorms", "Thunderstorms");
          weather.Add("Wet_Flurries", "Wet Flurries");
          weather.Add("Wet_Flurries_Night", "Wet Flurries Night");

          cmbWeather.ItemsSource = weather;
          cmbWeather.DisplayMemberPath = "Value";
          cmbWeather.SelectedValuePath = "Key";
          cmbWeather.SelectedIndex = 0;
        }
        #endregion

        #region Event Handlers
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
          if ((bool)rbnTile.IsChecked) sendTile();
          else if ((bool)rbnRemoteTile.IsChecked) sendRemoteTile();
          else if ((bool)rbnHttp.IsChecked) sendHttp();
          else if ((bool)rbnToast.IsChecked) sendToast();
        }

        private void sendToast()
        {
          //TODO - Add TOAST notifications sending logic here
        }

        private void sendTile()
        {
            //TODO - Add TILE notifications sending logic here
        }

        private void sendRemoteTile()
        {
            //TODO - Add TILE with remote image URI logic here
        }

        private void sendHttp()
        {
          //TODO - Add RAW notifications preparation and sending logic here
        }
        #endregion

        #region Private functionality
        //TODO - additional private functionality here...
        #endregion
    }
}