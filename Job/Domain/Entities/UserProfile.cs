using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUP { get; set; }

        [Required]
        public string ? Nickname { get; set; }

        [Required]
        [Phone]
        public string ? PhoneNumber { get; set; }

        [Url]
        public string ? Resume { get; set; }

        [Required]
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User ? User { get; set; }
    }
}
