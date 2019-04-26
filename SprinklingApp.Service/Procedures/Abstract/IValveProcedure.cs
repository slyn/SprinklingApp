using System.Collections.Generic;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;

namespace SprinklingApp.Service.Procedures.Abstract {

    public interface IValveProcedure : IProcedure {
        ValveResponseModel Get(long id);
        IEnumerable<ValveResponseModel> GetList();
        ValveResponseModel Insert(InsertValveRequestModel requestModel);
        ValveResponseModel Update(UpdateValveRequestModel requestModel);
        void Delete(long id);
    }

}