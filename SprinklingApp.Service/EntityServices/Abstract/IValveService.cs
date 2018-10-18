using SprinklingApp.Model.Entities.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Abstract
{
    public interface IValveService
    {
        Valve Get(long id);
        IEnumerable<Valve> GetList();
        IEnumerable<Valve> GetListByIds(IList<long> ids);
        Valve Insert(Valve dtoItem);
        Valve Update(Valve dtoItem);
        void Delete(long id);
    }
}
