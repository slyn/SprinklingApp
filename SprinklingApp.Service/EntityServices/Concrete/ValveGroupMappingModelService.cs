using SprinklingApp.DataAccess;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Concrete
{
    public class ValveGroupMappingModelService : BaseModelService, IValveGroupMappingService
    {

        private readonly DataAccessor _accessor;
        public ValveGroupMappingModelService(IRepository repo)
        {
            _accessor = new DataAccessor(repo);
        }

        public ValveGroupMapping Get(long id)
        {
            var item = _accessor.Get<ValveGroupMapping>(x=> x.IsActive && x.Id == id);
            return item;
        }

        public IEnumerable<ValveGroupMapping> GetList()
        {
            var itemList = _accessor.GetList<ValveGroupMapping>(x => x.IsActive);
            return itemList;
        }

        public IEnumerable<ValveGroupMapping> GetListByGroup(long groupid)
        {
            var itemList = _accessor.GetList<ValveGroupMapping>(x => x.IsActive && x.GroupId == groupid);
            return itemList;
        }

        public IEnumerable<ValveGroupMapping> GetListByValve(long valveid)
        {
            var itemList = _accessor.GetList<ValveGroupMapping>(x => x.IsActive && x.ValveId == valveid);
            return itemList;
        }

        public ValveGroupMapping Insert(ValveGroupMapping entity)
        {
            entity = _accessor.Insert(entity);
            return entity;
        }

        public ValveGroupMapping Update(ValveGroupMapping entity)
        {
            _accessor.Update(entity);
            return entity;
        }

        public void Delete(long id)
        {
            var item = Get(id);
            _accessor.Delete(item);
        }
    }
}
