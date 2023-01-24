using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
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
    public partial class BrowserSettingsPage : Page
    {
        public string json;
        public string url = "http://localhost:5000/api/settings/BrowserSettings";
        public BrowserSettingsPage()
        {
            InitializeComponent();
            Button_Click();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        private void Button_Click()
        {
            try
            {
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
            //string temp;
            //var data = (JObject)JsonConvert.DeserializeObject(json);

            Browser_Settings DeserializedBrowserSettings = JsonConvert.DeserializeObject<Browser_Settings>(json);

            TrebuchetDataSource.Text = DeserializedBrowserSettings.trebuchetDataSource.ToString();
            TestMode.IsChecked = DeserializedBrowserSettings.testMode;
            TabContentHEight.Text = DeserializedBrowserSettings.tabContentHeight.ToString();
            DisableCertificateValidation.IsChecked = DeserializedBrowserSettings.disableCertificateValidation;
            AllowUnsafeLabels.IsChecked= DeserializedBrowserSettings.allowUnsafeLabels;
            InLineBrowserDisplayExtensions.Text = DeserializedBrowserSettings.inlineBrowserDisplayExtensions.ToString();
            LookupAlwaysEnabled.IsChecked = DeserializedBrowserSettings.lookupAlwaysEnabled;
            QueryRequestLimit.Text = DeserializedBrowserSettings.queryRequestLimit.ToString();
            UseCdn.IsChecked = DeserializedBrowserSettings.useCdn;
            UseHttpCompression.IsChecked = DeserializedBrowserSettings.useHttpCompression;
            LoadAllFilesIndividually.IsChecked = DeserializedBrowserSettings.loadAllFilesIndividually;
            EnableSessionSerialization.IsChecked= DeserializedBrowserSettings.enableSessionSerialization;
            AlwaysLoadKeys.IsChecked = DeserializedBrowserSettings.alwaysLoadKeys;
            UiInteractionTimeoutInSeconds.Text = DeserializedBrowserSettings.uiInteractionTimeoutInSeconds.ToString();
            AllowScriptsInRecords.IsChecked = DeserializedBrowserSettings.allowScriptsInReports;
            DisableAnchoring.IsChecked = DeserializedBrowserSettings.disableAnchoring;
            DisableSplitters.IsChecked= DeserializedBrowserSettings.disableSplitters;
            UseLegacyCompleteResponse.IsChecked = DeserializedBrowserSettings.useLegacyCompleteResponse;
            SignalRConnectionTimeoutInSeconds.Text = DeserializedBrowserSettings.signalRConnectionTimeoutInSeconds.ToString();
            SignalRDisconnectTimeoutInSeconds.Text = DeserializedBrowserSettings.signalRDisconnectTimeoutInSeconds.ToString();
            SignalRKeepAliveInSecconds.Text = DeserializedBrowserSettings.signalRKeepAliveInSeconds.ToString();
            ScanditLicenseKey.Text = DeserializedBrowserSettings.scanditLicenseKey.ToString();
            RedirectHttpToHttps.IsChecked = DeserializedBrowserSettings.redirectHttpToHttps;
            EnableInsecureDeepLinks.IsChecked = DeserializedBrowserSettings.enableInsecureDeepLinks;
            AutoSizeLabels.IsChecked = DeserializedBrowserSettings.autoSizeLabels;
            AuthLogFile.Text= DeserializedBrowserSettings.authLogFile.ToString();
            DefaultAuthMode.Text = DeserializedBrowserSettings.defaultAuthMode.ToString();
        }
    }
}
