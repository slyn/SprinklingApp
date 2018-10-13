using SprinklingApp.Model.DTOs.Abstract;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;
using System.Collections.Generic;

namespace SprinklingApp.Model.DTOs.Concrete
{
    public class GroupDTO:BaseModelDTO
    {
        public virtual string Name { get; set; }
        public virtual IEnumerable<Valve> ValveList { get; set; }
        public virtual int Duration { get; set; }
        public virtual TimeUnit Unit { get; set; }
    }
}
