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
    public partial class TAServerLogs : Page
    {
        public string url = "http://localhost:5000/api/settings/TrustedAgentServerSettings";
        public TAServerLogs()
        {
            InitializeComponent();
        }

        private void loadSettings()
        {
            LoadSettings loader = new LoadSettings();

            TA_server DeserializedTAS = JsonConvert.DeserializeObject<TA_server>(loader.GetResult(url));

            eventLogLevel.Text = DeserializedTAS.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedTAS.loggerSettings.fileLogLevel.ToString();
            if (DeserializedTAS.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedTAS.loggerSettings.fileNameOverride.ToString(); }
            else { fileNameOverride.Text = ""; }
            isLoggingEnabled.IsChecked = DeserializedTAS.loggerSettings.isLoggingEnabled;
            isLogServerSettings.IsChecked = DeserializedTAS.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedTAS.loggerSettings.logFilePath.ToString();

            ignoreCertErrors.IsChecked = DeserializedTAS.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedTAS.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedTAS.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedTAS.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsType.Text = DeserializedTAS.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnectionSettings.Text = DeserializedTAS.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedTAS.loggerSettings.logServerConnectionSettings.userName.ToString();
            logServerLogLevel.Text = DeserializedTAS.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedTAS.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedTAS.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedTAS.loggerSettings.logToConsoleLevel.ToString();
            logToEventLog.IsChecked = DeserializedTAS.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializedTAS.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializedTAS.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedTAS.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedTAS.loggerSettings.maxFileSizeInMB.ToString();

            urlSumoLogicConnectionSettings.Text = DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagesPerRequest.Text = DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

            logToSumoLogic.IsChecked = DeserializedTAS.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedTAS.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedTAS.loggerSettings.settingsType.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadSettings();
        }
    }
}
