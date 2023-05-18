using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.ServiceProcess;
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
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CherwellOVerwatch
{
    public partial class ServiceHostLog : Page
    {
        public string url = "http://localhost:5000/api/settings/ServiceHostSettings";
        public string json;
        public ServiceHostLog()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            LoadSettings loader = new LoadSettings();
            json = loader.GetResult(url);
            Service_host DeserializedSH = JsonConvert.DeserializeObject<Service_host>(json);

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
        private void Button_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                save_status.Text = "Saving...!";
                // Restart service
                ServiceController service = new ServiceController("Cherwell Overwatch");
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);

                Service_host DeserializedSH = JsonConvert.DeserializeObject<Service_host>(json);

                // Build JSON
                var data = new JObject
                {
                    ["disableCompression"] = DeserializedSH.disableCompression,
                    ["installed"] = DeserializedSH.installed,
                    ["lastError"] = DeserializedSH.lastError.ToString(),
                    ["lastErrorDetails"] = DeserializedSH.lastErrorDetails.ToString(),
                    ["connection"] = DeserializedSH.connection.ToString(),
                    ["encryptedPassword"] = DeserializedSH.encryptedPassword.ToString(),
                    ["useDefaultRoleOfUser"] = DeserializedSH.useDefaultRoleOfUser,
                    ["userId"] = DeserializedSH.userId.ToString(),
                    ["useWindowsLogin"] = DeserializedSH.useWindowsLogin,
                    ["hostMaxWorkers"] = DeserializedSH.hostMaxWorkers.ToString(),
                ["loggerSettings"] = DeserializedSH.loggerSettings == null ? null : new JObject
                    {
                        ["eventLogLevel"] = eventLogLevel?.Text ?? "",
                        ["fileLogLevel"] = fileLogLevel?.Text ?? "",
                        ["fileNameOverride"] = fileNameOverride?.Text ?? "",
                        ["isLoggingEnabled"] = isLoggingEnabled?.IsChecked,
                        ["isServerSettings"] = isLogServerSettings?.IsChecked,
                        ["logFilePath"] = logFilePath?.Text ?? "",

                        ["logServerLogLevel"] = logServerLogLevel?.Text ?? "",
                        ["logToComplianceLog"] = logToComplianceLog?.IsChecked,
                        ["logToConsole"] = logToConsole?.IsChecked,
                        ["logToConsoleLevel"] = logToConsoleLevel?.Text ?? "",
                        ["logToEventLog"] = logToEventLog?.IsChecked,
                        ["logToFile"] = logToFile?.IsChecked,
                        ["logToLogServer"] = logToLogServer?.IsChecked,
                        ["maxFilesBeforeRollover"] = maxFilesBeforeRollover?.Text ?? "",
                        ["maxFileSizeInMB"] = maxFileSizeInMB?.Text ?? "",
                        ["logToSumoLogic"] = logToSumoLogic?.IsChecked,
                        ["sumoLogicLogLevel"] = sumoLogicLogLevel?.Text ?? "",
                        ["settingsType"] = settingsType?.Text ?? "",
                        ["logServerConnectionSettings"] = DeserializedSH.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = ignoreCertErrors?.IsChecked,
                            ["isConfigured"] = isConfigured?.IsChecked,
                            ["isServerSettings"] = isServerSettingsConnectionSettings?.IsChecked,
                            ["password"] = password?.Text ?? "",
                            ["settingsType"] = settingsType?.Text ?? "",
                            ["url"] = urlLogServerConnectionSettings?.Text ?? "",
                            ["userName"] = userName?.Text ?? "",
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedSH.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
                        {
                            ["url"] = urlSumoLogicConnectionSettings?.Text ?? "",
                            ["retryInterval"] = retryInterval?.Text ?? "",
                            ["connectionTimeout"] = connectionTimeout?.Text ?? "",
                            ["flushingAccuracy"] = flushingAccuracy?.Text ?? "",
                            ["maxFlushInterval"] = maxFlushInterval?.Text ?? "",
                            ["messagesPerRequest"] = messagesPerRequest?.Text ?? "",
                            ["maxQueueSizeBytes"] = maxQueueSizeBytes?.Text ?? "",
                        }
                    }
                };

                var settingData = new JObject
                {
                    ["setting"] = JsonConvert.SerializeObject(data),
                    ["publish"] = true
                };

                var jsonData = JsonConvert.SerializeObject(settingData);

                // Send request
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = TokenInterface.OWToken;
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                save_status.Text = httpResponse.StatusCode.ToString();
            }
            catch
            {
                MessageBox.Show("Not Connected");
            }
        }
    }
}
