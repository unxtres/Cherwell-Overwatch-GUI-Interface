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
    public partial class WebHooks : Page
    {
        public string url = "http://localhost:5000/api/settings/WebhookLeaderSettings";
        public WebHooks()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            LoadSettings loader = new LoadSettings();

            Web_Hooks DeserializedWH = JsonConvert.DeserializeObject<Web_Hooks>(loader.GetResult(url));

            enabled.IsChecked = DeserializedWH.enabled;
            if (DeserializedWH.groupId != null) { groupId.Text = DeserializedWH.groupId.ToString(); }
            else { groupId.Text = ""; }
            if (DeserializedWH.groupName != null) { groupName.Text = DeserializedWH.groupName.ToString(); }
            else { groupName.Text = ""; }
            heartbeatInterval.Text = DeserializedWH.heartbeatInterval.ToString();
            maxWorkers.Text= DeserializedWH.maxWorkers.ToString();
            waitTime.Text = DeserializedWH.waitTime.ToString();
            waitTime.Text = DeserializedWH.waitTime.ToString();
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }
}
