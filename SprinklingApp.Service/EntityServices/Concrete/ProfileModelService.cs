using System.Collections.Generic;
using SprinklingApp.DataAccess;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;

namespace SprinklingApp.Service.EntityServices.Concrete
{
    public class ProfileModelService : BaseModelService, IProfileService
    {
        private readonly DataAccessor _accessor;
        public ProfileModelService(IRepository repo)
        {
            _accessor = new DataAccessor(repo);
        }

        public Profile Get(long id)
        {
            var profileItem = _accessor.Get<Profile>(x => x.IsActive && x.Id == id);
            return profileItem;
           
        }

       
        public IEnumerable<Profile> GetList()
        {
            var profileItems = _accessor.GetList<Profile>(x => x.IsActive);
            return profileItems;
            
        }

        public Profile Insert(Profile entity)
        {
            entity = _accessor.Insert(entity);
            return entity;
        }

        public Profile Update(Profile entity)
        {
            _accessor.Update(entity);
            return entity;
            //var commingIds = dtoItem.Groups.Select(x => x.Id);
            //var savedMappings = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.ProfileId == entity.Id);

            //// delete exist items
            //var deletedGroupMappings = savedMappings.Where(x=>!commingIds.Contains(x.GroupId));
            //foreach (var mappingItem in deletedGroupMappings)
            //{
            //    _accessor.Delete(mappingItem);
            //}

            //// insert new items
            //var savedGroupIds = savedMappings.Select(x => x.GroupId);
            //var newestGroupIds = commingIds.Where(x => !savedGroupIds.Contains(x));
            //foreach (var group in newestGroupIds)
            //{
            //    var newMappingItem = new ProfileGroupMapping();
            //    newMappingItem.IsActive = true;
            //    newMappingItem.ProfileId = entity.Id;
            //    newMappingItem.GroupId = group;

            //    _accessor.Insert(newMappingItem);
            //}

            //return dtoItem;
        }

        public void Delete(long id)
        {
            var entity = _accessor.Get<Profile>(x => x.Id == id);
            var mappings = _accessor.GetList<ProfileGroupMapping>(x => x.IsActive && x.ProfileId == id);
            foreach (var item in mappings)
            {
                _accessor.Delete(item);
            }
            _accessor.Delete(entity);
            
        }
    }
}
