using SprinklingApp.Model.Entities.Abstract;
using System;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class OperationItem : BaseEntity
    {

        // requested or planned operation info
        public virtual  DateTime PlannedDate { get; set; }

        // occured operation info
        public virtual DateTime? OccuredDate { get; set; }
    }
}
