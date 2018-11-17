using System;
using System.Collections.Generic;
using SprinklingApp.Model.ApiRequestModels.Abstract;

namespace SprinklingApp.Model.ApiRequestModels.Concrete
{
    public class InsertValveOperationRequestModel : BaseOperationRequest
    {
        public IEnumerable<long> Valves { get; set; }
    }
}
