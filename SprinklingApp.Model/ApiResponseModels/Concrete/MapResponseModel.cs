using System.Collections.Generic;
using SprinklingApp.Model.ApiResponseModels.Abstract;

namespace SprinklingApp.Model.ApiResponseModels.Concrete
{
    public class MapResponseModel:IApiResponse
    {
        public IEnumerable<RaspberryMapItem> Raspberry { get; set; }
    }
}
