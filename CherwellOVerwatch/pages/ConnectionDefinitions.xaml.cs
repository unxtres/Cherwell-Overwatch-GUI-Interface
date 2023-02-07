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
            loadData(Current_Connection);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loadData(Current_Connection);
        }

        private void Button_previous(object sender, RoutedEventArgs e)
        {
            if (Current_Connection > 0)
            {
                Current_Connection--;
                loadData(Current_Connection);
                currCon.Text = (Current_Connection + 1).ToString() + "/" + Number_of_Connections.ToString();
            }
        }

        private void Button_next(object sender, RoutedEventArgs e)
        {
            if (Current_Connection >= 0 && Current_Connection<=(Number_of_Connections-1))
            {
                Current_Connection++;
                loadData(Current_Connection);
                currCon.Text = (Current_Connection).ToString() + "/" + Number_of_Connections.ToString();
            }
        }

        private void loadData(int i)
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
                numberOfConnections.Text = Number_of_Connections.ToString() + " connections found";
                currCon.Text = (Current_Connection + 1).ToString() + "/" + Number_of_Connections.ToString();
                buttonPrevious.IsEnabled = true;
                buttonNext.IsEnabled = true;
            }
            adminConn.Text = DeserializedConDefSettings.connectionDefs[i].adminConn.ToString();
            appServerHostMode.Text = DeserializedConDefSettings.connectionDefs[i].appServerHostMode.ToString();
            certificationValidationMode.Text = DeserializedConDefSettings.connectionDefs[i].certificationValidationMode.ToString();
            connectionRemotingType.Text = DeserializedConDefSettings.connectionDefs[i].connectionRemotingType.ToString();
            createdDateTime.Text = DeserializedConDefSettings.connectionDefs[i].createdDateTime.ToString();
            dbConn.Text = DeserializedConDefSettings.connectionDefs[i].dbConn.ToString();
            dbOwner.Text = DeserializedConDefSettings.connectionDefs[i].dbOwner.ToString();
            endCharForNameWithSpace.Text = DeserializedConDefSettings.connectionDefs[i].endCharForNameWithSpace.ToString();
            engine.Text = DeserializedConDefSettings.connectionDefs[i].engine.ToString();
            maxTransactionLogSizeInMB.Text = DeserializedConDefSettings.connectionDefs[i].maxTransactionLogSizeInMB.ToString();
            name.Text = DeserializedConDefSettings.connectionDefs[i].name.ToString();
            recoveryMode.Text = DeserializedConDefSettings.connectionDefs[i].recoveryMode.ToString();
            remotingSecurityMode.Text = DeserializedConDefSettings.connectionDefs[i].remotingSecurityMode.ToString();
            source.Text = DeserializedConDefSettings.connectionDefs[i].source.ToString();
            startCharForNameWithSpace.Text = DeserializedConDefSettings.connectionDefs[i].startCharForNameWithSpace.ToString();
            transactionLogGrowth.Text = DeserializedConDefSettings.connectionDefs[i].transactionLogGrowth.ToString();
            urltext.Text = DeserializedConDefSettings.connectionDefs[i].url.ToString();
            enableTransactionLogAutogrowth.IsChecked = DeserializedConDefSettings.connectionDefs[i].enableTransactionLogAutogrowth;
            transactionLogGrowthIsPercentage.IsChecked = DeserializedConDefSettings.connectionDefs[i].transactionLogGrowthIsPercentage;
            treatsEmptyStringAsNull.IsChecked = DeserializedConDefSettings.connectionDefs[i].treatsEmptyStringAsNull;
            useRest.IsChecked = DeserializedConDefSettings.connectionDefs[i].useRest;
            useSoap.IsChecked = DeserializedConDefSettings.connectionDefs[i].useSoap;
        }
    }
}
