using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Enums;
using SprinklingApp.Service.Helper;

namespace SprinklingApp.Service.Hubs
{
    public class ClientHub : Hub
    {
        public async Task SendOpenedValve(long valveId)
        {
            await Clients.All.SendAsync("ChangeStatu", AccessTypes.Opened, valveId);
        }

        public async Task SendClosedValve(long valveId)
        {
            await Clients.All.SendAsync("ChangeStatu",AccessTypes.Closed,valveId);
        }

        public async Task GetFullMap(MapResponseModel map)
        {
            await Clients.Caller.SendAsync("ReceiveFullMap", map);
        }
    }
}
