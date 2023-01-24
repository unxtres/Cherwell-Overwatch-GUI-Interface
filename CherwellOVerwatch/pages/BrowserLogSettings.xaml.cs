using System;
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
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class BrowserLogSettings : Page
    {
        public string json;
        public string url = "http://localhost:5000/api/settings/BrowserSettings";
        public BrowserLogSettings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                    json = result;
                }
            }
            catch
            {
                MessageBox.Show("Not Connected");
                throw;
            }
            //string temp;
            //var data = (JObject)JsonConvert.DeserializeObject(json);

            Browser_Settings DeserializedBrowserSettings = JsonConvert.DeserializeObject<Browser_Settings>(json);
            
            eventLogLevel.Text = DeserializedBrowserSettings.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedBrowserSettings.loggerSettings.fileLogLevel.ToString();
            filenameOverride.Text = DeserializedBrowserSettings.loggerSettings.fileNameOverride.ToString();
            isLoggingEnabled.IsChecked = DeserializedBrowserSettings.loggerSettings.isLoggingEnabled;
            isServerSettings.IsChecked = DeserializedBrowserSettings.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedBrowserSettings.loggerSettings.logFilePath.ToString();
            logServerLogLevel.Text = DeserializedBrowserSettings.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedBrowserSettings.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedBrowserSettings.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedBrowserSettings.loggerSettings.logToConsoleLevel.ToString();
            LogToEventLog.IsChecked = DeserializedBrowserSettings.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializedBrowserSettings.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializedBrowserSettings.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedBrowserSettings.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedBrowserSettings.loggerSettings.maxFileSizeInMB.ToString();
            logToSumoLogic.IsChecked = DeserializedBrowserSettings.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedBrowserSettings.loggerSettings.settingsType.ToString();

            //LOG SERVER CONNECTION SETTINGS

            ignoreCertErrors.IsChecked = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsTypeConnection.Text = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnection.Text = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.userName.ToString();

            //SUMO LOGIC CONNECTION SETTINGS

            urlSumoLogic.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagePerRequest.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();


        }
    }
}
