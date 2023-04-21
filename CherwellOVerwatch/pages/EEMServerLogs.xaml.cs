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
    public partial class EEMServerLogs : Page
    {
        public string url = "http://localhost:5000/api/settings/EEMServerSettings";
        public EEMServerLogs()
        {
            InitializeComponent();
        }

        private void loadSettings()
        {
            LoadSettings loader = new LoadSettings();

            EEM_Server DeserializedEEMServerLogs = JsonConvert.DeserializeObject<EEM_Server>(loader.GetResult(url));

            eventLogLevel.Text = DeserializedEEMServerLogs.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedEEMServerLogs.loggerSettings.fileLogLevel.ToString();
            if (DeserializedEEMServerLogs.loggerSettings.fileNameOverride == null)
                filenameOverride.Text = "";
            else
                filenameOverride.Text = DeserializedEEMServerLogs.loggerSettings.fileNameOverride.ToString();
            isLoggingEnabled.IsChecked = DeserializedEEMServerLogs.loggerSettings.isLoggingEnabled;
            isServerSettings.IsChecked = DeserializedEEMServerLogs.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedEEMServerLogs.loggerSettings.logFilePath.ToString();
            logServerLogLevel.Text = DeserializedEEMServerLogs.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedEEMServerLogs.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedEEMServerLogs.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedEEMServerLogs.loggerSettings.logToConsoleLevel.ToString();
            LogToEventLog.IsChecked = DeserializedEEMServerLogs.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializedEEMServerLogs.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializedEEMServerLogs.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedEEMServerLogs.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedEEMServerLogs.loggerSettings.maxFileSizeInMB.ToString();
            logToSumoLogic.IsChecked = DeserializedEEMServerLogs.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedEEMServerLogs.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedEEMServerLogs.loggerSettings.settingsType.ToString();

            //LOG SERVER CONNECTION SETTINGS

            ignoreCertErrors.IsChecked = DeserializedEEMServerLogs.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedEEMServerLogs.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedEEMServerLogs.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedEEMServerLogs.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsTypeConnection.Text = DeserializedEEMServerLogs.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnection.Text = DeserializedEEMServerLogs.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedEEMServerLogs.loggerSettings.logServerConnectionSettings.userName.ToString();

            //SUMO LOGIC CONNECTION SETTINGS

            urlSumoLogic.Text = DeserializedEEMServerLogs.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedEEMServerLogs.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedEEMServerLogs.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedEEMServerLogs.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedEEMServerLogs.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagePerRequest.Text = DeserializedEEMServerLogs.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedEEMServerLogs.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadSettings();
        }
    }
}
