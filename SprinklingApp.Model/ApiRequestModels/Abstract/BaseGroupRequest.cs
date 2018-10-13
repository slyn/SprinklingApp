using SprinklingApp.Model.Enums;
using System.Collections.Generic;

namespace SprinklingApp.Model.ApiRequestModels.Abstract
{
    public abstract class BaseGroupRequest : IApiRequest
    {
        public virtual string Name { get; set; }
        public virtual IEnumerable<long> ValveIdList { get; set; }
        public virtual int Duration { get; set; }
        public virtual TimeUnit Unit { get; set; }
    }
}
