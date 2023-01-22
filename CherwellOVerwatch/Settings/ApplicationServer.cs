// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class LoggerSettings
{
    public int eventLogLevel { get; set; }
    public int fileLogLevel { get; set; }
    public object fileNameOverride { get; set; }
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

public class ApplicationServer
{
    public bool disableCompression { get; set; }
    public bool installed { get; set; }
    public object lastError { get; set; }
    public object lastErrorDetails { get; set; }
    public LoggerSettings loggerSettings { get; set; }
    public bool isHttp { get; set; }
    public bool isTcp { get; set; }
    public int minMessageSizeToCompressHigh { get; set; }
    public int minMessageSizeToCompressLow { get; set; }
    public int minMessageSizeToCompressMedium { get; set; }
    public int wcfMaxBufferPoolSize { get; set; }
    public int wcfMaxBufferSize { get; set; }
    public int wcfMaxReceivedMessageSize { get; set; }
    public int wcfReaderMaxDepth { get; set; }
    public int wcfReaderMaxNameTableCharCount { get; set; }
    public long wcfReaderMaxStringContentLength { get; set; }
    public long wcfReaderMaxArrayLength { get; set; }
    public int wcfOperationTimeoutOverride { get; set; }
    public bool wcfUseMessageCompression { get; set; }
    public int wcfTcpMaxConnections { get; set; }
    public int wcfMaxConcurrentCalls { get; set; }
    public int wcfMaxConcurrentInstances { get; set; }
    public int wcfMaxConcurrentSessions { get; set; }
    public bool wcfEnablePerformanceCounters { get; set; }
    public int wcfListenBacklog { get; set; }
    public int appServerHostMode { get; set; }
    public int protocol { get; set; }
    public int certificateStoreLocation { get; set; }
    public int certificateStoreName { get; set; }
    public string certificateSubject { get; set; }
    public string certificateThumbprint { get; set; }
    public int certificateValidationModeForAutoClient { get; set; }
    public string connection { get; set; }
    public bool enableTcpOption { get; set; }
    public string instanceGuid { get; set; }
    public string oldTcpPort { get; set; }
    public int port { get; set; }
    public WcfMethodTimeoutOverrides wcfMethodTimeoutOverrides { get; set; }
    public int securityMode { get; set; }
    public string serverName { get; set; }
    public bool useRest { get; set; }
    public string serverConfigToolComments { get; set; }
    public int loggedInUserCacheExpiryMins { get; set; }
    public bool useRecoveryFile { get; set; }
    public string recoveryFilePath { get; set; }
    public string recoveryFileName { get; set; }
    public int recoveryFilePersistIntervalSeconds { get; set; }
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

public class WcfMethodTimeoutOverrides
{
}

