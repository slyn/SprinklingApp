using SprinklingApp.Model.Entities.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Abstract
{
    public interface IProfileGroupMappingService
    {
        ProfileGroupMapping Get(long id);
        IEnumerable<ProfileGroupMapping> GetList();
        IEnumerable<ProfileGroupMapping> GetListByGroup(long groupid);
        IEnumerable<ProfileGroupMapping> GetListByProfile(long profileId);
        ProfileGroupMapping Insert(ProfileGroupMapping entity);
        ProfileGroupMapping Update(ProfileGroupMapping entity);
        void Delete(long id);
    }
}
