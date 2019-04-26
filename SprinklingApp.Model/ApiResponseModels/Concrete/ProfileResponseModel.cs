using System.Collections.Generic;
using SprinklingApp.Model.ApiResponseModels.Abstract;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Model.Enums;

namespace SprinklingApp.Model.ApiResponseModels.Concrete {

    public class ProfileResponseModel : IApiResponse {
        public long Id { get; set; }

        public virtual string Name { get; set; }
        public virtual int StartHour { get; set; }
        public virtual int StartMinute { get; set; }


        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public virtual IEnumerable<Group> Groups { get; set; }
    }

}