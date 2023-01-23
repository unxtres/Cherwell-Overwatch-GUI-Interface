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
        public int eventLogLevel { get; set; }
        public int fileLogLevel { get; set; }
        public string fileNameOverride { get; set; }
        public bool isLoggingEnabled { get; set; }
        public bool isServerSettings { get; set; }
        public string logFilePath { get; set; }
        public LogServerConnectionSettings logServerConnectionSettings { get; set; }
        public int logServerLogLevel { get; set; }
        public bool logToComplianceLog { get; set; }
        public bool logToConsole { get; set; }
        public int logToConsoleLevel { get; set; }
        public bool logToEventLog { get; set; }
        public bool logToFile { get; set; }
        public bool logToLogServer { get; set; }
        public int maxFilesBeforeRollover { get; set; }
        public int maxFileSizeInMB { get; set; }
        public SumoLogicConnectionSettings sumoLogicConnectionSettings { get; set; }
        public bool logToSumoLogic { get; set; }
        public int sumoLogicLogLevel { get; set; }
        public int settingsType { get; set; }
    }

    public class LogServerConnectionSettings
    {
        public bool ignoreCertErrors { get; set; }
        public bool isConfigured { get; set; }
        public bool isServerSettings { get; set; }
        public string password { get; set; }
        public int settingsType { get; set; }
        public string url { get; set; }
        public string userName { get; set; }
    }

    public class Browser_Settings
    {
        public LoggerSettings loggerSettings { get; set; }
        public string trebuchetDataSource { get; set; }
        public bool testMode { get; set; }
        public int tabContentHeight { get; set; }
        public bool disableCertificateValidation { get; set; }
        public bool allowUnsafeLabels { get; set; }
        public string inlineBrowserDisplayExtensions { get; set; }
        public bool lookupAlwaysEnabled { get; set; }
        public int queryRequestLimit { get; set; }
        public bool useCdn { get; set; }
        public bool useHttpCompression { get; set; }
        public bool loadAllFilesIndividually { get; set; }
        public bool enableSessionSerialization { get; set; }
        public bool alwaysLoadKeys { get; set; }
        public int uiInteractionTimeoutInSeconds { get; set; }
        public bool allowScriptsInReports { get; set; }
        public bool disableAnchoring { get; set; }
        public bool disableSplitters { get; set; }
        public bool useLegacyCompleteResponse { get; set; }
        public int signalRConnectionTimeoutInSeconds { get; set; }
        public int signalRDisconnectTimeoutInSeconds { get; set; }
        public int signalRKeepAliveInSeconds { get; set; }
        public string scanditLicenseKey { get; set; }
        public bool redirectHttpToHttps { get; set; }
        public bool enableInsecureDeepLinks { get; set; }
        public bool autoSizeLabels { get; set; }
        public string authLogFile { get; set; }
        public string defaultAuthMode { get; set; }
    }

    public class SumoLogicConnectionSettings
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
