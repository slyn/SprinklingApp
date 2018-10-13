using SprinklingApp.Common.FileOperator;
using SprinklingApp.Common.SerializationOperator;
using SprinklingApp.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace SprinklingApp.DataAccess
{
    public class FileRepository<TSerialization> : IRepository where TSerialization : ISerialization, new()
    {
        private readonly TSerialization _serializor;
        private readonly string _storageDirectory;
        public FileRepository(string storageDirectory)
        {
            if (string.IsNullOrWhiteSpace(storageDirectory))
                throw new Exception("Storage directory can not be null or empty!");

            _storageDirectory = storageDirectory;
            _serializor = new TSerialization();
        }

        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity, new()
        {
            var file = $"storage_{typeof(TEntity).Name}.json";
            var filePath = Path.Combine(_storageDirectory, file);
            var content = FileOps.ReadText(filePath);
            var dataList = (IEnumerable<TEntity>)_serializor.Deserialize<IEnumerable<TEntity>>(content);

            var responseItem = dataList.SingleOrDefault(filter.Compile());
            return responseItem;
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity, new()
        {
            var file = $"storage_{typeof(TEntity).Name}.json";
            var filePath = Path.Combine(_storageDirectory, file);

            if (!FileOps.IsExistingFile(filePath))
                return new List<TEntity>();

            var content = FileOps.ReadText(filePath);
            var responseList = (IEnumerable<TEntity>)_serializor.Deserialize<IEnumerable<TEntity>>(content);

            return responseList;
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            IList<TEntity> responseList;
            string content = default(string);

            var file = $"storage_{typeof(TEntity).Name}.json";
            var filePath = Path.Combine(_storageDirectory, file);
            if (FileOps.IsExistingFile(filePath))
            {
                content = FileOps.ReadText(filePath);
                responseList = (IList<TEntity>)_serializor.Deserialize<IEnumerable<TEntity>>(content);
            }
            else responseList = new List<TEntity>();
            responseList.Add(entity);
            content = _serializor.Serialize(responseList).ToString();
            FileOps.WriteFile(filePath, content, new FileWriteOptions { OverwriteFileIfExists = true, CreateDirectoryIfNotExists = true });

            return entity;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            var file = $"storage_{typeof(TEntity).Name}.json";
            var filePath = Path.Combine(_storageDirectory, file);

            if (!FileOps.IsExistingFile(filePath))
                throw new Exception("Update operation failed! Stroage file not found.");

            var content = FileOps.ReadText(filePath);
            var dataList = (IList<TEntity>)_serializor.Deserialize<IEnumerable<TEntity>>(content);

            var responseItem = dataList.SingleOrDefault(x => x.Id == entity.Id);
            if (responseItem == null)
                throw new Exception($"Update operation failed! Item not found Type: {entity.GetType().Name} Id:{entity.Id}");

            responseItem = entity;

            content = _serializor.Serialize(dataList).ToString();
            FileOps.WriteFile(filePath, content, new FileWriteOptions { OverwriteFileIfExists = true, CreateDirectoryIfNotExists = true });
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            var file = $"storage_{typeof(TEntity).Name}.json";
            var filePath = Path.Combine(_storageDirectory, file);

            if (!FileOps.IsExistingFile(filePath))
                throw new Exception("Delete operation failed! Stroage file not found.");

            var content = FileOps.ReadText(filePath);
            var dataList = (IList<TEntity>)_serializor.Deserialize<IEnumerable<TEntity>>(content);

            var responseItem = dataList.SingleOrDefault(x => x.Id == entity.Id);
            if (responseItem == null)
                throw new Exception($"Delete operation failed! Item not found Type: {entity.GetType().Name} Id:{entity.Id}");

            dataList.Remove(responseItem);

            content = _serializor.Serialize(dataList).ToString();
            FileOps.WriteFile(filePath, content, new FileWriteOptions { OverwriteFileIfExists = true, CreateDirectoryIfNotExists = true });
            
        }
    }
}
