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
    public partial class ServiceHost : Page
    {
        public string url = "http://localhost:5000/api/settings/ServiceHostSettings";
        public string json;
        public ServiceHost()
        {
            InitializeComponent();
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            LoadSettings loader = new LoadSettings();
            json = loader.GetResult(url)
            Service_host DeserializedSH = JsonConvert.DeserializeObject<Service_host>(json);

            disableCompression.IsChecked = DeserializedSH.disableCompression;
            installed.IsChecked = DeserializedSH.installed;
            lastError.Text = DeserializedSH.lastError.ToString();
            lastErrorDetails.Text = DeserializedSH.lastErrorDetails.ToString();
            connection.Text = DeserializedSH.connection.ToString();
            encryptedPassword.Text = DeserializedSH.encryptedPassword.ToString();
            useDefaultRoleOfUser.IsChecked = DeserializedSH.useDefaultRoleOfUser;
            userId.Text = DeserializedSH.userId.ToString();
            useWindowsLogin.IsChecked = DeserializedSH.useWindowsLogin;
            hostMaxWorkers.Text = DeserializedSH.hostMaxWorkers.ToString();
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
                    ["disableCompression"] = disableCompression?.IsChecked,
                    ["installed"] = installed?.IsChecked,
                    ["lastError"] = lastError?.Text ?? "",
                    ["lastErrorDetails"] = lastErrorDetails?.Text ?? "",
                    ["connection"] = connection?.Text ?? "",
                    ["encryptedPassword"] = encryptedPassword?.Text ?? "",
                    ["useDefaultRoleOfUser"] = useDefaultRoleOfUser?.IsChecked,
                    ["userId"] = userId?.Text ?? "",
                    ["useWindowsLogin"] = useWindowsLogin?.IsChecked,
                    ["hostMaxWorkers"] = hostMaxWorkers?.Text ?? "",
                    ["loggerSettings"] = DeserializedSH.loggerSettings == null ? null : new JObject
                    {
                        ["eventLogLevel"] = Convert.ToInt32(DeserializedSH.loggerSettings.eventLogLevel),
                        ["fileLogLevel"] = Convert.ToInt32(DeserializedSH.loggerSettings.fileLogLevel),
                        ["fileNameOverride"] = Convert.ToString(DeserializedSH.loggerSettings.fileNameOverride),
                        ["isLoggingEnabled"] = DeserializedSH.loggerSettings.isLoggingEnabled,
                        ["isServerSettings"] = DeserializedSH.loggerSettings.isServerSettings,
                        ["logFilePath"] = Convert.ToString(DeserializedSH.loggerSettings.logFilePath),

                        ["logServerLogLevel"] = Convert.ToInt32(DeserializedSH.loggerSettings.logServerLogLevel),
                        ["logToComplianceLog"] = DeserializedSH.loggerSettings.logToComplianceLog,
                        ["logToConsole"] = DeserializedSH.loggerSettings.logToConsole,
                        ["logToConsoleLevel"] = Convert.ToInt32(DeserializedSH.loggerSettings.logToConsoleLevel),
                        ["logToEventLog"] = DeserializedSH.loggerSettings.logToEventLog,
                        ["logToFile"] = DeserializedSH.loggerSettings.logToFile,
                        ["logToLogServer"] = DeserializedSH.loggerSettings.logToLogServer,
                        ["maxFilesBeforeRollover"] = Convert.ToInt32(DeserializedSH.loggerSettings.maxFilesBeforeRollover),
                        ["maxFileSizeInMB"] = Convert.ToInt32(DeserializedSH.loggerSettings.maxFileSizeInMB),
                        ["logToSumoLogic"] = DeserializedSH.loggerSettings.logToSumoLogic,
                        ["sumoLogicLogLevel"] = Convert.ToInt32(DeserializedSH.loggerSettings.sumoLogicLogLevel),
                        ["settingsType"] = Convert.ToInt32(DeserializedSH.loggerSettings.settingsType),
                        ["logServerConnectionSettings"] = DeserializedSH.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = DeserializedSH.loggerSettings.logServerConnectionSettings.ignoreCertErrors,
                            ["isConfigured"] = DeserializedSH.loggerSettings.logServerConnectionSettings.isConfigured,
                            ["isServerSettings"] = DeserializedSH.loggerSettings.logServerConnectionSettings.isServerSettings,
                            ["password"] = Convert.ToString(DeserializedSH.loggerSettings.logServerConnectionSettings.password),
                            ["settingsType"] = Convert.ToString(DeserializedSH.loggerSettings.logServerConnectionSettings.settingsType),
                            ["url"] = Convert.ToString(DeserializedSH.loggerSettings.logServerConnectionSettings.url),
                            ["userName"] = Convert.ToString(DeserializedSH.loggerSettings.logServerConnectionSettings.userName),
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedSH.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
                        {
                            ["url"] = Convert.ToString(DeserializedSH.loggerSettings.sumoLogicConnectionSettings.url),
                            ["retryInterval"] = Convert.ToInt32(DeserializedSH.loggerSettings.sumoLogicConnectionSettings.retryInterval),
                            ["connectionTimeout"] = Convert.ToInt32(DeserializedSH.loggerSettings.sumoLogicConnectionSettings.connectionTimeout),
                            ["flushingAccuracy"] = Convert.ToInt32(DeserializedSH.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy),
                            ["maxFlushInterval"] = Convert.ToInt32(DeserializedSH.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval),
                            ["messagesPerRequest"] = Convert.ToInt32(DeserializedSH.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest),
                            ["maxQueueSizeBytes"] = Convert.ToInt32(DeserializedSH.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes)
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
