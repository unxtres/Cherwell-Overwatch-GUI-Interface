﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
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
using System.Xml.Linq;
using CherwellOVerwatch.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CherwellOVerwatch
{
    public partial class MQSSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/MessageQueueSettings";
        public MQSSettings()
        {
            InitializeComponent();
        }

        private void loadData()
        {
            LoadSettings loader = new LoadSettings();

            MQS_Settings DeserializedMQSServer = JsonConvert.DeserializeObject<MQS_Settings>(loader.GetResult(url));

            decryptedPassword.Text = DeserializedMQSServer.decryptedPassword.ToString();
            encryptedPassword.Text = DeserializedMQSServer.encryptedPassword.ToString();
            hostName.Text = DeserializedMQSServer.hostName.ToString();
            port.Text = DeserializedMQSServer.port.ToString();
            isConfigured.IsChecked = DeserializedMQSServer.isConfigured;
            userName.Text = DeserializedMQSServer.userName.ToString();
            virtualHost.Text = DeserializedMQSServer.virtualHost.ToString();
            rabbitMQPath.Text = DeserializedMQSServer.rabbitMQPath.ToString();
            erlangPath.Text = DeserializedMQSServer.erlangPath.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }
}
