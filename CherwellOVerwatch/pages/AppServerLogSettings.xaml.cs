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
    public partial class AppServerLogSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/AppServerSettings";
        public string json;
        public AppServerLogSettings()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                json = loader.GetResult(url);
                ApplicationServer DeserializedLogger = JsonConvert.DeserializeObject<ApplicationServer>(json);

                eventLogLevel.Text = DeserializedLogger.loggerSettings.eventLogLevel.ToString();
                fileLogLevel.Text = DeserializedLogger.loggerSettings.fileLogLevel.ToString();
                if (DeserializedLogger.loggerSettings.fileNameOverride == null)
                    filenameOverride.Text = "";
                else
                    filenameOverride.Text = DeserializedLogger.loggerSettings.fileNameOverride.ToString();
                isLoggingEnabled.IsChecked = DeserializedLogger.loggerSettings.isLoggingEnabled;
                isServerSettings.IsChecked = DeserializedLogger.loggerSettings.isServerSettings;
                logFilePath.Text = DeserializedLogger.loggerSettings.logFilePath.ToString();
                logServerLogLevel.Text = DeserializedLogger.loggerSettings.logServerLogLevel.ToString();
                logToComplianceLog.IsChecked = DeserializedLogger.loggerSettings.logToComplianceLog;
                logToConsole.IsChecked = DeserializedLogger.loggerSettings.logToConsole;
                logToConsoleLevel.Text = DeserializedLogger.loggerSettings.logToConsoleLevel.ToString();
                LogToEventLog.IsChecked = DeserializedLogger.loggerSettings.logToEventLog;
                logToFile.IsChecked = DeserializedLogger.loggerSettings.logToFile;
                logToLogServer.IsChecked = DeserializedLogger.loggerSettings.logToLogServer;
                maxFilesBeforeRollover.Text = DeserializedLogger.loggerSettings.maxFilesBeforeRollover.ToString();
                maxFileSizeInMB.Text = DeserializedLogger.loggerSettings.maxFileSizeInMB.ToString();
                logToSumoLogic.IsChecked = DeserializedLogger.loggerSettings.logToSumoLogic;
                sumoLogicLogLevel.Text = DeserializedLogger.loggerSettings.sumoLogicLogLevel.ToString();
                settingsType.Text = DeserializedLogger.loggerSettings.settingsType.ToString();

                //LOG SERVER CONNECTION SETTINGS

                ignoreCertErrors.IsChecked = DeserializedLogger.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
                isConfigured.IsChecked = DeserializedLogger.loggerSettings.logServerConnectionSettings.isConfigured;
                isServerSettingsConnectionSettings.IsChecked = DeserializedLogger.loggerSettings.logServerConnectionSettings.isServerSettings;
                password.Text = DeserializedLogger.loggerSettings.logServerConnectionSettings.password.ToString();
                settingsTypeConnection.Text = DeserializedLogger.loggerSettings.logServerConnectionSettings.settingsType.ToString();
                urlLogServerConnection.Text = DeserializedLogger.loggerSettings.logServerConnectionSettings.url.ToString();
                userName.Text = DeserializedLogger.loggerSettings.logServerConnectionSettings.userName.ToString();

                //SUMO LOGIC CONNECTION SETTINGS

                urlSumoLogic.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.url.ToString();
                retryInterval.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.retryInterval.ToString();
                connectionTimeout.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.connectionTimeout.ToString();
                flushingAccuracy.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.flushingAccuracy.ToString();
                maxFlushInterval.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.maxFlushInterval.ToString();
                messagePerRequest.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.messagesPerRequest.ToString();
                maxQueueSizeBytes.Text = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings.maxQueueSizeBytes.ToString();
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

                ApplicationServer DeserializedLogger = JsonConvert.DeserializeObject<ApplicationServer>(json);

                // Build JSON
                var data = new JObject
                {
                    ["disableCompression"] = DeserializedLogger.disableCompression,
                    ["installed"] = DeserializedLogger.installed,
                    ["lastError"] = DeserializedLogger.lastError.ToString(),
                    ["lastErrorDetails"] = DeserializedLogger.lastErrorDetails.ToString(),
                    ["isHttp"] = DeserializedLogger.isHttp,
                    ["isTcp"] = DeserializedLogger.isTcp,
                    ["appServerHostMode"] = DeserializedLogger.appServerHostMode.ToString(),
                    ["protocol"] = DeserializedLogger.protocol.ToString(),
                    ["connection"] = DeserializedLogger.connection.ToString(),
                    ["enableTcpOption"] = DeserializedLogger.enableTcpOption,
                    ["instanceGuid"] = DeserializedLogger.instanceGuid.ToString(),
                    ["oldTcpPort"] = DeserializedLogger.oldTcpPort.ToString(),
                    ["port"] = DeserializedLogger.port.ToString(),
                    ["useRest"] = DeserializedLogger.useRest,
                    ["securityMode"] = DeserializedLogger.securityMode.ToString(),
                    ["serverName"] = DeserializedLogger.serverName.ToString(),
                    ["serverConfigToolComments"] = DeserializedLogger.serverConfigToolComments.ToString(),
                    ["loggedInUserCacheExpiryMins"] = DeserializedLogger.loggedInUserCacheExpiryMins.ToString(),
                    ["useRecoveryFile"] = DeserializedLogger.useRecoveryFile,
                    ["recoveryFilePath"] = DeserializedLogger.recoveryFilePath.ToString(),
                    ["recoveryFileName"] = DeserializedLogger.recoveryFileName.ToString(),
                    ["recoveryFilePersistIntervalSeconds"] = DeserializedLogger.recoveryFilePersistIntervalSeconds.ToString(),
                    ["minMessageSizeToCompressHigh"] = DeserializedLogger.minMessageSizeToCompressHigh.ToString(),
                    ["minMessageSizeToCompressLow"] = DeserializedLogger.minMessageSizeToCompressLow.ToString(),
                    ["minMessageSizeToCompressMedium"] = DeserializedLogger.minMessageSizeToCompressMedium.ToString(),
                    ["wcfMaxBufferPoolSize"] = DeserializedLogger.wcfMaxBufferPoolSize.ToString(),
                    ["wcfMaxBufferSize"] = DeserializedLogger.wcfMaxBufferSize.ToString(),
                    ["wcfMaxReceivedMessageSize"] = DeserializedLogger.wcfMaxReceivedMessageSize.ToString(),
                    ["wcfReaderMaxNameTableCharCount"] = DeserializedLogger.wcfReaderMaxNameTableCharCount.ToString(),
                    ["wcfReaderMaxStringContentLength"] = DeserializedLogger.wcfReaderMaxStringContentLength.ToString(),
                    ["wcfReaderMaxArrayLength"] = DeserializedLogger.wcfReaderMaxArrayLength.ToString(),
                    ["wcfOperationTimeoutOverride"] = DeserializedLogger.wcfOperationTimeoutOverride.ToString(),
                    ["wcfUseMessageCompression"] = DeserializedLogger.wcfUseMessageCompression,
                    ["wcfTcpMaxConnections"] = DeserializedLogger.wcfTcpMaxConnections.ToString(),
                    ["wcfMaxConcurrentCalls"] = DeserializedLogger.wcfMaxConcurrentCalls.ToString(),
                    ["wcfMaxConcurrentInstances"] = DeserializedLogger.wcfMaxConcurrentInstances.ToString(),
                    ["wcfMaxConcurrentSessions"] = DeserializedLogger.wcfMaxConcurrentSessions.ToString(),
                    ["wcfEnablePerformanceCounters"] = DeserializedLogger.wcfEnablePerformanceCounters,
                    ["wcfListenBacklog"] = DeserializedLogger.wcfListenBacklog.ToString(),
                    ["certificateStoreLocation"] = DeserializedLogger.certificateStoreLocation.ToString(),
                    ["certificateStoreName"] = DeserializedLogger.certificateStoreName.ToString(),
                    ["certificateSubject"] = DeserializedLogger.certificateSubject.ToString(),
                    ["certificateThumbprint"] = DeserializedLogger.certificateThumbprint.ToString(),
                    ["certificateValidationModeForAutoClient"] = DeserializedLogger.certificateValidationModeForAutoClient.ToString(),
                    ["loggerSettings"] = DeserializedLogger.loggerSettings == null ? null : new JObject
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
                        ["logServerConnectionSettings"] = DeserializedLogger.loggerSettings.logServerConnectionSettings == null ? null : new JObject
                        {
                            ["ignoreCertErrors"] = ignoreCertErrors?.IsChecked,
                            ["isConfigured"] = isConfigured?.IsChecked,
                            ["isServerSettings"] = isServerSettings?.IsChecked,
                            ["password"] = password?.Text ?? "",
                            ["settingsType"] = settingsType?.Text ?? "",
                            ["url"] = urlLogServerConnection?.Text ?? "",
                            ["userName"] = userName?.Text ?? "",
                        },
                        ["sumoLogicConnectionSettings"] = DeserializedLogger.loggerSettings.sumoLogicConnectionSettings == null ? null : new JObject
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