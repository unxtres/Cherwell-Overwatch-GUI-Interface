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
        public int Current_Connection = 0;
        public int Number_of_Connections;
        public ConnectionDefinitions()
        {
            InitializeComponent();
            buttonPrevious.IsEnabled = false;
            buttonNext.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadSettings loader = new LoadSettings();

            Connection_Definitions DeserializedConDefSettings = JsonConvert.DeserializeObject<Connection_Definitions>(loader.GetResult(url));
            Number_of_Connections = DeserializedConDefSettings.connectionDefs.Count;

            if (Number_of_Connections == 1)
            {
                numberOfConnections.Text = "1 Connection Found";
                currCon.Text = "1/1";
            }
            else 
            { 
                numberOfConnections.Text = Number_of_Connections.ToString()+" connections found";
                currCon.Text = (Current_Connection+1).ToString()+"/"+Number_of_Connections.ToString();
                buttonPrevious.IsEnabled = true;
                buttonNext.IsEnabled = true;
            }
        }

        private void Button_previous(object sender, RoutedEventArgs e)
        {
            if (Current_Connection > 0)
            {
                Current_Connection--;
                currCon.Text = (Current_Connection + 1).ToString() + "/" + Number_of_Connections.ToString();
            }
        }

        private void Button_next(object sender, RoutedEventArgs e)
        {
            if (Current_Connection >= 0 && Current_Connection<=(Number_of_Connections-1))
            {
                Current_Connection++;
                currCon.Text = (Current_Connection + 1).ToString() + "/" + Number_of_Connections.ToString();
            }
        }
    }
}
