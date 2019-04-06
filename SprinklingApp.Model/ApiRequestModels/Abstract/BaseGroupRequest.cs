using System.Collections.Generic;
using SprinklingApp.Model.Enums;

namespace SprinklingApp.Model.ApiRequestModels.Abstract {

    public abstract class BaseGroupRequest : IApiRequest {
        public virtual string Name { get; set; }
        public virtual IEnumerable<long> ValveIdList { get; set; }
        public virtual int Duration { get; set; }
        public virtual TimeUnit Unit { get; set; }
    }

}