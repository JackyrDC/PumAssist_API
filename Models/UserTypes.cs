using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PumAssist_API.Models
{
    [Table("UserTypes")]
    public class UserTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index(IsUnique = true)]
        public int IdType { get; set; }
        public string type { get; set; }
    }
}
