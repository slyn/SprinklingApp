using SprinklingApp.Model.Entities.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Abstract
{
    public interface IProfileService
    {
        Profile Get(long id);
        IEnumerable<Profile> GetList();
        Profile Insert(Profile dtoItem);
        Profile Update(Profile dtoItem);
        void Delete(long id);
    }
}
