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
    public partial class EEMServer : Page
    {
        public string url = "http://localhost:5000/api/settings/EEMServerSettings";
        public EEMServer()
        {
            InitializeComponent();
        }

        private void loadData()
        {
            LoadSettings loader = new LoadSettings();

            EEM_Server DeserializedEEMServer = JsonConvert.DeserializeObject<EEM_Server>(loader.GetResult(url));

            DisableCompression.IsChecked = DeserializedEEMServer.disableCompression;
            installed.IsChecked = DeserializedEEMServer.installed;
            lastError.Text = DeserializedEEMServer.lastError.ToString();
            lastErrorDetails.Text = DeserializedEEMServer.lastErrorDetails.ToString();
            //if (DeserializedEEMServer.connection != null) { connection.Text = DeserializedEEMServer.connection.ToString(); }
            //else { connection.Text = ""; }
            encryptedPassword.Text = DeserializedEEMServer.encryptedPassword.ToString();
            useDefaultRoleOfUser.IsChecked = DeserializedEEMServer.useDefaultRoleOfUser;
            userId.Text = DeserializedEEMServer.userId.ToString();
            useWindowsLogin.IsChecked = DeserializedEEMServer.useWindowsLogin;
            emailReadLimit.Text = DeserializedEEMServer.emailReadLimit.ToString();
            monitorRepeatLimit.Text = DeserializedEEMServer.monitorRepeatLimit.ToString();
            showRegEx.IsChecked = DeserializedEEMServer.showRegEx;

            ticks.Text = DeserializedEEMServer.addlItemsWait.ticks.ToString();
            days.Text = DeserializedEEMServer.addlItemsWait.days.ToString();
            hours.Text = DeserializedEEMServer.addlItemsWait.hours.ToString();
            milliseconds.Text = DeserializedEEMServer.addlItemsWait.milliseconds.ToString();
            minutes.Text = DeserializedEEMServer.addlItemsWait.minutes.ToString();
            seconds.Text = DeserializedEEMServer.addlItemsWait.seconds.ToString();
            totalDays.Text = DeserializedEEMServer.addlItemsWait.totalDays.ToString();
            totalHours.Text = DeserializedEEMServer.addlItemsWait.totalHours.ToString();
            totalMilliseconds.Text = DeserializedEEMServer.addlItemsWait.totalMilliseconds.ToString();
            totalMinutes.Text = DeserializedEEMServer.addlItemsWait.totalMinutes.ToString();
            totalSeconds.Text = DeserializedEEMServer.addlItemsWait.totalSeconds.ToString();

            no_ticks.Text = DeserializedEEMServer.noItemsWait.ticks.ToString();
            no_days.Text = DeserializedEEMServer.noItemsWait.days.ToString();
            no_hours.Text = DeserializedEEMServer.noItemsWait.hours.ToString();
            no_milliseconds.Text = DeserializedEEMServer.noItemsWait.milliseconds.ToString();
            no_minutes.Text = DeserializedEEMServer.noItemsWait.minutes.ToString();
            no_seconds.Text = DeserializedEEMServer.noItemsWait.seconds.ToString();
            no_totalDays.Text = DeserializedEEMServer.noItemsWait.totalDays.ToString();
            no_totalHours.Text = DeserializedEEMServer.noItemsWait.totalHours.ToString();
            no_totalMilliseconds.Text = DeserializedEEMServer.noItemsWait.totalMilliseconds.ToString();
            no_totalMinutes.Text = DeserializedEEMServer.noItemsWait.totalMinutes.ToString();
            no_totalSeconds.Text = DeserializedEEMServer.noItemsWait.totalSeconds.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }
}
