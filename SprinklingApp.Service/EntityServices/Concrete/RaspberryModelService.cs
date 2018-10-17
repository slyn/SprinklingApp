using System;
using System.Collections.Generic;
using System.Linq;
using SprinklingApp.DataAccess;
using SprinklingApp.Model.DTOs.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;

namespace SprinklingApp.Service.EntityServices.Concrete
{
    public class RaspberryModelService : BaseModelService, IRaspberryService
    {
        private readonly DataAccessor _accessor;
        public RaspberryModelService(IRepository repo)
        {
            _accessor = new DataAccessor(repo);
        }

        public RaspberryDTO Get(long id)
        {

            var raspberryItem = _accessor.Get<Raspberry>(x => x.IsActive && x.Id == id);
            var valves = _accessor.GetList<Valve>(x => x.IsActive && x.RaspberryId == id);

            var item = ModelBinder.Instance.ConvertToRaspberryDTO(raspberryItem, valves);
            return item;
        }

        public IEnumerable<RaspberryDTO> GetList()
        {
            var raspberryItems = _accessor.GetList<Raspberry>(x => x.IsActive);
            if (raspberryItems == null)
                return null;
            var valves = _accessor.GetList<Valve>(x => x.IsActive);

            var resultList = new List<RaspberryDTO>();

            foreach (var raspberry in raspberryItems)
            {
                var valveItems = valves?.Where(x => x.RaspberryId == raspberry.Id);
                var dtoItem = ModelBinder.Instance.ConvertToRaspberryDTO(raspberry, valveItems);
                resultList.Add(dtoItem);
            }

            return resultList;

        }

        public RaspberryDTO Insert(RaspberryDTO dtoItem)
        {
            var entity = ModelBinder.Instance.ConvertToRaspberry(dtoItem);
            entity = _accessor.Insert(entity);

            foreach (var valve in dtoItem.Valves)
            {
                var valveItem = new Valve()
                {
                    IsActive = true,
                    Name = valve.Name,
                    ActivatePin = valve.ActivatePin,
                    DisabledPin = valve.DisabledPin,
                    Pressure = valve.Pressure,
                    RaspberryId = entity.Id
                };

                _accessor.Insert(valveItem);
            }

            dtoItem.Id = entity.Id;
            return dtoItem;
        }

        public RaspberryDTO Update(RaspberryDTO dtoItem)
        {
            var entity = ModelBinder.Instance.ConvertToRaspberry(dtoItem);
            _accessor.Update(entity);

            var commingIds = dtoItem.Valves.Select(x => x.Id);
            var savedValves = _accessor.GetList<Valve>(x => x.IsActive && x.RaspberryId == entity.Id);

            // delete exist items
            var deletedValveMappings = savedValves.Where(x => !commingIds.Contains(x.Id));
            foreach (var mappingItem in deletedValveMappings)
            {
                _accessor.Delete(mappingItem);
            }

            // insert & update items
            foreach (var valve in dtoItem.Valves)
            {
                if (valve.Id != default(long))
                {
                    // update
                    var valveItem = new Valve()
                    {
                        IsActive = true,
                        Name = valve.Name,
                        ActivatePin = valve.ActivatePin,
                        DisabledPin = valve.DisabledPin,
                        Pressure = valve.Pressure,
                        RaspberryId = dtoItem.Id
                    };

                    valveItem = _accessor.Insert(valveItem);

                }
                else
                {
                    // insert
                    var valveItem = new Valve()
                    {
                        IsActive = true,
                        Name = valve.Name,
                        ActivatePin = valve.ActivatePin,
                        DisabledPin = valve.DisabledPin,
                        Pressure = valve.Pressure,
                        RaspberryId = dtoItem.Id
                    };
                    valveItem = _accessor.Insert(valveItem);

                    valve.Id = valveItem.Id;
                }
            }

            return dtoItem;
        }

        public void Delete(long id)
        {
            var entity = _accessor.Get<Raspberry>(x => x.Id == id);
            var valves = _accessor.GetList<Valve>(x => x.IsActive && x.RaspberryId == id);

            _accessor.Delete(entity);
            if (valves != null)
            {
                foreach (var item in valves)
                {
                    _accessor.Delete(item);
                }
            }
        }
    }
}
