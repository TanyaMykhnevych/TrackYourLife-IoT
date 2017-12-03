using System.Linq;
using Windows.Storage;
using Newtonsoft.Json;

namespace TrackYourLife_IoT.Business.Services.Implementations
{
    class PreferencesService : IPreferencesService
    {
        private readonly ApplicationDataContainer _localSettings;
        
        private const string UserInfoKey = "userInfo";

        public PreferencesService()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
        }
        
        public void Clear()
        {
            _localSettings.Values.Clear();
        }

        private void AddOrUpdateValue<T>(string key, T value)
        {
            if (_localSettings.Values.Keys.Any(x => x == key))
            {
                _localSettings.Values[key] = Serialize(value);
            }
            else
            {
                _localSettings.Values.Add(key, Serialize(value));
            }
        }

        private T TryGetValue<T>(string key)
        {
            if (_localSettings.Values.Keys.Any(x => x == key))
            {
                var value = _localSettings.Values[key];
                return value != null ? Deserialize<T>(_localSettings.Values[key].ToString()) : default(T);
            }

            return default(T);
        }

        private string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}