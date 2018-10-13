namespace SprinklingApp.Model.DTOs.Abstract
{
    public abstract class BaseModelDTO:IModelDTO
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
    }
}
