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
    public partial class WebAPI : Page
    {
        public string url = "http://localhost:5000/api/settings/WebApiServiceSettings";
        public string json;
        public WebAPI()
        {
            InitializeComponent();
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            LoadSettings loader = new LoadSettings();
            json = loader.GetResult(url);
            Web_api DeserializedAPI = JsonConvert.DeserializeObject<Web_api>(json);

            apiClientId.Text = DeserializedAPI.apiClientId.ToString();
            rateLimitingEnabled.IsChecked = DeserializedAPI.rateLimitingEnabled;
            maxConcurrentRequests.Text = DeserializedAPI.maxConcurrentRequests.ToString();
            loadFromFileFromBin.IsChecked = DeserializedAPI.loadFromFileFromBin;
            authenticationProvider.Text = DeserializedAPI.authenticationProvider.ToString();
            trebuchetDataSource.Text = DeserializedAPI.trebuchetDataSource.ToString();
            useSamlAdfsRedirect.IsChecked = DeserializedAPI.useSamlAdfsRedirect;
            idpIsAdfs.IsChecked = DeserializedAPI.idpIsAdfs;
            samlAdfsIdpInitiatedUrl.Text = DeserializedAPI.samlAdfsIdpInitiatedUrl.ToString();
            samlServerTimeAllowance.Text = DeserializedAPI.samlServerTimeAllowance.ToString();
            certificateUserEmail.Text = DeserializedAPI.certificateUserEmail.ToString();
            certSubjectName.Text = DeserializedAPI.certSubjectName.ToString();
            certIssuer.Text = DeserializedAPI.certIssuer.ToString();
            certThumbprint.Text = DeserializedAPI.certThumbprint.ToString();
            authorizationCodeExpirationMinutes.Text = DeserializedAPI.authorizationCodeExpirationMinutes.ToString();
            recoveryFilePersistIntervalSeconds.Text = DeserializedAPI.recoveryFilePersistIntervalSeconds.ToString();
            tenant.Text = DeserializedAPI.tenant.ToString();
            audience.Text = DeserializedAPI.audience.ToString();
            aadClientId.Text = DeserializedAPI.aadClientId.ToString();
            aadClientUserEmail.Text = DeserializedAPI.aadClientUserEmail.ToString();
            grantType.Text = DeserializedAPI.grantType.ToString();
            authorizationUrl.Text = DeserializedAPI.authorizationUrl.ToString();
            useRecoveryFile.IsChecked = DeserializedAPI.useRecoveryFile;
            recoveryFilePath.Text = DeserializedAPI.recoveryFilePath.ToString();
            recoveryFileName.Text = DeserializedAPI.recoveryFileName.ToString();
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
                    ["apiClientId"] = apiClientId?.Text ?? "",
                    ["rateLimitingEnabled"] = rateLimitingEnabled?.IsChecked,
                    ["maxConcurrentRequests"] = maxConcurrentRequests?.Text ?? "",
                    ["loadFromFileFromBin"] = loadFromFileFromBin?.IsChecked,
                    ["authenticationProvider"] = authenticationProvider?.Text ?? "",
                    ["trebuchetDataSource"] = trebuchetDataSource?.Text ?? "",
                    ["useSamlAdfsRedirect"] = useSamlAdfsRedirect?.IsChecked,
                    ["idpIsAdfs"] = idpIsAdfs?.IsChecked,
                    ["samlAdfsIdpInitiatedUrl"] = samlAdfsIdpInitiatedUrl?.Text ?? "",
                    ["samlServerTimeAllowance"] = samlServerTimeAllowance?.Text ?? "",
                    ["certificateUserEmail"] = certificateUserEmail?.Text ?? "",
                    ["certSubjectName"] = certSubjectName?.Text ?? "",
                    ["certIssuer"] = certIssuer?.Text ?? "",
                    ["certThumbprint"] = certThumbprint?.Text ?? "",
                    ["authorizationCodeExpirationMinutes"] = authorizationCodeExpirationMinutes?.Text ?? "",
                    ["recoveryFilePersistIntervalSeconds"] = recoveryFilePersistIntervalSeconds?.Text ?? "",
                    ["tenant"] = tenant?.Text ?? "",
                    ["audience"] = audience?.Text ?? "",
                    ["aadClientId"] = aadClientId?.Text ?? "",
                    ["aadClientUserEmail"] = aadClientUserEmail?.Text ?? "",
                    ["grantType"] = grantType?.Text ?? "",
                    ["authorizationUrl"] = authorizationUrl?.Text ?? "",
                    ["useRecoveryFile"] = useRecoveryFile?.IsChecked,
                    ["recoveryFilePath"] = recoveryFilePath?.Text ?? "",
                    ["recoveryFileName"] = recoveryFileName?.Text ?? "",

                    ["loggerSettings"] = DeserializedAPI.loggerSettings == null ? null : new JObject
                    {
                        ["eventLogLevel"] = Convert.ToInt32(DeserializedAPI.loggerSettings.eventLogLevel),
                        ["fileLogLevel"] = Convert.ToInt32(DeserializedAPI.loggerSettings.fileLogLevel),
                        ["fileNameOverride"] = Convert.ToString(DeserializedAPI.loggerSettings.fileNameOverride),
                        ["isLoggingEnabled"] = DeserializedAPI.loggerSettings.isLoggingEnabled,
                        ["isServerSettings"] = DeserializedAPI.loggerSettings.isServerSettings,
                        ["logFilePath"] = Convert.ToString(DeserializedAPI.loggerSettings.logFilePath),

                        ["logServerLogLevel"] = Convert.ToInt32(DeserializedAPI.loggerSettings.logServerLogLevel),
                        ["logToComplianceLog"] = DeserializedAPI.loggerSettings.logToComplianceLog,
                        ["logToConsole"] = DeserializedAPI.loggerSettings.logToConsole,
                        ["logToConsoleLevel"] = Convert.ToInt32(DeserializedAPI.loggerSettings.logToConsoleLevel),
                        ["logToEventLog"] = DeserializedAPI.loggerSettings.logToEventLog,
                        ["logToFile"] = DeserializedAPI.loggerSettings.logToFile,
                        ["logToLogServer"] = DeserializedAPI.loggerSettings.logToLogServer,
                        ["maxFilesBeforeRollover"] = Convert.ToInt32(DeserializedAPI.loggerSettings.maxFilesBeforeRollover),
                        ["maxFileSizeInMB"] = Convert.ToInt32(DeserializedAPI.loggerSettings.maxFileSizeInMB),
                        ["logToSumoLogic"] = DeserializedAPI.loggerSettings.logToSumoLogic,
                        ["sumoLogicLogLevel"] = Convert.ToInt32(DeserializedAPI.loggerSettings.sumoLogicLogLevel),
                        ["settingsType"] = Convert.ToInt32(DeserializedAPI.loggerSettings.settingsType),
                        ["logServerConnectionSettings"] = DeserializedAPI.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = DeserializedAPI.loggerSettings.logServerConnectionSettings.ignoreCertErrors,
                            ["isConfigured"] = DeserializedAPI.loggerSettings.logServerConnectionSettings.isConfigured,
                            ["isServerSettings"] = DeserializedAPI.loggerSettings.logServerConnectionSettings.isServerSettings,
                            ["password"] = Convert.ToString(DeserializedAPI.loggerSettings.logServerConnectionSettings.password),
                            ["settingsType"] = Convert.ToString(DeserializedAPI.loggerSettings.logServerConnectionSettings.settingsType),
                            ["url"] = Convert.ToString(DeserializedAPI.loggerSettings.logServerConnectionSettings.url),
                            ["userName"] = Convert.ToString(DeserializedAPI.loggerSettings.logServerConnectionSettings.userName),
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedAPI.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
                        {
                            ["url"] = Convert.ToString(DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.url),
                            ["retryInterval"] = Convert.ToInt32(DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.retryInterval),
                            ["connectionTimeout"] = Convert.ToInt32(DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.connectionTimeout),
                            ["flushingAccuracy"] = Convert.ToInt32(DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy),
                            ["maxFlushInterval"] = Convert.ToInt32(DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval),
                            ["messagesPerRequest"] = Convert.ToInt32(DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest),
                            ["maxQueueSizeBytes"] = Convert.ToInt32(DeserializedAPI.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes)
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
