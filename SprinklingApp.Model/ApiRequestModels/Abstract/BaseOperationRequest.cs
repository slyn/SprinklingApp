using System;

namespace SprinklingApp.Model.ApiRequestModels.Abstract
{
    public abstract class BaseOperationRequest:IApiRequest
    {
        public virtual DateTime PlannedStart { get; set; }
        public virtual DateTime PlannedEnd { get; set; }
    }
}
