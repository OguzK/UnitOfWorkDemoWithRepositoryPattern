using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork.Repositories.Common
{
    interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> DbSet { get; }
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector);
        IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector);
        BindingList<TEntity> ToBindingList(Expression<Func<TEntity, bool>> filter = null);


        TEntity Find(params object[] keys);
        TEntity Single(Expression<Func<TEntity, bool>> filter);
        TEntity First(Expression<Func<TEntity, bool>> filter);




        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(params object[] keys);



    }
}
