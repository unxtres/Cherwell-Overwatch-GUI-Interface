using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherwellOVerwatch.Settings
{
    public class ConnectionDef
    {
        public string adminConn { get; set; }
        public int appServerHostMode { get; set; }
        public int certificationValidationMode { get; set; }
        public int connectionRemotingType { get; set; }
        public DateTime createdDateTime { get; set; }
        public string dbConn { get; set; }
        public string dbOwner { get; set; }
        public bool enableTransactionLogAutogrowth { get; set; }
        public string endCharForNameWithSpace { get; set; }
        public string engine { get; set; }
        public int maxTransactionLogSizeInMB { get; set; }
        public string name { get; set; }
        public int recoveryMode { get; set; }
        public int remotingSecurityMode { get; set; }
        public string source { get; set; }
        public string startCharForNameWithSpace { get; set; }
        public int transactionLogGrowth { get; set; }
        public bool transactionLogGrowthIsPercentage { get; set; }
        public bool treatsEmptyStringAsNull { get; set; }
        public string url { get; set; }
        public bool useRest { get; set; }
        public bool useSoap { get; set; }
    }

    public class ConnectionDefinitions
    {
        public List<ConnectionDef> connectionDefs { get; set; }
    }
}
