using Demo.DAL.Data.Contexts;
using Demo.DAL.Models.Shared;
using Demo.DAL.Repositories.Interfaces;

namespace Demo.DAL.Repositories.classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(keyValues: id);

        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _dbContext.Set<TEntity>().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
