using System;
using System.Collections.Concurrent;
using SprinklingApp.Model.ApiResponseModels.Concrete;

namespace SprinklingApp.Service.Helper
{
    public class ActiveValveManager : IValveManager
    {
        private ConcurrentDictionary<long,string> ActiveValves { get; }

        public ActiveValveManager()
        {
            ActiveValves = new ConcurrentDictionary<long, string>();
        }

        public MapResponseModel GetMapResponse()
        {
            // TODO :: Implement
            throw new NotImplementedException();
        }

        public bool IsValveOpened(long valveId)
        {
            var getResult = ActiveValves.TryGetValue(valveId, out string _);
            return getResult;
        }

        public void Opened(long valveId,string ipAddress)
        {
            var result = ActiveValves.TryAdd(valveId, ipAddress);
            if(!result)
                throw  new Exception("Vana açık olarak işaretlenemedi!");
            
        }

        public void Closed(long valveId)
        {

            var getResult = ActiveValves.TryGetValue(valveId, out string ip);
            if (getResult)
            {
                // vana daha önce eklenmiş mi?
                var removeResult = ActiveValves.TryRemove(valveId, out ip);
                if (!removeResult)
                    throw new Exception("Vana kapatma işlemi başarısız!");
            }
            else
                throw new Exception("Vana zaten kapalı!");
            
        }
    }
}
