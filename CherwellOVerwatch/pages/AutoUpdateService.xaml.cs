using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using CherwellOVerwatch.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;

namespace CherwellOVerwatch
{
    public partial class AutoUpdateService : Page
    {
        public string url = "http://localhost:5000/api/settings/AutoUpdateServiceSettings";
        public string json;
        public AutoUpdateService()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings loader = new LoadSettings();
                json = loader.GetResult(url);
                AutoUpdate DeserializedAutoUpdate = JsonConvert.DeserializeObject<AutoUpdate>(json);

                downloadPath.Text = DeserializedAutoUpdate.downloadPath.ToString();
                updateCheckInterval.Text = DeserializedAutoUpdate.updateCheckInterval.ToString();
                defaultUpdateCheckIntervalValue.Text = DeserializedAutoUpdate.defaultUpdateCheckIntervalValue.ToString();
                minimumUpdateCheckIntervalValue.Text = DeserializedAutoUpdate.minimumUpdateCheckIntervalValue.ToString();
                if (DeserializedAutoUpdate.application.name != null)
                    name.Text = DeserializedAutoUpdate.application.name.ToString();
                else name.Text = "";

                applicationType.Text = DeserializedAutoUpdate.application.applicationType.ToString();
                updatePath.Text = DeserializedAutoUpdate.application.updatePath.ToString();
                versionFile.Text = DeserializedAutoUpdate.application.versionFile.ToString();
                if (DeserializedAutoUpdate.application.currentVersion != null)
                {
                    currentVersion.Text = DeserializedAutoUpdate.application.currentVersion.ToString();
                }
                else { currentVersion.Text = ""; }
            }

            catch
            {
                MessageBox.Show("Not Connected");
            }

        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                save_status.Text = "Saving...!";
                // Restart service
                ServiceController service = new ServiceController("Cherwell Overwatch");
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);

                ApplicationServer DeserializedLogger = JsonConvert.DeserializeObject<ApplicationServer>(json);

                // Build JSON
                var data = new JObject
                {
                    ["downloadPath"] = downloadPath?.Text ?? "",
                    ["updateCheckInterval"] = updateCheckInterval?.Text ?? "",
                    ["defaultUpdateCheckIntervalValue"] = defaultUpdateCheckIntervalValue?.Text ?? "",
                    ["minimumUpdateCheckIntervalValue"] = minimumUpdateCheckIntervalValue?.Text ?? "",
                    ["name"] = name?.Text ?? "",
                    ["applicationType"] = applicationType?.Text ?? "",
                    ["updatePath"] = updatePath?.Text ?? "",
                    ["versionFile"] = versionFile?.Text ?? "",
                    ["currentVersion"] = currentVersion?.Text ?? ""
                };

                var settingData = new JObject
                {
                    ["setting"] = JsonConvert.SerializeObject(data),
                    ["publish"] = true
                };

                var jsonData = JsonConvert.SerializeObject(settingData);

                // Send request
                string url = "http://localhost:5000/api/settings/AutoUpdateServiceSettings";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = TokenInterface.OWToken;
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                save_status.Text = httpResponse.StatusCode.ToString();
            }
            catch
            {
                MessageBox.Show("Not Connected");
            }
        }
    }
}
