using System.Collections.Generic;
using SprinklingApp.DataAccess;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;

namespace SprinklingApp.Service.EntityServices.Concrete {

    public class GroupModelService : BaseModelService, IGroupService {
        private readonly DataAccessor _accessor;

        public GroupModelService(IRepository repo) {
            _accessor = new DataAccessor(repo);
        }

        public Group Get(long id) {
            Group groupItem = _accessor.Get<Group>(x => x.IsActive && x.Id == id);
            return groupItem;
        }

        public IEnumerable<Group> GetList() {
            IEnumerable<Group> groupItems = _accessor.GetList<Group>(x => x.IsActive);
            return groupItems;
        }

        public Group Insert(Group entity) {
            entity = _accessor.Insert(entity);
            return entity;
        }

        public Group Update(Group entity) {
            _accessor.Update(entity);
            return entity;
        }

        public void Delete(long id) {
            Group entity = _accessor.Get<Group>(x => x.Id == id);

            IEnumerable<ValveGroupMapping> valveMappings = _accessor.GetList<ValveGroupMapping>(x => x.IsActive && x.GroupId == id);
            IEnumerable<ProfileGroupMapping> profileMappings = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.GroupId == id);
            foreach (ValveGroupMapping item in valveMappings) {
                _accessor.Delete(item);
            }

            foreach (ProfileGroupMapping item in profileMappings) {
                _accessor.Delete(item);
            }

            _accessor.Delete(entity);
        }

        public IEnumerable<Group> GetListByIds(IList<long> ids) {
            IEnumerable<Group> groupItems = _accessor.GetList<Group>(x => x.IsActive && ids.Contains(x.Id));
            return groupItems;
        }
    }

}