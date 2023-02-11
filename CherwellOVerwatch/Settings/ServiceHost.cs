using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherwellOVerwatch.Settings
{
    public class SHLoggerSettings
    {
        public int eventLogLevel { get; set; }
        public int fileLogLevel { get; set; }
        public object fileNameOverride { get; set; }
        public bool isLoggingEnabled { get; set; }
        public bool isServerSettings { get; set; }
        public string logFilePath { get; set; }
        public SHLogServerConnectionSettings logServerConnectionSettings { get; set; }
        public int logServerLogLevel { get; set; }
        public bool logToComplianceLog { get; set; }
        public bool logToConsole { get; set; }
        public int logToConsoleLevel { get; set; }
        public bool logToEventLog { get; set; }
        public bool logToFile { get; set; }
        public bool logToLogServer { get; set; }
        public int maxFilesBeforeRollover { get; set; }
        public int maxFileSizeInMB { get; set; }
        public SHSumoLogicConnectionSettings sumoLogicConnectionSettings { get; set; }
        public bool logToSumoLogic { get; set; }
        public int sumoLogicLogLevel { get; set; }
        public int settingsType { get; set; }
    }

    public class SHLogServerConnectionSettings
    {
        public bool ignoreCertErrors { get; set; }
        public bool isConfigured { get; set; }
        public bool isServerSettings { get; set; }
        public string password { get; set; }
        public int settingsType { get; set; }
        public string url { get; set; }
        public string userName { get; set; }
    }

    public class Service_host
    {
        public bool disableCompression { get; set; }
        public bool installed { get; set; }
        public string lastError { get; set; }
        public string lastErrorDetails { get; set; }
        public SHLoggerSettings loggerSettings { get; set; }
        public string connection { get; set; }
        public string encryptedPassword { get; set; }
        public bool useDefaultRoleOfUser { get; set; }
        public string userId { get; set; }
        public bool useWindowsLogin { get; set; }
        public int hostMaxWorkers { get; set; }
    }

    public class SHSumoLogicConnectionSettings
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
