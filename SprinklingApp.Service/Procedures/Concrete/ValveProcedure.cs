using System.Collections.Generic;
using System.Linq;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;
using SprinklingApp.Service.Procedures.Abstract;

namespace SprinklingApp.Service.Procedures.Concrete
{
    public class ValveProcedure : IValveProcedure
    {
        private readonly IValveService _valveService;

        public ValveProcedure(IValveService valveService)
        {
            _valveService = valveService;

        }

        public ValveResponseModel Get(long id)
        {
            var dtoItem = _valveService.Get(id);
            var item = ModelBinder.Instance.ConvertToValveResponseModel(dtoItem);
            return item;
        }

        public IEnumerable<ValveResponseModel> GetList()
        {
            var dtoItems = _valveService.GetList();
            var itemList = dtoItems?.Select(x => ModelBinder.Instance.ConvertToValveResponseModel(x));
            return itemList;
        }

        public ValveResponseModel Insert(InsertValveRequestModel requestModel)
        {
            var dtoItem = ModelBinder.Instance.ConvertToValveDTO(requestModel);
            dtoItem = _valveService.Insert(dtoItem);
            var resultModel = ModelBinder.Instance.ConvertToValveResponseModel(dtoItem);
            return resultModel;
        }

        public ValveResponseModel Update(UpdateValveRequestModel requestModel)
        {
            var dtoItem = ModelBinder.Instance.ConvertToValveDTO(requestModel);
            dtoItem = _valveService.Update(dtoItem);
            var resultModel = ModelBinder.Instance.ConvertToValveResponseModel(dtoItem);
            return resultModel;
        }
        public void Delete(long id)
        {
            _valveService.Delete(id);
        }

    }
}
