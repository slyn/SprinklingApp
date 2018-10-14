using SprinklingApp.Model.DTOs.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Abstract
{
    public interface IRaspberryService
    {
        RaspberryDTO Get(long id);
        IEnumerable<RaspberryDTO> GetList();
        RaspberryDTO Insert(RaspberryDTO dtoItem);
        RaspberryDTO Update(RaspberryDTO dtoItem);
        void Delete(long id);
    }
}
