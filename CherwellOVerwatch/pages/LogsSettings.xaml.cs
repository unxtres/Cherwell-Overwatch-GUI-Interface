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
    public partial class LogsSettings : Page
    {
        public LogsSettings()
        {
            InitializeComponent();
        }
        private void Button_Logger(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/AppServerLogSettings.xaml", UriKind.Relative));
        }

        private void Button_BrowserLogSettings(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/BrowserLogSettings.xaml", UriKind.Relative));
        }

        private void Button_EEMServerLogSettings(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/EEMServerLogs.xaml", UriKind.Relative));
        }

        private void Button_MQSLogSettings(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/MQSLogSettings.xaml", UriKind.Relative));
        }

        private void Button_PortalLogSettings(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/PortalLogSettings.xaml", UriKind.Relative));
        }

        private void Button_SchedulingLogSettings(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/SchedulingLogSettings.xaml", UriKind.Relative));
        }

        private void Button_SHLogSettings(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/ServiceHostLog.xaml", UriKind.Relative));
        }

        private void Button_TAHubLogs(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/TAHubLogs.xaml", UriKind.Relative));
        }

        private void Button_TAServerLogs(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/TAServerLogs.xaml", UriKind.Relative));
        }

        private void Button_WebApi(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/WebAPILogs.xaml", UriKind.Relative));
        }

        private void Button_WebHooks(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/WebHooksLogs.xaml", UriKind.Relative));
        }
    }
}
