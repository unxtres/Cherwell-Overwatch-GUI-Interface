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
    public partial class WebAPILogs : Page
    {
        public string url = "http://localhost:5000/api/settings/WebApiServiceSettings";
        public WebAPILogs()
        {
            InitializeComponent();
        }

        private void loadSettings()
        {
            LoadSettings loader = new LoadSettings();

            Web_api DeserializedAPI = JsonConvert.DeserializeObject<Web_api>(loader.GetResult(url));

            eventLogLevel.Text = DeserializedAPI.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedAPI.loggerSettings.fileLogLevel.ToString();
            if (DeserializedAPI.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedAPI.loggerSettings.fileNameOverride.ToString(); }
            else { fileNameOverride.Text = ""; }
            isLoggingEnabled.IsChecked = DeserializedAPI.loggerSettings.isLoggingEnabled;
            isLogServerSettings.IsChecked = DeserializedAPI.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedAPI.loggerSettings.logFilePath.ToString();

            ignoreCertErrors.IsChecked = DeserializedAPI.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedAPI.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedAPI.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedAPI.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsType.Text = DeserializedAPI.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnectionSettings.Text = DeserializedAPI.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedAPI.loggerSettings.logServerConnectionSettings.userName.ToString();
            logServerLogLevel.Text = DeserializedAPI.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedAPI.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedAPI.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedAPI.loggerSettings.logToConsoleLevel.ToString();
            logToEventLog.IsChecked = DeserializedAPI.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializedAPI.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializedAPI.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedAPI.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedAPI.loggerSettings.maxFileSizeInMB.ToString();

            urlSumoLogicConnectionSettings.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagesPerRequest.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

            logToSumoLogic.IsChecked = DeserializedAPI.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedAPI.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedAPI.loggerSettings.settingsType.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadSettings();
        }
    }
}
