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
    public class ProfileModelService : BaseModelService, IProfileService
    {
        private readonly DataAccessor _accessor;
        public ProfileModelService(IRepository repo)
        {
            _accessor = new DataAccessor(repo);
        }

        private IEnumerable<Group> GetProfileGroups(long profileId)
        {
            var groupIds = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.ProfileId == profileId)?.Select(x => x.GroupId);
            var groups = _accessor.GetList<Group>(x => x.IsActive && groupIds.Contains(x.Id));
            return groups;
        }

        public ProfileDTO Get(long id)
        {
            var profileItem = _accessor.Get<Profile>(x => x.IsActive && x.Id == id);
            var groups = GetProfileGroups(id);
            var item = ModelBinder.Instance.ConvertToProfileDTO(profileItem, groups);
            return item;
        }

        private IEnumerable<Group> GetProfileGroup(long profileId, IEnumerable<ProfileGroupMapping> groupMappingList, IEnumerable<Group> groupList)
        {
            var groupIds = groupMappingList.Where(x => x.IsActive && x.ProfileId == profileId).Select(x => x.GroupId);
            var groups = groupList.Where(x => x.IsActive && groupIds.Contains(x.Id));
            return groups;
        }

        public IEnumerable<ProfileDTO> GetList()
        {
            var profileItems = _accessor.GetList<Profile>(x => x.IsActive);
            var profileGroupMappings = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive);
            var groups = _accessor.GetList<Group>(x => x.IsActive);

            var resultList = new List<ProfileDTO>();

            foreach (var profile in profileItems)
            {
                var groupItems = GetProfileGroup(profile.Id, profileGroupMappings, groups);
                var dtoItem = ModelBinder.Instance.ConvertToProfileDTO(profile, groupItems);
                resultList.Add(dtoItem);
            }

            return resultList;
        }

        public ProfileDTO Insert(ProfileDTO dtoItem)
        {
            if (dtoItem.Groups.Any(x => x.Id == default(long)))
                throw new Exception("Insert failed! Profile groups are not found in storage");


            var entity = ModelBinder.Instance.ConvertToProfile(dtoItem);
            entity = _accessor.Insert(entity);

            foreach (var group in dtoItem.Groups)
            {
                var newMappingItem = new ProfileGroupMapping();
                newMappingItem.IsActive = true;
                newMappingItem.ProfileId = entity.Id;
                newMappingItem.GroupId = group.Id;

                _accessor.Insert(newMappingItem);
            }

            dtoItem.Id = entity.Id;

            return dtoItem;
        }

        public ProfileDTO Update(ProfileDTO dtoItem)
        {
            if (dtoItem.Groups.Any(x => x.Id == default(long)))
                throw new Exception("Update failed! Group valves are not found in storage");

            var entity = ModelBinder.Instance.ConvertToProfile(dtoItem);
            _accessor.Update(entity);

            var commingIds = dtoItem.Groups.Select(x => x.Id);
            var savedMappings = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.ProfileId == entity.Id);

            // delete exist items
            var deletedGroupMappings = savedMappings.Where(x=>!commingIds.Contains(x.GroupId));
            foreach (var mappingItem in deletedGroupMappings)
            {
                _accessor.Delete(mappingItem);
            }

            // insert new items
            var savedGroupIds = savedMappings.Select(x => x.GroupId);
            var newestGroupIds = commingIds.Where(x => !savedGroupIds.Contains(x));
            foreach (var group in newestGroupIds)
            {
                var newMappingItem = new ProfileGroupMapping();
                newMappingItem.IsActive = true;
                newMappingItem.ProfileId = entity.Id;
                newMappingItem.GroupId = group;

                _accessor.Insert(newMappingItem);
            }

            return dtoItem;
        }

        public void Delete(long id)
        {
            var entity = _accessor.Get<Profile>(x => x.Id == id);
            var mappings = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.ProfileId == id);
            _accessor.Delete(entity);
            foreach (var item in mappings)
            {
                _accessor.Delete(item);
            }
        }
    }
}
