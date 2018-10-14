﻿using SprinklingApp.Model.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class ValveGroupMapping : BaseEntity
    {
        [ForeignKey("Valve")]
        public virtual long ValveId { get; set; }
        [ForeignKey("Group")]
        public virtual long GroupId { get; set; }

        public virtual Valve Valve { get; set; }
        public virtual Group Group { get; set; }
    }
}
