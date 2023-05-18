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
    public partial class TAServerLogs : Page
    {
        public string url = "http://localhost:5000/api/settings/TrustedAgentServerSettings";
        public string json;
        public TAServerLogs()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                json = loader.GetResult(url);
                TA_server DeserializedTAS = JsonConvert.DeserializeObject<TA_server>(json);

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

                TA_server DeserializedTAS = JsonConvert.DeserializeObject<TA_server>(json);

                // Build JSON
                var data = new JObject
                {
                    ["disableCompression"] = DeserializedTAS.disableCompression,
                    ["installed"] = DeserializedTAS.installed,
                    ["lastError"] = DeserializedTAS.lastError.ToString(),
                    ["lastErrorDetails"] = DeserializedTAS.lastErrorDetails.ToString(),
                    ["connection"] = DeserializedTAS.connection.ToString(),
                    ["encryptedPassword"] = DeserializedTAS.encryptedPassword.ToString(),
                    ["useDefaultRoleOfUser"] = DeserializedTAS.useDefaultRoleOfUser,
                    ["userId"] = DeserializedTAS.userId.ToString(),
                    ["useWindowsLogin"] = DeserializedTAS.useWindowsLogin,
                    ["displayName"] = DeserializedTAS.displayName.ToString(),
                    ["hubPingFrequency"] = DeserializedTAS.hubPingFrequency.ToString(),
                    ["id"] = DeserializedTAS.id.ToString(),
                    ["sharedKey"] = DeserializedTAS.sharedKey.ToString(),
                    ["signalRHubUrl"] = DeserializedTAS.signalRHubUrl.ToString(),
                    ["loggerSettings"] = DeserializedTAS.loggerSettings == null ? null : new JObject
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
                        ["logServerConnectionSettings"] = DeserializedTAS.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = ignoreCertErrors?.IsChecked,
                            ["isConfigured"] = isConfigured?.IsChecked,
                            ["isServerSettings"] = isServerSettingsConnectionSettings?.IsChecked,
                            ["password"] = password?.Text ?? "",
                            ["settingsType"] = settingsType?.Text ?? "",
                            ["url"] = urlLogServerConnectionSettings?.Text ?? "",
                            ["userName"] = userName?.Text ?? "",
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedTAS.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
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
