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
        private readonly IProfileGroupMappingService _profileGroupMappingService;
        private readonly IValveGroupMappingService _valveGroupMappingService;

        public OpenCloseOperationItemModelService(IRepository repo, IProfileGroupMappingService profileGroupMappingService, IValveGroupMappingService valveGroupMappingService)
        {
            _profileGroupMappingService = profileGroupMappingService;
            _valveGroupMappingService = valveGroupMappingService;
            _accessor = new DataAccessor(repo);
        }


        public IEnumerable<OpenCloseOperationItem> GetList()
        {
            var openCloseOperationItems = _accessor.GetList<OpenCloseOperationItem>(x => x.IsActive);
            return openCloseOperationItems;
        }

        public IEnumerable<OpenCloseOperationItem> GetListByDate(DateTime date)
        {
            var openCloseOperationItems = _accessor.GetList<OpenCloseOperationItem>(x => x.IsActive && x.PlannedDateClose >= date && x.PlannedDateClose.Date == date.Date); // year,month,day are equal
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

        public void RefreshOpenCloseOperationItemListByDay(DateTime date)
        {
            var resultList = new List<OpenCloseOperationItem>();

            // datetime -> day 
            var today = DateTime.Today.DayOfWeek.ConvertToDays();
            // profilde o gün olan grupları bul
            var profileGroupList = _profileGroupMappingService.GetList().Where(x => x.Profile.DayOfWeek.HasFlag(today));

            // grupların içerdiği vanalar için nesne oluştur
            foreach (var mappingItem in profileGroupList)
            {
                var valves = _valveGroupMappingService.GetListByGroup(mappingItem.GroupId);

                foreach (var valveMapping in valves)
                {
                    var tempItem = new OpenCloseOperationItem()
                    {
                        IsActive = true,
                        OperationType = OperationTypes.PlannedGroupWork,
                        PlannedDateOpen = new DateTime(DateTime.Today.Year, DateTime.Now.Month, DateTime.Now.Day, mappingItem.Profile.StartHour, mappingItem.Profile.StartMinute, 0),
                        ValveId = valveMapping.ValveId

                    };

                    switch (mappingItem.Group.Unit)
                    {
                        case TimeUnit.Hour:
                            tempItem.PlannedDateClose = tempItem.PlannedDateOpen.AddHours(mappingItem.Group.Duration);
                            break;
                        case TimeUnit.Minute:
                            tempItem.PlannedDateClose = tempItem.PlannedDateOpen.AddMinutes(mappingItem.Group.Duration);
                            break;
                        case TimeUnit.Second:
                            tempItem.PlannedDateClose = tempItem.PlannedDateOpen.AddSeconds(mappingItem.Group.Duration);
                            break;
                        default:
                            throw new Exception($"Beklenen dışında bir süre birimi ile karşılaşıldı.[{mappingItem.Group.Unit}]");
                    }

                    resultList.Add(tempItem);
                }
            }

            // bugün olup da saat dolayısı ile geçmiş olanları db save or update listesinden sil
            resultList = resultList.Where(x => x.PlannedDateClose <= DateTime.Now).ToList();

            // oluşturulanları db save or update
            var existOperations = this.GetListByDate(date); // o gün için oluşturulmuş işlemler
            var updateCloseNow = existOperations.Where(x => x.OperationType == OperationTypes.PlannedGroupWork && x.OccuredDateClose == null && x.OccuredDateOpen != null).ToList(); // planlamadan dolayı açık olan vanalar kapatılır
            var deleteNow = existOperations.Where(x => x.OperationType == OperationTypes.PlannedGroupWork && x.OccuredDateClose == null && x.OccuredDateOpen == null).ToList(); // planlanmış ama daha açılmamış vanalar için planan işler silinir

            // close operation - all opened
            foreach (var operation in updateCloseNow)
            {
                operation.PlannedDateClose = DateTime.Now;
                this.Update(operation);
            }

            // delete active operations
            foreach (var item in deleteNow)
            {
                this.Delete(item.Id);
            }

            // save
            foreach (var newOperation in resultList)
            {
                this.Insert(newOperation);
            }

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
