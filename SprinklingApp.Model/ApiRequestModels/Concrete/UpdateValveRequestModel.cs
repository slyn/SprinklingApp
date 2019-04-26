using SprinklingApp.Model.ApiRequestModels.Abstract;

namespace SprinklingApp.Model.ApiRequestModels.Concrete {

    public class UpdateValveRequestModel : BaseValveRequest {
        public virtual long ValveId { get; set; }
    }

}