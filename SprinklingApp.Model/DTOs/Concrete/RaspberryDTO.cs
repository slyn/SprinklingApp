using SprinklingApp.Model.DTOs.Abstract;
using SprinklingApp.Model.Entities.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Model.DTOs.Concrete
{
    public class RaspberryDTO: BaseModelDTO
    {
        public RaspberryDTO()
        {
            this.Valves = new List<Valve>();
        }
        public virtual string IPAddress { get; set; }
        public virtual string Name { get; set; }

        public virtual IEnumerable<Valve> Valves { get; set; }
    }
}
