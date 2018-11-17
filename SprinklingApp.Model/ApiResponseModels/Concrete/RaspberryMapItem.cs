using System.Collections.Generic;

namespace SprinklingApp.Model.ApiResponseModels.Concrete
{
    public class RaspberryMapItem
    {
        public long RaspberryId { get; set; }
        public string IPAddress { get; set; }
        public IEnumerable<ValveMapItem> Valves { get; set; }
    
    }
}
