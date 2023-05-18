using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CherwellOVerwatch
{
    public partial class TAHubLogs : Page
    {
        public string url = "http://localhost:5000/api/settings/TrustedAgentHubSettings";
        public string json;
        public TAHubLogs()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                json = loader.GetResult(url);
                TA_Hub DeserializedTAHub = JsonConvert.DeserializeObject<TA_Hub>(json);

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
            catch
            {
                MessageBox.Show("Not Connected");
            }
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

                TA_Hub DeserializedTAHub = JsonConvert.DeserializeObject<TA_Hub>(json);

                // Build JSON
                var data = new JObject
                {
                    ["disableCompression"] = DeserializedTAHub.disableCompression,
                    ["installed"] = DeserializedTAHub.installed,
                    ["lastError"] = DeserializedTAHub.lastError.ToString(),
                    ["lastErrorDetails"] = DeserializedTAHub.lastErrorDetails.ToString(),
                    ["operationTimeout"] = DeserializedTAHub.operationTimeout.ToString(),
                    ["registrationTimeout"] = DeserializedTAHub.registrationTimeout.ToString(),
                    ["sharedKey"] = DeserializedTAHub.sharedKey.ToString(),
                    ["signalRHubUrl"] = DeserializedTAHub.signalRHubUrl.ToString(),
                    ["useTrustedAgents"] = DeserializedTAHub.useTrustedAgents,
                    ["loggerSettings"] = DeserializedTAHub.loggerSettings == null ? null : new JObject
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
                        ["logServerConnectionSettings"] = DeserializedTAHub.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = ignoreCertErrors?.IsChecked,
                            ["isConfigured"] = isConfigured?.IsChecked,
                            ["isServerSettings"] = isServerSettingsConnectionSettings?.IsChecked,
                            ["password"] = password?.Text ?? "",
                            ["settingsType"] = settingsType?.Text ?? "",
                            ["url"] = urlLogServerConnectionSettings?.Text ?? "",
                            ["userName"] = userName?.Text ?? "",
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
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
