using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork.Models;

namespace UnitOfWork.Repositories.Common
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>, IWorkerObjects where TEntity : class
    {
        private DbSet<TEntity> _dbSet;
        public DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = Worker.DbContext.Set<TEntity>();
                }
                return _dbSet;
            }

        }

        /// <summary>
        /// Implement from IWorkerObjects Interface
        /// </summary>
        public Worker Worker { get; set; }


        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Update(TEntity entity)
        {
            Worker.DbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(params object[] keys)
        {
            Delete(Find(keys));
        }
        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }


        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }
        public virtual IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return DbSet.Select(selector);
        }
        public virtual IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector)
        {
            return Filter(filter).Select(selector);
        }
        public virtual BindingList<TEntity> ToBindingList(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null)
            {
                DbSet.Load();
            }
            else
            {
                Filter(filter).Load();
            }
            return DbSet.Local.ToBindingList();
        }

        public virtual TEntity Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }
        public virtual TEntity Single(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Single(filter);
        }
        public virtual TEntity First(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.First(filter);
        }
    }
}
