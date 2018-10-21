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
            var valveItem = _valveService.Get(id);
            var item = ModelBinder.Instance.ConvertToValveResponseModel(valveItem);
            return item;
        }

        public IEnumerable<ValveResponseModel> GetList()
        {
            var valveItems = _valveService.GetList();
            var itemList = valveItems?.Select(x => ModelBinder.Instance.ConvertToValveResponseModel(x));
            return itemList;
        }

        public ValveResponseModel Insert(InsertValveRequestModel requestModel)
        {
            var valveItem = ModelBinder.Instance.ConvertToValve(requestModel);
            valveItem = _valveService.Insert(valveItem);
            var resultModel = ModelBinder.Instance.ConvertToValveResponseModel(valveItem);
            return resultModel;
        }

        public ValveResponseModel Update(UpdateValveRequestModel requestModel)
        {
            var valveItem = ModelBinder.Instance.ConvertToValve(requestModel);
            valveItem = _valveService.Update(valveItem);
            var resultModel = ModelBinder.Instance.ConvertToValveResponseModel(valveItem);
            return resultModel;
        }
        public void Delete(long id)
        {
            _valveService.Delete(id);
        }

    }
}
