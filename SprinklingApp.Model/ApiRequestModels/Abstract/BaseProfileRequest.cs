using SprinklingApp.Model.Enums;
using System.Collections.Generic;

namespace SprinklingApp.Model.ApiRequestModels.Abstract
{
    public abstract class BaseProfileRequest : IApiRequest
    {
        public virtual string Name { get; set; }
        public virtual Days DayOfWeek { get; set; }
        public virtual int StartHour { get; set; }
        public virtual int StartMinute { get; set; }

        public virtual IEnumerable<long> GroupIdList { get; set; }
    }
}
