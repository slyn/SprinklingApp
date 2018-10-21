using System.Collections.Generic;
using System.Linq;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;
using SprinklingApp.Service.Procedures.Abstract;

namespace SprinklingApp.Service.Procedures.Concrete
{
    public class RaspberryProcedure : IRaspberryProcedure
    {
        private readonly IRaspberryService _raspberryService;
        private readonly IValveService _valveService;

        public RaspberryProcedure(IRaspberryService raspberryService, IValveService valveService)
        {
            _raspberryService = raspberryService;
            _valveService = valveService;
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
            var valves = GetContainedValveItems(requestModel?.ValveIdList);
            var raspberryItem = ModelBinder.Instance.ConvertToRaspberry(requestModel,valves);
            raspberryItem = _raspberryService.Insert(raspberryItem);
            var resultModel = ModelBinder.Instance.ConvertToRaspberryResponseModel(raspberryItem);
            return resultModel;
        }

        public RaspberryResponseModel Update(UpdateRaspberryRequestModel requestModel)
        {
            var valves = GetContainedValveItems(requestModel?.ValveIdList);
            var raspberryItem = ModelBinder.Instance.ConvertToRaspberry(requestModel,valves);
            raspberryItem = _raspberryService.Update(raspberryItem);
            var resultModel = ModelBinder.Instance.ConvertToRaspberryResponseModel(raspberryItem);
            return resultModel;
        }

        public void Delete(long id)
        {
            // TODO :: delete valve items
            _raspberryService.Delete(id);
        }

        private IEnumerable<Valve> GetContainedValveItems(IEnumerable<long> ids)
        {
            IEnumerable<Valve> valves;
            if (ids != null && ids.Count() > 0)
                valves = _valveService.GetListByIds(ids.ToList());
            else valves = new List<Valve>();

            return valves;
        }

    }
}
