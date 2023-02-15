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
    public partial class SchedulingServer : Page
    {
        public string url = "http://localhost:5000/api/settings/SchedulingServerSettings";
        public SchedulingServer()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            LoadSettings loader = new LoadSettings();

            Scheduling_server DeserializeSchedulingserver = JsonConvert.DeserializeObject<Scheduling_server>(loader.GetResult(url));

            disableCompression.IsChecked = DeserializeSchedulingserver.disableCompression;
            installed.IsChecked = DeserializeSchedulingserver.installed;
            lastError.Text = DeserializeSchedulingserver.lastError.ToString();
            lastErrorDetails.Text = DeserializeSchedulingserver.lastErrorDetails.ToString();
            if (DeserializeSchedulingserver.connection != null) { connection.Text = DeserializeSchedulingserver.connection.ToString(); }
            else { connection.Text = ""; }
            encryptedPassword.Text = DeserializeSchedulingserver.encryptedPassword.ToString();
            useDefaultRoleOfUser.IsChecked = DeserializeSchedulingserver.useDefaultRoleOfUser;
            userId.Text = DeserializeSchedulingserver.userId.ToString();
            useWindowsLogin.IsChecked = DeserializeSchedulingserver.useWindowsLogin;
            groupId.Text = DeserializeSchedulingserver.groupId.ToString();
            groupName.Text = DeserializeSchedulingserver.groupName.ToString();
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }
}
