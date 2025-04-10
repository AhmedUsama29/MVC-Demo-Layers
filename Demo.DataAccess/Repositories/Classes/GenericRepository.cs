
namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDBContext _DbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _DbContext.Set<TEntity>().ToList();
            else
                return _DbContext.Set<TEntity>().AsNoTracking().ToList();
        }
        //GetById
        public TEntity? GetById(int id)
        {
            return _DbContext.Set<TEntity>().Find(id);
        }
        //Add
        public int Add(TEntity entity)
        {
            _DbContext.Set<TEntity>().Add(entity);
            return _DbContext.SaveChanges();
        }
        //Edit
        public int Update(TEntity entity)
        {
            _DbContext.Set<TEntity>().Update(entity);
            return _DbContext.SaveChanges();
        }
        //Delete
        public int Remove(TEntity entity)
        {
            _DbContext.Set<TEntity>().Remove(entity);
            return _DbContext.SaveChanges();

        }

    }
}
