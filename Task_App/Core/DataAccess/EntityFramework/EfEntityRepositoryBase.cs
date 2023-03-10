using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        public EfEntityRepositoryBase(TContext context)
        {
            Context = context;
        }

        protected TContext Context { get; }

        public TEntity Add(TEntity entity)
        {
            return Context.Add(entity).Entity;
        }
        public TEntity AddRange(TEntity entity)
        {
            Context.Set<TEntity>().AddRange(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>().FirstOrDefault(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? Context.Set<TEntity>().ToList() : Context.Set<TEntity>().Where(filter).ToList();
        }

        public TEntity Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return entity;
        }

        public TEntity RemoveRange(TEntity entity)
        {
            Context.Set<TEntity>().RemoveRange(entity);
            return entity;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            Context.Update(entity);
            return entity;
        }
    }
}
