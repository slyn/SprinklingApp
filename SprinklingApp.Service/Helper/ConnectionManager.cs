using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SprinklingApp.Service.Helper
{
    public class ConnectionManager:IConnectionManager
    {
        private ConcurrentDictionary<string, string> SlaveConnections { get; }
        public ConnectionManager()
        {
            SlaveConnections = new ConcurrentDictionary<string, string>();
        }

        public bool IsConnectedIp(string ipAddress)
        {
            var result = SlaveConnections.ContainsKey(ipAddress);
            return result;
        }

        public void AddConnection(string  ipAddress, string connectionId)
        {
            var getResult = SlaveConnections.TryGetValue(ipAddress,out string connId);
            if (!getResult)
            {
                // ip raspberry olarak eklenmiş mi ?
                var addResult = SlaveConnections.TryAdd(ipAddress, connectionId);
                if (!addResult)
                    throw new Exception("Bağlantı ekleme işlemi başarısız!");
            }
            else if (getResult && connectionId != connId)
                throw new Exception("Zaten IP adresi ile kurulmuş bir bağlantı var! Yeni kurulan bağlantı eklenemedi.");
        }

        public void RemoveConnection(string ipAddress)
        {
            var getResult = SlaveConnections.TryGetValue(ipAddress, out string connId);
            if (getResult)
            {
                // ip raspberry olarak eklenmiş mi ?
                var removeResult = SlaveConnections.TryRemove(ipAddress,out connId);
                if (!removeResult)
                    throw new Exception("Bağlantı silme işlemi başarısız!");
            }
            else
                throw new Exception("Ip adresi için eklenmiş bir bağlantı bulunamadı!");
        }

        public string GetConnectionByIp(string ipAddress)
        {
            var getResult = SlaveConnections.TryGetValue(ipAddress, out string connectionId);
            if (!getResult)
                throw new Exception("Ip adresi ile kurulmuş bağlantı bulunamadı!");
            return connectionId;
        }

        public List<string> ConnectedIpAddresses()
        {
            var keys = SlaveConnections.Keys.ToList();

            return keys;
        }
    }
}
