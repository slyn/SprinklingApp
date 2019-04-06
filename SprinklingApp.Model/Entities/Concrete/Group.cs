using SprinklingApp.Model.Entities.Abstract;
using SprinklingApp.Model.Enums;

namespace SprinklingApp.Model.Entities.Concrete {

    public class Group : BaseEntity {
        public virtual string Name { get; set; }
        public virtual int Duration { get; set; }
        public virtual TimeUnit Unit { get; set; }
    }

}