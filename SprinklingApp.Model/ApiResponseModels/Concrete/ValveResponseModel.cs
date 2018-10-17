﻿using SprinklingApp.Model.ApiResponseModels.Abstract;

namespace SprinklingApp.Model.ApiResponseModels.Concrete
{
    public class ValveResponseModel : IApiResponse
    {
        public long Id { get; set; }

        public virtual int ActivatePin { get; set; }
        public virtual int DisabledPin { get; set; }
        public virtual float Pressure { get; set; }
        public virtual string Name { get; set; }

        public virtual long RaspberryId { get; set; }
    }
}
