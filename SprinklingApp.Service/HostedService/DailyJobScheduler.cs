using System;
using System.Threading;
using System.Threading.Tasks;
using SprinklingApp.Service.Helper;

namespace SprinklingApp.Service.HostedService
{
    public class DailyJobScheduler : BaseHostedService
    {
        private readonly IScheduleManager _scheduleManager;

        public DailyJobScheduler(IScheduleManager scheduleManager)
        {
            _scheduleManager = scheduleManager;
        }

        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            _scheduleManager.RefreshOpenCloseOperationByDay();

            // run daily
            await Task.Delay(TimeSpan.FromDays(1), cToken);
        }
    }
}
