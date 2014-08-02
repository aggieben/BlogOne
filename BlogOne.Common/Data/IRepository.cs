using System;
using System.Collections.Generic;

namespace BlogOne.Common.Data
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        void Update(IEnumerable<T> items);
        T FindBySid(Guid id);
        T FindById(int id);
        IEnumerable<T> FindAll(int page = 1, int pageSize = int.MaxValue);
    }
}
