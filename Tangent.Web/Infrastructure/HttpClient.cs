using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tangent.Web.Infrastructure
{
    public class HttpClient : IHttpClient
    {
        private readonly System.Net.Http.HttpClient httpClient;
        private readonly string TangentAuthUrl;
        private bool isAuthenticated = false;

        public HttpClient(string BaseUrl, string TangentAuthUrl)
        {
            httpClient = new System.Net.Http.HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.TangentAuthUrl = TangentAuthUrl;
        }

        public async Task<HttpResult> ReadAsStringAsync(string Url)
        {
            if (!isAuthenticated)
            {
                GenerateAuthToken();
            }
            
            var response = await httpClient.GetAsync(Url);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new HttpResult { StatusCode = (int)response.StatusCode, Content = null };
            }

            var jsonResult = await response.Content.ReadAsStringAsync();

            return new HttpResult { StatusCode = (int)response.StatusCode, Content = jsonResult };
        }

        public async Task<HttpResult> PostAsync(string Url, string Content)
        {
            if (!isAuthenticated)
            {
                GenerateAuthToken();
            }

            StringContent postContent = new StringContent(Content, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(Url, postContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            return new HttpResult { StatusCode = (int)response.StatusCode, Content = responseContent };
        }

        private void GenerateAuthToken()
        {
            dynamic auth = new { username = "pravin.gordhan", password = "pravin.gordhan" };

            StringContent postContent = new StringContent(JsonConvert.SerializeObject(auth), Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync(TangentAuthUrl, postContent).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            dynamic token = JsonConvert.DeserializeObject(responseString);
            string tokenValue = (string)token.token;

            var authValue = new AuthenticationHeaderValue("token", tokenValue);

            httpClient.DefaultRequestHeaders.Authorization = authValue;

            isAuthenticated = true;
        }
    }

    public struct HttpResult
    {
        public int StatusCode { get; set; }

        public string Content { get; set; }
    }
}
