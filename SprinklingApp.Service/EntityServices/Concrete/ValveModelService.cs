using System.Collections.Generic;
using SprinklingApp.DataAccess;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;

namespace SprinklingApp.Service.EntityServices.Concrete {

    public class ValveModelService : BaseModelService, IValveService {
        private readonly DataAccessor _accessor;

        public ValveModelService(IRepository repo) {
            _accessor = new DataAccessor(repo);
        }


        public Valve Get(long id) {
            Valve valveItem = _accessor.Get<Valve>(x => x.IsActive && x.Id == id);
            return valveItem;
        }

        public IEnumerable<Valve> GetList() {
            IEnumerable<Valve> valveItems = _accessor.GetList<Valve>(x => x.IsActive);
            return valveItems;
        }

        public Valve Insert(Valve entity) {
            entity = _accessor.Insert(entity);
            return entity;
        }

        public Valve Update(Valve entity) {
            _accessor.Update(entity);

            return entity;
        }

        public void Delete(long id) {
            Valve entity = _accessor.Get<Valve>(x => x.Id == id);
            IEnumerable<ValveGroupMapping> valveGroupMappings = _accessor.GetList<ValveGroupMapping>(x => x.IsActive && x.ValveId == id);
            if (valveGroupMappings != null) {
                foreach (ValveGroupMapping item in valveGroupMappings) {
                    _accessor.Delete(item);
                }
            }

            _accessor.Delete(entity);
        }

        public IEnumerable<Valve> GetListByIds(IList<long> ids) {
            IEnumerable<Valve> valveItems = _accessor.GetList<Valve>(x => x.IsActive && ids.Contains(x.Id));
            return valveItems;
        }
    }

}