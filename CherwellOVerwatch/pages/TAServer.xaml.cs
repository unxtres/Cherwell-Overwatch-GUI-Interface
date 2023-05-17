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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CherwellOVerwatch
{
    public partial class TAServer : Page
    {
        public string url = "http://localhost:5000/api/settings/TrustedAgentServerSettings";
        public string json;
        public TAServer()
        {
            InitializeComponent();
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            LoadSettings loader = new LoadSettings();
            json = loader.GetResult(url);
            TA_server DeserializedTAS = JsonConvert.DeserializeObject<TA_server>(json);

            disableCompression.IsChecked = DeserializedTAS.disableCompression;
            installed.IsChecked = DeserializedTAS.installed;
            lastError.Text = DeserializedTAS.lastError.ToString();
            lastErrorDetails.Text = DeserializedTAS.lastErrorDetails.ToString();
            connection.Text = DeserializedTAS.connection.ToString();
            encryptedPassword.Text= DeserializedTAS.encryptedPassword.ToString();
            useDefaultRoleOfUser.IsChecked = DeserializedTAS.useDefaultRoleOfUser;
            userId.Text = DeserializedTAS.userId.ToString();
            useWindowsLogin.IsChecked= DeserializedTAS.useWindowsLogin;
            displayName.Text = DeserializedTAS.displayName.ToString();
            hubPingFrequency.Text = DeserializedTAS.hubPingFrequency.ToString();
            id.Text= DeserializedTAS.id.ToString();
            sharedKey.Text= DeserializedTAS.sharedKey.ToString();
            signalRHubUrl.Text = DeserializedTAS.signalRHubUrl.ToString();
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
                    ["disableCompression"] = disableCompression?.IsChecked,
                    ["installed"] = installed?.IsChecked,
                    ["lastError"] = lastError?.Text ?? "",
                    ["lastErrorDetails"] = lastErrorDetails?.Text ?? "",
                    ["connection"] = connection?.Text ?? "",
                    ["encryptedPassword"] = encryptedPassword?.Text ?? "",
                    ["useDefaultRoleOfUser"] = useDefaultRoleOfUser?.IsChecked,
                    ["userId"] = userId?.Text ?? "",
                    ["useWindowsLogin"] = useWindowsLogin?.IsChecked,
                    ["displayName"] = displayName?.Text ?? "",
                    ["hubPingFrequency"] = hubPingFrequency?.Text ?? "",
                    ["id"] = id?.Text ?? "",
                    ["sharedKey"] = sharedKey?.Text ?? "",
                    ["signalRHubUrl"] = signalRHubUrl?.Text ?? "",
                    ["loggerSettings"] = DeserializedTAS.loggerSettings == null ? null : new JObject
                    {
                        ["eventLogLevel"] = Convert.ToInt32(DeserializedTAS.loggerSettings.eventLogLevel),
                        ["fileLogLevel"] = Convert.ToInt32(DeserializedTAS.loggerSettings.fileLogLevel),
                        ["fileNameOverride"] = Convert.ToString(DeserializedTAS.loggerSettings.fileNameOverride),
                        ["isLoggingEnabled"] = DeserializedTAS.loggerSettings.isLoggingEnabled,
                        ["isServerSettings"] = DeserializedTAS.loggerSettings.isServerSettings,
                        ["logFilePath"] = Convert.ToString(DeserializedTAS.loggerSettings.logFilePath),

                        ["logServerLogLevel"] = Convert.ToInt32(DeserializedTAS.loggerSettings.logServerLogLevel),
                        ["logToComplianceLog"] = DeserializedTAS.loggerSettings.logToComplianceLog,
                        ["logToConsole"] = DeserializedTAS.loggerSettings.logToConsole,
                        ["logToConsoleLevel"] = Convert.ToInt32(DeserializedTAS.loggerSettings.logToConsoleLevel),
                        ["logToEventLog"] = DeserializedTAS.loggerSettings.logToEventLog,
                        ["logToFile"] = DeserializedTAS.loggerSettings.logToFile,
                        ["logToLogServer"] = DeserializedTAS.loggerSettings.logToLogServer,
                        ["maxFilesBeforeRollover"] = Convert.ToInt32(DeserializedTAS.loggerSettings.maxFilesBeforeRollover),
                        ["maxFileSizeInMB"] = Convert.ToInt32(DeserializedTAS.loggerSettings.maxFileSizeInMB),
                        ["logToSumoLogic"] = DeserializedTAS.loggerSettings.logToSumoLogic,
                        ["sumoLogicLogLevel"] = Convert.ToInt32(DeserializedTAS.loggerSettings.sumoLogicLogLevel),
                        ["settingsType"] = Convert.ToInt32(DeserializedTAS.loggerSettings.settingsType),
                        ["logServerConnectionSettings"] = DeserializedTAS.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = DeserializedTAS.loggerSettings.logServerConnectionSettings.ignoreCertErrors,
                            ["isConfigured"] = DeserializedTAS.loggerSettings.logServerConnectionSettings.isConfigured,
                            ["isServerSettings"] = DeserializedTAS.loggerSettings.logServerConnectionSettings.isServerSettings,
                            ["password"] = Convert.ToString(DeserializedTAS.loggerSettings.logServerConnectionSettings.password),
                            ["settingsType"] = Convert.ToString(DeserializedTAS.loggerSettings.logServerConnectionSettings.settingsType),
                            ["url"] = Convert.ToString(DeserializedTAS.loggerSettings.logServerConnectionSettings.url),
                            ["userName"] = Convert.ToString(DeserializedTAS.loggerSettings.logServerConnectionSettings.userName),
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedTAS.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
                        {
                            ["url"] = Convert.ToString(DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.url),
                            ["retryInterval"] = Convert.ToInt32(DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.retryInterval),
                            ["connectionTimeout"] = Convert.ToInt32(DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.connectionTimeout),
                            ["flushingAccuracy"] = Convert.ToInt32(DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy),
                            ["maxFlushInterval"] = Convert.ToInt32(DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval),
                            ["messagesPerRequest"] = Convert.ToInt32(DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest),
                            ["maxQueueSizeBytes"] = Convert.ToInt32(DeserializedTAS.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes)
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
