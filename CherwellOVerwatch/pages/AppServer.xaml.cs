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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CherwellOVerwatch
{
    public partial class AppServer : Page
    {
        public string url = "http://localhost:5000/api/settings/AppServerSettings";
        public string json;
        public AppServer()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                string urls = "http://localhost:5000/api/settings/AppServerSettings";
                var httpRequest = (HttpWebRequest)WebRequest.Create(urls);
                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = TokenInterface.OWToken;

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    json = result;
                }
            }
            catch
            {
                MessageBox.Show("Not Connected");
                throw;
            }
            ApplicationServer DeserializedAppServ = JsonConvert.DeserializeObject<ApplicationServer>(json);

            LoadSettings loader = new LoadSettings();

            var data = (JObject)JsonConvert.DeserializeObject(loader.GetResult(url));

            disableCompression.IsChecked = DeserializedAppServ.disableCompression;
            installed.IsChecked = DeserializedAppServ.installed;

            if (DeserializedAppServ.lastError == null)
                lastError.Text = "";
            else
                lastError.Text = DeserializedAppServ.lastError.ToString();

            if (DeserializedAppServ.lastErrorDetails != null)
                lastErrorDetails.Text = DeserializedAppServ.lastErrorDetails.ToString();
            else
                lastErrorDetails.Text = "";


            isHttp.IsChecked = DeserializedAppServ.isHttp;
            isTcp.IsChecked = DeserializedAppServ.isTcp;

            appServerHostMode.Text = DeserializedAppServ.appServerHostMode.ToString();
            protocol.Text = DeserializedAppServ.protocol.ToString();
            connection.Text = DeserializedAppServ.connection.ToString();

            enableTcpOption.IsChecked = DeserializedAppServ.enableTcpOption;

            instanceGuid.Text = DeserializedAppServ.instanceGuid.ToString();
            oldTcpPort.Text = DeserializedAppServ.oldTcpPort.ToString();
            port.Text = DeserializedAppServ.port.ToString();

            useRest.IsChecked = DeserializedAppServ.useRest;

            securityMode.Text = DeserializedAppServ.securityMode.ToString();
            serverName.Text = DeserializedAppServ.serverName.ToString();
            serverConfigToolComments.Text = DeserializedAppServ.serverConfigToolComments.ToString();
            loggedInUserCacheExpiryMins.Text = DeserializedAppServ.loggedInUserCacheExpiryMins.ToString();

            useRecoveryFile.IsChecked = DeserializedAppServ.useRecoveryFile;

            recoveryFilePath.Text = DeserializedAppServ.recoveryFilePath.ToString();
            recoveryFileName.Text = DeserializedAppServ.recoveryFileName.ToString();
            recoveryFilePersistIntervalSeconds.Text = DeserializedAppServ.recoveryFilePersistIntervalSeconds.ToString();

            minMessageSizeToCompressHigh.Text = DeserializedAppServ.minMessageSizeToCompressHigh.ToString();
            minMessageSizeToCompressLow.Text = DeserializedAppServ.minMessageSizeToCompressLow.ToString();
            minMessageSizeToCompressMedium.Text = DeserializedAppServ.minMessageSizeToCompressMedium.ToString();

            wcfMaxBufferPoolSize.Text = DeserializedAppServ.wcfMaxBufferPoolSize.ToString();
            wcfMaxBufferSize.Text = DeserializedAppServ.wcfMaxBufferSize.ToString();
            wcfMaxReceivedMessageSize.Text = DeserializedAppServ.wcfMaxReceivedMessageSize.ToString();
            wcfReaderMaxNameTableCharCount.Text = DeserializedAppServ.wcfReaderMaxNameTableCharCount.ToString();
            wcfReaderMaxStringContentLength.Text = DeserializedAppServ.wcfReaderMaxStringContentLength.ToString();
            wcfReaderMaxArrayLength.Text = DeserializedAppServ.wcfReaderMaxArrayLength.ToString();
            wcfOperationTimeoutOverride.Text = DeserializedAppServ.wcfOperationTimeoutOverride.ToString();

            wcfUseMessageCompression.IsChecked = DeserializedAppServ.wcfUseMessageCompression;

            wcfTcpMaxConnections.Text = DeserializedAppServ.wcfTcpMaxConnections.ToString();
            wcfMaxConcurrentCalls.Text = DeserializedAppServ.wcfMaxConcurrentCalls.ToString();
            wcfMaxConcurrentInstances.Text = DeserializedAppServ.wcfMaxConcurrentInstances.ToString();
            wcfMaxConcurrentSessions.Text = DeserializedAppServ.wcfMaxConcurrentSessions.ToString();

            wcfEnablePerformanceCounters.IsChecked = DeserializedAppServ.wcfEnablePerformanceCounters;

            wcfListenBacklog.Text = DeserializedAppServ.wcfListenBacklog.ToString();

            certificateStoreLocation.Text = DeserializedAppServ.certificateStoreLocation.ToString();
            certificateStoreName.Text = DeserializedAppServ.certificateStoreName.ToString();
            certificateSubject.Text = DeserializedAppServ.certificateSubject.ToString();
            certificateThumbprint.Text = DeserializedAppServ.certificateThumbprint.ToString();
            certificateValidationModeForAutoClient.Text = DeserializedAppServ.certificateValidationModeForAutoClient.ToString();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                // Restart service
                ServiceController service = new ServiceController("Cherwell Overwatch");
                if (service.Status == ServiceControllerStatus.Running)
                {
                    save_status.Text = "Saving...";
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);

                ApplicationServer DeserializedLogger = JsonConvert.DeserializeObject<ApplicationServer>(json);

                // Build JSON
                var data = new JObject
                {
                    ["disableCompression"] = disableCompression?.IsChecked,
                    ["installed"] = installed?.IsChecked,
                    ["lastError"] = lastError?.Text ?? "",
                    ["lastErrorDetails"] = lastErrorDetails?.Text ?? "",
                    ["isHttp"] = isHttp?.IsChecked,
                    ["isTcp"] = isTcp?.IsChecked,
                    ["appServerHostMode"] = appServerHostMode?.Text ?? "",
                    ["protocol"] = protocol?.Text ?? "",
                    ["connection"] = connection?.Text ?? "",
                    ["enableTcpOption"] = enableTcpOption?.IsChecked,
                    ["instanceGuid"] = instanceGuid?.Text ?? "",
                    ["oldTcpPort"] = oldTcpPort?.Text ?? "",
                    ["port"] = port?.Text ?? "",
                    ["useRest"] = useRest?.IsChecked,
                    ["securityMode"] = securityMode?.Text ?? "",
                    ["serverName"] = serverName?.Text ?? "",
                    ["serverConfigToolComments"] = serverConfigToolComments?.Text ?? "",
                    ["loggedInUserCacheExpiryMins"] = loggedInUserCacheExpiryMins?.Text ?? "",
                    ["useRecoveryFile"] = useRecoveryFile?.IsChecked,
                    ["recoveryFilePath"] = recoveryFilePath?.Text ?? "",
                    ["recoveryFileName"] = recoveryFileName?.Text ?? "",
                    ["recoveryFilePersistIntervalSeconds"] = recoveryFilePersistIntervalSeconds?.Text ?? "",
                    ["minMessageSizeToCompressHigh"] = minMessageSizeToCompressHigh?.Text ?? "",
                    ["minMessageSizeToCompressLow"] = minMessageSizeToCompressLow?.Text ?? "",
                    ["minMessageSizeToCompressMedium"] = minMessageSizeToCompressMedium?.Text ?? "",
                    ["wcfMaxBufferPoolSize"] = wcfMaxBufferPoolSize?.Text ?? "",
                    ["wcfMaxBufferSize"] = wcfMaxBufferSize?.Text ?? "",
                    ["wcfMaxReceivedMessageSize"] = wcfMaxReceivedMessageSize?.Text ?? "",
                    ["wcfReaderMaxNameTableCharCount"] = wcfReaderMaxNameTableCharCount?.Text ?? "",
                    ["wcfReaderMaxStringContentLength"] = wcfReaderMaxStringContentLength?.Text ?? "",
                    ["wcfReaderMaxArrayLength"] = wcfReaderMaxArrayLength?.Text ?? "",
                    ["wcfOperationTimeoutOverride"] = wcfOperationTimeoutOverride?.Text ?? "",
                    ["wcfUseMessageCompression"] = wcfUseMessageCompression?.IsChecked,
                    ["wcfTcpMaxConnections"] = wcfTcpMaxConnections?.Text ?? "",
                    ["wcfMaxConcurrentCalls"] = wcfMaxConcurrentCalls?.Text ?? "",
                    ["wcfMaxConcurrentInstances"] = wcfMaxConcurrentInstances?.Text ?? "",
                    ["wcfMaxConcurrentSessions"] = wcfMaxConcurrentSessions?.Text ?? "",
                    ["wcfEnablePerformanceCounters"] = wcfEnablePerformanceCounters?.IsChecked,
                    ["wcfListenBacklog"] = wcfListenBacklog?.Text ?? "",
                    ["certificateStoreLocation"] = certificateStoreLocation?.Text ?? "",
                    ["certificateStoreName"] = certificateStoreName?.Text ?? "",
                    ["certificateSubject"] = certificateSubject?.Text ?? "",
                    ["certificateThumbprint"] = certificateThumbprint?.Text ?? "",
                    ["certificateValidationModeForAutoClient"] = certificateValidationModeForAutoClient?.Text ?? "",
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
                string url = "http://localhost:5000/api/settings/AppServerSettings";
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
