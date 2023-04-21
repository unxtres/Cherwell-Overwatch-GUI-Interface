using System;
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
                throw;
            }

            var data = (JObject)JsonConvert.DeserializeObject(json);


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
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = TokenInterface.OWToken;
            httpRequest.ContentType = "application/json";

            var data = @"";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            save_status.Text = httpResponse.StatusCode.ToString();

        }
    }
}
