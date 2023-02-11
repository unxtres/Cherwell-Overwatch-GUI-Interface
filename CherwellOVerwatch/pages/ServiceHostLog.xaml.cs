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
    public partial class ServiceHostLog : Page
    {
        public string url = "http://localhost:5000/api/settings/ServiceHostSettings";
        public ServiceHostLog()
        {
            InitializeComponent();
        }

        private void loadSettings()
        {
            LoadSettings loader = new LoadSettings();

            Service_host DeserializedSH = JsonConvert.DeserializeObject<Service_host>(loader.GetResult(url));

            eventLogLevel.Text = DeserializedSH.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedSH.loggerSettings.fileLogLevel.ToString();
            if (DeserializedSH.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedSH.loggerSettings.fileNameOverride.ToString(); }
            else { fileNameOverride.Text = ""; }
            isLoggingEnabled.IsChecked = DeserializedSH.loggerSettings.isLoggingEnabled;
            isLogServerSettings.IsChecked = DeserializedSH.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedSH.loggerSettings.logFilePath.ToString();

            ignoreCertErrors.IsChecked = DeserializedSH.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedSH.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedSH.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedSH.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsType.Text = DeserializedSH.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnectionSettings.Text = DeserializedSH.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedSH.loggerSettings.logServerConnectionSettings.userName.ToString();
            logServerLogLevel.Text = DeserializedSH.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedSH.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedSH.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedSH.loggerSettings.logToConsoleLevel.ToString();
            logToEventLog.IsChecked = DeserializedSH.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializedSH.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializedSH.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedSH.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedSH.loggerSettings.maxFileSizeInMB.ToString();

            urlSumoLogicConnectionSettings.Text = DeserializedSH.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedSH.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedSH.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedSH.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedSH.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagesPerRequest.Text = DeserializedSH.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedSH.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

            logToSumoLogic.IsChecked = DeserializedSH.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedSH.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedSH.loggerSettings.settingsType.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadSettings();
        }
    }
}
