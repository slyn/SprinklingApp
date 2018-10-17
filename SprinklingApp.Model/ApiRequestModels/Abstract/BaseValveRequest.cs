﻿namespace SprinklingApp.Model.ApiRequestModels.Abstract
{
    public class BaseValveRequest:IApiRequest
    {
        public virtual int ActivatePin { get; set; }
        public virtual int DisabledPin { get; set; }
        public virtual float Pressure { get; set; }
        public virtual string Name { get; set; }

        public virtual long RaspberryId { get; set; }
    }
}
