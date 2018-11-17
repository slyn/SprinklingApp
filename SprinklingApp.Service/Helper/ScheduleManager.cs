using SprinklingApp.Service.EntityServices.Abstract;
using System;

namespace SprinklingApp.Service.Helper
{
    public class ScheduleManager : IScheduleManager
    {
        private readonly IOpenCloseOperationItemService _openCloseOperationItemService;

      
        public ScheduleManager(IOpenCloseOperationItemService openCloseOperationItemService)
        {
            _openCloseOperationItemService = openCloseOperationItemService;
        }

        public void RefreshOpenCloseOperationByDay()
        {
            // generate list for today
            _openCloseOperationItemService.RefreshOpenCloseOperationItemListByDay(DateTime.Now);
            // generate list for tomorrow
            _openCloseOperationItemService.RefreshOpenCloseOperationItemListByDay(DateTime.Now.AddDays(1));

        }
    }
}
