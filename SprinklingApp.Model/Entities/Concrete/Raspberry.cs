using SprinklingApp.Model.Entities.Abstract;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class Raspberry : BaseEntity
    {
        public virtual string IPAddress { get; set; }
        public virtual string Name { get; set; }


    }
}
