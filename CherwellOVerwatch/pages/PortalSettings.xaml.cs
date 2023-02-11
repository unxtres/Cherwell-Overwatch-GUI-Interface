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
    public partial class PortalSettings : Page
    {
        public string url = "http://localhost:5000/api/settings/PortalSettings";
        public PortalSettings()
        {
            InitializeComponent();
        }

        private void loadData()
        {
            LoadSettings loader = new LoadSettings();

            Portal_Settings DeserializedPortalSettings = JsonConvert.DeserializeObject<Portal_Settings>(loader.GetResult(url));

            trebuchetDataSource.Text = DeserializedPortalSettings.trebuchetDataSource.ToString();
            testMode.IsChecked = DeserializedPortalSettings.testMode;
            tabContentHeight.Text = DeserializedPortalSettings.tabContentHeight.ToString();
            disableCertificateValidation.IsChecked = DeserializedPortalSettings.disableCertificateValidation;
            allowUnsafeLabels.IsChecked = DeserializedPortalSettings.allowUnsafeLabels;
            inlineBrowserDisplayExtensions.Text = DeserializedPortalSettings.inlineBrowserDisplayExtensions.ToString();
            lookupAlwaysEnabled.IsChecked = DeserializedPortalSettings.lookupAlwaysEnabled;
            queryRequestLimit.Text = DeserializedPortalSettings.queryRequestLimit.ToString();
            useCdn.IsChecked = DeserializedPortalSettings.useCdn;
            useHttpCompression.IsChecked = DeserializedPortalSettings.useHttpCompression;
            loadAllFilesIndividually.IsChecked = DeserializedPortalSettings.loadAllFilesIndividually;
            enableSessionSerialization.IsChecked = DeserializedPortalSettings.enableSessionSerialization;
            alwaysLoadKeys.IsChecked= DeserializedPortalSettings.alwaysLoadKeys;
            uiInteractionTimeoutInSeconds.Text = DeserializedPortalSettings.uiInteractionTimeoutInSeconds.ToString();
            allowScriptsInReports.IsChecked = DeserializedPortalSettings.allowScriptsInReports;
            signalRConnectionTimeoutInSeconds.Text = DeserializedPortalSettings.signalRConnectionTimeoutInSeconds.ToString();
            signalRDisconnectTimeoutInSeconds.Text = DeserializedPortalSettings.signalRDisconnectTimeoutInSeconds.ToString();
            signalRKeepAliveInSeconds.Text = DeserializedPortalSettings.signalRKeepAliveInSeconds.ToString();
            disableAnchoring.IsChecked = DeserializedPortalSettings.disableAnchoring;
            disableSplitters.IsChecked = DeserializedPortalSettings.disableSplitters;
            useLegacyCompleteResponse.IsChecked = DeserializedPortalSettings.useLegacyCompleteResponse;
            scanditLicenseKey.Text = DeserializedPortalSettings.scanditLicenseKey.ToString();
            redirectHttpToHttps.IsChecked = DeserializedPortalSettings.redirectHttpToHttps;
            enableInsecureDeepLinks.IsChecked = DeserializedPortalSettings.enableInsecureDeepLinks;
            autoSizeLabels.IsChecked = DeserializedPortalSettings.autoSizeLabels;
            authLogFile.Text = DeserializedPortalSettings.authLogFile.ToString();
            defaultAuthMode.Text = DeserializedPortalSettings.defaultAuthMode.ToString();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }
}
