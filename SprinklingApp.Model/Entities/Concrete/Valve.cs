using SprinklingApp.Model.Entities.Abstract;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class Valve : BaseEntity
    {
        public virtual int Port { get; set; }
        public virtual string Name { get; set; }
        public virtual Raspberry Raspery { get; set; }
    }
}
