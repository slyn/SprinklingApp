using System;
using System.Collections.Generic;
using System.Linq;
using SprinklingApp.DataAccess;
using SprinklingApp.Model.DTOs.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;

namespace SprinklingApp.Service.EntityServices.Concrete
{
    public class GroupModelService:BaseModelService, IGroupService
    {
        private readonly DataAccessor _accessor;
        public GroupModelService(IRepository repo)
        {
            _accessor = new DataAccessor(repo);
        }

        private IEnumerable<Valve> GetGroupValves(long groupId)
        {
            var valveIds = _accessor.GetList<ValveGroupMapping>(x => x.IsActive && x.GroupId == groupId)?.Select(x => x.ValveId);
            var valves = _accessor.GetList<Valve>(x => x.IsActive && valveIds.Contains(x.Id));
            return valves;
        }

        private IEnumerable<Valve> GetGroupValves(long groupId,IEnumerable<ValveGroupMapping> groupMappingList, IEnumerable<Valve> valveList)
        {
            var valveIds = groupMappingList.Where(x => x.IsActive && x.GroupId == groupId)?.Select(x => x.ValveId);
            var valves = valveList.Where(x => x.IsActive && valveIds.Contains(x.Id));
            return valves;
        }

        public GroupDTO Get(long id)
        {
            var groupItem = _accessor.Get<Group>(x => x.IsActive && x.Id == id);
            var valves = GetGroupValves(id);
            var item = ModelBinder.Instance.ConvertToGroupDTO(groupItem,valves);
            return item;
        }

        public IEnumerable<GroupDTO> GetList()
        {
            var groupItems = _accessor.GetList<Group>(x => x.IsActive);
            if (groupItems == null)
                return null;
            var valveGroupMappings = _accessor.GetList<ValveGroupMapping>(x => x.IsActive);
            var valves = _accessor.GetList<Valve>(x => x.IsActive);

            var resultList = new List<GroupDTO>();

            foreach (var group in groupItems)
            {
                var valveItems = GetGroupValves(group.Id, valveGroupMappings, valves);
                var dtoItem = ModelBinder.Instance.ConvertToGroupDTO(group, valveItems);
                resultList.Add(dtoItem);
            }
            
            return resultList;
        }

        public GroupDTO Insert(GroupDTO dtoItem)
        {
            if (dtoItem.Valves.Any(x => x.Id == default(long)))
                throw new Exception("Insert failed! Group valves are not found in storage");

            var entity = ModelBinder.Instance.ConvertToGroup(dtoItem);
            entity = _accessor.Insert(entity);

            foreach (var valve in dtoItem.Valves)
            {
                    var newMappingItem = new ValveGroupMapping();
                    newMappingItem.IsActive = true;
                    newMappingItem.ValveId = valve.Id;
                    newMappingItem.GroupId = entity.Id;

                    _accessor.Insert(newMappingItem);
            }

            dtoItem.Id = entity.Id;

            return dtoItem;
        }

        public GroupDTO Update(GroupDTO dtoItem)
        {
            if (dtoItem.Valves.Any(x => x.Id == default(long)))
                throw new Exception("Update failed! Group valves are not found in storage");

            var entity = ModelBinder.Instance.ConvertToGroup(dtoItem);
            _accessor.Update(entity);

            var commingIds = dtoItem.Valves.Select(x => x.Id);
            var savedMappings = _accessor.GetList<ValveGroupMapping>(x=>x.IsActive && x.GroupId == entity.Id);
            
            // delete exist items
            var deletedValveMappings = savedMappings.Where(x=> !commingIds.Contains(x.ValveId));
            foreach (var mappingItem in deletedValveMappings)
            {
                _accessor.Delete(mappingItem);
            }

            // insert new items
            var savedValveIds = savedMappings.Select(x => x.ValveId);
            var newestValveIds = commingIds.Where(x => !savedValveIds.Contains(x));
            foreach (var valve in newestValveIds)
            {
                
                var newMappingItem = new ValveGroupMapping();
                newMappingItem.IsActive = true;
                newMappingItem.ValveId = valve;
                newMappingItem.GroupId = entity.Id;

                _accessor.Insert(newMappingItem);
            }
            
            return dtoItem;
        }

        public void Delete(long id)
        {
            var entity = _accessor.Get<Group>(x=>x.Id == id);

            var valveMappings = _accessor.GetList<ValveGroupMapping>(x => x.IsActive && x.GroupId == id);
            var profileMappings = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.GroupId == id);
            _accessor.Delete(entity);
            foreach (var item in valveMappings)
            {
                _accessor.Delete(item);
            }
            foreach (var item in profileMappings)
            {
                _accessor.Delete(item);
            }
        }
    }
}
