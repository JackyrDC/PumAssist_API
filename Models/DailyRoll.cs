using PumAssist_API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DailyRoll
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idDailyRoll { get; set; }
    public int idRoll { get; set; }
    [ForeignKey("idRoll")]
    public virtual Roll Roll { get; set; }

    public System.DateTime creationDate { get; set; }

    public virtual ICollection<PermanentRoll> studentsList { get; set; }

    public bool IsDeleted { get; set; } = false;
}
