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
    public partial class MQSLogSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/MessageQueueSettings";
        public MQSLogSettings()
        {
            InitializeComponent();
        }

        private void loadSettings()
        {
            LoadSettings loader = new LoadSettings();

            MQS_Settings DeserializedMQSLogServer = JsonConvert.DeserializeObject<MQS_Settings>(loader.GetResult(url));

            eventLogLevel.Text = DeserializedMQSLogServer.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedMQSLogServer.loggerSettings.fileLogLevel.ToString();
            if (DeserializedMQSLogServer.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedMQSLogServer.loggerSettings.fileNameOverride.ToString(); }
            else { fileNameOverride.Text = ""; }
            isLoggingEnabled.IsChecked = DeserializedMQSLogServer.loggerSettings.isLoggingEnabled;
            isLogServerSettings.IsChecked = DeserializedMQSLogServer.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedMQSLogServer.loggerSettings.logFilePath.ToString();

            ignoreCertErrors.IsChecked = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsType.Text = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnectionSettings.Text = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.userName.ToString();
            logServerLogLevel.Text = DeserializedMQSLogServer.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedMQSLogServer.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedMQSLogServer.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedMQSLogServer.loggerSettings.logToConsoleLevel.ToString();
            logToEventLog.IsChecked = DeserializedMQSLogServer.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializedMQSLogServer.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializedMQSLogServer.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedMQSLogServer.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedMQSLogServer.loggerSettings.maxFileSizeInMB.ToString();

            urlSumoLogicConnectionSettings.Text = DeserializedMQSLogServer.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedMQSLogServer.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedMQSLogServer.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedMQSLogServer.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedMQSLogServer.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagesPerRequest.Text = DeserializedMQSLogServer.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedMQSLogServer.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

            logToSumoLogic.IsChecked = DeserializedMQSLogServer.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedMQSLogServer.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedMQSLogServer.loggerSettings.settingsType.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadSettings();
        }
    }
}
