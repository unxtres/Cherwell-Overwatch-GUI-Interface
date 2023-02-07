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
using CherwellOVerwatch.Settings;

namespace CherwellOVerwatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        HttpClient client = new HttpClient();
        public string link = "http://localhost:5000/api/";
        public string token;

        private async void Button_Get_Token(object sender, RoutedEventArgs e)
        {
            //ApiHelper connect = new ApiHelper();
            //string registration_key = regkey.Text;
            //connect.get_token(registration_key);
            //TokenInterface.OWToken = connect.token;
            //regkey.Text = token;
            string reglink = "http://localhost:5000/api/Registration/generate?key=" + regkey.Text;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(reglink),
                Content = new StringContent("body", Encoding.UTF8, "application/json"),
            };
            var response = await client.SendAsync(request).ConfigureAwait(false);
            var responsebody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            token = responsebody.ToString();
            TokenInterface.OWToken = token;
            this.Dispatcher.Invoke(() =>
            {
                regkey.Text = token;
            });
        }

        //private void Button_AppServerSettings(object sender, RoutedEventArgs e)
        //{
        //    string url = link + "settings/AppServerSettings";
        //    //var request = new HttpRequestMessage
        //    //{
        //    //    Method = HttpMethod.Get,
        //    //    RequestUri = new Uri(url),
        //    //    Content = new StringContent("body", Encoding.UTF8, "application/json"),
        //    //    Headers = new HttpRequestHeaders()
        //    //};
        //    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
        //    httpRequest.Accept = "application/json";
        //    httpRequest.Headers["Authorization"] = token;

        //    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //    {
        //        var result = streamReader.ReadToEnd();
        //        //settings.Text = result;

        //    }
        //}

        private void Button_AppServerSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new AppServer();
        }

        private void Button_Logger(object sender, RoutedEventArgs e)
        {
            Main.Content = new Logger();
        }

        private void Button_AutoDeploy(object sender, RoutedEventArgs e)
        {
            Main.Content = new AutoDeploy();
        }

        private void Button_AutoUpdate(object sender, RoutedEventArgs e)
        {
            Main.Content = new AutoUpdateService();
        }

        private void Button_BrowserSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new BrowserSettingsPage();
        }

        private void Button_BrowserLogSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new BrowserLogSettings();
        }

        private void Button_ConnectionDefSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new ConnectionDefinitionss();
        }
    }
}
