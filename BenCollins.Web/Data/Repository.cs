using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;

namespace BenCollins.Web.Data
{
    public abstract class Repository<T> : IRepository<T>
    {
        private readonly IDbConnectionFactory _connectionFactory;
        protected IDbConnection Connection 
        { 
            get 
            {
                var cn = _connectionFactory.CreateConnection();
                cn.Open();
                return cn;
            } 
        }

        public Repository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public abstract void Add(T item);
        public abstract void Remove(T item);
        public abstract void Update(T item);
        public abstract T FindBySid(Guid id);
        public abstract T FindById(int id);
        public abstract IEnumerable<T> FindAll();
    }
}