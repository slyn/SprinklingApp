using System.Collections.Generic;
using SprinklingApp.DataAccess;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;

namespace SprinklingApp.Service.EntityServices.Concrete
{
    public class GroupModelService:BaseModelService, IGroupService
    {
        private readonly DataAccessor _accessor;
        public GroupModelService(IRepository repo)
        {
            _accessor = new DataAccessor(repo);
        }
        
        public Group Get(long id)
        {
            var groupItem = _accessor.Get<Group>(x => x.IsActive && x.Id == id);
            return groupItem;
        }

        public IEnumerable<Group> GetList()
        {
            var groupItems = _accessor.GetList<Group>(x => x.IsActive);
            return groupItems;
        }

        public Group Insert(Group entity)
        {
            entity = _accessor.Insert(entity);
            return entity;
            
        }

        public Group Update(Group entity)
        {
            _accessor.Update(entity);
            return entity;
        }

        public void Delete(long id)
        {
            var entity = _accessor.Get<Group>(x=>x.Id == id);

            var valveMappings = _accessor.GetList<ValveGroupMapping>(x => x.IsActive && x.GroupId == id);
            var profileMappings = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.GroupId == id);
            _accessor.Delete(entity);
            foreach (var item in valveMappings)
            {
                _accessor.Delete(item);
            }
            foreach (var item in profileMappings)
            {
                _accessor.Delete(item);
            }
        }

        public IEnumerable<Group> GetListByIds(IList<long> ids)
        {
            var groupItems = _accessor.GetList<Group>(x => x.IsActive && ids.Contains(x.Id));
            return groupItems;
        }
    }
}
