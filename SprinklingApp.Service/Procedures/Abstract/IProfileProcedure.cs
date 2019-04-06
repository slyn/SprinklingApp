using System.Collections.Generic;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;

namespace SprinklingApp.Service.Procedures.Abstract {

    public interface IProfileProcedure : IProcedure {
        ProfileResponseModel Get(long id);
        IEnumerable<ProfileResponseModel> GetList();
        ProfileResponseModel Insert(InsertProfileRequestModel requestModel);
        ProfileResponseModel Update(UpdateProfileRequestModel requestModel);
        void Delete(long id);
    }

}