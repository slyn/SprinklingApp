using SprinklingApp.DataAccess;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SprinklingApp.Service.EntityServices.Concrete
{
    public class OpenCloseOperationItemModelService : BaseModelService, IOpenCloseOperationItemService
    {

        private readonly DataAccessor _accessor;
        public OpenCloseOperationItemModelService(IRepository repo)
        {
            _accessor = new DataAccessor(repo);
        }


        public IEnumerable<OpenCloseOperationItem> GetList()
        {
            var openCloseOperationItems = _accessor.GetList<OpenCloseOperationItem>(x => x.IsActive);
            return openCloseOperationItems;
        }

        public OpenCloseOperationItem Get(long id)
        {
            var openCloseOperationItem = _accessor.Get<OpenCloseOperationItem>(x => x.IsActive && x.Id == id);
            return openCloseOperationItem;
        }

        public OpenCloseOperationItem Insert(OpenCloseOperationItem entity)
        {
            entity = _accessor.Insert(entity);
            return entity;
        }

        public OpenCloseOperationItem Update(OpenCloseOperationItem entity)
        {
            _accessor.Update(entity);
            return entity;
        }

        public void Delete(long id)
        {
            var entity = _accessor.Get<OpenCloseOperationItem>(x => x.Id == id);
            _accessor.Delete(entity);
        }


        public IEnumerable<OpenCloseOperationItem> GetItemsForOperation(DateTime date)
        {
            // başlatılma zamanı geldi geçiyor ve başaltılmamışsa veya kapatma zamanı geldi geçiyor ve kapatılmamışsa işlem yapılacaklar listesine eklenir.
            var resultList = _accessor.GetList<OpenCloseOperationItem>(x => 
                ( x.PlannedDateOpen <= DateTime.Now && x.OccuredDateOpen == default(DateTime?)) || 
                ( x.PlannedDateClose <= DateTime.Now && x.OccuredDateClose == default(DateTime?)));
            
            return resultList;
        }

        //public IEnumerable<OpenCloseOperationItem> GetActivated()
        //{
        //    var openCloseOperationItems = _accessor.GetList<OpenCloseOperationItem>(x => x.IsActive && x.PlannedDate != default(DateTime) && x.OccuredDate == null);
        //    return openCloseOperationItems;
        //}

        //public IEnumerable<OpenCloseOperationItem> GetByDay(Days day)
        //{
        //    var openCloseOperationItems = _accessor.GetList<OpenCloseOperationItem>(x => x.IsActive && x.PlannedDate.IsDays(day));
        //    return openCloseOperationItems;
        //}
        //public IEnumerable<OpenCloseOperationItem> GetByDate(Days day)
        //{
        //    var openCloseOperationItems = _accessor.GetList<OpenCloseOperationItem>(x => x.IsActive && x.PlannedDate.IsDays(day));
        //    return openCloseOperationItems;
        //}
        //public IEnumerable<OpenCloseOperationItem> GetByValve(long valveId)
        //{
        //    var openCloseOperationItems = _accessor.GetList<OpenCloseOperationItem>(x => x.IsActive && x.ValveId == valveId);
        //    return openCloseOperationItems;
        //}

        //public IEnumerable<OpenCloseOperationItem> GetInanctivated()
        //{
        //    var openCloseOperationItems = _accessor.GetList<OpenCloseOperationItem>(x => x.IsActive && x.OccuredDate != null);
        //    return openCloseOperationItems;
        //}


        //public OpenCloseOperationItem Get(Days day, long valveId, int hour, int minute)
        //{
        //    var openCloseOperationItems = _accessor.Get<OpenCloseOperationItem>(x => 
        //                                                                            x.IsActive && 
        //                                                                            x.ValveId == valveId && 
        //                                                                            x.PlannedDate.IsDays(day) && 
        //                                                                            x.PlannedDate.Date.Hour == hour &&
        //                                                                            x.PlannedDate.Date.Minute == minute);

        //    return openCloseOperationItems;
        //}

        //public IEnumerable<OpenCloseOperationItem> GetByDayAndValve(Days day, long valveId)
        //{
        //    var openCloseOperationItems = _accessor.GetList<OpenCloseOperationItem>(x => 
        //                                                                                x.IsActive &&
        //                                                                                x.ValveId == valveId &&
        //                                                                                x.PlannedDate.IsDays(day));

        //    return openCloseOperationItems;
        //}
    }
}
