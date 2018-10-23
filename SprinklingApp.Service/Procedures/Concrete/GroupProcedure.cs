using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Entities.Concrete;
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
        private readonly IValveGroupMappingService _valveGroupMappingService;
        private readonly IValveService _valveService;

        public GroupProcedure(IGroupService groupService, IValveService valveService, IValveGroupMappingService valveGroupMappingService)
        {
            _groupService = groupService;
            _valveService = valveService;
            _valveGroupMappingService = valveGroupMappingService;
        }

        public GroupResponseModel Get(long id)
        {
            var group = _groupService.Get(id);
            IEnumerable<Valve> valves = new List<Valve>();

            if (group!= null)
            {
                var valveIds = _valveGroupMappingService.GetListByGroup(group.Id).Select(x => x.ValveId).ToList();
                valves = valveIds.Count() != 0 ? _valveService.GetListByIds(valveIds):valves;
            }

            var responseModel = ModelBinder.Instance.ConvertToGroupResponseModel(group, valves);

            return responseModel;
        }

        public IEnumerable<GroupResponseModel> GetList()
        {
            var groupItems = _groupService.GetList();
            var totalGroupValveMappingList = _valveGroupMappingService.GetList();
            var totalValveList = _valveService.GetList();

            var resultList = new List<GroupResponseModel>();
            foreach (var group in groupItems)
            {
                var valveIds = totalGroupValveMappingList.Where(x => x.GroupId == group.Id).Select(x=>x.ValveId).ToList();
                var valve = totalValveList.Where(x => valveIds.Contains(x.Id));
                var tempResponseItem = ModelBinder.Instance.ConvertToGroupResponseModel(group, valve);

                resultList.Add(tempResponseItem);
            }
            return resultList;
        }

        public GroupResponseModel Insert(InsertGroupRequestModel requestModel)
        {
            var groupItem = ModelBinder.Instance.ConvertToGroup(requestModel);
            groupItem = _groupService.Insert(groupItem);

            var valves = _valveService.GetListByIds(requestModel.ValveIdList.ToList());

            foreach (var valve in valves)
            {
                _valveGroupMappingService.Insert(new ValveGroupMapping {
                    IsActive = true,
                    GroupId = groupItem.Id,
                    ValveId = valve.Id
                });
            }
            var resultModel = ModelBinder.Instance.ConvertToGroupResponseModel(groupItem,valves);
            return resultModel;
        }

        public GroupResponseModel Update(UpdateGroupRequestModel requestModel)
        {
            var group = ModelBinder.Instance.ConvertToGroup(requestModel);
            group = _groupService.Update(group);

            var existedMappings = _valveGroupMappingService.GetListByGroup(group.Id);
            // delete mapping
            foreach (var item in existedMappings)
            {
                if (!requestModel.ValveIdList.Contains(item.ValveId))
                {
                    _valveGroupMappingService.Delete(item.Id);
                }
            }

            // insert mapping
            var latesValves = _valveService.GetListByIds(requestModel.ValveIdList.ToList());
            var mappedValveIds = existedMappings.Select(x => x.ValveId);
            foreach (var item in latesValves)
            {
                if (!mappedValveIds.Contains(item.Id))
                {
                    _valveGroupMappingService.Insert(new ValveGroupMapping
                    {
                        IsActive = true,
                        GroupId = group.Id,
                        ValveId = item.Id,
                    });
                }
                
            }

            var resultModel = ModelBinder.Instance.ConvertToGroupResponseModel(group,latesValves);
            return resultModel;
        }

        public void Delete(long id)
        {
            //// delete mapping
            //var mappings = _valveGroupMappingService.GetListByGroup(id);
            //foreach (var item in mappings)
            //{
            //    _valveGroupMappingService.Delete(item.Id);
            //}
            // delete group
            _groupService.Delete(id);
        }
    }
}
