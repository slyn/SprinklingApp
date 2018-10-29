using System.Collections.Generic;

namespace SprinklingApp.Service.Helper
{
    public interface IConnectionManager
    {
        bool IsConnectedIp(string ipAddress);
        void AddConnection(string ipAddress, string connectionId);
        void RemoveConnection(string ipAddress);
        string GetConnectionByIp(string ipAddress);
        List<string> ConnectedIpAddresses();
    }
}
