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
    public partial class Logger : Page
    {
        public string json;
        public Logger()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = "http://localhost:5000/api/settings/AppServerSettings";
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
            ApplicationServer DeserializedLogger = JsonConvert.DeserializeObject<ApplicationServer>(json);

            EventLogLevel.Text = DeserializedLogger.loggerSettings.eventLogLevel.ToString();
            fileLogLevel.Text = DeserializedLogger.loggerSettings.fileLogLevel.ToString();
            if (DeserializedLogger.loggerSettings.fileNameOverride != null) { fileNameOverride.Text = DeserializedLogger.loggerSettings.fileNameOverride.ToString(); }
            else { fileNameOverride.Text = ""; }
            isLoggingEnabled.IsChecked = DeserializedLogger.loggerSettings.isLoggingEnabled;
            isServerSettings.IsChecked = DeserializedLogger.loggerSettings.isServerSettings;
            logFilePath.Text = DeserializedLogger.loggerSettings.logFilePath.ToString();

            ignoreCertErrors.IsChecked = DeserializedLogger.loggerSettings.logServerConnectionSettings.ignoreCertErrors;
            isConfigured.IsChecked = DeserializedLogger.loggerSettings.logServerConnectionSettings.isConfigured;
            isServerSettingsConnectionSettings.IsChecked = DeserializedLogger.loggerSettings.logServerConnectionSettings.isServerSettings;
        }
    }
}
