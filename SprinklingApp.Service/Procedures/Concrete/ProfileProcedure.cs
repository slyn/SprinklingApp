using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
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

        public ProfileProcedure(IProfileService profileService,IGroupService groupService)
        {
            _profileService = profileService;
            _groupService = groupService;
        }
        
        public ProfileResponseModel Get(long id)
        {
            var dtoItem = _profileService.Get(id);
            var item = ModelBinder.Instance.ConvertToProfileResponseModel(dtoItem);
            return item;
        }

        public IEnumerable<ProfileResponseModel> GetList()
        {
            var dtoItem = _profileService.GetList();
            var itemList = dtoItem.Select(x => ModelBinder.Instance.ConvertToProfileResponseModel(x));
            return itemList;
        }

        public ProfileResponseModel Insert(InsertProfileRequestModel requestModel)
        {
            var groups = _groupService.GetListByIds(requestModel.GroupIdList.ToList());
            var dtoItem = ModelBinder.Instance.ConvertToProfileDTO(requestModel, groups);
            dtoItem = _profileService.Insert(dtoItem);
            var resultModel = ModelBinder.Instance.ConvertToProfileResponseModel(dtoItem);
            return resultModel;
        }

        public ProfileResponseModel Update(UpdateProfileRequestModel requestModel)
        {
            var groups = _groupService.GetListByIds(requestModel.GroupIdList.ToList());
            var dtoItem = ModelBinder.Instance.ConvertToProfileDTO(requestModel, groups);
            dtoItem = _profileService.Update(dtoItem);
            var resultModel = ModelBinder.Instance.ConvertToProfileResponseModel(dtoItem);
            return resultModel;
        }

        public void Delete(long id)
        {
            _profileService.Delete(id);
        }
    }
}
