using System;
using System.Collections.Generic;
using System.Net.Http;

namespace TrackYourLife_IoT.Data.Api.Rest
{
    public class RestApiBase
    {
        private readonly IList<IRequestInterceptor> _requestInterceptors;

        public RestApiBase(Uri baseAddress)
        {
            _requestInterceptors = new List<IRequestInterceptor>();
            BaseAddress = baseAddress;
        }

        public Uri BaseAddress { get; }

        protected string QueryParam(string property, string value)
        {
            return $"{property}={value}";
        }
        
        internal void CallInterceptors(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Clear();

            foreach (var requestInterceptor in _requestInterceptors)
                requestInterceptor.Intercept(httpClient);
        }

        protected UrlRequestBuilder Url(string url)
        {
            return new UrlRequestBuilder(this, url);
        }

        public void AddInterceptor(IRequestInterceptor requestInterceptor)
        {
            _requestInterceptors.Add(requestInterceptor);
        }
    }
}
