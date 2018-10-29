using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using SprinklingApp.Service.Helper;
using System;
using System.Threading.Tasks;

namespace SprinklingApp.Service.Hubs
{
    public class SlaveHub : Hub
    {
        private IConnectionManager _connectionManager;
        public SlaveHub(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        public override Task OnConnectedAsync()
        {
            var ipAddress = Context.Features.Get<IHttpConnectionFeature>()?.RemoteIpAdd‌​ress.ToString();

            _connectionManager.AddConnection(ipAddress,Context.ConnectionId);

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var ipAddress = Context.Features.Get<IHttpConnectionFeature>()?.RemoteIpAdd‌​ress.ToString();

            _connectionManager.RemoveConnection(ipAddress);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
