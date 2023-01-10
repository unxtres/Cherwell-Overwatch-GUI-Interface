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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AppServer : Page
    {
        public AppServer()
        {
            InitializeComponent();
            
            string url = "http://localhost:5000/api/settings/AppServerSettings";
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri(url),
            //    Content = new StringContent("body", Encoding.UTF8, "application/json"),
            //    Headers = new HttpRequestHeaders()
            //};
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = TokenInterface.OWToken;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                settings.Text = result;

            }
        }
    }
}
