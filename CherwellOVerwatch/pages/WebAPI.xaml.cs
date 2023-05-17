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
            idpIsAdfs.IsChecked= DeserializedAPI.idpIsAdfs;
            samlAdfsIdpInitiatedUrl.Text = DeserializedAPI.samlAdfsIdpInitiatedUrl.ToString();
            samlServerTimeAllowance.Text = DeserializedAPI.samlServerTimeAllowance.ToString();
            certificateUserEmail.Text = DeserializedAPI.certificateUserEmail.ToString();
            certSubjectName.Text = DeserializedAPI.certSubjectName.ToString();
            certIssuer.Text= DeserializedAPI.certIssuer.ToString();
            certThumbprint.Text = DeserializedAPI.certThumbprint.ToString();
            authorizationCodeExpirationMinutes.Text = DeserializedAPI.authorizationCodeExpirationMinutes.ToString();
            recoveryFilePersistIntervalSeconds.Text = DeserializedAPI.recoveryFilePersistIntervalSeconds.ToString();
            tenant.Text = DeserializedAPI.tenant.ToString();
            audience.Text= DeserializedAPI.audience.ToString();
            aadClientId.Text= DeserializedAPI.aadClientId.ToString();
            aadClientUserEmail.Text = DeserializedAPI.aadClientUserEmail.ToString();
            grantType.Text = DeserializedAPI.grantType.ToString();
            authorizationUrl.Text = DeserializedAPI.authorizationUrl.ToString();
            useRecoveryFile.IsChecked = DeserializedAPI.useRecoveryFile;
            recoveryFilePath.Text= DeserializedAPI.recoveryFilePath.ToString();
            recoveryFileName.Text = DeserializedAPI.recoveryFileName.ToString();
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

            Browser_Settings DeserializedLogger = JsonConvert.DeserializeObject<Browser_Settings>(json);

            // Build JSON
            var data = new JObject
            {
                ["apiClientId"] = apiClientId?.Text ?? "",
                ["testMode"] = TestMode?.IsChecked,
                ["tabContentHeight"] = TabContentHEight?.Text ?? "",
                ["disableCertificateValidation"] = DisableCertificateValidation?.IsChecked,
                ["allowUnsafeLabels"] = AllowUnsafeLabels?.IsChecked,
                ["inlineBrowserDisplayExtensions"] = InLineBrowserDisplayExtensions?.Text ?? "",
                ["lookupAlwaysEnabled"] = LookupAlwaysEnabled?.IsChecked,
                ["queryRequestLimit"] = QueryRequestLimit?.Text ?? "",
                ["useCdn"] = UseCdn?.IsChecked,
                ["useHttpCompression"] = UseHttpCompression?.IsChecked,
                ["loadAllFilesIndividually"] = LoadAllFilesIndividually?.IsChecked,
                ["enableSessionSerialization"] = EnableSessionSerialization?.IsChecked,
                ["alwaysLoadKeys"] = AlwaysLoadKeys?.IsChecked,
                ["uiInteractionTimeoutInSeconds"] = UiInteractionTimeoutInSeconds?.Text ?? "",
                ["allowScriptsInReports"] = AllowScriptsInRecords?.IsChecked,
                ["disableAnchoring"] = DisableAnchoring?.IsChecked,
                ["disableSplitters"] = DisableSplitters?.IsChecked,
                ["useLegacyCompleteResponse"] = UseLegacyCompleteResponse?.IsChecked,
                ["signalRConnectionTimeoutInSeconds"] = SignalRConnectionTimeoutInSeconds?.Text ?? "",
                ["signalRDisconnectTimeoutInSeconds"] = SignalRDisconnectTimeoutInSeconds?.Text ?? "",
                ["signalRKeepAliveInSeconds"] = SignalRKeepAliveInSecconds?.Text ?? "",
                ["scanditLicenseKey"] = ScanditLicenseKey?.Text ?? "",
                ["redirectHttpToHttps"] = RedirectHttpToHttps?.IsChecked,
                ["enableInsecureDeepLinks"] = EnableInsecureDeepLinks?.IsChecked,
                ["autoSizeLabels"] = AutoSizeLabels?.IsChecked,
                ["authLogFile"] = AuthLogFile?.Text ?? "",
                ["defaultAuthMode"] = DefaultAuthMode?.Text ?? "",

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
