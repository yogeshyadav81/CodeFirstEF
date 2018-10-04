using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Web;
using CodeFirstPrimer.Entities;
namespace CodeFirstPrimer.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
       // private readonly IUnitOfWork unitOfWork;
        public NhlContext context;
        public DbSet<TEntity> dbset;
        public BaseRepository(NhlContext context) 
        {
            this.context = context;
            dbset = context.Set<TEntity>();
        }

       public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
                                     Func<IQueryable<TEntity>,
                                     IOrderedQueryable<TEntity>> orderBy = null,
                                     string includeProperties = "")
        {
            IQueryable<TEntity> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }


        public TEntity GetById(Func<TEntity, bool> predicate) 
        {
            // return dbset.//entities.Contacts.FirstOrDefault(predicate);
            return context.Set<TEntity>().FirstOrDefault(predicate);
        }

        //public TEntity GetById(string id)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(id, @"[0-9]"))
        //    {
        //        return dbset.Find(int.Parse(id));
        //    }

        //    return dbset.Find(id);
        //}


        //public IEnumerable<TEntity> GetAll()
        //{
        //    return dbset;
        //}

        public void Insert(TEntity entity)
        {
            dbset.Add(entity);
        }


        public void Edit(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }


        public void Delete(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }
    }
}