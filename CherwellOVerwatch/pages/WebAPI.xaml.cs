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
    public partial class WebAPI : Page
    {
        public string url = "http://localhost:5000/api/settings/WebApiServiceSettings";
        public WebAPI()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            LoadSettings loader = new LoadSettings();

            Web_api DeserializedAPI = JsonConvert.DeserializeObject<Web_api>(loader.GetResult(url));

            apiClientId.Text = DeserializedAPI.apiClientId.ToString();
            rateLimitingEnabled.IsChecked = DeserializedAPI.rateLimitingEnabled;
            maxConcurrentRequests.Text = DeserializedAPI.maxConcurrentRequests.ToString();
            loadFromFileFromBin.IsChecked = DeserializedAPI.loadFromFileFromBin;
            authenticationProvider.Text = DeserializedAPI.authenticationProvider.ToString();
            trebuchetDataSource.Text = DeserializedAPI.trebuchetDataSource.ToString();
            useSamlAdfsRedirect.IsChecked = DeserializedAPI.useSamlAdfsRedirect;
            idpIsAdfs.IsChecked= DeserializedAPI.idpIsAdfs;
            samlAdfsIdpInitiatedUrl.Text = DeserializedAPI.samlAdfsIdpInitiatedUrl.ToString();
            samlServerTimeAllowance.Text = DeserializedAPI.samlServerTimeAllowance.ToString();
            certificateUserEmail.Text = DeserializedAPI.certificateUserEmail.ToString();
            certSubjectName.Text = DeserializedAPI.certSubjectName.ToString();
            certIssuer.Text= DeserializedAPI.certIssuer.ToString();
            certThumbprint.Text = DeserializedAPI.certThumbprint.ToString();
            authorizationCodeExpirationMinutes.Text = DeserializedAPI.authorizationCodeExpirationMinutes.ToString();
            recoveryFilePersistIntervalSeconds.Text = DeserializedAPI.recoveryFilePersistIntervalSeconds.ToString();
            tenant.Text = DeserializedAPI.tenant.ToString();
            audience.Text= DeserializedAPI.audience.ToString();
            aadClientId.Text= DeserializedAPI.aadClientId.ToString();
            aadClientUserEmail.Text = DeserializedAPI.aadClientUserEmail.ToString();
            grantType.Text = DeserializedAPI.grantType.ToString();
            authorizationUrl.Text = DeserializedAPI.authorizationUrl.ToString();
            useRecoveryFile.IsChecked = DeserializedAPI.useRecoveryFile;
            recoveryFilePath.Text= DeserializedAPI.recoveryFilePath.ToString();
            recoveryFileName.Text = DeserializedAPI.recoveryFileName.ToString();
        }
        private void Button_Load(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }
}
