using SprinklingApp.Model.DTOs.Abstract;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;
using System.Collections.Generic;

namespace SprinklingApp.Model.DTOs.Concrete
{
    public class ProfileDTO : BaseModelDTO
    {
        public ProfileDTO()
        {
            this.Groups = new List<Group>();
        }
        public virtual string Name { get; set; }
        public virtual Days DayOfWeek { get; set; }
        public virtual int StartHour { get; set; }
        public virtual int StartMinute { get; set; }

        public virtual IEnumerable<Group> Groups { get; set; }

    }
}
