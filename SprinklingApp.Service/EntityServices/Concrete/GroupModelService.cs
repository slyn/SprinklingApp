using System.Collections.Generic;
using System.Linq;
using SprinklingApp.DataAccess;
using SprinklingApp.Model.DTOs.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;

namespace SprinklingApp.Service.EntityServices.Concrete
{
    public class GroupModelService:BaseModelService, IGroupService
    {
        private readonly DataAccessor _accessor;
        public GroupModelService(IRepository repo)
        {
            _accessor = new DataAccessor(repo);
        }
        
        public GroupDTO Get(long id)
        {
            var groupItem = _accessor.Get<Group>(x => x.IsActive && x.Id == id);
            var item = ModelBinder.Instance.ConvertToGroupDTO(groupItem);
            return item;
        }

        public IEnumerable<GroupDTO> GetList()
        {
            var groupItems = _accessor.GetList<Group>(x => x.IsActive);
            var items = groupItems.Select(x => ModelBinder.Instance.ConvertToGroupDTO(x));
            
            return items;
        }

        public GroupDTO Insert(GroupDTO dtoItem)
        {
            var entity = ModelBinder.Instance.ConvertToGroup(dtoItem);
            entity = _accessor.Insert(entity);
            var item = ModelBinder.Instance.ConvertToGroupDTO(entity);
            return item;
        }

        public GroupDTO Update(GroupDTO dtoItem)
        {
            var entity = ModelBinder.Instance.ConvertToGroup(dtoItem);
            _accessor.Update(entity);
            var item = ModelBinder.Instance.ConvertToGroupDTO(entity);
            return item;
        }

        public void Delete(long id)
        {
            var entity = _accessor.Get<Group>(x=>x.Id == id);
            _accessor.Delete(entity);
        }
    }
}
