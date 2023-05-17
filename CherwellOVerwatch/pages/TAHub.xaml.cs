using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public partial class TAHub : Page
    {
        public string url = "http://localhost:5000/api/settings/TrustedAgentHubSettings";
        public string json;
        public TAHub()
        {
            InitializeComponent();
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            LoadSettings loader = new LoadSettings();
            json = loader.GetResult(url);
            TA_Hub DeserializedTAHub = JsonConvert.DeserializeObject<TA_Hub>(json);

            disableCompression.IsChecked = DeserializedTAHub.disableCompression;
            installed.IsChecked = DeserializedTAHub.installed;
            lastError.Text = DeserializedTAHub.lastError.ToString();
            lastErrorDetails.Text = DeserializedTAHub.lastErrorDetails.ToString();
            operationTimeout.Text = DeserializedTAHub.operationTimeout.ToString();
            registrationTimeout.Text = DeserializedTAHub.registrationTimeout.ToString();
            sharedKey.Text = DeserializedTAHub.sharedKey.ToString();
            signalRHubUrl.Text = DeserializedTAHub.signalRHubUrl.ToString();
            useTrustedAgents.IsChecked = DeserializedTAHub.useTrustedAgents;
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
                    ["disableCompression"] = disableCompression?.IsChecked,
                    ["installed"] = installed?.IsChecked,
                    ["lastError"] = lastError?.Text ?? "",
                    ["lastErrorDetails"] = lastErrorDetails?.Text ?? "",
                    ["operationTimeout"] = operationTimeout?.Text ?? "",
                    ["registrationTimeout"] = registrationTimeout?.Text ?? "",
                    ["sharedKey"] = sharedKey?.Text ?? "",
                    ["signalRHubUrl"] = signalRHubUrl?.Text ?? "",
                    ["useTrustedAgents"] = useTrustedAgents?.IsChecked,
                    ["loggerSettings"] = DeserializedTAHub.loggerSettings == null ? null : new JObject
                    {
                        ["eventLogLevel"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.eventLogLevel),
                        ["fileLogLevel"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.fileLogLevel),
                        ["fileNameOverride"] = Convert.ToString(DeserializedTAHub.loggerSettings.fileNameOverride),
                        ["isLoggingEnabled"] = DeserializedTAHub.loggerSettings.isLoggingEnabled,
                        ["isServerSettings"] = DeserializedTAHub.loggerSettings.isServerSettings,
                        ["logFilePath"] = Convert.ToString(DeserializedTAHub.loggerSettings.logFilePath),

                        ["logServerLogLevel"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.logServerLogLevel),
                        ["logToComplianceLog"] = DeserializedTAHub.loggerSettings.logToComplianceLog,
                        ["logToConsole"] = DeserializedTAHub.loggerSettings.logToConsole,
                        ["logToConsoleLevel"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.logToConsoleLevel),
                        ["logToEventLog"] = DeserializedTAHub.loggerSettings.logToEventLog,
                        ["logToFile"] = DeserializedTAHub.loggerSettings.logToFile,
                        ["logToLogServer"] = DeserializedTAHub.loggerSettings.logToLogServer,
                        ["maxFilesBeforeRollover"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.maxFilesBeforeRollover),
                        ["maxFileSizeInMB"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.maxFileSizeInMB),
                        ["logToSumoLogic"] = DeserializedTAHub.loggerSettings.logToSumoLogic,
                        ["sumoLogicLogLevel"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.sumoLogicLogLevel),
                        ["settingsType"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.settingsType),
                        ["logServerConnectionSettings"] = DeserializedTAHub.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = DeserializedTAHub.loggerSettings.logServerConnectionSettings.ignoreCertErrors,
                            ["isConfigured"] = DeserializedTAHub.loggerSettings.logServerConnectionSettings.isConfigured,
                            ["isServerSettings"] = DeserializedTAHub.loggerSettings.logServerConnectionSettings.isServerSettings,
                            ["password"] = Convert.ToString(DeserializedTAHub.loggerSettings.logServerConnectionSettings.password),
                            ["settingsType"] = Convert.ToString(DeserializedTAHub.loggerSettings.logServerConnectionSettings.settingsType),
                            ["url"] = Convert.ToString(DeserializedTAHub.loggerSettings.logServerConnectionSettings.url),
                            ["userName"] = Convert.ToString(DeserializedTAHub.loggerSettings.logServerConnectionSettings.userName),
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
                        {
                            ["url"] = Convert.ToString(DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.url),
                            ["retryInterval"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.retryInterval),
                            ["connectionTimeout"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.connectionTimeout),
                            ["flushingAccuracy"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy),
                            ["maxFlushInterval"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval),
                            ["messagesPerRequest"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest),
                            ["maxQueueSizeBytes"] = Convert.ToInt32(DeserializedTAHub.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes)
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
