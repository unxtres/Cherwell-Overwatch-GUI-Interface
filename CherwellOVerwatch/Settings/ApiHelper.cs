using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CherwellOVerwatch.Settings
{
    class ApiHelper
    {
        private string link = "http://localhost:5000/api/";
        public static string token;
        public async void get_token(string regkey)
        {
            HttpClient client = new HttpClient();
            string reglink = "http://localhost:5000/api/Registration/generate?key=" + regkey;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(reglink),
                Content = new StringContent("body", Encoding.UTF8, "application/json"),
            };
            var response = await client.SendAsync(request).ConfigureAwait(false);
            var responsebody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            token = responsebody.ToString();
        }
    }

    public static class TokenInterface
    {
        public static string OWToken;
    }

    public class LoadSettings
    {
        string Result;

        public string GetResult(string url)
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = TokenInterface.OWToken;

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    Result = streamReader.ReadToEnd();
                }
            }
            catch
            {
                MessageBox.Show("Not Connected");
                throw;
            }
            return Result;
        }
    }
}
