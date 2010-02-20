using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using FubuDinner.Web.Model;

namespace FubuDinner.Web.Infrastructure
{
    public class Db4ORepository : IRepository
    {
        private readonly IDb4OConnection _connection;

        public Db4ORepository(IDb4OConnection connection)
        {
            _connection = connection;
        }

        public IObjectContainer Db { get { return _connection.Current; } }


        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return Db.Query<T>();
        }

        public T Get<T>(Guid id) where T : Entity
        {
            return Db.Query<T>(t => t.Id == id).SingleOrDefault();
        }

        public void Save<T>(T entity) where T : Entity
        {
            Db.Store(entity);
        }

        public IQueryable<T> Find<T>(Expression<Func<T, bool>> where) where T : Entity
        {
            return Db.AsQueryable<T>().Where(where);
        }
    }
}