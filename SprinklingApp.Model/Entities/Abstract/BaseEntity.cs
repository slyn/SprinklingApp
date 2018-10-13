namespace SprinklingApp.Model.Entities.Abstract
{
    public class BaseEntity : Entity
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
    }
}
