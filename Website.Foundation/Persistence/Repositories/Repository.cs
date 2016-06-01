using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection;
using Ninject;
using Ratul.Utility;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Aggregates;
using System.Linq.Expressions;
using Website.Foundation.Core.Container;

namespace Website.Foundation.Persistence.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DbContext Context;
        public Repository(DbContext context)
        {
            this.Context = context;
        }


        public void Add(TEntity entity)
        {
            this.Context.Set<TEntity>().Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.Context.Set<TEntity>().AddRange(entities);
        }


        public void Update(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
        }


        public void Remove(Guid ID)
        {
            TEntity currentItem = this.Get(ID);
            this.Remove(currentItem);
        }
        public void Remove(TEntity entity)
        {
            this.Context.Set<TEntity>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.Context.Set<TEntity>().RemoveRange(entities);
        }


        public TEntity Get(Guid ID)
        {
            return this.Context.Set<TEntity>().Find(ID);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return this.Context.Set<TEntity>().ToList();
        }
        public IEnumerable<TEntity> GetBy(int index, int size, SortBy<TEntity> sortBy)
        {
            ICollection<TEntity> listEntity = this.Context.Set<TEntity>()
                .OrderByDirection(sortBy.PredicateOrderBy, sortBy.IsAscending)
                .Skip(index).Take(size).ToList<TEntity>();
            return listEntity;
        }
        

        public int GetTotal()
        {
            return this.Context.Set<TEntity>().Count();
        }
        
    }
}
