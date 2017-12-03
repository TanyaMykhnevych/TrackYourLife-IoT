using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace TrackYourLife_IoT.Data.Api.Rest
{
    public class RequestInterceptorBase : IRequestInterceptor
    {
        protected readonly List<KeyValuePair<string, string>> _queryItems;

        public RequestInterceptorBase(params KeyValuePair<string, string>[] queryItems)
        {
            _queryItems = queryItems.ToList();
        }

        public virtual void Intercept(HttpClient httpClient)
        {
            foreach (var queryItem in _queryItems)
            {
                httpClient.DefaultRequestHeaders.Remove(queryItem.Key);
                httpClient.DefaultRequestHeaders.Add(queryItem.Key, queryItem.Value);
            }
        }

        public void AddInterceptor(KeyValuePair<string, string> interceptor)
        {
            RemoveInterceptorIfExist(interceptor.Key);
            _queryItems.Add(interceptor);
        }

        public void RemoveInterceptorIfExist(string key)
        {
            bool isAlreadyExist = _queryItems.Any(x => x.Key == key);
            if (isAlreadyExist)
            {
                var itemToRemove = _queryItems.Single(x => x.Key == key);
                _queryItems.Remove(itemToRemove);
            }
        }
    }
}
