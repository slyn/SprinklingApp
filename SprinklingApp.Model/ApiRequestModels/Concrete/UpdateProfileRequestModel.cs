using SprinklingApp.Model.ApiRequestModels.Abstract;

namespace SprinklingApp.Model.ApiRequestModels.Concrete {

    public class UpdateProfileRequestModel : BaseProfileRequest {
        public virtual long Id { get; set; }
    }

}