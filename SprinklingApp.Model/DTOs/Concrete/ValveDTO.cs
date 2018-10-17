using SprinklingApp.Model.DTOs.Abstract;
using SprinklingApp.Model.Entities.Concrete;

namespace SprinklingApp.Model.DTOs.Concrete
{
    public class ValveDTO: BaseModelDTO
    {
        public virtual int ActivatePin { get; set; }
        public virtual int DisabledPin { get; set; }
        public virtual float Pressure { get; set; }
        public virtual string Name { get; set; }

        public virtual long RaspberryId { get; set; }

        public virtual Raspberry Raspberry { get; set; }
    }
}
