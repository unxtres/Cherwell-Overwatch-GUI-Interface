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
    public partial class WebHooksLogs : Page
    {
        public string url = "http://localhost:5000/api/settings/WebhookLeaderSettings";
        public WebHooksLogs()
        {
            InitializeComponent();
        }

        private void loadSettings()
        {
            LoadSettings loader = new LoadSettings();

            Web_Hooks DeserializedWH = JsonConvert.DeserializeObject<Web_Hooks>(loader.GetResult(url));

            eventLogLevel.Text = DeserializedWH.logSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedWH.logSettings.fileLogLevel.ToString();
            if (DeserializedWH.logSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedWH.logSettings.fileNameOverride.ToString(); }
            else { fileNameOverride.Text = ""; }
            isLoggingEnabled.IsChecked = DeserializedWH.logSettings.isLoggingEnabled;
            isLogServerSettings.IsChecked = DeserializedWH.logSettings.isServerSettings;
            logFilePath.Text = DeserializedWH.logSettings.logFilePath.ToString();

            ignoreCertErrors.IsChecked = DeserializedWH.logSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedWH.logSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedWH.logSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedWH.logSettings.logServerConnectionSettings.password.ToString();
            settingsType.Text = DeserializedWH.logSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnectionSettings.Text = DeserializedWH.logSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedWH.logSettings.logServerConnectionSettings.userName.ToString();
            logServerLogLevel.Text = DeserializedWH.logSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedWH.logSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedWH.logSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedWH.logSettings.logToConsoleLevel.ToString();
            logToEventLog.IsChecked = DeserializedWH.logSettings.logToEventLog;
            logToFile.IsChecked = DeserializedWH.logSettings.logToFile;
            logToLogServer.IsChecked = DeserializedWH.logSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedWH.logSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedWH.logSettings.maxFileSizeInMB.ToString();

            urlSumoLogicConnectionSettings.Text = DeserializedWH.logSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedWH.logSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedWH.logSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedWH.logSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedWH.logSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagesPerRequest.Text = DeserializedWH.logSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedWH.logSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

            logToSumoLogic.IsChecked = DeserializedWH.logSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedWH.logSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedWH.logSettings.settingsType.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadSettings();
        }
    }
}
