using System.Collections.Generic;

namespace SprinklingApp.Model.ApiRequestModels.Abstract {

    public class BaseRaspberryRequest : IApiRequest {
        public virtual string IPAddress { get; set; }
        public virtual string Name { get; set; }

        public virtual IEnumerable<long> ValveIdList { get; set; }
    }

}