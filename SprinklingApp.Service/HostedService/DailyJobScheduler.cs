using System;
using System.Threading;
using System.Threading.Tasks;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.Helper;

namespace SprinklingApp.Service.HostedService
{
    public class DailyJobScheduler : BaseHostedService
    {
        private readonly IProfileService _profileService;
        private readonly IValveGroupMappingService _valveGroupMappingService;
        public DailyJobScheduler(IProfileService profileService, IValveGroupMappingService valveGroupMappingService)
        {
            _profileService = profileService;
            _valveGroupMappingService = valveGroupMappingService;
        }

        protected override Task ExecuteAsync(CancellationToken cToken)
        { 
            // profile ve grup incelemeleri sonrası günlük işlem listesi oluşturulacak
            var today = DateTime.Today.DayOfWeek.ConvertToDays();
            var tomorrow = DateTime.Today.AddDays(1).DayOfWeek.ConvertToDays();
            
            // bugün için liste oluştur
            // yarın için liste oluştur

            throw new NotImplementedException();
        }
    }
}
