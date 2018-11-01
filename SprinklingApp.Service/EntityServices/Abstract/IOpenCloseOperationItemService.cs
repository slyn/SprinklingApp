using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;
using System;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Abstract
{
    public interface IOpenCloseOperationItemService
    {
        IEnumerable<OpenCloseOperationItem> GetList();
        IEnumerable<OpenCloseOperationItem> GetListByDate(DateTime date)
        OpenCloseOperationItem Get(long id);
        OpenCloseOperationItem Insert(OpenCloseOperationItem entity);
        OpenCloseOperationItem Update(OpenCloseOperationItem entity);
        void Delete(long id);

        IEnumerable<OpenCloseOperationItem> GetItemsForOperation(DateTime date);

        //IEnumerable<OpenCloseOperationItem> GetActivated();
        //IEnumerable<OpenCloseOperationItem> GetInactivated();
        
        //IEnumerable<OpenCloseOperationItem> GetByValve(long valveId);


        //OpenCloseOperationItem Get(Days day,long valveId,int hour,int minute);
        //IEnumerable<OpenCloseOperationItem> GetByDay(Days day);


    }
}
