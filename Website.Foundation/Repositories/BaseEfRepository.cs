﻿using Website.Foundation.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection;

namespace Website.Foundation.Repositories
{
    public class BaseEfRepository<TEntity> : IBaseEfRepository<TEntity> where TEntity : Entity, IEntity, new()
    {
        private TableContext _context;
        private DbSet<TEntity> _entitySet;
        public BaseEfRepository()
        {
            _context = new TableContext();

            PropertyInfo[] infos = _context.GetType().GetProperties();
            foreach(PropertyInfo info in infos)
            {
                if (info.PropertyType == typeof(DbSet<TEntity>))
                {
                    _entitySet = info.GetValue(_context) as DbSet<TEntity>;
                    break;
                }
            }
        }


        public ICollection<IEntity> GetAll()
        {
            return _entitySet.ToList<IEntity>();
        }

        public IEntity Get(Guid ID)
        {
            return _entitySet.Where(c => c.ID == ID).FirstOrDefault();
        }

        public void Add(IEntity entity)
        {
            _entitySet.Add((TEntity)entity);
            _context.SaveChanges();
        }

        public void Update(IEntity entity)
        {
            IEntity currentItem = Get(entity.ID);
            if (currentItem == null)
                return;
            _context.Entry(currentItem).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }

        public void Remove(Guid ID)
        {
            IEntity currentItem = Get(ID);
            if (currentItem == null)
                return;
            _entitySet.Remove((TEntity)currentItem);
            _context.SaveChanges();
        }

        public int GetTotal()
        {
            return _entitySet.Count();
        }
    }
}
