﻿using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;
using SprinklingApp.Service.Procedures.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace SprinklingApp.Service.Procedures.Concrete
{
    public class GroupProcedure : IGroupProcedure
    {
        private readonly IGroupService _groupService;
        private readonly IValveService _valveService;

        public GroupProcedure(IGroupService groupService, IValveService valveService)
        {
            _groupService = groupService;
            _valveService = valveService;
        }

        public GroupResponseModel Get(long id)
        {
            var dtoItem = _groupService.Get(id);
            var item = ModelBinder.Instance.ConvertToGroupResponseModel(dtoItem);
            return item;
        }

        public IEnumerable<GroupResponseModel> GetList()
        {
            var dtoItem = _groupService.GetList();
            var itemList = dtoItem?.Select(x => ModelBinder.Instance.ConvertToGroupResponseModel(x));
            return itemList;
        }

        public GroupResponseModel Insert(InsertGroupRequestModel requestModel)
        {
            var valves = _valveService.GetListByIds(requestModel.ValveIdList.ToList());
            var dtoItem = ModelBinder.Instance.ConvertToGroupDTO(requestModel,valves);
            dtoItem = _groupService.Insert(dtoItem);
            var resultModel = ModelBinder.Instance.ConvertToGroupResponseModel(dtoItem);
            return resultModel;
        }

        public GroupResponseModel Update(UpdateGroupRequestModel requestModel)
        {
            var valves = _valveService.GetListByIds(requestModel.ValveIdList.ToList());
            var dtoItem = ModelBinder.Instance.ConvertToGroupDTO(requestModel,valves);
            dtoItem = _groupService.Update(dtoItem);
            var resultModel = ModelBinder.Instance.ConvertToGroupResponseModel(dtoItem);
            return resultModel;
        }

        public void Delete(long id)
        {
            _groupService.Delete(id);
        }
    }
}
