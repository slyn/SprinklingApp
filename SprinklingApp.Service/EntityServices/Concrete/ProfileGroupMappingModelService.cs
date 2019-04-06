using System.Collections.Generic;
using SprinklingApp.DataAccess;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;

namespace SprinklingApp.Service.EntityServices.Concrete {

    public class ProfileGroupMappingModelService : BaseModelService, IProfileGroupMappingService {
        private readonly DataAccessor _accessor;

        public ProfileGroupMappingModelService(IRepository repo) {
            _accessor = new DataAccessor(repo);
        }


        public ProfileGroupMapping Get(long id) {
            ProfileGroupMapping item = _accessor.Get<ProfileGroupMapping>(x => x.IsActive && x.Id == id);
            return item;
        }

        public IEnumerable<ProfileGroupMapping> GetList() {
            IEnumerable<ProfileGroupMapping> itemList = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive);
            return itemList;
        }

        public IEnumerable<ProfileGroupMapping> GetListByGroup(long groupid) {
            IEnumerable<ProfileGroupMapping> itemList = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.GroupId == groupid);
            return itemList;
        }

        public IEnumerable<ProfileGroupMapping> GetListByProfile(long profileId) {
            IEnumerable<ProfileGroupMapping> itemList = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.ProfileId == profileId);
            return itemList;
        }

        public ProfileGroupMapping Insert(ProfileGroupMapping entity) {
            entity = _accessor.Insert(entity);
            return entity;
        }

        public ProfileGroupMapping Update(ProfileGroupMapping entity) {
            _accessor.Update(entity);
            return entity;
        }

        public void Delete(long id) {
            ProfileGroupMapping item = Get(id);
            _accessor.Delete(item);
        }
    }

}