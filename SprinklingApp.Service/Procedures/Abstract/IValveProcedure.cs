using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Service.Procedures.Abstract
{
    public interface IValveProcedure:IProcedure
    {
        ValveResponseModel Get(long id);
        IEnumerable<ValveResponseModel> GetList();
        ValveResponseModel Insert(InsertValveRequestModel requestModel);
        ValveResponseModel Update(UpdateValveRequestModel requestModel);
        void Delete(long id);
    }
}
