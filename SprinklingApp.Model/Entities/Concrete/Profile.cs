using SprinklingApp.Model.Entities.Abstract;
using SprinklingApp.Model.Enums;
using System.Collections.Generic;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class Profile:BaseEntity
    {
        public virtual ICollection<Group> Groups { get; set; }
        public virtual Days DayOfWeek { get; set; }
        public virtual int StartHour { get; set; }
    }
}
