using SprinklingApp.Model.Entities.Abstract;
using SprinklingApp.Model.Enums;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class Profile:BaseEntity
    {
        public virtual Days DayOfWeek { get; set; }
        public virtual int StartHour { get; set; }
        public virtual int StartMinute { get; set; }
        public virtual string Name { get; set; }
    }
}
