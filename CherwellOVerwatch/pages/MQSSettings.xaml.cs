﻿using System;
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
    public partial class MQSSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/MessageQueueSettings";
        public string json;
        public MQSSettings()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                MQS_Settings DeserializedMQSServer = JsonConvert.DeserializeObject<MQS_Settings>(loader.GetResult(url));

                decryptedPassword.Text = DeserializedMQSServer.decryptedPassword.ToString();
                encryptedPassword.Text = DeserializedMQSServer.encryptedPassword.ToString();
                hostName.Text = DeserializedMQSServer.hostName.ToString();
                port.Text = DeserializedMQSServer.port.ToString();
                isConfigured.IsChecked = DeserializedMQSServer.isConfigured;
                userName.Text = DeserializedMQSServer.userName.ToString();
                virtualHost.Text = DeserializedMQSServer.virtualHost.ToString();
                rabbitMQPath.Text = DeserializedMQSServer.rabbitMQPath.ToString();
                erlangPath.Text = DeserializedMQSServer.erlangPath.ToString();
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

                MQS_Settings DeserializedLogger = JsonConvert.DeserializeObject<MQS_Settings>(json);

                // Build JSON
                var data = new JObject
                {
                    ["decryptedPassword"] = decryptedPassword?.Text ?? "",
                    ["encryptedPassword"] = encryptedPassword?.Text ?? "",
                    ["hostName"] = hostName?.Text ?? "",
                    ["port"] = port?.Text ?? "",
                    ["isConfigured"] = isConfigured?.IsChecked,
                    ["userName"] = userName?.Text ?? "",
                    ["virtualHost"] = virtualHost?.Text ?? "",
                    ["rabbitMQPath"] = rabbitMQPath?.Text ?? "",
                    ["erlangPath"] = erlangPath?.Text ?? "",
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
                string url = "http://localhost:5000/api/settings/MessageQueueSettings";
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
