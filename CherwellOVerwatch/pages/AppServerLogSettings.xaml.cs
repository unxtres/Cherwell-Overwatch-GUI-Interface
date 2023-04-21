using System;
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
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AppServerLogSettings : Page
    {
        public string json;
        public AppServerLogSettings()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
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
            ApplicationServer DeserializedLogger = JsonConvert.DeserializeObject<ApplicationServer>(json);

            eventLogLevel.Text = DeserializedLogger.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedLogger.loggerSettings.fileLogLevel.ToString();
            if (DeserializedLogger.loggerSettings.fileNameOverride == null)
                filenameOverride.Text = "";
            else
                filenameOverride.Text = DeserializedLogger.loggerSettings.fileNameOverride.ToString();
            isLoggingEnabled.IsChecked = DeserializedLogger.loggerSettings.isLoggingEnabled;
            isServerSettings.IsChecked = DeserializedLogger.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedLogger.loggerSettings.logFilePath.ToString();
            logServerLogLevel.Text = DeserializedLogger.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedLogger.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedLogger.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedLogger.loggerSettings.logToConsoleLevel.ToString();
            LogToEventLog.IsChecked = DeserializedLogger.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializedLogger.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializedLogger.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedLogger.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedLogger.loggerSettings.maxFileSizeInMB.ToString();
            logToSumoLogic.IsChecked = DeserializedLogger.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedLogger.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedLogger.loggerSettings.settingsType.ToString();

            //LOG SERVER CONNECTION SETTINGS

            ignoreCertErrors.IsChecked = DeserializedLogger.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedLogger.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedLogger.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedLogger.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsTypeConnection.Text = DeserializedLogger.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnection.Text = DeserializedLogger.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedLogger.loggerSettings.logServerConnectionSettings.userName.ToString();

            //SUMO LOGIC CONNECTION SETTINGS

            urlSumoLogic.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagePerRequest.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();
        }
    }
}
