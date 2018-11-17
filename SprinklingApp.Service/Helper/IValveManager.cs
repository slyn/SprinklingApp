using SprinklingApp.Model.ApiResponseModels.Concrete;

namespace SprinklingApp.Service.Helper
{
    public interface IValveManager
    {
        MapResponseModel GetMapResponse();
        bool IsValveOpened(long valveId);
        void Opened(long valveId, string ipAddress);
        void Closed(long valveId);
    }
}
