using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SprinklingApp.Model.Entities.Abstract;

namespace SprinklingApp.DataAccess {

    public interface IRepository {
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity, new();
        IEnumerable<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity, new();
        TEntity Insert<TEntity>(TEntity entity) where TEntity : BaseEntity, new();
        void Update<TEntity>(TEntity entity) where TEntity : BaseEntity, new();
        void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity, new();
    }

}