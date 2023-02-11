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
    public partial class ServiceHost : Page
    {
        public string url = "http://localhost:5000/api/settings/ServiceHostSettings";
        public ServiceHost()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            LoadSettings loader = new LoadSettings();

            Service_host DeserializedSH = JsonConvert.DeserializeObject<Service_host>(loader.GetResult(url));

            disableCompression.IsChecked = DeserializedSH.disableCompression;
            installed.IsChecked = DeserializedSH.installed;
            lastError.Text = DeserializedSH.lastError.ToString();
            lastErrorDetails.Text = DeserializedSH.lastErrorDetails.ToString();
            connection.Text = DeserializedSH.connection.ToString();
            encryptedPassword.Text = DeserializedSH.encryptedPassword.ToString();
            useDefaultRoleOfUser.IsChecked = DeserializedSH.useDefaultRoleOfUser;
            userId.Text = DeserializedSH.userId.ToString();
            useWindowsLogin.IsChecked = DeserializedSH.useWindowsLogin;
            hostMaxWorkers.Text = DeserializedSH.hostMaxWorkers.ToString();
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }
}
