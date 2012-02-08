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
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.ServiceModel;
using System.ServiceModel.Description;
using WPPushNotification.ServerSideWeatherSimulator.Service;

namespace WPPushNotification.ServerSideWeatherSimulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ServiceHost host;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                //TODO - remove remark after creating registration service
                //host = new ServiceHost(typeof(RegistrationService));
                //host.Open();
            }
            catch (TimeoutException timeoutException)
            {
                MessageBox.Show(String.Format("The service operation timed out. {0}", timeoutException.Message));
            }
            catch (CommunicationException communicationException)
            {
                MessageBox.Show(String.Format("Could not start service host. {0}", communicationException.Message));
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (host != null)
            {
                try
                {
                    host.Close();
                }
                catch (TimeoutException)
                {
                    host.Abort();
                }
                catch (CommunicationException)
                {
                    host.Abort();
                }
            }
            base.OnExit(e);
        }
    }
}