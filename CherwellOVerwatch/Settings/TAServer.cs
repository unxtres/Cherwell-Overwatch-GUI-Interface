using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherwellOVerwatch.Settings
{
    public class TASLoggerSettings
    {
        public int eventLogLevel { get; set; }
        public int fileLogLevel { get; set; }
        public object fileNameOverride { get; set; }
        public bool isLoggingEnabled { get; set; }
        public bool isServerSettings { get; set; }
        public string logFilePath { get; set; }
        public TASLogServerConnectionSettings logServerConnectionSettings { get; set; }
        public int logServerLogLevel { get; set; }
        public bool logToComplianceLog { get; set; }
        public bool logToConsole { get; set; }
        public int logToConsoleLevel { get; set; }
        public bool logToEventLog { get; set; }
        public bool logToFile { get; set; }
        public bool logToLogServer { get; set; }
        public int maxFilesBeforeRollover { get; set; }
        public int maxFileSizeInMB { get; set; }
        public TASSumoLogicConnectionSettings sumoLogicConnectionSettings { get; set; }
        public bool logToSumoLogic { get; set; }
        public int sumoLogicLogLevel { get; set; }
        public int settingsType { get; set; }
    }

    public class TASLogServerConnectionSettings
    {
        public bool ignoreCertErrors { get; set; }
        public bool isConfigured { get; set; }
        public bool isServerSettings { get; set; }
        public string password { get; set; }
        public int settingsType { get; set; }
        public string url { get; set; }
        public string userName { get; set; }
    }

    public class TA_server
    {
        public bool disableCompression { get; set; }
        public bool installed { get; set; }
        public string lastError { get; set; }
        public string lastErrorDetails { get; set; }
        public TASLoggerSettings loggerSettings { get; set; }
        public string connection { get; set; }
        public string encryptedPassword { get; set; }
        public bool useDefaultRoleOfUser { get; set; }
        public string userId { get; set; }
        public bool useWindowsLogin { get; set; }
        public string displayName { get; set; }
        public int hubPingFrequency { get; set; }
        public string id { get; set; }
        public string sharedKey { get; set; }
        public string signalRHubUrl { get; set; }
    }

    public class TASSumoLogicConnectionSettings
    {
        public string url { get; set; }
        public int retryInterval { get; set; }
        public int connectionTimeout { get; set; }
        public int flushingAccuracy { get; set; }
        public int maxFlushInterval { get; set; }
        public int messagesPerRequest { get; set; }
        public int maxQueueSizeBytes { get; set; }
    }
}
