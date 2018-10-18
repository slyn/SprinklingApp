using SprinklingApp.Model.Entities.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Abstract
{
    public interface IRaspberryService
    {
        Raspberry Get(long id);
        IEnumerable<Raspberry> GetList();
        Raspberry Insert(Raspberry dtoItem);
        Raspberry Update(Raspberry dtoItem);
        void Delete(long id);
    }
}
