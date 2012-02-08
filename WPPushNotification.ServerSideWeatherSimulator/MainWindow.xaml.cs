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
using System.Collections.ObjectModel;
using System.Threading;
using System.IO;
using System.Xml;
using WPPushNotification.ServerSideWeatherSimulator.Service;
using WindowsPhone.Recipes.Push.Messasges;

namespace WPPushNotification.ServerSideWeatherSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private variables
        private ObservableCollection<PushNotificationsLogMsg> trace = new ObservableCollection<PushNotificationsLogMsg>();
        private RawPushNotificationMessage rawPushNotificationMessage = new RawPushNotificationMessage(MessageSendPriority.High);
        private TilePushNotificationMessage tilePushNotificationMessage = new TilePushNotificationMessage(MessageSendPriority.High);
        private ToastPushNotificationMessage toastPushNotificationMessage = new ToastPushNotificationMessage(MessageSendPriority.High);
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            InitializeWeather();
            InitializeLocations();

            Log.ItemsSource = trace;
            RegistrationService.Subscribed += new EventHandler<RegistrationService.SubscriptionEventArgs>(RegistrationService_Subscribed);
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
            string msg = txtToastMessage.Text;
            txtToastMessage.Text = "";
            List<Uri> subscribers = RegistrationService.GetSubscribers();

            toastPushNotificationMessage.Title = "WEATHER ALERT";
            toastPushNotificationMessage.SubTitle = msg;

            subscribers.ForEach(uri => toastPushNotificationMessage.SendAsync(uri,
                (result) => OnMessageSent(NotificationType.Toast, result),
                (result) => { }));
        }

        private void sendTile()
        {
            string weatherType = cmbWeather.SelectedValue as string;
            int temperature = (int)(sld.Value + 0.5);
            string location = cmbLocation.SelectedValue as string;
            List<Uri> subscribers = RegistrationService.GetSubscribers();

            tilePushNotificationMessage.BackgroundImageUri = new Uri("/Images/" + weatherType + ".png", UriKind.Relative);
            tilePushNotificationMessage.Count = temperature;
            tilePushNotificationMessage.Title = location;

            subscribers.ForEach(uri => tilePushNotificationMessage.SendAsync(uri,
                (result) => OnMessageSent(NotificationType.Token, result),
                (result) => { }));
        }

        private void sendRemoteTile()
        {
            List<Uri> subscribers = RegistrationService.GetSubscribers();

            tilePushNotificationMessage.BackgroundImageUri = new Uri(
                "http://www.larvalabs.com/user_images/screens_thumbs/12555452181.jpg");

            subscribers.ForEach(uri => tilePushNotificationMessage.SendAsync(uri,
                (result) => OnMessageSent(NotificationType.Token, result),
                (result) => { }));
        }

        private void sendHttp()
        {
            //Get the list of subscribed WP7 clients
            List<Uri> subscribers = RegistrationService.GetSubscribers();
            //Prepare payload
            byte[] payload = prepareRAWPayload(
                cmbLocation.SelectedValue as string,
                 sld.Value.ToString("F1"),
                cmbWeather.SelectedValue as string);

            rawPushNotificationMessage.RawData = payload;
            subscribers.ForEach(uri => rawPushNotificationMessage.SendAsync(uri,
                (result) => OnMessageSent(NotificationType.Raw, result),
                (result) => { }));

        }

        void RegistrationService_Subscribed(object sender, RegistrationService.SubscriptionEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            { UpdateStatus(); }));
        }

        private void OnMessageSent(NotificationType type, MessageSendResult result)
        {
            PushNotificationsLogMsg msg = new PushNotificationsLogMsg(type, result);
            Dispatcher.BeginInvoke((Action)(() => 
            { trace.Add(msg); }));
        }

        #endregion

        #region Private functionality
        private void UpdateStatus()
        {
            int activeSubscribers = RegistrationService.GetSubscribers().Count;
            bool isReady = (activeSubscribers > 0);
            txtActiveConnections.Text = activeSubscribers.ToString();
            txtStatus.Text = isReady ? "Ready" : "Waiting for connection...";
        }

        private static byte[] prepareRAWPayload(string location, string temperature, string weatherType)
        {
            MemoryStream stream = new MemoryStream();

            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
            XmlWriter writer = XmlTextWriter.Create(stream, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("WeatherUpdate");

            writer.WriteStartElement("Location");
            writer.WriteValue(location);
            writer.WriteEndElement();

            writer.WriteStartElement("Temperature");
            writer.WriteValue(temperature);
            writer.WriteEndElement();

            writer.WriteStartElement("WeatherType");
            writer.WriteValue(weatherType);
            writer.WriteEndElement();

            writer.WriteStartElement("LastUpdated");
            writer.WriteValue(DateTime.Now.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

            byte[] payload = stream.ToArray();
            return payload;
        }
        #endregion
    }
}