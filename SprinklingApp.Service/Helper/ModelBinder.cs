using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.DTOs.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SprinklingApp.Service.Helper
{
    public class ModelBinder
    {
        #region [ CTOR ]
        private ModelBinder()
        {
        }
        #endregion

        #region [ Lazy-Singleton ]
        private static readonly Lazy<ModelBinder> _instance = new Lazy<ModelBinder>(() => new ModelBinder(), LazyThreadSafetyMode.ExecutionAndPublication);
        internal static ModelBinder Instance => _instance.Value;
        #endregion

        public Group ConvertToGroup(GroupDTO dtoItem)
        {
            var result = new Group()
            {
                Id = dtoItem.Id,
                IsActive = dtoItem.IsActive,
                Duration = dtoItem.Duration,
                Name = dtoItem.Name,
                Unit = dtoItem.Unit,
                ValveList = dtoItem.ValveList
            };

            return result;
        }
        public GroupDTO ConvertToGroupDTO(Group entity)
        {
            var result = new GroupDTO()
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                Duration = entity.Duration,
                Name = entity.Name,
                Unit = entity.Unit,
                ValveList = entity.ValveList
            };

            return result;

        }
        public GroupDTO ConvertToGroupDTO(InsertGroupRequestModel requestModel)
        {
            var result = new GroupDTO()
            {
                IsActive = true,
                Duration = requestModel.Duration,
                Name = requestModel.Name,
                Unit = requestModel.Unit,
                //ValveList = requestModel.ValveList // TODO :: Get Valve List
            };

            return result;

        }
        public GroupDTO ConvertToGroupDTO(UpdateGroupRequestModel requestModel)
        {
            var result = new GroupDTO()
            {
                Id = requestModel.GroupId,
                IsActive = true,
                Duration = requestModel.Duration,
                Name = requestModel.Name,
                Unit = requestModel.Unit,
                //ValveList = requestModel.ValveList // TODO :: Get Valve List
            };

            return result;

        }
        public GroupResponseModel ConvertToGroupResponseModel(GroupDTO dtoItem)
        {
            var result = new GroupResponseModel()
            {
                Id = dtoItem.Id,
                IsActive = dtoItem.IsActive,
                Duration = dtoItem.Duration,
                Name = dtoItem.Name,
                Unit = dtoItem.Unit,
                ValveList = dtoItem.ValveList
            };

            return result;

        }
    }
}
