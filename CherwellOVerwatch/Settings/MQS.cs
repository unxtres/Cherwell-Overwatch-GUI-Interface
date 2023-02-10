using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherwellOVerwatch.Settings
{
    public class MQSLoggerSettings
    {
        public int eventLogLevel { get; set; }
        public int fileLogLevel { get; set; }
        public object fileNameOverride { get; set; }
        public bool isLoggingEnabled { get; set; }
        public bool isServerSettings { get; set; }
        public object logFilePath { get; set; }
        public MQSLogServerConnectionSettings logServerConnectionSettings { get; set; }
        public int logServerLogLevel { get; set; }
        public bool logToComplianceLog { get; set; }
        public bool logToConsole { get; set; }
        public int logToConsoleLevel { get; set; }
        public bool logToEventLog { get; set; }
        public bool logToFile { get; set; }
        public bool logToLogServer { get; set; }
        public int maxFilesBeforeRollover { get; set; }
        public int maxFileSizeInMB { get; set; }
        public MQSSumoLogicConnectionSettings sumoLogicConnectionSettings { get; set; }
        public bool logToSumoLogic { get; set; }
        public int sumoLogicLogLevel { get; set; }
        public int settingsType { get; set; }
    }

    public class MQSLogServerConnectionSettings
    {
        public bool ignoreCertErrors { get; set; }
        public bool isConfigured { get; set; }
        public bool isServerSettings { get; set; }
        public object password { get; set; }
        public int settingsType { get; set; }
        public object url { get; set; }
        public object userName { get; set; }
    }

    public class MQS_Settings
    {
        public MQSLoggerSettings loggerSettings { get; set; }
        public string decryptedPassword { get; set; }
        public string encryptedPassword { get; set; }
        public string hostName { get; set; }
        public int port { get; set; }
        public bool isConfigured { get; set; }
        public string userName { get; set; }
        public string virtualHost { get; set; }
        public string rabbitMQPath { get; set; }
        public string erlangPath { get; set; }
    }

    public class MQSSumoLogicConnectionSettings
    {
        public object url { get; set; }
        public int retryInterval { get; set; }
        public int connectionTimeout { get; set; }
        public int flushingAccuracy { get; set; }
        public int maxFlushInterval { get; set; }
        public int messagesPerRequest { get; set; }
        public int maxQueueSizeBytes { get; set; }
    }




}
