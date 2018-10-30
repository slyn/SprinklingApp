using SprinklingApp.Model.Entities.Abstract;
using System;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class OperationItem : BaseEntity
    {

        // requested or planned operation info
        public virtual  DateTime PlannedDateOpen{ get; set; }
        public virtual  DateTime PlannedDateClose{ get; set; }

        // occured operation info
        public virtual DateTime? OccuredDateOpen { get; set; }
        public virtual DateTime? OccuredDateClose { get; set; }
    }
}
