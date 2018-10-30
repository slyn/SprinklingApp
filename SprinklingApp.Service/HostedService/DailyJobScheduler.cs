using System;
using System.Threading;
using System.Threading.Tasks;

namespace SprinklingApp.Service.HostedService
{
    public class DailyJobScheduler : BaseHostedService
    {
        protected override Task ExecuteAsync(CancellationToken cToken)
        {
            // profile ve grup incelemeleri sonrası günlük işlem listesi oluşturulacak
            throw new NotImplementedException();
        }
    }
}
