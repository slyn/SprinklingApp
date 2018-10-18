using Newtonsoft.Json;
using SprinklingApp.Model.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class Valve : BaseEntity
    {
        public virtual int ActivatePin { get; set; }
        public virtual int DisabledPin { get; set; }
        public virtual float Pressure { get; set; }
        public virtual string Name { get; set; }

        [ForeignKey("Raspberry")]
        public virtual long RaspberryId { get; set; }

        public virtual Raspberry Raspberry { get; set; }
    }
}
