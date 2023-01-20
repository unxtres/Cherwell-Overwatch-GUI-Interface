using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherwellOVerwatch.Settings
{
    // AutoUpdate myDeserializedClass = JsonConvert.DeserializeObject<AutoUpdate>(myJsonResponse);
    public class Application
    {
        public string name { get; set; }
        public string applicationType { get; set; }
        public string updatePath { get; set; }
        public string versionFile { get; set; }
        public string currentVersion { get; set; }
    }

    public class AutoUpdate
    {
        public string downloadPath { get; set; }
        public Application application { get; set; }
        public int updateCheckInterval { get; set; }
        public int defaultUpdateCheckIntervalValue { get; set; }
        public int minimumUpdateCheckIntervalValue { get; set; }
    }
}
