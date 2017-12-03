using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrackYourLife_IoT.Data.Api.Rest
{
    internal class Request
    {
        private readonly RestApiBase _restApiBase;
        private readonly string _url;
        private readonly IList<KeyValuePair<string, string>> _queryItems;

        public Request(RestApiBase restApiBase, string url, IList<KeyValuePair<string, string>> queryItems)
        {
            _restApiBase = restApiBase;
            _url = url;
            _queryItems = queryItems;
        }

        internal async Task<T> Get<T>()
        {
            using (var httpClient = CreateHttpClient())
            {
                ProcessInterceptors(httpClient);

                var response = CheckResponse(await httpClient.GetAsync(PrepareUrl()));

                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        internal async Task<T> PostFormUrlEncoded<T>(IList<KeyValuePair<string, string>> @params)
        {
            using (var httpClient = CreateHttpClient())
            {
                ProcessInterceptors(httpClient);

                var response = CheckResponse(await httpClient.PostAsync(_url, new FormUrlEncodedContent(@params)));

                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        private HttpResponseMessage CheckResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(response.ReasonPhrase);
            }

            return response;
        }

        private void ProcessInterceptors(HttpClient httpClient)
        {
            _restApiBase.CallInterceptors(httpClient);
        }

        private HttpClient CreateHttpClient()
        {
            return new HttpClient
            {
                BaseAddress = _restApiBase.BaseAddress
            };
        }

        private string PrepareUrl()
        {
            StringBuilder sb = new StringBuilder(_url);

            if (_queryItems.Count > 0)
            {
                sb.Append('?').Append(string.Join("&", _queryItems.Select(it => $"{it.Key}={it.Value}")));
            }

            return sb.ToString();
        }
    }
}