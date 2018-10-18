using SprinklingApp.DataAccess;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprinklingApp.Service.EntityServices.Concrete
{
    public class ProfileGroupMappingModelService : BaseModelService, IProfileGroupMappingService
    {

        private readonly DataAccessor _accessor;
        public ProfileGroupMappingModelService(IRepository repo)
        {
            _accessor = new DataAccessor(repo);
        }
        
       
        ProfileGroupMapping IProfileGroupMappingService.Get(long id)
        {
            var item = _accessor.Get<ProfileGroupMapping>(x => x.IsActive && x.Id == id);
            return item;
        }

        IEnumerable<ProfileGroupMapping> IProfileGroupMappingService.GetList()
        {
            var itemList = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive);
            return itemList;
        }

        IEnumerable<ProfileGroupMapping> IProfileGroupMappingService.GetListByGroup(long groupid)
        {
            var itemList = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.GroupId == groupid);
            return itemList;
        }

        public IEnumerable<ProfileGroupMapping> GetListByProfile(long profileId)
        {
            var itemList = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.ProfileId == profileId);
            return itemList;
        }

        public ProfileGroupMapping Insert(ProfileGroupMapping entity)
        {
            entity = _accessor.Insert(entity);
            return entity;
        }

        public ProfileGroupMapping Update(ProfileGroupMapping entity)
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
