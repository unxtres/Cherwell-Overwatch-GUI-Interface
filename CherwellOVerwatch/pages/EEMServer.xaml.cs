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
using static System.Net.WebRequestMethods;

namespace CherwellOVerwatch
{
    public partial class EEMServer : Page
    {
        public string url = "http://localhost:5000/api/settings/EEMServerSettings";
        public string json;
        public EEMServer()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();

                EEM_Server DeserializedEEMServer = JsonConvert.DeserializeObject<EEM_Server>(loader.GetResult(url));

                DisableCompression.IsChecked = DeserializedEEMServer.disableCompression;
                installed.IsChecked = DeserializedEEMServer.installed;
                lastError.Text = DeserializedEEMServer.lastError.ToString();
                lastErrorDetails.Text = DeserializedEEMServer.lastErrorDetails.ToString();
                if (DeserializedEEMServer.connection != null) { connection.Text = DeserializedEEMServer.connection.ToString(); }
                else { connection.Text = ""; }
                encryptedPassword.Text = DeserializedEEMServer.encryptedPassword.ToString();
                useDefaultRoleOfUser.IsChecked = DeserializedEEMServer.useDefaultRoleOfUser;
                userId.Text = DeserializedEEMServer.userId.ToString();
                useWindowsLogin.IsChecked = DeserializedEEMServer.useWindowsLogin;
                emailReadLimit.Text = DeserializedEEMServer.emailReadLimit.ToString();
                monitorRepeatLimit.Text = DeserializedEEMServer.monitorRepeatLimit.ToString();
                showRegEx.IsChecked = DeserializedEEMServer.showRegEx;

                ticks.Text = DeserializedEEMServer.addlItemsWait.ticks.ToString();
                days.Text = DeserializedEEMServer.addlItemsWait.days.ToString();
                hours.Text = DeserializedEEMServer.addlItemsWait.hours.ToString();
                milliseconds.Text = DeserializedEEMServer.addlItemsWait.milliseconds.ToString();
                minutes.Text = DeserializedEEMServer.addlItemsWait.minutes.ToString();
                seconds.Text = DeserializedEEMServer.addlItemsWait.seconds.ToString();
                totalDays.Text = DeserializedEEMServer.addlItemsWait.totalDays.ToString();
                totalHours.Text = DeserializedEEMServer.addlItemsWait.totalHours.ToString();
                totalMilliseconds.Text = DeserializedEEMServer.addlItemsWait.totalMilliseconds.ToString();
                totalMinutes.Text = DeserializedEEMServer.addlItemsWait.totalMinutes.ToString();
                totalSeconds.Text = DeserializedEEMServer.addlItemsWait.totalSeconds.ToString();

                no_ticks.Text = DeserializedEEMServer.noItemsWait.ticks.ToString();
                no_days.Text = DeserializedEEMServer.noItemsWait.days.ToString();
                no_hours.Text = DeserializedEEMServer.noItemsWait.hours.ToString();
                no_milliseconds.Text = DeserializedEEMServer.noItemsWait.milliseconds.ToString();
                no_minutes.Text = DeserializedEEMServer.noItemsWait.minutes.ToString();
                no_seconds.Text = DeserializedEEMServer.noItemsWait.seconds.ToString();
                no_totalDays.Text = DeserializedEEMServer.noItemsWait.totalDays.ToString();
                no_totalHours.Text = DeserializedEEMServer.noItemsWait.totalHours.ToString();
                no_totalMilliseconds.Text = DeserializedEEMServer.noItemsWait.totalMilliseconds.ToString();
                no_totalMinutes.Text = DeserializedEEMServer.noItemsWait.totalMinutes.ToString();
                no_totalSeconds.Text = DeserializedEEMServer.noItemsWait.totalSeconds.ToString();
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

                EEM_Server DeserializedLogger = JsonConvert.DeserializeObject<EEM_Server>(json);

                // Build JSON
                var data = new JObject
                {
                    ["disableCompression"] = DisableCompression?.IsChecked,
                    ["installed"] = installed?.IsChecked,
                    ["lastError"] = lastError?.Text ?? "",
                    ["lastErrorDetails"] = lastErrorDetails?.Text ?? "",
                    ["encryptedPassword"] = encryptedPassword?.Text ??"",
                    ["useDefaultRoleOfUser"] = useDefaultRoleOfUser?.IsChecked,
                    ["userId"] = userId?.Text ?? "",
                    ["useWindowsLogin"] = useWindowsLogin?.IsChecked,
                    ["emailReadLimit"] = emailReadLimit?.Text ?? "",
                    ["monitorRepeatLimit"] = monitorRepeatLimit?.Text ?? "",
                    ["showRegEx"] = showRegEx?.IsChecked,
                    ["addlItemsWait"] = DeserializedLogger.addlItemsWait == null ? null : new JObject
                    {
                        ["ticks"] = ticks?.Text ?? "",
                        ["days"] = days?.Text ?? "",
                        ["hours"] = hours?.Text ?? "",
                        ["milliseconds"] = milliseconds?.Text ?? "",
                        ["minutes"] = minutes?.Text ?? "",
                        ["seconds"] = seconds?.Text ?? "",
                        ["totalDays"] = totalDays?.Text ?? "",
                        ["totalHours"] = totalHours?.Text ?? "",
                        ["totalMilliseconds"] = totalMilliseconds?.Text ?? "",
                        ["totalMinutes"] = totalMinutes?.Text ?? "",
                        ["totalSeconds"] = totalSeconds?.Text ?? "",
                    }
                    ["noItemsWait"] = DeserializedLogger.noItemsWait == null ? null : new JObject
                    {
                        ["ticks"] = no_ticks?.Text ?? "",
                        ["days"] = no_days?.Text ?? "",
                        ["hours"] = no_hours?.Text ?? "",
                        ["milliseconds"] = no_milliseconds?.Text ?? "",
                        ["minutes"] = no_minutes?.Text ?? "",
                        ["seconds"] = no_seconds?.Text ?? "",
                        ["totalDays"] = no_totalDays?.Text ?? "",
                        ["totalHours"] = no_totalHours?.Text ?? "",
                        ["totalMilliseconds"] = no_totalMilliseconds?.Text ?? "",
                        ["totalMinutes"] = no_totalMinutes?.Text ?? "",
                        ["totalSeconds"] = no_totalSeconds?.Text ?? "",
                    }
                    ["loggerSettings"] = DeserializedLogger.loggerSettings == null ? null : new JObject
                    {
                        ["eventLogLevel"] = Convert.ToInt32(DeserializedLogger.loggerSettings.eventLogLevel),
                        ["fileLogLevel"] = Convert.ToInt32(DeserializedLogger.loggerSettings.fileLogLevel),
                        ["fileNameOverride"] = Convert.ToString(DeserializedLogger.loggerSettings.fileNameOverride),
                        ["isLoggingEnabled"] = DeserializedLogger.loggerSettings.isLoggingEnabled,
                        ["isServerSettings"] = DeserializedLogger.loggerSettings.isServerSettings,
                        ["logFilePath"] = Convert.ToString(DeserializedLogger.loggerSettings.logFilePath),

                        ["logServerLogLevel"] = Convert.ToInt32(DeserializedLogger.loggerSettings.logServerLogLevel),
                        ["logToComplianceLog"] = DeserializedLogger.loggerSettings.logToComplianceLog,
                        ["logToConsole"] = DeserializedLogger.loggerSettings.logToConsole,
                        ["logToConsoleLevel"] = Convert.ToInt32(DeserializedLogger.loggerSettings.logToConsoleLevel),
                        ["logToEventLog"] = DeserializedLogger.loggerSettings.logToEventLog,
                        ["logToFile"] = DeserializedLogger.loggerSettings.logToFile,
                        ["logToLogServer"] = DeserializedLogger.loggerSettings.logToLogServer,
                        ["maxFilesBeforeRollover"] = Convert.ToInt32(DeserializedLogger.loggerSettings.maxFilesBeforeRollover),
                        ["maxFileSizeInMB"] = Convert.ToInt32(DeserializedLogger.loggerSettings.maxFileSizeInMB),
                        ["logToSumoLogic"] = DeserializedLogger.loggerSettings.logToSumoLogic,
                        ["sumoLogicLogLevel"] = Convert.ToInt32(DeserializedLogger.loggerSettings.sumoLogicLogLevel),
                        ["settingsType"] = Convert.ToInt32(DeserializedLogger.loggerSettings.settingsType),
                        ["logServerConnectionSettings"] = DeserializedLogger.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = DeserializedLogger.loggerSettings.logServerConnectionSettings.ignoreCertErrors,
                            ["isConfigured"] = DeserializedLogger.loggerSettings.logServerConnectionSettings.isConfigured,
                            ["isServerSettings"] = DeserializedLogger.loggerSettings.logServerConnectionSettings.isServerSettings,
                            ["password"] = Convert.ToString(DeserializedLogger.loggerSettings.logServerConnectionSettings.password),
                            ["settingsType"] = Convert.ToString(DeserializedLogger.loggerSettings.logServerConnectionSettings.settingsType),
                            ["url"] = Convert.ToString(DeserializedLogger.loggerSettings.logServerConnectionSettings.url),
                            ["userName"] = Convert.ToString(DeserializedLogger.loggerSettings.logServerConnectionSettings.userName),
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
                        {
                            ["url"] = Convert.ToString(DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.url),
                            ["retryInterval"] = Convert.ToInt32(DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.retryInterval),
                            ["connectionTimeout"] = Convert.ToInt32(DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.connectionTimeout),
                            ["flushingAccuracy"] = Convert.ToInt32(DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy),
                            ["maxFlushInterval"] = Convert.ToInt32(DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval),
                            ["messagesPerRequest"] = Convert.ToInt32(DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest),
                            ["maxQueueSizeBytes"] = Convert.ToInt32(DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes)
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
