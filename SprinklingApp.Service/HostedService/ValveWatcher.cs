using System;
using System.Threading;
using System.Threading.Tasks;
using SprinklingApp.Service.EntityServices.Abstract;

namespace SprinklingApp.Service.HostedService
{
    public class ValveWatcher : BaseHostedService
    {
        private readonly IOpenCloseOperationItemService _openCloseOperationItemService;

        public ValveWatcher(IOpenCloseOperationItemService openCloseOperationItemService)
        {
            _openCloseOperationItemService = openCloseOperationItemService;
        }

        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                var ops = _openCloseOperationItemService.GetItemsForOperation(DateTime.Now);
                foreach (var operationItem in ops)
                {
                    if (operationItem.PlannedDateOpen <= DateTime.Now && operationItem.OccuredDateOpen == default(DateTime?))
                    {
                        // try open
                    }
                    else if(operationItem.PlannedDateClose <= DateTime.Now && operationItem.OccuredDateClose == default(DateTime?))
                    {
                        // try close
                    }
                }
                // run every minute
                await Task.Delay(TimeSpan.FromMinutes(1), cToken);


            }
        }
    }
}
