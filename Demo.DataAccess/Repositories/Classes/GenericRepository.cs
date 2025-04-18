﻿
using System.Linq.Expressions;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDBContext _DbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        #region Get
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _DbContext.Set<TEntity>().Where(entity => entity.IsDeleted == false).ToList();
            else
                return _DbContext.Set<TEntity>().Where(entity => entity.IsDeleted == false).AsNoTracking().ToList();
        }
        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _DbContext.Set<TEntity>()
                             .Select(selector).ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return _DbContext.Set<TEntity>()
                              .Where(entity => entity.IsDeleted == false)
                              .Where(filter).ToList();
        }

        //GetById
        public TEntity? GetById(int id)
        {
            return _DbContext.Set<TEntity>().Find(id);
        } 
        #endregion
        //Add
        public void Add(TEntity entity)
        {
            _DbContext.Set<TEntity>().Add(entity);
        }
        //Edit
        public void Update(TEntity entity)
        {
            _DbContext.Set<TEntity>().Update(entity);
        }
        //Delete
        public void Remove(TEntity entity)
        {
            _DbContext.Set<TEntity>().Remove(entity);

        }

     
    }
}
