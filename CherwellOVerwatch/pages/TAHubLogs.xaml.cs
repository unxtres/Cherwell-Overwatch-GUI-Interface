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
    public partial class TAHubLogs : Page
    {
        public string url = "http://localhost:5000/api/settings/TrustedAgentHubSettings";
        public TAHubLogs()
        {
            InitializeComponent();
        }

        private void loadSettings()
        {
            LoadSettings loader = new LoadSettings();

            TA_Hub DeserializedTAHub = JsonConvert.DeserializeObject<TA_Hub>(loader.GetResult(url));

            eventLogLevel.Text = DeserializedTAHub.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedTAHub.loggerSettings.fileLogLevel.ToString();
            if (DeserializedTAHub.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedTAHub.loggerSettings.fileNameOverride.ToString(); }
            else { fileNameOverride.Text = ""; }
            isLoggingEnabled.IsChecked = DeserializedTAHub.loggerSettings.isLoggingEnabled;
            isLogServerSettings.IsChecked = DeserializedTAHub.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedTAHub.loggerSettings.logFilePath.ToString();

            ignoreCertErrors.IsChecked = DeserializedTAHub.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedTAHub.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedTAHub.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedTAHub.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsType.Text = DeserializedTAHub.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnectionSettings.Text = DeserializedTAHub.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedTAHub.loggerSettings.logServerConnectionSettings.userName.ToString();
            logServerLogLevel.Text = DeserializedTAHub.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedTAHub.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedTAHub.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedTAHub.loggerSettings.logToConsoleLevel.ToString();
            logToEventLog.IsChecked = DeserializedTAHub.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializedTAHub.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializedTAHub.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedTAHub.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedTAHub.loggerSettings.maxFileSizeInMB.ToString();

            urlSumoLogicConnectionSettings.Text = DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagesPerRequest.Text = DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

            logToSumoLogic.IsChecked = DeserializedTAHub.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedTAHub.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedTAHub.loggerSettings.settingsType.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadSettings();
        }
    }
}
