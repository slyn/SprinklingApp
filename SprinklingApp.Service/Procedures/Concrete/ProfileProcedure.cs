using System.Collections.Generic;
using System.Linq;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;
using SprinklingApp.Service.Procedures.Abstract;

namespace SprinklingApp.Service.Procedures.Concrete {

    public class ProfileProcedure : IProfileProcedure {
        private readonly IGroupService _groupService;
        private readonly IProfileGroupMappingService _profileGroupMappingService;
        private readonly IProfileService _profileService;

        public ProfileProcedure(IProfileService profileService, IGroupService groupService, IProfileGroupMappingService profileGroupMappingService) {
            _profileService = profileService;
            _groupService = groupService;
            _profileGroupMappingService = profileGroupMappingService;
        }

        public ProfileResponseModel Get(long id) {
            Profile profile = _profileService.Get(id);
            List<long> groupIds = _profileGroupMappingService.GetListByProfile(id).Select(x => x.GroupId).ToList();
            IEnumerable<Group> groups = groupIds.Count() != 0 ? _groupService.GetListByIds(groupIds) : new List<Group>();
            ProfileResponseModel item = ModelBinder.Instance.ConvertToProfileResponseModel(profile, groups);
            return item;
        }

        public IEnumerable<ProfileResponseModel> GetList() {
            IEnumerable<Profile> profileItems = _profileService.GetList();
            IEnumerable<ProfileGroupMapping> totalProfileGroupMappingList = _profileGroupMappingService.GetList();
            IEnumerable<Group> totalGroupList = _groupService.GetList();

            List<ProfileResponseModel> resultList = new List<ProfileResponseModel>();
            foreach (Profile profile in profileItems) {
                List<long> groupIds = totalProfileGroupMappingList.Where(x => x.ProfileId == profile.Id).Select(x => x.GroupId).ToList();
                IEnumerable<Group> groups = totalGroupList.Where(x => groupIds.Contains(x.Id));
                ProfileResponseModel tempResponseItem = ModelBinder.Instance.ConvertToProfileResponseModel(profile, groups);

                resultList.Add(tempResponseItem);
            }

            return resultList;
        }

        public ProfileResponseModel Insert(InsertProfileRequestModel requestModel) {
            Profile profileItem = ModelBinder.Instance.ConvertToProfile(requestModel);
            profileItem = _profileService.Insert(profileItem);

            IEnumerable<Group> groups = _groupService.GetListByIds(requestModel.GroupIdList.ToList());

            foreach (Group group in groups) {
                _profileGroupMappingService.Insert(
                    new ProfileGroupMapping {
                        IsActive = true,
                        ProfileId = profileItem.Id,
                        GroupId = group.Id
                    });
            }

            ProfileResponseModel resultModel = ModelBinder.Instance.ConvertToProfileResponseModel(profileItem, groups);
            return resultModel;
        }

        public ProfileResponseModel Update(UpdateProfileRequestModel requestModel) {
            Profile profileItem = ModelBinder.Instance.ConvertToProfile(requestModel);
            profileItem = _profileService.Update(profileItem);

            IEnumerable<ProfileGroupMapping> existedMappings = _profileGroupMappingService.GetListByGroup(profileItem.Id);
            // delete mapping
            foreach (ProfileGroupMapping item in existedMappings) {
                if (!requestModel.GroupIdList.Contains(item.GroupId)) {
                    _profileGroupMappingService.Delete(item.Id);
                }
            }

            // insert mapping
            IEnumerable<Group> latesGroups = _groupService.GetListByIds(requestModel.GroupIdList.ToList());
            IEnumerable<long> mappedGroupIds = existedMappings.Select(x => x.ProfileId);
            foreach (Group item in latesGroups) {
                if (!mappedGroupIds.Contains(item.Id)) {
                    _profileGroupMappingService.Insert(
                        new ProfileGroupMapping {
                            IsActive = true,
                            ProfileId = profileItem.Id,
                            GroupId = item.Id
                        });
                }
            }

            ProfileResponseModel resultModel = ModelBinder.Instance.ConvertToProfileResponseModel(profileItem, latesGroups);
            return resultModel;
        }

        public void Delete(long id) {
            _profileService.Delete(id);
        }
    }

}