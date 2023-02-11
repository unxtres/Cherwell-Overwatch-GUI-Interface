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
    public partial class PortalLogSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/PortalSettings";
        public PortalLogSettings()
        {
            InitializeComponent();
        }

        private void loadSettings()
        {
            LoadSettings loader = new LoadSettings();

            Portal_Settings DeserializedPortalLogSettings = JsonConvert.DeserializeObject<Portal_Settings>(loader.GetResult(url));

            eventLogLevel.Text = DeserializedPortalLogSettings.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedPortalLogSettings.loggerSettings.fileLogLevel.ToString();
            if (DeserializedPortalLogSettings.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedPortalLogSettings.loggerSettings.fileNameOverride.ToString(); }
            else { fileNameOverride.Text = ""; }
            isLoggingEnabled.IsChecked = DeserializedPortalLogSettings.loggerSettings.isLoggingEnabled;
            isLogServerSettings.IsChecked = DeserializedPortalLogSettings.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedPortalLogSettings.loggerSettings.logFilePath.ToString();

            ignoreCertErrors.IsChecked = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsType.Text = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnectionSettings.Text = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.userName.ToString();
            logServerLogLevel.Text = DeserializedPortalLogSettings.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedPortalLogSettings.loggerSettings.logToConsoleLevel.ToString();
            logToEventLog.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedPortalLogSettings.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedPortalLogSettings.loggerSettings.maxFileSizeInMB.ToString();

            urlSumoLogicConnectionSettings.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagesPerRequest.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

            logToSumoLogic.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedPortalLogSettings.loggerSettings.settingsType.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadSettings();
        }
    }
}
