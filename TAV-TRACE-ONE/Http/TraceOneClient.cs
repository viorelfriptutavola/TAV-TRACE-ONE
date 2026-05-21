using Config;
using Json;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TAV_TRACE_ONE.Models;

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

        private async Task<ApiResult> PostJsonAsync(string endpoint,object payload)
        {
            var url = _tenant + "/" + endpoint;

            var json = JsonCfg.ToJson(payload);

            var req = new HttpRequestMessage(
                HttpMethod.Post,
                url
            );

            req.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var res = await WithRetry(
                () => _http.SendAsync(req)
            );

            var body =
                await res.Content.ReadAsStringAsync();


            bool businessSuccess =
                res.IsSuccessStatusCode;

            string businessError = null;

            try
            {
                var apiResponse =
                    JsonConvert.DeserializeObject<
                        ApiResponseRoot>(body);

                if (apiResponse?.ResponseMessages != null)
                {
                    foreach (var msg
                        in apiResponse.ResponseMessages)
                    {
                        if (
                            msg.Status != null
                            &&
                            msg.Status.ToUpper() == "ERROR"
                        )
                        {
                            businessSuccess = false;

                            businessError =
                                msg.ErrorMessage;

                            break;
                        }
                    }
                }
            }
            catch
            {
                // ignore parsing errors
            }


            return new ApiResult
            {
                Success =
                    res.IsSuccessStatusCode,

                            BusinessSuccess =
                    businessSuccess,

                            BusinessError =
                    businessError,

                            StatusCode =
                    (int)res.StatusCode,

                            ReasonPhrase =
                    res.ReasonPhrase,

                            RequestBody =
                    json,

                            ResponseBody =
                    body
            };
        }

        public Task<ApiResult>
    SendCustomersAsync(
        Models.CustomersRoot data)
    => PostJsonAsync(
        AppSettings.EpCustomers,
        data
    );

        public Task<ApiResult>
            SendContactsAsync(
                Models.ContactRoot data)
            => PostJsonAsync(
                AppSettings.EpContacts,
                data
            );

        public Task<ApiResult>
            SendMaterialCustomersAsync(
                Models.MaterialCustomersRoot data)
            => PostJsonAsync(
                AppSettings.EpMaterialCustomers,
                data
            );

        public Task<ApiResult>
            SendCustomerSpectypesAsync(
                Models.CustomerSpectypesRoot data)
            => PostJsonAsync(
                AppSettings.EpCustomerSpectypes,
                data
            );
    }
}
