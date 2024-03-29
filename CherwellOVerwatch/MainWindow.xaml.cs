﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CherwellOVerwatch.Settings;

namespace CherwellOVerwatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        HttpClient client = new HttpClient();
        public string link = "http://localhost:5000/api/";
        public string token;

        private async void Button_Get_Token(object sender, RoutedEventArgs e)
        {
            //ApiHelper connect = new ApiHelper();
            //string registration_key = regkey.Text;
            //connect.get_token(registration_key);
            //TokenInterface.OWToken = connect.token;
            //regkey.Text = token;
            string reglink = "http://localhost:5000/api/Registration/generate?key=" + regkey.Text;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(reglink),
                Content = new StringContent("body", Encoding.UTF8, "application/json"),
            };
            var response = await client.SendAsync(request).ConfigureAwait(false);
            var responsebody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            token = responsebody.ToString();
            TokenInterface.OWToken = token;
            this.Dispatcher.Invoke(() =>
            {
                regkey.Text = token;
            });
        }

        private void Button_AppServerSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new AppServer();
        }

        private void Button_Logger(object sender, RoutedEventArgs e)
        {
            Main.Content = new AppServerLogSettings();
        }

        private void Button_AutoDeploy(object sender, RoutedEventArgs e)
        {
            Main.Content = new AutoDeploy();
        }

        private void Button_AutoUpdate(object sender, RoutedEventArgs e)
        {
            Main.Content = new AutoUpdateService();
        }

        private void Button_BrowserSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new BrowserSettingsPage();
        }

        private void Button_BrowserLogSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new BrowserLogSettings();
        }

        private void Button_ConnectionDefSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new ConnectionDefinitions();
        }

        private void Button_EEMServerSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new EEMServer();
        }

        private void Button_EEMServerLogSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new EEMServerLogs();
        }

        private void Button_MQSSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new MQSSettings();
        }

        private void Button_MQSLogSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new MQSLogSettings();
        }

        private void Button_PortalSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new PortalSettings();
        }

        private void Button_PortalLogSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new PortalLogSettings();
        }

        private void Button_LogsSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new LogsSettings();
        }

        private void Button_SchedulingServer(object sender, RoutedEventArgs e)
        {
            Main.Content = new SchedulingServer();
        }

        private void Button_SH(object sender, RoutedEventArgs e)
        {
            Main.Content = new ServiceHost();
        }

        private void Button_TAHub(object sender, RoutedEventArgs e)
        {
            Main.Content = new TAHub();
        }

        private void Button_TAServer(object sender, RoutedEventArgs e)
        {
            Main.Content = new TAServer();
        }

        private void Button_API(object sender, RoutedEventArgs e)
        {
            Main.Content = new WebAPI();
        }

        private void Button_WHL(object sender, RoutedEventArgs e)
        {
            Main.Content = new WebHooks();
        }
    }
}