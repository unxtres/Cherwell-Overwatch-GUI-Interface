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
    public partial class MQSLogSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/MessageQueueSettings";
        public string json;
        public MQSLogSettings()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                json = loader.GetResult(url);
                MQS_Settings DeserializedMQSLogServer = JsonConvert.DeserializeObject<MQS_Settings>(json);

                eventLogLevel.Text = DeserializedMQSLogServer.loggerSettings.eventLogLevel.ToString();
                fileLogLevel.Text = DeserializedMQSLogServer.loggerSettings.fileLogLevel.ToString();
                if (DeserializedMQSLogServer.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedMQSLogServer.loggerSettings.fileNameOverride.ToString(); }
                else { fileNameOverride.Text = ""; }
                isLoggingEnabled.IsChecked = DeserializedMQSLogServer.loggerSettings.isLoggingEnabled;
                isLogServerSettings.IsChecked = DeserializedMQSLogServer.loggerSettings.isServerSettings;
                if (DeserializedMQSLogServer.loggerSettings.logFilePath != null) { logFilePath.Text = DeserializedMQSLogServer.loggerSettings.logFilePath.ToString(); }
                else { logFilePath.Text = ""; }

                ignoreCertErrors.IsChecked = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
                isConfigured.IsChecked = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.isConfigured;
                isServerSettingsConnectionSettings.IsChecked = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.isServerSettings;
                if (DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.password != null) { password.Text = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.password.ToString(); }
                else { password.Text = ""; }
                settingsType.Text = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.settingsType.ToString();
                if (DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.url != null) { urlLogServerConnectionSettings.Text = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.url.ToString(); }
                else { urlLogServerConnectionSettings.Text = ""; }
                if (DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.userName != null) { userName.Text = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings.userName.ToString(); }
                else { userName.Text = ""; }
                logServerLogLevel.Text = DeserializedMQSLogServer.loggerSettings.logServerLogLevel.ToString();
                logToComplianceLog.IsChecked = DeserializedMQSLogServer.loggerSettings.logToComplianceLog;
                logToConsole.IsChecked = DeserializedMQSLogServer.loggerSettings.logToConsole;
                logToConsoleLevel.Text = DeserializedMQSLogServer.loggerSettings.logToConsoleLevel.ToString();
                logToEventLog.IsChecked = DeserializedMQSLogServer.loggerSettings.logToEventLog;
                logToFile.IsChecked = DeserializedMQSLogServer.loggerSettings.logToFile;
                logToLogServer.IsChecked = DeserializedMQSLogServer.loggerSettings.logToLogServer;
                maxFilesBeforeRollover.Text = DeserializedMQSLogServer.loggerSettings.maxFilesBeforeRollover.ToString();
                maxFileSizeInMB.Text = DeserializedMQSLogServer.loggerSettings.maxFileSizeInMB.ToString();

                if (DeserializedMQSLogServer.loggerSettings.sumoLogicConnectionSettings.url != null) { urlSumoLogicConnectionSettings.Text = DeserializedMQSLogServer.loggerSettings.sumoLogicConnectionSettings.url.ToString(); }
                else { urlSumoLogicConnectionSettings.Text = ""; }
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

                MQS_Settings DeserializedMQSLogServer = JsonConvert.DeserializeObject<MQS_Settings>(json);

                // Build JSON
                var data = new JObject
                {
                    ["decryptedPassword"] = DeserializedMQSLogServer.decryptedPassword.ToString(),
                    ["encryptedPassword"] = DeserializedMQSLogServer.encryptedPassword.ToString(),
                    ["hostName"] = DeserializedMQSLogServer.hostName.ToString(),
                    ["port"] = DeserializedMQSLogServer.port.ToString(),
                    ["isConfigured"] = DeserializedMQSLogServer.isConfigured,
                    ["userName"] = DeserializedMQSLogServer.userName.ToString(),
                    ["virtualHost"] = DeserializedMQSLogServer.virtualHost.ToString(),
                    ["rabbitMQPath"] = DeserializedMQSLogServer.rabbitMQPath.ToString(),
                    ["erlangPath"] = DeserializedMQSLogServer.erlangPath.ToString(),
                    ["loggerSettings"] = DeserializedMQSLogServer.loggerSettings == null ? null : new JObject
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
                        ["logServerConnectionSettings"] = DeserializedMQSLogServer.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = ignoreCertErrors?.IsChecked,
                            ["isConfigured"] = isConfigured?.IsChecked,
                            ["isServerSettings"] = isServerSettingsConnectionSettings?.IsChecked,
                            ["password"] = password?.Text ?? "",
                            ["settingsType"] = settingsType?.Text ?? "",
                            ["url"] = urlLogServerConnectionSettings?.Text ?? "",
                            ["userName"] = userName?.Text ?? "",
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedMQSLogServer.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
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
