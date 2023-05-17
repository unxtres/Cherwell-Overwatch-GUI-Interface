using System;
using System.Collections.Generic;
using System.Configuration.Install;
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
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using MessageBox = System.Windows.MessageBox;

namespace CherwellOVerwatch
{
    public partial class EEMServerLogs : Page
    {
        public string url = "http://localhost:5000/api/settings/EEMServerSettings";
        public string json;
        public EEMServerLogs()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                json = loader.GetResult(url);
                EEM_Server DeserializedEEMServerLogs = JsonConvert.DeserializeObject<EEM_Server>(json);

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

                EEM_Server DeserializedEEMServerLogs = JsonConvert.DeserializeObject<EEM_Server>(json);

                // Build JSON
                var data = new JObject
                {
                    ["DisableCompression"] = DeserializedEEMServerLogs.disableCompression,
                    ["installed"] = DeserializedEEMServerLogs.installed,
                    ["lastError"] = DeserializedEEMServerLogs.lastError.ToString(),
                    ["lastErrorDetails"] = DeserializedEEMServerLogs.lastErrorDetails.ToString(),
                    ["connection"] = DeserializedEEMServerLogs.connection.ToString(),
                    ["encryptedPassword"] = DeserializedEEMServerLogs.encryptedPassword.ToString(),
                    ["useDefaultRoleOfUser"] = DeserializedEEMServerLogs.useDefaultRoleOfUser,
                    ["userId"] = DeserializedEEMServerLogs.userId.ToString(),
                    ["useWindowsLogin"] = DeserializedEEMServerLogs.useWindowsLogin,
                    ["emailReadLimit"] = DeserializedEEMServerLogs.emailReadLimit.ToString(),
                    ["monitorRepeatLimit"] = DeserializedEEMServerLogs.monitorRepeatLimit.ToString(),
                    ["showRegEx"] = DeserializedEEMServerLogs.showRegEx,

                    ["ticks"] = DeserializedEEMServerLogs.addlItemsWait.ticks.ToString(),
                    ["days"] = DeserializedEEMServerLogs.addlItemsWait.days.ToString(),
                    ["hours"] = DeserializedEEMServerLogs.addlItemsWait.hours.ToString(),
                    ["milliseconds"] = DeserializedEEMServerLogs.addlItemsWait.milliseconds.ToString(),
                    ["minutes"] = DeserializedEEMServerLogs.addlItemsWait.minutes.ToString(),
                    ["seconds"] = DeserializedEEMServerLogs.addlItemsWait.seconds.ToString(),
                    ["totalDays"] = DeserializedEEMServerLogs.addlItemsWait.totalDays.ToString(),
                    ["totalHours"] = DeserializedEEMServerLogs.addlItemsWait.totalHours.ToString(),
                    ["totalMilliseconds"] = DeserializedEEMServerLogs.addlItemsWait.totalMilliseconds.ToString(),
                    ["totalMinutes"] = DeserializedEEMServerLogs.addlItemsWait.totalMinutes.ToString(),
                    ["totalSeconds"] = DeserializedEEMServerLogs.addlItemsWait.totalSeconds.ToString(),

                    ["no_ticks"] = DeserializedEEMServerLogs.noItemsWait.ticks.ToString(),
                    ["no_days"] = DeserializedEEMServerLogs.noItemsWait.days.ToString(),
                    ["no_hours"] = DeserializedEEMServerLogs.noItemsWait.hours.ToString(),
                    ["no_milliseconds"] = DeserializedEEMServerLogs.noItemsWait.milliseconds.ToString(),
                    ["no_minutes"] = DeserializedEEMServerLogs.noItemsWait.minutes.ToString(),
                    ["no_seconds"] = DeserializedEEMServerLogs.noItemsWait.seconds.ToString(),
                    ["no_totalDays"] = DeserializedEEMServerLogs.noItemsWait.totalDays.ToString(),
                    ["no_totalHours"] = DeserializedEEMServerLogs.noItemsWait.totalHours.ToString(),
                    ["no_totalMilliseconds"] = DeserializedEEMServerLogs.noItemsWait.totalMilliseconds.ToString(),
                    ["no_totalMinutes"] = DeserializedEEMServerLogs.noItemsWait.totalMinutes.ToString(),
                    ["no_totalSeconds"] = DeserializedEEMServerLogs.noItemsWait.totalSeconds.ToString(),
                    ["loggerSettings"] = DeserializedEEMServerLogs.loggerSettings == null ? null : new JObject
                    {
                        ["eventLogLevel"] = eventLogLevel?.Text ?? "",
                        ["fileLogLevel"] = fileLogLevel?.Text ?? "",
                        ["fileNameOverride"] = filenameOverride?.Text ?? "",
                        ["isLoggingEnabled"] = isLoggingEnabled?.IsChecked,
                        ["isServerSettings"] = isServerSettings?.IsChecked,
                        ["logFilePath"] = logFilePath?.Text ?? "",

                        ["logServerLogLevel"] = logServerLogLevel?.Text ?? "",
                        ["logToComplianceLog"] = logToComplianceLog?.IsChecked,
                        ["logToConsole"] = logToConsole?.IsChecked,
                        ["logToConsoleLevel"] = logToConsoleLevel?.Text ?? "",
                        ["logToEventLog"] = LogToEventLog?.IsChecked,
                        ["logToFile"] = logToFile?.IsChecked,
                        ["logToLogServer"] = logToLogServer?.IsChecked,
                        ["maxFilesBeforeRollover"] = maxFilesBeforeRollover?.Text ?? "",
                        ["maxFileSizeInMB"] = maxFileSizeInMB?.Text ?? "",
                        ["logToSumoLogic"] = logToSumoLogic?.IsChecked,
                        ["sumoLogicLogLevel"] = sumoLogicLogLevel?.Text ?? "",
                        ["settingsType"] = settingsType?.Text ?? "",
                        ["logServerConnectionSettings"] = DeserializedEEMServerLogs.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = ignoreCertErrors?.IsChecked,
                            ["isConfigured"] = isConfigured?.IsChecked,
                            ["isServerSettings"] = isServerSettings?.IsChecked,
                            ["password"] = password?.Text ?? "",
                            ["settingsType"] = settingsType?.Text ?? "",
                            ["url"] = urlLogServerConnection?.Text ?? "",
                            ["userName"] = userName?.Text ?? "",
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedEEMServerLogs.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
                        {
                            ["url"] = urlSumoLogic?.Text ?? "",
                            ["retryInterval"] = retryInterval?.Text ?? "",
                            ["connectionTimeout"] = connectionTimeout?.Text ?? "",
                            ["flushingAccuracy"] = flushingAccuracy?.Text ?? "",
                            ["maxFlushInterval"] = maxFlushInterval?.Text ?? "",
                            ["messagesPerRequest"] = messagePerRequest?.Text ?? "",
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
