using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVinCar.Infra.Data.Repositories
{
    public class BaseRepository <TEntity, TKey> where TEntity : class
    {
        protected readonly DevInCarDbContext _context;
        public BaseRepository(DevInCarDbContext context)
        {
            _context = context;
        }

        public virtual void InsertBase (TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

        }
        public virtual TEntity GetByIdBase (TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual void UpdateBase (TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public virtual void RemoveBase (TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual IQueryable <TEntity> QueryBase()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
      
    }
}