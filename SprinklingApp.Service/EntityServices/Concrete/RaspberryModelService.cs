using System.Collections.Generic;
using SprinklingApp.DataAccess;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;

namespace SprinklingApp.Service.EntityServices.Concrete {

    public class RaspberryModelService : BaseModelService, IRaspberryService {
        private readonly DataAccessor _accessor;

        public RaspberryModelService(IRepository repo) {
            _accessor = new DataAccessor(repo);
        }

        public Raspberry Get(long id) {
            Raspberry raspberryItem = _accessor.Get<Raspberry>(x => x.IsActive && x.Id == id);
            return raspberryItem;
        }

        public IEnumerable<Raspberry> GetList() {
            IEnumerable<Raspberry> raspberryItems = _accessor.GetList<Raspberry>(x => x.IsActive);
            return raspberryItems;
        }

        public Raspberry Insert(Raspberry entity) {
            entity = _accessor.Insert(entity);
            return entity;
        }

        public Raspberry Update(Raspberry entity) {
            _accessor.Update(entity);
            return entity;
            //var commingIds = dtoItem.Valves.Select(x => x.Id);
            //var savedValves = _accessor.GetList<Valve>(x => x.IsActive && x.RaspberryId == entity.Id);

            //// delete exist items
            //var deletedValveMappings = savedValves.Where(x => !commingIds.Contains(x.Id));
            //foreach (var mappingItem in deletedValveMappings)
            //{
            //    _accessor.Delete(mappingItem);
            //}

            //// insert & update items
            //foreach (var valve in dtoItem.Valves)
            //{
            //    if (valve.Id != default(long))
            //    {
            //        // update
            //        var valveItem = new Valve()
            //        {
            //            IsActive = true,
            //            Name = valve.Name,
            //            ActivatePin = valve.ActivatePin,
            //            DisabledPin = valve.DisabledPin,
            //            Pressure = valve.Pressure,
            //            RaspberryId = dtoItem.Id
            //        };

            //        valveItem = _accessor.Insert(valveItem);

            //    }
            //    else
            //    {
            //        // insert
            //        var valveItem = new Valve()
            //        {
            //            IsActive = true,
            //            Name = valve.Name,
            //            ActivatePin = valve.ActivatePin,
            //            DisabledPin = valve.DisabledPin,
            //            Pressure = valve.Pressure,
            //            RaspberryId = dtoItem.Id
            //        };
            //        valveItem = _accessor.Insert(valveItem);

            //        valve.Id = valveItem.Id;
            //    }
            //}

            //return dtoItem;
        }

        public void Delete(long id) {
            Raspberry entity = _accessor.Get<Raspberry>(x => x.Id == id);
            IEnumerable<Valve> valves = _accessor.GetList<Valve>(x => x.IsActive && x.RaspberryId == id);

            if (valves != null) {
                foreach (Valve item in valves) {
                    _accessor.Delete(item);
                }
            }

            _accessor.Delete(entity);
        }
    }

}