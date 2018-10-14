using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Service.Procedures.Abstract
{
    public interface IRaspberryProcedure:IProcedure
    {
        RaspberryResponseModel Get(long id);
        IEnumerable<RaspberryResponseModel> GetList();
        RaspberryResponseModel Insert(InsertRaspberryRequestModel requestModel);
        RaspberryResponseModel Update(UpdateRaspberryRequestModel requestModel);
        void Delete(long id);
    }
}
