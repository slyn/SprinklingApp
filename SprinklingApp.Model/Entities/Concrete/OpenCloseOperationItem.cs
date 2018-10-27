using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class OpenCloseOperationItem : OperationItem
    {
        [ForeignKey("Raspberry")]
        public virtual long ValveId { get; set; }

        public virtual Valve Valve { get; set; }

        public virtual Reason OpenCloseReason { get; set; }


    }
}
