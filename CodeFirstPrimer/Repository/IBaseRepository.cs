using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstPrimer.Repository
{
    public interface IBaseRepository<TEntity>
    {
        //TEntity GetById(string id);
        TEntity GetById(Func<TEntity, bool> predicate);

        //// IQueryable<TEntity> GetAll();
        // IEnumerable<TEntity> GetAll();

        // TEntity Get(Func<TEntity, bool> predicate);
        // IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate = null);

     //Func<T> denotes a delegate that is pretty much a pointer to a method and Expression<Func<T>> denotes a tree data structure for a lambda expression.
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
                                    Func<IQueryable<TEntity>, 
                                    IOrderedQueryable<TEntity>> orderBy = null,
                                    string includeProperties = "");

       // TEntity GetP(Func<TEntity, bool> predicate);


        void Edit(TEntity entity);

        void Insert(TEntity entity);

        void Delete(TEntity entity);
    }
}
