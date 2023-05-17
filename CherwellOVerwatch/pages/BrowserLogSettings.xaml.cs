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
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class BrowserLogSettings : Page
    {
        public string json;
        public string url = "http://localhost:5000/api/settings/BrowserSettings";
        public BrowserLogSettings()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                json = loader.GetResult(url);
                Browser_Settings DeserializedBrowserSettings = JsonConvert.DeserializeObject<Browser_Settings>(json);

                eventLogLevel.Text = DeserializedBrowserSettings.loggerSettings.eventLogLevel.ToString();
                fileLogLevel.Text = DeserializedBrowserSettings.loggerSettings.fileLogLevel.ToString();
                filenameOverride.Text = DeserializedBrowserSettings.loggerSettings.fileNameOverride.ToString();
                isLoggingEnabled.IsChecked = DeserializedBrowserSettings.loggerSettings.isLoggingEnabled;
                isServerSettings.IsChecked = DeserializedBrowserSettings.loggerSettings.isServerSettings;
                logFilePath.Text = DeserializedBrowserSettings.loggerSettings.logFilePath.ToString();
                logServerLogLevel.Text = DeserializedBrowserSettings.loggerSettings.logServerLogLevel.ToString();
                logToComplianceLog.IsChecked = DeserializedBrowserSettings.loggerSettings.logToComplianceLog;
                logToConsole.IsChecked = DeserializedBrowserSettings.loggerSettings.logToConsole;
                logToConsoleLevel.Text = DeserializedBrowserSettings.loggerSettings.logToConsoleLevel.ToString();
                LogToEventLog.IsChecked = DeserializedBrowserSettings.loggerSettings.logToEventLog;
                logToFile.IsChecked = DeserializedBrowserSettings.loggerSettings.logToFile;
                logToLogServer.IsChecked = DeserializedBrowserSettings.loggerSettings.logToLogServer;
                maxFilesBeforeRollover.Text = DeserializedBrowserSettings.loggerSettings.maxFilesBeforeRollover.ToString();
                maxFileSizeInMB.Text = DeserializedBrowserSettings.loggerSettings.maxFileSizeInMB.ToString();
                logToSumoLogic.IsChecked = DeserializedBrowserSettings.loggerSettings.logToSumoLogic;
                sumoLogicLogLevel.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicLogLevel.ToString();
                settingsType.Text = DeserializedBrowserSettings.loggerSettings.settingsType.ToString();

                //LOG SERVER CONNECTION SETTINGS

                ignoreCertErrors.IsChecked = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
                isConfigured.IsChecked = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.isConfigured;
                isServerSettingsConnectionSettings.IsChecked = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.isServerSettings;
                password.Text = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.password.ToString();
                settingsTypeConnection.Text = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.settingsType.ToString();
                urlLogServerConnection.Text = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.url.ToString();
                userName.Text = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings.userName.ToString();

                //SUMO LOGIC CONNECTION SETTINGS

                urlSumoLogic.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.url.ToString();
                retryInterval.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
                connectionTimeout.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
                flushingAccuracy.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
                maxFlushInterval.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
                messagePerRequest.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
                maxQueueSizeBytes.Text = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();
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

                Browser_Settings DeserializedBrowserSettings = JsonConvert.DeserializeObject<Browser_Settings>(json);

                // Build JSON
                var data = new JObject
                {
                    ["TrebuchetDataSource"] = DeserializedBrowserSettings.trebuchetDataSource.ToString(),
                    ["TestMode"] = DeserializedBrowserSettings.testMode,
                    ["TabContentHeight"] = DeserializedBrowserSettings.tabContentHeight.ToString(),
                    ["DisableCertificateValidation"] = DeserializedBrowserSettings.disableCertificateValidation,
                    ["DisableCertificateValidation"] = DeserializedBrowserSettings.disableCertificateValidation,
                    ["AllowUnsafeLabels"] = DeserializedBrowserSettings.allowUnsafeLabels,
                    ["InLineBrowserDisplayExtensions"] = DeserializedBrowserSettings.inlineBrowserDisplayExtensions.ToString(),
                    ["LookupAlwaysEnabled"] = DeserializedBrowserSettings.lookupAlwaysEnabled,
                    ["QueryRequestLimit"] = DeserializedBrowserSettings.queryRequestLimit.ToString(),
                    ["UseCdn"] = DeserializedBrowserSettings.useCdn,
                    ["UseHttpCompression"] = DeserializedBrowserSettings.useHttpCompression,
                    ["LoadAllFilesIndividually"] = DeserializedBrowserSettings.loadAllFilesIndividually,
                    ["EnableSessionSerialization"] = DeserializedBrowserSettings.enableSessionSerialization,
                    ["AlwaysLoadKeys"] = DeserializedBrowserSettings.alwaysLoadKeys,
                    ["UiInteractionTimeoutInSeconds"] = DeserializedBrowserSettings.uiInteractionTimeoutInSeconds.ToString(),
                    ["AllowScriptsInRecords"] = DeserializedBrowserSettings.allowScriptsInReports,
                    ["DisableAnchoring"] = DeserializedBrowserSettings.disableAnchoring,
                    ["DisableSplitters"] = DeserializedBrowserSettings.disableSplitters,
                    ["UseLegacyCompleteResponse"] = DeserializedBrowserSettings.useLegacyCompleteResponse,
                    ["SignalRConnectionTimeoutInSeconds"] = DeserializedBrowserSettings.signalRConnectionTimeoutInSeconds.ToString(),
                    ["SignalRDisconnectTimeoutInSeconds"] = DeserializedBrowserSettings.signalRDisconnectTimeoutInSeconds.ToString(),
                    ["SignalRKeepAliveInSecconds"] = DeserializedBrowserSettings.signalRKeepAliveInSeconds.ToString(),
                    ["ScanditLicenseKey"] = DeserializedBrowserSettings.scanditLicenseKey.ToString(),
                    ["RedirectHttpToHttps"] = DeserializedBrowserSettings.redirectHttpToHttps,
                    ["EnableInsecureDeepLinks"] = DeserializedBrowserSettings.enableInsecureDeepLinks,
                    ["AutoSizeLabels"] = DeserializedBrowserSettings.autoSizeLabels,
                    ["AuthLogFile"] = DeserializedBrowserSettings.authLogFile.ToString(),
                    ["DefaultAuthMode"] = DeserializedBrowserSettings.defaultAuthMode.ToString(),
                    ["loggerSettings"] = DeserializedBrowserSettings.loggerSettings == null ? null : new JObject
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
                        ["logServerConnectionSettings"] = DeserializedBrowserSettings.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = ignoreCertErrors?.IsChecked,
                            ["isConfigured"] = isConfigured?.IsChecked,
                            ["isServerSettings"] = isServerSettings?.IsChecked,
                            ["password"] = password?.Text ?? "",
                            ["settingsType"] = settingsType?.Text ?? "",
                            ["url"] = urlLogServerConnection?.Text ?? "",
                            ["userName"] = userName?.Text ?? "",
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedBrowserSettings.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
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
