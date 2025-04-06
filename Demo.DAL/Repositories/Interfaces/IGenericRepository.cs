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
      
       int Add(TEntity entity);
       
       IEnumerable<TEntity> GetAll(bool withTracking = false);
        
       IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity,TResult>> selector);
       TEntity? GetById(int id);
       
       int Remove(TEntity entity);
        
       int Update(TEntity entity);

     
    }
}
