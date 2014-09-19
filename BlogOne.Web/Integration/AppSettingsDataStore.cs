using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using BlogOne.Common.Extensions;
using BlogOne.Web.Extensions;
using Google.Apis.Util.Store;
using System;

namespace BlogOne.Web.Integration
{
    public class AppSettingsDataStore : IDataStore
    {
        private const string KeyPrefix = "Google.Apis.DataStore";

        public async Task ClearAsync()
        {
            var keys = await AllKeysAsync();
            var sets = new List<Task>();
            foreach (var k in keys)
            {
                var value = await GetAsync(k);
                if (String.IsNullOrWhiteSpace(value))
                {
                    sets.Add(SetAsync(k, string.Empty));
                }
            }

            await Task.WhenAll(sets);
        }

        public async Task DeleteAsync<T>(string key)
        {
            await SetAsync(key, string.Empty);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var stringValue = await GetAsync(key);
            var tVal = Jil.JSON.Deserialize<T>(stringValue);

            return tVal;
        }

        public async Task StoreAsync<T>(string key, T value)
        {
            var stringVal = Jil.JSON.Serialize(value);

            await SetAsync(key, stringVal);
        }

        private static Task<IEnumerable<string>> AllKeysAsync()
        {
            return Task<IEnumerable<string>>.Factory.StartNew(
                () => WebConfigurationManager.OpenWebConfiguration("~").AppSettings.Settings.AllKeys.Where(k => k.StartsWith(KeyPrefix)));
        }

        private static Task<string> GetAsync(string key)
        {
            return Task<string>.Factory.StartNew(
                () => WebConfigurationManager.OpenWebConfiguration("~").AppSettings.Settings["{0}:{1}".f(KeyPrefix, key)].Value);
        }

        private static Task SetAsync(string key, string value)
        {
            return Task.Factory.StartNew(() =>
            {
                var config = WebConfigurationManager.OpenWebConfiguration("~");
                if (String.IsNullOrWhiteSpace(value))
                {
                    config.AppSettings.Settings.Ensure("{0}:{1}".f(KeyPrefix, key), value);
                }
                else
                {
                    config.AppSettings.Settings.Remove("{0}:{1}".f(KeyPrefix, key));
                }
                config.Save();
            });
        }
    }
}