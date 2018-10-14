using SprinklingApp.Model.Entities.Abstract;
using System.Collections.Generic;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class Raspberry : BaseEntity
    {
        public virtual string IPAddress { get; set; }
        public virtual string Name { get; set; }

        public virtual IEnumerable<Valve> Valves { get; set; }
    }
}
