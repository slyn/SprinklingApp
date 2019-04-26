using SprinklingApp.Model.ApiResponseModels.Abstract;

namespace SprinklingApp.Model.ApiResponseModels.Concrete {

    public class ValveResponseModel : IApiResponse {
        public long Id { get; set; }

        public virtual int EnablePin { get; set; }
        public virtual int DisablePin { get; set; }
        public virtual float Pressure { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsOpen { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual long RaspberryId { get; set; }
    }

}