using System.Collections.Generic;
using SprinklingApp.Model.Entities.Concrete;

namespace SprinklingApp.Service.EntityServices.Abstract {

    public interface IRaspberryService {
        Raspberry Get(long id);
        IEnumerable<Raspberry> GetList();
        Raspberry Insert(Raspberry dtoItem);
        Raspberry Update(Raspberry dtoItem);
        void Delete(long id);
    }

}