using Config;
using Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Http
{
    public sealed class TraceOneClient
    {
        private readonly HttpClient _http;
        private readonly string _tenant;

        public TraceOneClient(string baseApi, string tenant, string bearerToken)
        {
            _tenant = tenant;
            _http = new HttpClient { BaseAddress = new Uri(baseApi) };
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        }

        private static async Task<HttpResponseMessage> WithRetry(Func<Task<HttpResponseMessage>> action, int attempts = 3, int delayMs = 600)
        {
            Exception last = null;
            for (int i = 1; i <= attempts; i++)
            {
                try { return await action(); }
                catch (Exception ex) { last = ex; if (i < attempts) await Task.Delay(delayMs); }
            }
            throw last;
        }

        private async Task<string> PostJsonAsync(string endpoint, object payload)
        {
            var url = _tenant + "/" + endpoint;
            var json = JsonCfg.ToJson(payload);

            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await WithRetry(() => _http.SendAsync(req));
            var body = await res.Content.ReadAsStringAsync();
            return ((int)res.StatusCode) + " " + res.ReasonPhrase + Environment.NewLine + body;
        }

        public Task<string> SendCustomersAsync(Models.CustomersRoot data)
            => PostJsonAsync(AppSettings.EpCustomers, data);

        public Task<string> SendContactsAsync(Models.ContactRoot data)
            => PostJsonAsync(AppSettings.EpContacts, data);

        public Task<string> SendMaterialCustomersAsync(Models.MaterialCustomersRoot data)
            => PostJsonAsync(AppSettings.EpMaterialCustomers, data);

        public Task<string> SendCustomerSpectypesAsync(Models.CustomerSpectypesRoot data)
            => PostJsonAsync(AppSettings.EpCustomerSpectypes, data);
    }
}
