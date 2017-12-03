using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using TrackYourLife_IoT.Business.Services;

namespace TrackYourLife_IoT.Data.Api.Rest
{
    public class PostFormUrlEncodedRequestBuilder
    {
        private readonly UrlRequestBuilder _requestBuilder;
        private readonly IList<KeyValuePair<string, string>> _params;

        public PostFormUrlEncodedRequestBuilder(UrlRequestBuilder requestBuilder)
        {
            _requestBuilder = requestBuilder;
            _params = new List<KeyValuePair<string, string>>();
        }

        public PostFormUrlEncodedRequestBuilder Param(string key, string value)
        {
            _params.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        public Task<TResult> PostAsync<TResult>()
        {
            Request request = _requestBuilder.CreateRequest();
            return request.PostFormUrlEncoded<TResult>(_params);
        }
    }
}
