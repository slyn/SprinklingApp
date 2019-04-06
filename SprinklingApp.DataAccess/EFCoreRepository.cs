using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SprinklingApp.DataAccess.ORM.EFCore;
using SprinklingApp.Model.Entities.Abstract;

namespace SprinklingApp.DataAccess {

    public class EFCoreRepository : IRepository {
        private readonly DbContext context;

        #region [ CTOR ]

        public EFCoreRepository(SpringklingContext context) {
            this.context = context;
        }

        #endregion

        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity, new() {
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            TEntity result = dbSet.SingleOrDefault(filter);
            return result;
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity, new() {
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            List<TEntity> returnList = dbSet.Where(filter).ToList();
            return returnList;
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : BaseEntity, new() {
            context.AddAsync(entity);
            context.SaveChangesAsync();

            return entity;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity, new() {
            context.Update(entity);
            context.SaveChangesAsync();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity, new() {
            context.Remove(entity);
            context.SaveChangesAsync();
        }
    }

}