using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FubuDinner.Web.Model;

namespace FubuDinner.Web.Infrastructure
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>() where T : Entity;
        T Get<T>(Guid id) where T : Entity;
        void Save<T>(T entity) where T : Entity;
        IQueryable<T> Find<T>(Expression<Func<T, bool>> where) where T : Entity;
    }
}