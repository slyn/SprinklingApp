using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprinklingApp.Model.Entities.Abstract {

    public class BaseEntity : Entity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool IsActive { get; set; }
    }

}