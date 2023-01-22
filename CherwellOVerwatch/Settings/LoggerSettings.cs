using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherwellOVerwatch.Settings
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class LoggerSettings
    {
        public string eventLogLevel { get; set; }
        public string fileLogLevel { get; set; }
        public string fileNameOverride { get; set; }
        public bool isLoggingEnabled { get; set; }
        public bool isServerSettings { get; set; }
        public string logFilePath { get; set; }
        public LogServerConnectionSettings logServerConnectionSettings { get; set; }
        public string logServerLogLevel { get; set; }
        public bool logToComplianceLog { get; set; }
        public bool logToConsole { get; set; }
        public string logToConsoleLevel { get; set; }
        public bool logToEventLog { get; set; }
        public bool logToFile { get; set; }
        public bool logToLogServer { get; set; }
        public string maxFilesBeforeRollover { get; set; }
        public string maxFileSizeInMB { get; set; }
        public SumoLogicConnectionSettings sumoLogicConnectionSettings { get; set; }
        public bool logToSumoLogic { get; set; }
        public string sumoLogicLogLevel { get; set; }
        public string settingsType { get; set; }
    }

    public class LogServerConnectionSettings
    {
        public bool ignoreCertErrors { get; set; }
        public bool isConfigured { get; set; }
        public bool isServerSettings { get; set; }
        public string password { get; set; }
        public string settingsType { get; set; }
        public string url { get; set; }
        public string userName { get; set; }
    }

    public class SumoLogicConnectionSettings
    {
        public string url { get; set; }
        public string retryInterval { get; set; }
        public string connectionTimeout { get; set; }
        public string flushingAccuracy { get; set; }
        public string maxFlushInterval { get; set; }
        public string messagesPerRequest { get; set; }
        public string maxQueueSizeBytes { get; set; }
    }
}
