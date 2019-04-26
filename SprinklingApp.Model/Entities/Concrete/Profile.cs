using SprinklingApp.Model.Entities.Abstract;
using SprinklingApp.Model.Enums;

namespace SprinklingApp.Model.Entities.Concrete {

    public class Profile : BaseEntity {
        public virtual int StartHour { get; set; }
        public virtual int StartMinute { get; set; }
        public virtual string Name { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }

}