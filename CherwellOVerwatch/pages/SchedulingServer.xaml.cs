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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using CherwellOVerwatch.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using MessageBox = System.Windows.MessageBox;

namespace CherwellOVerwatch
{
    public partial class SchedulingServer : Page
    {
        public string url = "http://localhost:5000/api/settings/SchedulingServerSettings";
        public string json;
        public SchedulingServer()
        {
            InitializeComponent();
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                json = loader.GetResult(url);
                Scheduling_server DeserializeSchedulingserver = JsonConvert.DeserializeObject<Scheduling_server>(json);

                disableCompression.IsChecked = DeserializeSchedulingserver.disableCompression;
                installed.IsChecked = DeserializeSchedulingserver.installed;
                lastError.Text = DeserializeSchedulingserver.lastError.ToString();
                lastErrorDetails.Text = DeserializeSchedulingserver.lastErrorDetails.ToString();
                if (DeserializeSchedulingserver.connection != null) { connection.Text = DeserializeSchedulingserver.connection.ToString(); }
                else { connection.Text = ""; }
                encryptedPassword.Text = DeserializeSchedulingserver.encryptedPassword.ToString();
                useDefaultRoleOfUser.IsChecked = DeserializeSchedulingserver.useDefaultRoleOfUser;
                userId.Text = DeserializeSchedulingserver.userId.ToString();
                useWindowsLogin.IsChecked = DeserializeSchedulingserver.useWindowsLogin;
                groupId.Text = DeserializeSchedulingserver.groupId.ToString();
                groupName.Text = DeserializeSchedulingserver.groupName.ToString();
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

                Scheduling_server DeserializeSchedulingserver = JsonConvert.DeserializeObject<Scheduling_server>(json);

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
                    ["groupId"] = groupId?.Text ?? "",
                    ["groupName"] = groupName?.Text ?? "",
                    ["loggerSettings"] = DeserializeSchedulingserver.loggerSettings == null ? null : new JObject
                    {
                        ["eventLogLevel"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.eventLogLevel),
                        ["fileLogLevel"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.fileLogLevel),
                        ["fileNameOverride"] = Convert.ToString(DeserializeSchedulingserver.loggerSettings.fileNameOverride),
                        ["isLoggingEnabled"] = DeserializeSchedulingserver.loggerSettings.isLoggingEnabled,
                        ["isServerSettings"] = DeserializeSchedulingserver.loggerSettings.isServerSettings,
                        ["logFilePath"] = Convert.ToString(DeserializeSchedulingserver.loggerSettings.logFilePath),

                        ["logServerLogLevel"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.logServerLogLevel),
                        ["logToComplianceLog"] = DeserializeSchedulingserver.loggerSettings.logToComplianceLog,
                        ["logToConsole"] = DeserializeSchedulingserver.loggerSettings.logToConsole,
                        ["logToConsoleLevel"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.logToConsoleLevel),
                        ["logToEventLog"] = DeserializeSchedulingserver.loggerSettings.logToEventLog,
                        ["logToFile"] = DeserializeSchedulingserver.loggerSettings.logToFile,
                        ["logToLogServer"] = DeserializeSchedulingserver.loggerSettings.logToLogServer,
                        ["maxFilesBeforeRollover"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.maxFilesBeforeRollover),
                        ["maxFileSizeInMB"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.maxFileSizeInMB),
                        ["logToSumoLogic"] = DeserializeSchedulingserver.loggerSettings.logToSumoLogic,
                        ["sumoLogicLogLevel"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.sumoLogicLogLevel),
                        ["settingsType"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.settingsType),
                        ["logServerConnectionSettings"] = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.ignoreCertErrors,
                            ["isConfigured"] = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.isConfigured,
                            ["isServerSettings"] = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.isServerSettings,
                            ["password"] = Convert.ToString(DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.password),
                            ["settingsType"] = Convert.ToString(DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.settingsType),
                            ["url"] = Convert.ToString(DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.url),
                            ["userName"] = Convert.ToString(DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.userName),
                        },
                        ["sumoLogicConnectionSettings"] = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
                        {
                            ["url"] = Convert.ToString(DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.url),
                            ["retryInterval"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.retryInterval),
                            ["connectionTimeout"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.connectionTimeout),
                            ["flushingAccuracy"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy),
                            ["maxFlushInterval"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval),
                            ["messagesPerRequest"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest),
                            ["maxQueueSizeBytes"] = Convert.ToInt32(DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes)
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
