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
    public partial class SchedulingLogSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/SchedulingServerSettings";
        public SchedulingLogSettings()
        {
            InitializeComponent();
        }

        private void loadSettings()
        {
            LoadSettings loader = new LoadSettings();

            Scheduling_server DeserializeSchedulingserver = JsonConvert.DeserializeObject<Scheduling_server>(loader.GetResult(url));

            eventLogLevel.Text = DeserializeSchedulingserver.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializeSchedulingserver.loggerSettings.fileLogLevel.ToString();
            if (DeserializeSchedulingserver.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializeSchedulingserver.loggerSettings.fileNameOverride.ToString(); }
            else { fileNameOverride.Text = ""; }
            isLoggingEnabled.IsChecked = DeserializeSchedulingserver.loggerSettings.isLoggingEnabled;
            isLogServerSettings.IsChecked = DeserializeSchedulingserver.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializeSchedulingserver.loggerSettings.logFilePath.ToString();

            ignoreCertErrors.IsChecked = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsType.Text = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnectionSettings.Text = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.userName.ToString();
            logServerLogLevel.Text = DeserializeSchedulingserver.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializeSchedulingserver.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializeSchedulingserver.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializeSchedulingserver.loggerSettings.logToConsoleLevel.ToString();
            logToEventLog.IsChecked = DeserializeSchedulingserver.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializeSchedulingserver.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializeSchedulingserver.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializeSchedulingserver.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializeSchedulingserver.loggerSettings.maxFileSizeInMB.ToString();

            urlSumoLogicConnectionSettings.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagesPerRequest.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

            logToSumoLogic.IsChecked = DeserializeSchedulingserver.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializeSchedulingserver.loggerSettings.settingsType.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadSettings();
        }
    }
}
