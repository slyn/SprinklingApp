using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SprinklingApp.DataAccess.ORM.EFCore;
using SprinklingApp.Model.Entities.Abstract;

namespace SprinklingApp.DataAccess
{
    public class EFCoreRepository : IRepository
    {
        private readonly DbContext _context;

        #region [ CTOR ]
        public EFCoreRepository(SpringklingContext context)
        {
            _context = context;
        }

        #endregion
        
        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity, new()
        {
            var dbSet = _context.Set<TEntity>();
            var result = dbSet.SingleOrDefault(filter);
            return result;
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity, new()
        {
            var dbSet = _context.Set<TEntity>();
            var returnList = dbSet.Where(filter).ToList();
            return returnList;
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            _context.AddAsync(entity);
            _context.SaveChangesAsync();

            return entity;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            _context.Update(entity);
            _context.SaveChangesAsync();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            _context.Remove(entity);
            _context.SaveChangesAsync();
        }

    }
}
