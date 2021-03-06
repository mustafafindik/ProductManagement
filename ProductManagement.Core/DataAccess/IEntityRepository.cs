using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ProductManagement.Core.Entities;

namespace ProductManagement.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new() //Sınıf , IEntity implement Eden ve newlenebilen sınıflar sadece
    {
        T Get(Expression<Func<T, bool>> predicate);
        T Get(Expression<Func<T, bool>> predicate, params string[] nav);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> GetAll(params string[] nav);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, params string[] nav);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity, int id);
    }
}
