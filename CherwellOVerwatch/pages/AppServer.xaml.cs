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
using System.Xml.Linq;
using CherwellOVerwatch.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CherwellOVerwatch
{
    public partial class AppServer : Page
    {
        public string url = "http://localhost:5000/api/settings/AppServerSettings";
        public AppServer()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            LoadSettings loader = new LoadSettings();

            var data = (JObject)JsonConvert.DeserializeObject(loader.GetResult(url));

            disableCompression.IsChecked = data["disableCompression"].Value<bool>();
            installed.IsChecked = data["installed"].Value<bool>();

            lastError.Text = data["lastError"].Value<string>();
            lastErrorDetails.Text = data["lastErrorDetails"].Value<string>();

            isHttp.IsChecked = data["isHttp"].Value<bool>();
            isTcp.IsChecked = data["isTcp"].Value<bool>();

            appServerHostMode.Text = data["appServerHostMode"].Value<string>();
            protocol.Text = data["protocol"].Value<string>();
            connection.Text = data["connection"].Value<string>();

            enableTcpOption.IsChecked = data["enableTcpOption"].Value<bool>();

            instanceGuid.Text = data["instanceGuid"].Value<string>();
            oldTcpPort.Text = data["oldTcpPort"].Value<string>();
            port.Text = data["port"].Value<string>();

            useRest.IsChecked = data["useRest"].Value<bool>();

            securityMode.Text = data["securityMode"].Value<string>();
            serverName.Text = data["serverName"].Value<string>();
            serverConfigToolComments.Text = data["serverConfigToolComments"].Value<string>();
            loggedInUserCacheExpiryMins.Text = data["loggedInUserCacheExpiryMins"].Value<string>();

            useRecoveryFile.IsChecked = data["useRecoveryFile"].Value<bool>();

            recoveryFilePath.Text = data["recoveryFilePath"].Value<string>();
            recoveryFileName.Text = data["recoveryFileName"].Value<string>();
            recoveryFilePersistIntervalSeconds.Text = data["recoveryFilePersistIntervalSeconds"].Value<string>();

            minMessageSizeToCompressHigh.Text = data["minMessageSizeToCompressHigh"].Value<string>();
            minMessageSizeToCompressLow.Text = data["minMessageSizeToCompressLow"].Value<string>();
            minMessageSizeToCompressMedium.Text = data["minMessageSizeToCompressMedium"].Value<string>();

            wcfMaxBufferPoolSize.Text = data["wcfMaxBufferPoolSize"].Value<string>();
            wcfMaxBufferSize.Text = data["wcfMaxBufferSize"].Value<string>();
            wcfMaxReceivedMessageSize.Text = data["wcfMaxReceivedMessageSize"].Value<string>();
            wcfReaderMaxNameTableCharCount.Text = data["wcfReaderMaxNameTableCharCount"].Value<string>();
            wcfReaderMaxStringContentLength.Text = data["wcfReaderMaxStringContentLength"].Value<string>();
            wcfReaderMaxArrayLength.Text = data["wcfReaderMaxArrayLength"].Value<string>();
            wcfOperationTimeoutOverride.Text = data["wcfOperationTimeoutOverride"].Value<string>();

            wcfUseMessageCompression.IsChecked = data["wcfUseMessageCompression"].Value<bool>();

            wcfTcpMaxConnections.Text = data["wcfTcpMaxConnections"].Value<string>();
            wcfMaxConcurrentCalls.Text = data["wcfMaxConcurrentCalls"].Value<string>();
            wcfMaxConcurrentInstances.Text = data["wcfMaxConcurrentInstances"].Value<string>();
            wcfMaxConcurrentSessions.Text = data["wcfMaxConcurrentSessions"].Value<string>();

            wcfEnablePerformanceCounters.IsChecked = data["wcfEnablePerformanceCounters"].Value<bool>();

            wcfListenBacklog.Text = data["wcfListenBacklog"].Value<string>();

            certificateStoreLocation.Text = data["certificateStoreLocation"].Value<string>();
            certificateStoreName.Text = data["certificateStoreName"].Value<string>();
            certificateSubject.Text = data["certificateSubject"].Value<string>();
            certificateThumbprint.Text = data["certificateThumbprint"].Value<string>();
            certificateValidationModeForAutoClient.Text = data["certificateValidationModeForAutoClient"].Value<string>();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
 //           var httpRequest = (HttpWebRequest)WebRequest.Create(url);
 //           httpRequest.Method = "POST";

 //           httpRequest.Accept = "application/json";
 //           httpRequest.Headers["Authorization"] = TokenInterface.OWToken;
 //           httpRequest.ContentType = "application/json";

 //           var data = @"{
	//""port"": 88, ""publish"": true} ";

 //           using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
 //           {
 //               streamWriter.Write(data);
 //           }

 //           var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
 //           using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
 //           {
 //               var result = streamReader.ReadToEnd();
 //           }

 //           save_status.Text = httpResponse.StatusCode.ToString();

        }
    }
}
