using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Http
{
    public sealed class AuthClient
    {
        private readonly string _tokenUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly HttpClient _http;

        public AuthClient(string tokenUrl, string clientId, string clientSecret)
        {
            _tokenUrl = tokenUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _http = new HttpClient();
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var basic = Convert.ToBase64String(Encoding.ASCII.GetBytes(_clientId + ":" + _clientSecret));

            var req = new HttpRequestMessage(HttpMethod.Post, _tokenUrl);
            req.Headers.Authorization = new AuthenticationHeaderValue("Basic", basic);
            req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            });

            var res = await _http.SendAsync(req);
            res.EnsureSuccessStatusCode();

            var body = await res.Content.ReadAsStringAsync();
            var json = JObject.Parse(body);
            var token = (string)json["access_token"];
            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Access token non trovato nella risposta OAuth2.");

            return token;
        }
    }
}
