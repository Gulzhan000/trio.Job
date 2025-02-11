using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class UserProfile
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    public string Resume { get; set; }

    public string Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}


