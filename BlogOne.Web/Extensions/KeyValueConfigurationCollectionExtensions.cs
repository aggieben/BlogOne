using System.Configuration;
using System.Linq;

namespace BlogOne.Web.Extensions
{
    public static class KeyValueConfigurationCollectionExtensions
    {
        public static void Ensure(this KeyValueConfigurationCollection collection, string key, string value)
        {
            if (collection.AllKeys.Contains(key))
            {
                collection[key].Value = value;
            }
            else
            {
                collection.Add(key, value);
            }
        }
    }
}