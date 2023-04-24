﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
using System.ServiceProcess;

namespace CherwellOVerwatch
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AutoDeploy : Page
    {
        public string json;
        
        public AutoDeploy()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            }

            var data = (JObject)JsonConvert.DeserializeObject(json);
            var jsonFile = "AutoDeploy_Deserialized.json";
            if (data==null)
                data = new JObject();
            else
                File.WriteAllText(jsonFile, data.ToString());

            autoDeployDir.Text = data["autoDeployDir"].Value<string>();
            autoDeploySite.Text = data["autoDeploySite"].Value<string>();
            connectionName.Text = data["connectionName"].Value<string>();

            displayDebugInfo.IsChecked = data["displayDebugInfo"].Value<bool>();

            installAccounts.Text = data["installAccounts"].Value<string>();

            installAllUsers.IsChecked = data["installAllUsers"].Value<bool>();
            makeDefault.IsChecked = data["makeDefault"].Value<bool>();
            noPrompt.IsChecked = data["noPrompt"].Value<bool>();
            noUserOptions.IsChecked = data["noUserOptions"].Value<bool>();
            overwrite.IsChecked = data["overwrite"].Value<bool>();
            reqMinorReleases.IsChecked = data["reqMinorReleases"].Value<bool>();

            selectedInstallOption.Text = data["selectedInstallOption"].Value<string>();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                // Restart the Cherwell Overwatch service
                ServiceController service = new ServiceController("Cherwell Overwatch");
                if (service.Status == ServiceControllerStatus.Running)
                {
                    save_status.Text = "Saving...";
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);

                // Build the JSON data based on the UI fields
                var data = new JObject
                {
                    ["autoDeployDir"] = autoDeployDir.Text,
                    ["autoDeploySite"] = autoDeploySite.Text,
                    ["connectionName"] = connectionName.Text,
                    ["displayDebugInfo"] = displayDebugInfo.IsChecked,
                    ["installAccounts"] = installAccounts.Text,
                    ["installAllUsers"] = installAllUsers.IsChecked,
                    ["makeDefault"] = makeDefault.IsChecked,
                    ["noPrompt"] = noPrompt.IsChecked,
                    ["noUserOptions"] = noUserOptions.IsChecked,
                    ["overwrite"] = overwrite.IsChecked,
                    ["reqMinorReleases"] = reqMinorReleases.IsChecked,
                    ["selectedInstallOption"] = selectedInstallOption.Text,
                };

                var settingData = new JObject
                {
                    ["setting"] = JsonConvert.SerializeObject(data),
                    ["publish"] = true
                };

                var jsonData = JsonConvert.SerializeObject(settingData);

                // Send the HTTP POST request
                string url = "http://localhost:5000/api/settings/AutoDeploySettings";
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
