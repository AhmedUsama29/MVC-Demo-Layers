using Demo.DataAccess.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity>
    {

        void Add(TEntity entity);

        IEnumerable<TEntity> GetAll(bool withTracking = false);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);    

        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity,TResult>> selector);
        TEntity? GetById(int id);
        void Remove(TEntity entity);
        void Update(TEntity entity);

    }
}
