using System.Collections.Generic;
using SprinklingApp.Model.ApiResponseModels.Abstract;
using SprinklingApp.Model.Entities.Concrete;

namespace SprinklingApp.Model.ApiResponseModels.Concrete {

    public class RaspberryResponseModel : IApiResponse {
        public long Id { get; set; }

        public virtual string IPAddress { get; set; }
        public virtual string Name { get; set; }

        public virtual IEnumerable<Valve> Valves { get; set; }
    }

}