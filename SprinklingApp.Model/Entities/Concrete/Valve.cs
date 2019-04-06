using System.ComponentModel.DataAnnotations.Schema;
using SprinklingApp.Model.Entities.Abstract;

namespace SprinklingApp.Model.Entities.Concrete {

    public class Valve : BaseEntity {
        public virtual int EnablePin { get; set; }
        public virtual int DisablePin { get; set; }
        public virtual float Pressure { get; set; }
        public virtual string Name { get; set; }

        [ForeignKey("Raspberry")]
        public virtual long RaspberryId { get; set; }

        public virtual Raspberry Raspberry { get; set; }
    }

}