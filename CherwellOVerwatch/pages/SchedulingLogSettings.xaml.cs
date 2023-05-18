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
    public partial class SchedulingLogSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/SchedulingServerSettings";
        public string json;
        public SchedulingLogSettings()
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

                eventLogLevel.Text = DeserializeSchedulingserver.loggerSettings.eventLogLevel.ToString();
                fileLogLevel.Text = DeserializeSchedulingserver.loggerSettings.fileLogLevel.ToString();
                if (DeserializeSchedulingserver.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializeSchedulingserver.loggerSettings.fileNameOverride.ToString(); }
                else { fileNameOverride.Text = ""; }
                isLoggingEnabled.IsChecked = DeserializeSchedulingserver.loggerSettings.isLoggingEnabled;
                isLogServerSettings.IsChecked = DeserializeSchedulingserver.loggerSettings.isServerSettings;
                logFilePath.Text = DeserializeSchedulingserver.loggerSettings.logFilePath.ToString();

                ignoreCertErrors.IsChecked = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
                isConfigured.IsChecked = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.isConfigured;
                isServerSettingsConnectionSettings.IsChecked = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.isServerSettings;
                password.Text = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.password.ToString();
                settingsType.Text = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.settingsType.ToString();
                urlLogServerConnectionSettings.Text = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.url.ToString();
                userName.Text = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings.userName.ToString();
                logServerLogLevel.Text = DeserializeSchedulingserver.loggerSettings.logServerLogLevel.ToString();
                logToComplianceLog.IsChecked = DeserializeSchedulingserver.loggerSettings.logToComplianceLog;
                logToConsole.IsChecked = DeserializeSchedulingserver.loggerSettings.logToConsole;
                logToConsoleLevel.Text = DeserializeSchedulingserver.loggerSettings.logToConsoleLevel.ToString();
                logToEventLog.IsChecked = DeserializeSchedulingserver.loggerSettings.logToEventLog;
                logToFile.IsChecked = DeserializeSchedulingserver.loggerSettings.logToFile;
                logToLogServer.IsChecked = DeserializeSchedulingserver.loggerSettings.logToLogServer;
                maxFilesBeforeRollover.Text = DeserializeSchedulingserver.loggerSettings.maxFilesBeforeRollover.ToString();
                maxFileSizeInMB.Text = DeserializeSchedulingserver.loggerSettings.maxFileSizeInMB.ToString();

                urlSumoLogicConnectionSettings.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.url.ToString();
                retryInterval.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
                connectionTimeout.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
                flushingAccuracy.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
                maxFlushInterval.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
                messagesPerRequest.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
                maxQueueSizeBytes.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

                logToSumoLogic.IsChecked = DeserializeSchedulingserver.loggerSettings.logToSumoLogic;
                sumoLogicLogLevel.Text = DeserializeSchedulingserver.loggerSettings.sumoLogicLogLevel.ToString();
                settingsType.Text = DeserializeSchedulingserver.loggerSettings.settingsType.ToString();
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
                    ["disableCompression"] = DeserializeSchedulingserver.disableCompression,
                    ["installed"] = DeserializeSchedulingserver.installed,
                    ["lastError"] = DeserializeSchedulingserver.lastError.ToString(),
                    ["lastErrorDetails"] = DeserializeSchedulingserver.lastErrorDetails.ToString(),
                    ["connection"] = DeserializeSchedulingserver.connection.ToString(),
                    ["encryptedPassword"] = DeserializeSchedulingserver.encryptedPassword.ToString(),
                    ["useDefaultRoleOfUser"] = DeserializeSchedulingserver.useDefaultRoleOfUser,
                    ["userId"] = DeserializeSchedulingserver.userId.ToString(),
                    ["useWindowsLogin"] = DeserializeSchedulingserver.useWindowsLogin,
                    ["groupId"] = DeserializeSchedulingserver.groupId.ToString(),
                    ["groupName"] = DeserializeSchedulingserver.groupName.ToString(),
                    ["loggerSettings"] = DeserializeSchedulingserver.loggerSettings == null ? null : new JObject
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
                        ["logServerConnectionSettings"] = DeserializeSchedulingserver.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = ignoreCertErrors?.IsChecked,
                            ["isConfigured"] = isConfigured?.IsChecked,
                            ["isServerSettings"] = isServerSettingsConnectionSettings?.IsChecked,
                            ["password"] = password?.Text ?? "",
                            ["settingsType"] = settingsType?.Text ?? "",
                            ["url"] = urlLogServerConnectionSettings?.Text ?? "",
                            ["userName"] = userName?.Text ?? "",
                        },
                        ["sumoLogicConnectionSettings"] = DeserializeSchedulingserver.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
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
