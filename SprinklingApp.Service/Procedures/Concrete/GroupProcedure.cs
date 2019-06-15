using System.Collections.Generic;
using System.Linq;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;
using SprinklingApp.Service.Procedures.Abstract;

namespace SprinklingApp.Service.Procedures.Concrete {

    public class GroupProcedure : IGroupProcedure {
        private readonly IGroupService groupService;
        private readonly IValveGroupMappingService valveGroupMappingService;
        private readonly IValveService valveService;

        public GroupProcedure(IGroupService groupService, IValveService valveService, IValveGroupMappingService valveGroupMappingService) {
            this.groupService = groupService;
            this.valveService = valveService;
            this.valveGroupMappingService = valveGroupMappingService;
        }

        public GroupResponseModel Get(long id) {
            Group group = groupService.Get(id);
            IEnumerable<Valve> valves = new List<Valve>();

            if (group != null) {
                List<long> valveIds = valveGroupMappingService.GetListByGroup(group.Id).Select(x => x.ValveId).ToList();
                valves = valveIds.Count() != 0 ? valveService.GetListByIds(valveIds) : valves;
            }

            GroupResponseModel responseModel = ModelBinder.Instance.ConvertToGroupResponseModel(group, valves);

            return responseModel;
        }

        public IEnumerable<GroupResponseModel> GetList() {
            IEnumerable<Group> groupItems = groupService.GetList();
            IEnumerable<ValveGroupMapping> totalGroupValveMappingList = valveGroupMappingService.GetList();
            IEnumerable<Valve> totalValveList = valveService.GetList();

            List<GroupResponseModel> resultList = new List<GroupResponseModel>();
            foreach (Group group in groupItems) {
                List<long> valveIds = totalGroupValveMappingList.Where(x => x.GroupId == group.Id).Select(x => x.ValveId).ToList();
                IEnumerable<Valve> valve = totalValveList.Where(x => valveIds.Contains(x.Id));
                GroupResponseModel tempResponseItem = ModelBinder.Instance.ConvertToGroupResponseModel(group, valve);

                resultList.Add(tempResponseItem);
            }

            return resultList;
        }

        public GroupResponseModel Insert(InsertGroupRequestModel requestModel) {
            Group groupItem = ModelBinder.Instance.ConvertToGroup(requestModel);
            groupItem = groupService.Insert(groupItem);

            IEnumerable<Valve> valves = valveService.GetListByIds(requestModel.ValveIdList.ToList());

            foreach (Valve valve in valves) {
                valveGroupMappingService.Insert(
                    new ValveGroupMapping {
                        IsActive = true,
                        GroupId = groupItem.Id,
                        ValveId = valve.Id
                    });
            }

            GroupResponseModel resultModel = ModelBinder.Instance.ConvertToGroupResponseModel(groupItem, valves);
            return resultModel;
        }

        public GroupResponseModel Update(UpdateGroupRequestModel requestModel) {
            Group group = ModelBinder.Instance.ConvertToGroup(requestModel);
            group = groupService.Update(group);

            var existedMappings = valveGroupMappingService.GetListByGroup(group.Id).ToList();
            // delete mapping
            foreach (ValveGroupMapping item in existedMappings) {
                if (!requestModel.ValveIdList.Contains(item.ValveId)) {
                    valveGroupMappingService.Delete(item.Id);
                }
            }

            // insert mapping
            var latesValves = valveService.GetListByIds(requestModel.ValveIdList.ToList()).ToList();
            var mappedValveIds = existedMappings.Select(x => x.ValveId).ToList();
            foreach (Valve item in latesValves) {
                if (!mappedValveIds.Contains(item.Id)) {
                    valveGroupMappingService.Insert(
                        new ValveGroupMapping {
                            IsActive = true,
                            GroupId = group.Id,
                            ValveId = item.Id
                        });
                }
            }
            
            GroupResponseModel resultModel = ModelBinder.Instance.ConvertToGroupResponseModel(group, latesValves);
            return resultModel;
        }

        public void Delete(long id) {
            //// delete mapping
            //var mappings = _valveGroupMappingService.GetListByGroup(id);
            //foreach (var item in mappings)
            //{
            //    _valveGroupMappingService.Delete(item.Id);
            //}
            // delete group
            groupService.Delete(id);
        }
    }

}