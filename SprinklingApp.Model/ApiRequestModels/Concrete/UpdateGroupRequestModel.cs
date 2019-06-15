using SprinklingApp.Model.ApiRequestModels.Abstract;

namespace SprinklingApp.Model.ApiRequestModels.Concrete {

    public class UpdateGroupRequestModel : BaseGroupRequest {
        public virtual long Id { get; set; }
    }

}