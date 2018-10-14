using SprinklingApp.Model.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprinklingApp.Model.Entities.Concrete
{
    public class ProfileGroupMapping: BaseEntity
    {
        [ForeignKey("Profile")]
        public virtual long ProfileId { get; set; }
        [ForeignKey("Group")]
        public virtual long GroupId { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual Group Group { get; set; }
    }
}
