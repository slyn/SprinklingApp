using System;
using System.Collections.Generic;
using System.Linq;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;
using SprinklingApp.Service.EntityServices.Abstract;

namespace SprinklingApp.Service.Helper
{
    public class OperationGenerator
    {
        private readonly IProfileGroupMappingService _profileGroupMappingService;
        private readonly IValveGroupMappingService _valveGroupMappingService;
        private readonly IOpenCloseOperationItemService _openCloseOperationItemService;

        public OperationGenerator(IProfileGroupMappingService profileGroupMappingService, IValveGroupMappingService valveGroupMappingService,IOpenCloseOperationItemService openCloseOperationItemService)
        {
            _profileGroupMappingService = profileGroupMappingService;
            _valveGroupMappingService = valveGroupMappingService;
            _openCloseOperationItemService = openCloseOperationItemService;
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
                }
            }
            // bugün olup da saat dolayısı ile geçmiş olanları db save or update listesinden sil
            resultList = resultList.Where(x => x.PlannedDateClose <= DateTime.Now).ToList();
            
            // oluşturulanları db save or update
            var existOperations = _openCloseOperationItemService.GetListByDate(date); // güne ait hala geçerli işlemler
            // update
            // var olanların planlanan tarihlerini değiştir


            // save
        }
    }
}
