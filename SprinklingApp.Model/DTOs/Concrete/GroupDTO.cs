﻿using SprinklingApp.Model.DTOs.Abstract;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;
using System.Collections.Generic;

namespace SprinklingApp.Model.DTOs.Concrete
{
    public class GroupDTO:BaseModelDTO
    {
        public GroupDTO()
        {
            this.Valves = new List<Valve>();
        }
        public virtual string Name { get; set; }
        public virtual int Duration { get; set; }
        public virtual TimeUnit Unit { get; set; }

        public virtual IEnumerable<Valve> Valves { get; set; }

    }
}
