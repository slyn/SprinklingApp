﻿using SprinklingApp.Model.ApiRequestModels.Abstract;

namespace SprinklingApp.Model.ApiRequestModels.Concrete {

    public class UpdateRaspberryRequestModel : BaseRaspberryRequest {
        public virtual long Id { get; set; }
    }

}