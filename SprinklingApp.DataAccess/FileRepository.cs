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
            if (!FileOps.IsExistingFile(filePath))
                return null;
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
                return null;

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
            bool fileExist = FileOps.IsExistingFile(filePath);
            if (fileExist)
            {
                content = FileOps.ReadText(filePath);
                responseList = (IList<TEntity>)_serializor.Deserialize<IEnumerable<TEntity>>(content);
            }
            else responseList = new List<TEntity>();
            SetId(entity);
            responseList.Add(entity);
            content = _serializor.Serialize(responseList).ToString();
            if (fileExist)
                FileOps.DeleteFile(filePath);
            FileOps.WriteFile(filePath, content, new FileWriteOptions { OverwriteFileIfExists = true, CreateDirectoryIfNotExists = true });

            return entity;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            var file = $"storage_{typeof(TEntity).Name}.json";
            var filePath = Path.Combine(_storageDirectory, file);
            var fileExist = FileOps.IsExistingFile(filePath);
            if (!fileExist)
                throw new Exception("Update operation failed! Stroage file not found.");

            var content = FileOps.ReadText(filePath);
            var dataList = (IList<TEntity>)_serializor.Deserialize<IEnumerable<TEntity>>(content);

            var responseItem = dataList.SingleOrDefault(x => x.Id == entity.Id);
            if (responseItem == null)
                throw new Exception($"Update operation failed! Item not found Type: {entity.GetType().Name} Id:{entity.Id}");

            var index = dataList.IndexOf(responseItem);
            dataList[index] = entity;

            //responseItem = entity;

            content = _serializor.Serialize(dataList).ToString();
            if (fileExist)
                FileOps.DeleteFile(filePath);
            FileOps.WriteFile(filePath, content, new FileWriteOptions { OverwriteFileIfExists = true, CreateDirectoryIfNotExists = true, });
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            var file = $"storage_{typeof(TEntity).Name}.json";
            var filePath = Path.Combine(_storageDirectory, file);
            var fileExist = FileOps.IsExistingFile(filePath);
            if (!fileExist)
                throw new Exception("Delete operation failed! Stroage file not found.");

            var content = FileOps.ReadText(filePath);
            var dataList = (IList<TEntity>)_serializor.Deserialize<IEnumerable<TEntity>>(content);

            var responseItem = dataList.SingleOrDefault(x => x.Id == entity.Id);
            if (responseItem == null)
                throw new Exception($"Delete operation failed! Item not found Type: {entity.GetType().Name} Id:{entity.Id}");

            dataList.Remove(responseItem);

            content = _serializor.Serialize(dataList).ToString();
            if (fileExist)
                FileOps.DeleteFile(filePath);
            FileOps.WriteFile(filePath, content, new FileWriteOptions { OverwriteFileIfExists = true, CreateDirectoryIfNotExists = true });

        }
        private void SetId<TEntity>(TEntity item) where TEntity : BaseEntity, new()
        {
            if (item.Id != default(long))
                throw new Exception("Error! Id field should be default!");

            item.Id = GetNextIdValue<TEntity>();
        }

        private long GetNextIdValue<TEntity>() where TEntity : BaseEntity, new()
        {
            var list = GetList<TEntity>(x => true);
            var last = list?.LastOrDefault();
            if (last != null)
                return last.Id + 1;
            else return 1;
        }
    }
}
