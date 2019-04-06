using System.Collections.Generic;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;

namespace SprinklingApp.Service.Procedures.Abstract {

    public interface IGroupProcedure : IProcedure {
        GroupResponseModel Get(long id);
        IEnumerable<GroupResponseModel> GetList();
        GroupResponseModel Insert(InsertGroupRequestModel requestModel);
        GroupResponseModel Update(UpdateGroupRequestModel requestModel);
        void Delete(long id);
    }

}