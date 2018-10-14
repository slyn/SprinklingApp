using SprinklingApp.Model.ApiResponseModels.Abstract;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;
using System.Collections.Generic;

namespace SprinklingApp.Model.ApiResponseModels.Concrete
{
    public class ProfileResponseModel : IApiResponse
    {
        public long Id { get; set; }

        public virtual string Name { get; set; }
        public virtual Days DayOfWeek { get; set; }
        public virtual int StartHour { get; set; }
        public virtual int StartMinute { get; set; }

        public virtual IEnumerable<Group> Groups { get; set; }
    }
}
