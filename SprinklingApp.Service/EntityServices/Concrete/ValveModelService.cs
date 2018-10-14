using SprinklingApp.DataAccess;
using SprinklingApp.Model.DTOs.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Concrete
{
    public class ValveModelService : BaseModelService, IValveService
    {
        private readonly DataAccessor _accessor;
        public ValveModelService(IRepository repo)
        {
            _accessor = new DataAccessor(repo);
        }

        private Raspberry GetRaspberry(long raspberryId)
        {
            var item = _accessor.Get<Raspberry>(x => x.IsActive && x.Id == raspberryId);
            return item;
        }
        public ValveDTO Get(long id)
        {
            var valveItem = _accessor.Get<Valve>(x => x.IsActive && x.Id == id);
            if (valveItem == null)
                return null;
            var raspberry = GetRaspberry(valveItem.RaspberryId);
            var item = ModelBinder.Instance.ConvertToValveDTO(valveItem, raspberry);
            return item;
        }

        public IEnumerable<ValveDTO> GetList()
        {
            var valveItems = _accessor.GetList<Valve>(x => x.IsActive);
            if (valveItems == null)
                return null;

            var raspberries = _accessor.GetList<Raspberry>(x => x.IsActive);

            var resultList = new List<ValveDTO>();

            foreach (var valve in valveItems)
            {
                var raspberry = GetRaspberry(valve.RaspberryId);
                var dtoItem = ModelBinder.Instance.ConvertToValveDTO(valve, raspberry);
                resultList.Add(dtoItem);
            }

            return resultList;
        }

        public ValveDTO Insert(ValveDTO dtoItem)
        {
            var entity = ModelBinder.Instance.ConvertToValve(dtoItem);
            entity = _accessor.Insert(entity);

            dtoItem.Id = entity.Id;

            return dtoItem;
        }

        public ValveDTO Update(ValveDTO dtoItem)
        {
            var entity = ModelBinder.Instance.ConvertToValve(dtoItem);
            _accessor.Update(entity);

            return dtoItem;
        }

        public void Delete(long id)
        {
            var entity = _accessor.Get<Valve>(x => x.Id == id);
            var valveGroupMappings = _accessor.GetList<ValveGroupMapping>(x => x.IsActive && x.ValveId == id);
            _accessor.Delete(entity);
            if (valveGroupMappings != null)
            {
                foreach (var item in valveGroupMappings)
                {
                    _accessor.Delete(item);
                }
            }
        }

        public IEnumerable<ValveDTO> GetListByIds(IList<long> ids)
        {
            var raspberries = _accessor.GetList<Raspberry>(x => x.IsActive);
            var valveItems = _accessor.GetList<Valve>(x => x.IsActive && ids.Contains(x.Id));

            var resultList = new List<ValveDTO>();
            if (valveItems != null)
            {
                foreach (var valve in valveItems)
                {
                    var raspberry = GetRaspberry(valve.RaspberryId);
                    var dtoItem = ModelBinder.Instance.ConvertToValveDTO(valve, raspberry);
                    resultList.Add(dtoItem);
                }
            }

            return resultList;
        }
    }
}
