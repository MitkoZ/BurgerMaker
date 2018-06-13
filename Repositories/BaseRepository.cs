using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IEntity
    {

        protected DbSet<TEntity> DBSet
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        public BaseRepository(IDbContext burgerMakerDbContext)
        {
            this.context = burgerMakerDbContext;
        }

        protected IDbContext context;

        public List<TEntity> GetAll(Func<TEntity, bool> filter = null)
        {
            if (filter != null)
            {
                return context.Set<TEntity>().Where(filter).ToList();
            }

            return context.Set<TEntity>().ToList();
        }

        public void Create(TEntity item)
        {
            context.Set<TEntity>().Add(item);
        }

        public void Update(TEntity item, Func<TEntity, bool> findByPredicate)
        {
            var local = context.Set<TEntity>()
                           .Local
                           .FirstOrDefault(findByPredicate);
            context.Entry(item).State = EntityState.Modified;
        }

        public void DeleteById(int id)
        {
            TEntity dbItem = context.Set<TEntity>().Find(id);
            if (dbItem != null)
            {
                context.Set<TEntity>().Remove(dbItem);
            }
        }

        public void DeleteByPredicate(Func<TEntity, bool> filter)
        {
            IEnumerable<TEntity> itemsToRemove = context.Set<TEntity>().Where(filter);
            context.Set<TEntity>().RemoveRange(itemsToRemove);
        }

        public void Save(TEntity item)
        {
            if (item.Id == 0)
            {
                Create(item);
            }
            else
            {
                Update(item, x => x.Id == item.Id);
            }
        }
    }
}
