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
    public partial class TAServer : Page
    {
        public string url = "http://localhost:5000/api/settings/TrustedAgentServerSettings";
        public TAServer()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            LoadSettings loader = new LoadSettings();

            TA_server DeserializedTAS = JsonConvert.DeserializeObject<TA_server>(loader.GetResult(url));

            disableCompression.IsChecked = DeserializedTAS.disableCompression;
            installed.IsChecked = DeserializedTAS.installed;
            lastError.Text = DeserializedTAS.lastError.ToString();
            lastErrorDetails.Text = DeserializedTAS.lastErrorDetails.ToString();
            connection.Text = DeserializedTAS.connection.ToString();
            encryptedPassword.Text= DeserializedTAS.encryptedPassword.ToString();
            useDefaultRoleOfUser.IsChecked = DeserializedTAS.useDefaultRoleOfUser;
            userId.Text = DeserializedTAS.userId.ToString();
            useWindowsLogin.IsChecked= DeserializedTAS.useWindowsLogin;
            displayName.Text = DeserializedTAS.displayName.ToString();
            hubPingFrequency.Text = DeserializedTAS.hubPingFrequency.ToString();
            id.Text= DeserializedTAS.id.ToString();
            sharedKey.Text= DeserializedTAS.sharedKey.ToString();
            signalRHubUrl.Text = DeserializedTAS.signalRHubUrl.ToString();
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }
}
