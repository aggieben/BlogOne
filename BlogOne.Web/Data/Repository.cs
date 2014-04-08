using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System.Data.Common;

namespace BlogOne.Web.Data
{
    public abstract class Repository<T> : IRepository<T>
    {
        private readonly IDbConnectionFactory _connectionFactory;
        protected IDbConnection Connection 
        { 
            get 
            {
                var cn = _connectionFactory.CreateConnection();
                var pcn = new ProfiledDbConnection(cn as DbConnection, MiniProfiler.Current);
                //                                 ^^^^^^^^^^^^^^^^^^  really?
                pcn.Open();
                return pcn;
            } 
        }

        public Repository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Add(T item)
        {
            using (MiniProfiler.Current.Step("Repository.Add"))
                AddImpl(item);
        }

        public void Remove(T item)
        {
            using (MiniProfiler.Current.Step("Repository.Remove"))
                RemoveImpl(item);
        }

        public void Update(T item)
        {
            using (MiniProfiler.Current.Step("Repository.Update"))
                UpdateImpl(item);
        }

        public void Update(IEnumerable<T> items)
        {
            using (MiniProfiler.Current.Step("Repository.Update*"))
                UpdateImpl(items);
        }

        public T FindBySid(Guid id)
        {
            using (MiniProfiler.Current.Step("Repository.FindBySid"))
                return FindBySidImpl(id);
        }

        public T FindById(int id)
        {
            using (MiniProfiler.Current.Step("Repository.FindById"))
                return FindByIdImpl(id);
        }

        public IEnumerable<T> FindAll(int page = 1, int pageSize = int.MaxValue)
        {
            using (MiniProfiler.Current.Step("Repository.FindAll"))
                return FindAllImpl(page, pageSize);
        }

        public void UpdateImpl(IEnumerable<T> items)
        {
            using (MiniProfiler.Current.Step("Repository.Update*"))
                foreach (var t in items)
                {
                    UpdateImpl(t);
                }
        }

        protected abstract void AddImpl(T item);
        protected abstract void RemoveImpl(T item);
        protected abstract void UpdateImpl(T item);
        protected abstract T FindBySidImpl(Guid id);
        protected abstract T FindByIdImpl(int id);
        protected abstract IEnumerable<T> FindAllImpl(int page, int pageSize);
    }
}