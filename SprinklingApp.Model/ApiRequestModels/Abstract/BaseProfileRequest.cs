using System.Collections.Generic;
using SprinklingApp.Model.Enums;

namespace SprinklingApp.Model.ApiRequestModels.Abstract {

    public abstract class BaseProfileRequest : IApiRequest {
        public virtual string Name { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public virtual int StartHour { get; set; }
        public virtual int StartMinute { get; set; }

        public virtual IEnumerable<long> GroupIdList { get; set; }
    }

}