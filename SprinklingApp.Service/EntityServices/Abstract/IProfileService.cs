using SprinklingApp.Model.DTOs.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Abstract
{
    public interface IProfileService
    {
        ProfileDTO Get(long id);
        IEnumerable<ProfileDTO> GetList();
        ProfileDTO Insert(ProfileDTO dtoItem);
        ProfileDTO Update(ProfileDTO dtoItem);
        void Delete(long id);
    }
}
