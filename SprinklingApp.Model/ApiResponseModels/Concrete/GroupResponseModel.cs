using SprinklingApp.Model.ApiResponseModels.Abstract;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;
using System.Collections.Generic;

namespace SprinklingApp.Model.ApiResponseModels.Concrete
{
    public class GroupResponseModel : IApiResponse
    {
        public long Id { get; set; }

        public virtual string Name { get; set; }
        public virtual int Duration { get; set; }
        public virtual TimeUnit Unit { get; set; }
        public virtual IEnumerable<Valve> Valves { get; set; }

    }
}
