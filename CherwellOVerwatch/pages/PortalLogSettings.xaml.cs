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
    public partial class PortalLogSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/PortalSettings";
        public string json;
        public PortalLogSettings()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            LoadSettings loader = new LoadSettings();
            json = loader.GetResult(url);
            Portal_Settings DeserializedPortalLogSettings = JsonConvert.DeserializeObject<Portal_Settings>(json);

            eventLogLevel.Text = DeserializedPortalLogSettings.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedPortalLogSettings.loggerSettings.fileLogLevel.ToString();
            if (DeserializedPortalLogSettings.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedPortalLogSettings.loggerSettings.fileNameOverride.ToString(); }
            else { fileNameOverride.Text = ""; }
            isLoggingEnabled.IsChecked = DeserializedPortalLogSettings.loggerSettings.isLoggingEnabled;
            isLogServerSettings.IsChecked = DeserializedPortalLogSettings.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedPortalLogSettings.loggerSettings.logFilePath.ToString();

            ignoreCertErrors.IsChecked = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.isServerSettings;
            password.Text = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.password.ToString();
            settingsType.Text = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.settingsType.ToString();
            urlLogServerConnectionSettings.Text = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.url.ToString();
            userName.Text = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings.userName.ToString();
            logServerLogLevel.Text = DeserializedPortalLogSettings.loggerSettings.logServerLogLevel.ToString();
            logToComplianceLog.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToComplianceLog;
            logToConsole.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToConsole;
            logToConsoleLevel.Text = DeserializedPortalLogSettings.loggerSettings.logToConsoleLevel.ToString();
            logToEventLog.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToEventLog;
            logToFile.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToFile;
            logToLogServer.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToLogServer;
            maxFilesBeforeRollover.Text = DeserializedPortalLogSettings.loggerSettings.maxFilesBeforeRollover.ToString();
            maxFileSizeInMB.Text = DeserializedPortalLogSettings.loggerSettings.maxFileSizeInMB.ToString();

            urlSumoLogicConnectionSettings.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.url.ToString();
            retryInterval.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
            connectionTimeout.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
            flushingAccuracy.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
            maxFlushInterval.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
            messagesPerRequest.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
            maxQueueSizeBytes.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();

            logToSumoLogic.IsChecked = DeserializedPortalLogSettings.loggerSettings.logToSumoLogic;
            sumoLogicLogLevel.Text = DeserializedPortalLogSettings.loggerSettings.sumoLogicLogLevel.ToString();
            settingsType.Text = DeserializedPortalLogSettings.loggerSettings.settingsType.ToString();
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

                Portal_Settings DeserializedPortalLogSettings = JsonConvert.DeserializeObject<Portal_Settings>(json);

                // Build JSON
                var data = new JObject
                {
                    ["trebuchetDataSource"] = DeserializedPortalLogSettings.trebuchetDataSource.ToString(),
                    ["testMode"] = DeserializedPortalLogSettings.testMode,
                    ["tabContentHeight"] = DeserializedPortalLogSettings.tabContentHeight.ToString(),
                    ["disableCertificateValidation"] = DeserializedPortalLogSettings.disableCertificateValidation,
                    ["allowUnsafeLabels"] = DeserializedPortalLogSettings.allowUnsafeLabels,
                    ["inlineBrowserDisplayExtensions"] = DeserializedPortalLogSettings.inlineBrowserDisplayExtensions.ToString(),
                    ["lookupAlwaysEnabled"] = DeserializedPortalLogSettings.lookupAlwaysEnabled,
                    ["queryRequestLimit"] = DeserializedPortalLogSettings.queryRequestLimit.ToString(),
                    ["useCdn"] = DeserializedPortalLogSettings.useCdn,
                    ["useHttpCompression"] = DeserializedPortalLogSettings.useHttpCompression,
                    ["loadAllFilesIndividually"] = DeserializedPortalLogSettings.loadAllFilesIndividually,
                    ["enableSessionSerialization"] = DeserializedPortalLogSettings.enableSessionSerialization,
                    ["alwaysLoadKeys"] = DeserializedPortalLogSettings.alwaysLoadKeys,
                    ["uiInteractionTimeoutInSeconds"] = DeserializedPortalLogSettings.uiInteractionTimeoutInSeconds.ToString(),
                    ["allowScriptsInReports"] = DeserializedPortalLogSettings.allowScriptsInReports,
                    ["signalRConnectionTimeoutInSeconds"] = DeserializedPortalLogSettings.signalRConnectionTimeoutInSeconds.ToString(),
                    ["signalRDisconnectTimeoutInSeconds"] = DeserializedPortalLogSettings.signalRDisconnectTimeoutInSeconds.ToString(),
                    ["signalRKeepAliveInSeconds"] = DeserializedPortalLogSettings.signalRKeepAliveInSeconds.ToString(),
                    ["disableAnchoring"] = DeserializedPortalLogSettings.disableAnchoring,
                    ["disableSplitters"] = DeserializedPortalLogSettings.disableSplitters,
                    ["useLegacyCompleteResponse"] = DeserializedPortalLogSettings.useLegacyCompleteResponse,
                    ["scanditLicenseKey"] = DeserializedPortalLogSettings.scanditLicenseKey.ToString(),
                    ["redirectHttpToHttps"] = DeserializedPortalLogSettings.redirectHttpToHttps,
                    ["enableInsecureDeepLinks"] = DeserializedPortalLogSettings.enableInsecureDeepLinks,
                    ["autoSizeLabels"] = DeserializedPortalLogSettings.autoSizeLabels,
                    ["authLogFile"] = DeserializedPortalLogSettings.authLogFile.ToString(),
                    ["defaultAuthMode"] = DeserializedPortalLogSettings.defaultAuthMode.ToString(),
                    ["loggerSettings"] = DeserializedPortalLogSettings.loggerSettings == null ? null : new JObject
                    {
                        ["eventLogLevel"] = eventLogLevel?.Text ?? "",
                        ["fileLogLevel"] = fileLogLevel?.Text ?? "",
                        ["fileNameOverride"] = fileNameOverride?.Text ?? "",
                        ["isLoggingEnabled"] = isLoggingEnabled?.IsChecked,
                        ["isServerSettings"] = isServerSettingsConnectionSettings?.IsChecked,
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
                        ["logServerConnectionSettings"] = DeserializedPortalLogSettings.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = ignoreCertErrors?.IsChecked,
                            ["isConfigured"] = isConfigured?.IsChecked,
                            ["isServerSettings"] = isServerSettingsConnectionSettings?.IsChecked,
                            ["password"] = password?.Text ?? "",
                            ["settingsType"] = settingsType?.Text ?? "",
                            ["url"] = urlLogServerConnectionSettings?.Text ?? "",
                            ["userName"] = userName?.Text ?? "",
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedPortalLogSettings.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
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
