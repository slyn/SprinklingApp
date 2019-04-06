using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SprinklingApp.Model.Entities.Abstract;

namespace SprinklingApp.DataAccess {

    public class DataAccessor : IAccessor {
        private readonly IRepository _repo;

        public DataAccessor(IRepository repository) {
            _repo = repository;
        }


        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity, new() {
            TEntity response = _repo.Get(filter);
            return response;
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity, new() {
            IEnumerable<TEntity> responseList = _repo.GetList(filter);
            return responseList;
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : BaseEntity, new() {
            TEntity response = _repo.Insert(entity);
            return response;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity, new() {
            _repo.Update(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity, new() {
            _repo.Delete(entity);
        }
    }

}