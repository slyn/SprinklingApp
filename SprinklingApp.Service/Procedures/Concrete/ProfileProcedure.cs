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
    public class ProfileProcedure : IProfileProcedure
    {
        private readonly IProfileService _profileService;
        private readonly IGroupService _groupService;
        private readonly IProfileGroupMappingService _profileGroupMappingService;

        public ProfileProcedure(IProfileService profileService, IGroupService groupService, IProfileGroupMappingService profileGroupMappingService)
        {
            _profileService = profileService;
            _groupService = groupService;
            _profileGroupMappingService = profileGroupMappingService;
        }

        public ProfileResponseModel Get(long id)
        {
            var profile = _profileService.Get(id);
            var groupIds = _profileGroupMappingService.GetListByProfile(id).Select(x => x.GroupId).ToList();
            var groups = groupIds.Count() != 0 ? _groupService.GetListByIds(groupIds) : new List<Group>();
            var item = ModelBinder.Instance.ConvertToProfileResponseModel(profile, groups);
            return item;
        }

        public IEnumerable<ProfileResponseModel> GetList()
        {
            var profileItems = _profileService.GetList();
            var totalProfileGroupMappingList = _profileGroupMappingService.GetList();
            var totalGroupList = _groupService.GetList();

            var resultList = new List<ProfileResponseModel>();
            foreach (var profile in profileItems)
            {
                var groupIds = totalProfileGroupMappingList.Where(x => x.ProfileId == profile.Id).Select(x => x.GroupId).ToList();
                var groups = totalGroupList.Where(x => groupIds.Contains(x.Id));
                var tempResponseItem = ModelBinder.Instance.ConvertToProfileResponseModel(profile, groups);

                resultList.Add(tempResponseItem);
            }
            return resultList;
        }

        public ProfileResponseModel Insert(InsertProfileRequestModel requestModel)
        {
            var profileItem = ModelBinder.Instance.ConvertToProfile(requestModel);
            profileItem = _profileService.Insert(profileItem);

            var groups = _groupService.GetListByIds(requestModel.GroupIdList.ToList());

            foreach (var group in groups)
            {
                _profileGroupMappingService.Insert(new ProfileGroupMapping
                {
                    IsActive = true,
                    ProfileId = profileItem.Id,
                    GroupId = group.Id
                });
            }
            var resultModel = ModelBinder.Instance.ConvertToProfileResponseModel(profileItem, groups);
            return resultModel;
        }

        public ProfileResponseModel Update(UpdateProfileRequestModel requestModel)
        {
            var profileItem = ModelBinder.Instance.ConvertToProfile(requestModel);
            profileItem = _profileService.Update(profileItem);

            var existedMappings = _profileGroupMappingService.GetListByGroup(profileItem.Id);
            // delete mapping
            foreach (var item in existedMappings)
            {
                if (!requestModel.GroupIdList.Contains(item.GroupId))
                {
                    _profileGroupMappingService.Delete(item.Id);
                }
            }

            // insert mapping
            var latesGroups = _groupService.GetListByIds(requestModel.GroupIdList.ToList());
            var mappedGroupIds = existedMappings.Select(x => x.ProfileId);
            foreach (var item in latesGroups)
            {
                if (!mappedGroupIds.Contains(item.Id))
                {
                    _profileGroupMappingService.Insert(new ProfileGroupMapping
                    {
                        IsActive = true,
                        ProfileId = profileItem.Id,
                        GroupId = item.Id,
                    });
                }

            }

            var resultModel = ModelBinder.Instance.ConvertToProfileResponseModel(profileItem, latesGroups);
            return resultModel;
        }

        public void Delete(long id)
        {
            _profileService.Delete(id);
        }
    }
}
