using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models.Shared;

namespace Demo.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
      
       void Add(TEntity entity);
       
       IEnumerable<TEntity> GetAll(bool withTracking = false);
        
       IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity,TResult>> selector);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate);
        TEntity? GetById(int id);
       
       void Remove(TEntity entity);
        
       void Update(TEntity entity);

     
    }
}
