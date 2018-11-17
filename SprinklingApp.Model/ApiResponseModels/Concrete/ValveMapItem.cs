using SprinklingApp.Model.Enums;

namespace SprinklingApp.Model.ApiResponseModels.Concrete
{
    public class ValveMapItem
    {
        public long ValveId { get; set; }
        public AccessTypes Status { get; set; }
    }
}
