using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.AccessControl;
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
    public partial class WebAPILogs : Page
    {
        public string url = "http://localhost:5000/api/settings/WebApiServiceSettings";
        public string json;
        public WebAPILogs()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                json = loader.GetResult(url);
                Web_api DeserializedAPI = JsonConvert.DeserializeObject<Web_api>(json);

                eventLogLevel.Text = DeserializedAPI.loggerSettings.eventLogLevel.ToString();
                fileLogLevel.Text = DeserializedAPI.loggerSettings.fileLogLevel.ToString();
                if (DeserializedAPI.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedAPI.loggerSettings.fileNameOverride.ToString(); }
                else { fileNameOverride.Text = ""; }
                isLoggingEnabled.IsChecked = DeserializedAPI.loggerSettings.isLoggingEnabled;
                isLogServerSettings.IsChecked = DeserializedAPI.loggerSettings.isServerSettings;
                logFilePath.Text = DeserializedAPI.loggerSettings.logFilePath.ToString();

                ignoreCertErrors.IsChecked = DeserializedAPI.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
                isConfigured.IsChecked = DeserializedAPI.loggerSettings.logServerConnectionSettings.isConfigured;
                isServerSettingsConnectionSettings.IsChecked = DeserializedAPI.loggerSettings.logServerConnectionSettings.isServerSettings;
                password.Text = DeserializedAPI.loggerSettings.logServerConnectionSettings.password.ToString();
                settingsType.Text = DeserializedAPI.loggerSettings.logServerConnectionSettings.settingsType.ToString();
                urlLogServerConnectionSettings.Text = DeserializedAPI.loggerSettings.logServerConnectionSettings.url.ToString();
                userName.Text = DeserializedAPI.loggerSettings.logServerConnectionSettings.userName.ToString();
                logServerLogLevel.Text = DeserializedAPI.loggerSettings.logServerLogLevel.ToString();
                logToComplianceLog.IsChecked = DeserializedAPI.loggerSettings.logToComplianceLog;
                logToConsole.IsChecked = DeserializedAPI.loggerSettings.logToConsole;
                logToConsoleLevel.Text = DeserializedAPI.loggerSettings.logToConsoleLevel.ToString();
                logToEventLog.IsChecked = DeserializedAPI.loggerSettings.logToEventLog;
                logToFile.IsChecked = DeserializedAPI.loggerSettings.logToFile;
                logToLogServer.IsChecked = DeserializedAPI.loggerSettings.logToLogServer;
                maxFilesBeforeRollover.Text = DeserializedAPI.loggerSettings.maxFilesBeforeRollover.ToString();
                maxFileSizeInMB.Text = DeserializedAPI.loggerSettings.maxFileSizeInMB.ToString();

                urlSumoLogicConnectionSettings.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.url.ToString();
                retryInterval.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
                connectionTimeout.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
                flushingAccuracy.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
                maxFlushInterval.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
                messagesPerRequest.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
                maxQueueSizeBytes.Text = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

                logToSumoLogic.IsChecked = DeserializedAPI.loggerSettings.logToSumoLogic;
                sumoLogicLogLevel.Text = DeserializedAPI.loggerSettings.sumoLogicLogLevel.ToString();
                settingsType.Text = DeserializedAPI.loggerSettings.settingsType.ToString();
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

                Web_api DeserializedAPI = JsonConvert.DeserializeObject<Web_api>(json);

                // Build JSON
                var data = new JObject
                {
                    ["apiClientId"] = DeserializedAPI.apiClientId.ToString(),
                    ["rateLimitingEnabled"] = DeserializedAPI.rateLimitingEnabled,
                    ["maxConcurrentRequests"] = DeserializedAPI.maxConcurrentRequests.ToString(),
                    ["loadFromFileFromBin"] = DeserializedAPI.loadFromFileFromBin,
                    ["authenticationProvider"] = DeserializedAPI.authenticationProvider.ToString(),
                    ["trebuchetDataSource"] = DeserializedAPI.trebuchetDataSource.ToString(),
                    ["useSamlAdfsRedirect"] = DeserializedAPI.useSamlAdfsRedirect,
                    ["idpIsAdfs"] = DeserializedAPI.idpIsAdfs,
                    ["samlAdfsIdpInitiatedUrl"] = DeserializedAPI.samlAdfsIdpInitiatedUrl.ToString(),
                    ["samlServerTimeAllowance"] = DeserializedAPI.samlServerTimeAllowance.ToString(),
                    ["certificateUserEmail"] = DeserializedAPI.certificateUserEmail.ToString(),
                    ["certSubjectName"] = DeserializedAPI.certSubjectName.ToString(),
                    ["certIssuer"] = DeserializedAPI.certIssuer.ToString(),
                    ["certThumbprint"] = DeserializedAPI.certThumbprint.ToString(),
                    ["authorizationCodeExpirationMinutes"] = DeserializedAPI.authorizationCodeExpirationMinutes.ToString(),
                    ["recoveryFilePersistIntervalSeconds"] = DeserializedAPI.recoveryFilePersistIntervalSeconds.ToString(),
                    ["tenant"] = DeserializedAPI.tenant.ToString(),
                    ["audience"] = DeserializedAPI.audience.ToString(),
                    ["aadClientId"] = DeserializedAPI.aadClientId.ToString(),
                    ["aadClientUserEmail"] = DeserializedAPI.aadClientUserEmail.ToString(),
                    ["grantType"] = DeserializedAPI.grantType.ToString(),
                    ["authorizationUrl"] = DeserializedAPI.authorizationUrl.ToString(),
                    ["useRecoveryFile"] = DeserializedAPI.useRecoveryFile,
                    ["recoveryFilePath"] = DeserializedAPI.recoveryFilePath.ToString(),
                    ["recoveryFileName"] = DeserializedAPI.recoveryFileName.ToString(),
                    ["loggerSettings"] = DeserializedAPI.loggerSettings == null ? null : new JObject
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
                        ["logServerConnectionSettings"] = DeserializedAPI.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = ignoreCertErrors?.IsChecked,
                            ["isConfigured"] = isConfigured?.IsChecked,
                            ["isServerSettings"] = isServerSettingsConnectionSettings?.IsChecked,
                            ["password"] = password?.Text ?? "",
                            ["settingsType"] = settingsType?.Text ?? "",
                            ["url"] = urlLogServerConnectionSettings?.Text ?? "",
                            ["userName"] = userName?.Text ?? "",
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
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
