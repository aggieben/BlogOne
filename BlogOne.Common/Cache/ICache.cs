using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogOne.Common.Cache
{
    public interface ICache
    {
        T Get<T>(string key);

        void Set<T>(string key, T value);

        T GetSet<T>(string key, Func<T,T> inout);

        void Remove(string key);
    }
}
