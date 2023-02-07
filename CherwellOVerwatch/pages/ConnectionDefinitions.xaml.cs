using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
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
    public partial class ConnectionDefinitions : Page
    {
        public string json;
        public string url = "http://localhost:5000/api/settings/ConnectionDefSettings";
        public ConnectionDefinitions()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadSettings loader = new LoadSettings();

            Connection_Definitions DeserializedConDefSettings = JsonConvert.DeserializeObject<Connection_Definitions>(loader.GetResult(url));
            test.Text = DeserializedConDefSettings.connectionDefs.Count.ToString();
            test2.Text = DeserializedConDefSettings.connectionDefs[1].adminConn.ToString();
        }
    }
}
