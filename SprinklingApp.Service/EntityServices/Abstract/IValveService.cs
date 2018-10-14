using SprinklingApp.Model.DTOs.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Abstract
{
    public interface IValveService
    {
        ValveDTO Get(long id);
        IEnumerable<ValveDTO> GetList();
        IEnumerable<ValveDTO> GetListByIds(IList<long> ids);
        ValveDTO Insert(ValveDTO dtoItem);
        ValveDTO Update(ValveDTO dtoItem);
        void Delete(long id);
    }
}
