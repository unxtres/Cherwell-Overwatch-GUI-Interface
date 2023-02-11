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
    public partial class TAHub : Page
    {
        public string url = "http://localhost:5000/api/settings/TrustedAgentHubSettings";
        public TAHub()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            LoadSettings loader = new LoadSettings();

            TA_Hub DeserializedTAHub = JsonConvert.DeserializeObject<TA_Hub>(loader.GetResult(url));

            disableCompression.IsChecked = DeserializedTAHub.disableCompression;
            installed.IsChecked = DeserializedTAHub.installed;
            lastError.Text = DeserializedTAHub.lastError.ToString();
            lastErrorDetails.Text = DeserializedTAHub.lastErrorDetails.ToString();
            operationTimeout.Text = DeserializedTAHub.operationTimeout.ToString();
            registrationTimeout.Text = DeserializedTAHub.registrationTimeout.ToString();
            sharedKey.Text = DeserializedTAHub.sharedKey.ToString();
            signalRHubUrl.Text = DeserializedTAHub.signalRHubUrl.ToString();
            useTrustedAgents.IsChecked = DeserializedTAHub.useTrustedAgents;
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }
}
