using SprinklingApp.Model.ApiRequestModels.Abstract;
using System.Collections.Generic;

namespace SprinklingApp.Model.ApiRequestModels.Concrete
{
    public class InsertGroupOperationRequestModel: BaseOperationRequest
    {
        public IEnumerable<long> Groups { get; set; }
    }
}
