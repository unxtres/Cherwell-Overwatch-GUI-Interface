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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;

namespace CherwellOVerwatch
{
    public partial class PortalSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/PortalSettings";
        public string json;
        public PortalSettings()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                json = loader.GetResult(url);
                Portal_Settings DeserializedPortalSettings = JsonConvert.DeserializeObject<Portal_Settings>(json);

                trebuchetDataSource.Text = DeserializedPortalSettings.trebuchetDataSource.ToString();
                testMode.IsChecked = DeserializedPortalSettings.testMode;
                tabContentHeight.Text = DeserializedPortalSettings.tabContentHeight.ToString();
                disableCertificateValidation.IsChecked = DeserializedPortalSettings.disableCertificateValidation;
                allowUnsafeLabels.IsChecked = DeserializedPortalSettings.allowUnsafeLabels;
                inlineBrowserDisplayExtensions.Text = DeserializedPortalSettings.inlineBrowserDisplayExtensions.ToString();
                lookupAlwaysEnabled.IsChecked = DeserializedPortalSettings.lookupAlwaysEnabled;
                queryRequestLimit.Text = DeserializedPortalSettings.queryRequestLimit.ToString();
                useCdn.IsChecked = DeserializedPortalSettings.useCdn;
                useHttpCompression.IsChecked = DeserializedPortalSettings.useHttpCompression;
                loadAllFilesIndividually.IsChecked = DeserializedPortalSettings.loadAllFilesIndividually;
                enableSessionSerialization.IsChecked = DeserializedPortalSettings.enableSessionSerialization;
                alwaysLoadKeys.IsChecked = DeserializedPortalSettings.alwaysLoadKeys;
                uiInteractionTimeoutInSeconds.Text = DeserializedPortalSettings.uiInteractionTimeoutInSeconds.ToString();
                allowScriptsInReports.IsChecked = DeserializedPortalSettings.allowScriptsInReports;
                signalRConnectionTimeoutInSeconds.Text = DeserializedPortalSettings.signalRConnectionTimeoutInSeconds.ToString();
                signalRDisconnectTimeoutInSeconds.Text = DeserializedPortalSettings.signalRDisconnectTimeoutInSeconds.ToString();
                signalRKeepAliveInSeconds.Text = DeserializedPortalSettings.signalRKeepAliveInSeconds.ToString();
                disableAnchoring.IsChecked = DeserializedPortalSettings.disableAnchoring;
                disableSplitters.IsChecked = DeserializedPortalSettings.disableSplitters;
                useLegacyCompleteResponse.IsChecked = DeserializedPortalSettings.useLegacyCompleteResponse;
                scanditLicenseKey.Text = DeserializedPortalSettings.scanditLicenseKey.ToString();
                redirectHttpToHttps.IsChecked = DeserializedPortalSettings.redirectHttpToHttps;
                enableInsecureDeepLinks.IsChecked = DeserializedPortalSettings.enableInsecureDeepLinks;
                autoSizeLabels.IsChecked = DeserializedPortalSettings.autoSizeLabels;
                authLogFile.Text = DeserializedPortalSettings.authLogFile.ToString();
                defaultAuthMode.Text = DeserializedPortalSettings.defaultAuthMode.ToString();
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

                Portal_Settings DeserializedPortalSettings = JsonConvert.DeserializeObject<Portal_Settings>(json);

                // Build JSON
                var data = new JObject
                {
                    ["trebuchetDataSource"] = trebuchetDataSource?.Text ?? "",
                    ["testMode"] = testMode?.IsChecked,
                    ["tabContentHeight"] = tabContentHeight?.Text ?? "",
                    ["disableCertificateValidation"] = disableCertificateValidation?.IsChecked,
                    ["allowUnsafeLabels"] = allowUnsafeLabels?.IsChecked,
                    ["inlineBrowserDisplayExtensions"] = inlineBrowserDisplayExtensions?.Text ?? "",
                    ["lookupAlwaysEnabled"] = lookupAlwaysEnabled?.IsChecked,
                    ["queryRequestLimit"] = queryRequestLimit?.Text ?? "",
                    ["useCdn"] = useCdn?.IsChecked,
                    ["useHttpCompression"] = useHttpCompression?.IsChecked,
                    ["loadAllFilesIndividually"] = loadAllFilesIndividually?.IsChecked,
                    ["enableSessionSerialization"] = enableSessionSerialization?.IsChecked,
                    ["alwaysLoadKeys"] = alwaysLoadKeys?.IsChecked,
                    ["uiInteractionTimeoutInSeconds"] = uiInteractionTimeoutInSeconds?.Text ?? "",
                    ["allowScriptsInReports"] = allowScriptsInReports?.IsChecked,
                    ["signalRConnectionTimeoutInSeconds"] = signalRConnectionTimeoutInSeconds?.Text ?? "",
                    ["signalRDisconnectTimeoutInSeconds"] = signalRDisconnectTimeoutInSeconds?.Text ?? "",
                    ["signalRKeepAliveInSeconds"] = signalRKeepAliveInSeconds?.Text ?? "",
                    ["disableAnchoring"] = disableAnchoring?.IsChecked,
                    ["disableSplitters"] = disableSplitters?.IsChecked,
                    ["useLegacyCompleteResponse"] = useLegacyCompleteResponse?.IsChecked,
                    ["scanditLicenseKey"] = scanditLicenseKey?.Text ?? "",
                    ["redirectHttpToHttps"] = redirectHttpToHttps?.IsChecked,
                    ["enableInsecureDeepLinks"] = enableInsecureDeepLinks?.IsChecked,
                    ["autoSizeLabels"] = autoSizeLabels?.IsChecked,
                    ["authLogFile"] = authLogFile?.Text ?? "",
                    ["defaultAuthMode"] = defaultAuthMode?.Text ?? "",
                    ["loggerSettings"] = DeserializedPortalSettings.loggerSettings == null ? null : new JObject
                    {
                        ["eventLogLevel"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.eventLogLevel),
                        ["fileLogLevel"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.fileLogLevel),
                        ["fileNameOverride"] = Convert.ToString(DeserializedPortalSettings.loggerSettings.fileNameOverride),
                        ["isLoggingEnabled"] = DeserializedPortalSettings.loggerSettings.isLoggingEnabled,
                        ["isServerSettings"] = DeserializedPortalSettings.loggerSettings.isServerSettings,
                        ["logFilePath"] = Convert.ToString(DeserializedPortalSettings.loggerSettings.logFilePath),

                        ["logServerLogLevel"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.logServerLogLevel),
                        ["logToComplianceLog"] = DeserializedPortalSettings.loggerSettings.logToComplianceLog,
                        ["logToConsole"] = DeserializedPortalSettings.loggerSettings.logToConsole,
                        ["logToConsoleLevel"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.logToConsoleLevel),
                        ["logToEventLog"] = DeserializedPortalSettings.loggerSettings.logToEventLog,
                        ["logToFile"] = DeserializedPortalSettings.loggerSettings.logToFile,
                        ["logToLogServer"] = DeserializedPortalSettings.loggerSettings.logToLogServer,
                        ["maxFilesBeforeRollover"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.maxFilesBeforeRollover),
                        ["maxFileSizeInMB"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.maxFileSizeInMB),
                        ["logToSumoLogic"] = DeserializedPortalSettings.loggerSettings.logToSumoLogic,
                        ["sumoLogicLogLevel"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.sumoLogicLogLevel),
                        ["settingsType"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.settingsType),
                        ["logServerConnectionSettings"] = DeserializedPortalSettings.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = DeserializedPortalSettings.loggerSettings.logServerConnectionSettings.ignoreCertErrors,
                            ["isConfigured"] = DeserializedPortalSettings.loggerSettings.logServerConnectionSettings.isConfigured,
                            ["isServerSettings"] = DeserializedPortalSettings.loggerSettings.logServerConnectionSettings.isServerSettings,
                            ["password"] = Convert.ToString(DeserializedPortalSettings.loggerSettings.logServerConnectionSettings.password),
                            ["settingsType"] = Convert.ToString(DeserializedPortalSettings.loggerSettings.logServerConnectionSettings.settingsType),
                            ["url"] = Convert.ToString(DeserializedPortalSettings.loggerSettings.logServerConnectionSettings.url),
                            ["userName"] = Convert.ToString(DeserializedPortalSettings.loggerSettings.logServerConnectionSettings.userName),
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedPortalSettings.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
                        {
                            ["url"] = Convert.ToString(DeserializedPortalSettings.loggerSettings.sumoLogicConnectionSettings.url),
                            ["retryInterval"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.sumoLogicConnectionSettings.retryInterval),
                            ["connectionTimeout"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.sumoLogicConnectionSettings.connectionTimeout),
                            ["flushingAccuracy"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy),
                            ["maxFlushInterval"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval),
                            ["messagesPerRequest"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest),
                            ["maxQueueSizeBytes"] = Convert.ToInt32(DeserializedPortalSettings.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes)
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
