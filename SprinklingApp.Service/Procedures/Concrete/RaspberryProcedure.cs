using System.Collections.Generic;
using System.Linq;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;
using SprinklingApp.Service.Procedures.Abstract;

namespace SprinklingApp.Service.Procedures.Concrete
{
    public class RaspberryProcedure : IRaspberryProcedure
    {
        private readonly IRaspberryService _raspberryService;

        public RaspberryProcedure(IRaspberryService raspberryService)
        {
            _raspberryService = raspberryService;

        }
        
        public RaspberryResponseModel Get(long id)
        {
            var dtoItem = _raspberryService.Get(id);
            var item = ModelBinder.Instance.ConvertToRaspberryResponseModel(dtoItem);
            return item;
        }

        public IEnumerable<RaspberryResponseModel> GetList()
        {
            var dtoItems = _raspberryService.GetList();
            var itemList = dtoItems?.Select(x => ModelBinder.Instance.ConvertToRaspberryResponseModel(x));
            return itemList;
        }

        public RaspberryResponseModel Insert(InsertRaspberryRequestModel requestModel)
        {
            var dtoItem = ModelBinder.Instance.ConvertToRaspberryDTO(requestModel);
            dtoItem = _raspberryService.Insert(dtoItem);
            var resultModel = ModelBinder.Instance.ConvertToRaspberryResponseModel(dtoItem);
            return resultModel;
        }

        public RaspberryResponseModel Update(UpdateRaspberryRequestModel requestModel)
        {
            var dtoItem = ModelBinder.Instance.ConvertToRaspberryDTO(requestModel);
            dtoItem = _raspberryService.Update(dtoItem);
            var resultModel = ModelBinder.Instance.ConvertToRaspberryResponseModel(dtoItem);
            return resultModel;
        }

        public void Delete(long id)
        {
            _raspberryService.Delete(id);
        }

    }
}
