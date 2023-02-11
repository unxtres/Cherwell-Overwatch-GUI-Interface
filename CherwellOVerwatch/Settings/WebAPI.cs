using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherwellOVerwatch.Settings
{
    public class APILoggerSettings
    {
        public int eventLogLevel { get; set; }
        public int fileLogLevel { get; set; }
        public string fileNameOverride { get; set; }
        public bool isLoggingEnabled { get; set; }
        public bool isServerSettings { get; set; }
        public string logFilePath { get; set; }
        public APILogServerConnectionSettings logServerConnectionSettings { get; set; }
        public int logServerLogLevel { get; set; }
        public bool logToComplianceLog { get; set; }
        public bool logToConsole { get; set; }
        public int logToConsoleLevel { get; set; }
        public bool logToEventLog { get; set; }
        public bool logToFile { get; set; }
        public bool logToLogServer { get; set; }
        public int maxFilesBeforeRollover { get; set; }
        public int maxFileSizeInMB { get; set; }
        public APISumoLogicConnectionSettings sumoLogicConnectionSettings { get; set; }
        public bool logToSumoLogic { get; set; }
        public int sumoLogicLogLevel { get; set; }
        public int settingsType { get; set; }
    }

    public class APILogServerConnectionSettings
    {
        public bool ignoreCertErrors { get; set; }
        public bool isConfigured { get; set; }
        public bool isServerSettings { get; set; }
        public string password { get; set; }
        public int settingsType { get; set; }
        public string url { get; set; }
        public string userName { get; set; }
    }

    public class Web_api
    {
        public APILoggerSettings loggerSettings { get; set; }
        public string apiClientId { get; set; }
        public int authorizationCodeExpirationMinutes { get; set; }
        public bool rateLimitingEnabled { get; set; }
        public int maxConcurrentRequests { get; set; }
        public bool loadFromFileFromBin { get; set; }
        public string authenticationProvider { get; set; }
        public string trebuchetDataSource { get; set; }
        public bool useSamlAdfsRedirect { get; set; }
        public bool idpIsAdfs { get; set; }
        public string samlAdfsIdpInitiatedUrl { get; set; }
        public int samlServerTimeAllowance { get; set; }
        public string certificateUserEmail { get; set; }
        public string certSubjectName { get; set; }
        public string certIssuer { get; set; }
        public string certThumbprint { get; set; }
        public string tenant { get; set; }
        public string audience { get; set; }
        public string aadClientId { get; set; }
        public string aadClientUserEmail { get; set; }
        public string grantType { get; set; }
        public string authorizationUrl { get; set; }
        public bool useRecoveryFile { get; set; }
        public string recoveryFilePath { get; set; }
        public string recoveryFileName { get; set; }
        public int recoveryFilePersistIntervalSeconds { get; set; }
    }

    public class APISumoLogicConnectionSettings
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
