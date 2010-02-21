using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FubuDinner.Web.Infrastructure;
using FubuDinner.Web.Model;

namespace FubuDinner.Test
{
    public class InMemoryRepository : IRepository
    {
        private readonly IList<object> _items = new List<object>();
        private readonly ArrayList _savedItems = new ArrayList();
        private readonly ArrayList _deletedItems = new ArrayList();

        public IList SavedItems { get { return _savedItems; } }
        public IList DeletedItems { get { return _deletedItems; } }

        public void ClearHistory()
        {
            _savedItems.Clear();
            _deletedItems.Clear();
        }

        private IQueryable<T> find<T>()
        {
            return _items.Where(i => i is T).Cast<T>().AsQueryable();
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return find<T>();
        }

        public T Get<T>(Guid id) where T : Entity
        {
            return find<T>().Where(e => e.Id == id).SingleOrDefault();
        }

        public void Save<T>(T entity) where T : Entity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if( ! _items.Contains(entity ))
            {
                _items.Add(entity);
            }

            _savedItems.Add(entity);
        }

        public IQueryable<T> Find<T>(Expression<Func<T, bool>> where) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}