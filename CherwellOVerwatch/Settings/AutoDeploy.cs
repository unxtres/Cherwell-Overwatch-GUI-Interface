using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;



namespace CherwellOVerwatch.Settings
{
    public class AutoDeploy
    {
        public string json;
        public string autoDeployDir { get; set; }
        public string autoDeploySite { get; set; }
        public string connectionName { get; set; }
        public bool displayDebugInfo { get; set; }
        public string installAccounts { get; set; }
        public bool installAllUsers { get; set; }
        public bool makeDefault { get; set; }
        public bool noPrompt { get; set; }
        public bool noUserOptions { get; set; }
        public bool overwrite { get; set; }
        public bool reqMinorReleases { get; set; }
        public string selectedInstallOption { get; set; }

        public void connect()
        {
            try
            {
                string url = "http://localhost:5000/api/settings/AutoDeploySettings";
                //var request = new HttpRequestMessage
                //{
                //    Method = HttpMethod.Get,
                //    RequestUri = new Uri(url),
                //    Content = new StringContent("body", Encoding.UTF8, "application/json"),
                //    Headers = new HttpRequestHeaders()
                //};
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = TokenInterface.OWToken;

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    json = result;
                }
            }
            catch
            {
                MessageBox.Show("Not Connected");
                throw;
            }

            
        }
    }
    public class Root
    {
        [JsonProperty("autoDeployDir")]
        public string autoDeployDir { get; set; }
        public string autoDeploySite { get; set; }
        public string connectionName { get; set; }
        public bool displayDebugInfo { get; set; }
        public string installAccounts { get; set; }
        public bool installAllUsers { get; set; }
        public bool makeDefault { get; set; }
        public bool noPrompt { get; set; }
        public bool noUserOptions { get; set; }
        public bool overwrite { get; set; }
        public bool reqMinorReleases { get; set; }
        public string selectedInstallOption { get; set; }
    }
}
