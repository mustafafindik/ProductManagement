using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Entities;

namespace ProductManagement.Core.DataAccess.EntityFrameworkCore
{
    public class EntityRepository<T, TContext> : IEntityRepository<T> where T : class, IEntity, new() where TContext : DbContext // T:Sınıf,IentityImplementEden,Newlenebilir | TContext :DbContext inherit eden
    {
        private readonly TContext _context;
        public EntityRepository(TContext context)
        {
            _context = context;
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {

            return _context.Set<T>().AsNoTracking().SingleOrDefault(predicate);

        }

        public T Get(Expression<Func<T, bool>> predicate, params string[] nav)
        {


            var query = _context.Set<T>();
            query = nav.Aggregate(query, (current, n) => (DbSet<T>)current.Include(n));
            return query.AsNoTracking().SingleOrDefault(predicate);

        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {

            return predicate == null ? _context.Set<T>().AsNoTracking() : _context.Set<T>().Where(predicate).AsNoTracking();

        }

        public IQueryable<T> GetAll(params string[] nav)
        {

            var query = _context.Set<T>();
            query = nav.Aggregate(query, (current, n) => (DbSet<T>)current.Include(n));
            return query.AsNoTracking();

        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, params string[] nav)
        {

            var query = predicate == null ? _context.Set<T>() : _context.Set<T>().Where(predicate);
            query = nav.Aggregate(query, (current, n) => (DbSet<T>)current.Include(n));
            return query.AsNoTracking();

        }

        public void Add(T entity)
        {

            var addedEntityEntry = _context.Entry(entity);
            addedEntityEntry.State = EntityState.Added;
            _context.SaveChanges();

        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();

        }

        public void Update(T entity, int id)
        {
            var existingEntity = _context.Set<T>().Find(id);

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();


        }
    }
}
