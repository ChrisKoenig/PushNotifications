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
using System.Collections;

namespace WPPushNotification.ServerSideWeatherSimulator
{
    /// <summary>
    /// Interaction logic for PushNotificationsLogViewer.xaml
    /// </summary>
    public partial class PushNotificationsLogViewer : UserControl
    {
        public PushNotificationsLogViewer()
        {
            InitializeComponent();
            tracelog.LostFocus += (sender, e) => { tracelog.SelectedIndex = -1; };
        }

        public IEnumerable ItemsSource
        {
            get
            {
                return this.tracelog.ItemsSource;
            }

            set
            {
                this.tracelog.ItemsSource = value;
            }
        }
    }
}