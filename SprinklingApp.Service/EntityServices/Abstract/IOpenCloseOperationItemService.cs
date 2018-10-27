using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Abstract
{
    public interface IOpenCloseOperationItemService
    {
        OpenCloseOperationItem Get(long id);
        OpenCloseOperationItem Get(Days day,long valveId,int hour,int minute);
        IEnumerable<OpenCloseOperationItem> GetList();
        IEnumerable<OpenCloseOperationItem> GetByDay(Days day);
        IEnumerable<OpenCloseOperationItem> GetByValve(long valveId);
        IEnumerable<OpenCloseOperationItem> GetByDayAndValve(Days day,long valveId);
        IEnumerable<OpenCloseOperationItem> GetActivated();
        IEnumerable<OpenCloseOperationItem> GetInanctivated();
        OpenCloseOperationItem Insert(OpenCloseOperationItem entity);
        OpenCloseOperationItem Update(OpenCloseOperationItem entity);
        void Delete(long id);
    }
}
