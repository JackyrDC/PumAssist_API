using PumAssist_API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PumAssist_API.Models
{
    [Table("DailyRoll")]
    public class DailyRoll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idDailyRoll { get; set; }

        [ForeignKey("Roll")]
        public int idRoll { get; set; }

        public System.DateTime creationDate { get; set; }

        public virtual Roll roll { get; set; }

        public virtual ICollection<PermanentRoll> studentsList { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
