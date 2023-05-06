using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(Login), IsUnique = true)]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "login must consist of latin letters and numbers")]
    public string Login { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "login must consist of latin letters and numbers")]
    public string Password { get; set; }

    [RegularExpression("^[a-zA-ZА-Яа-я]*$", ErrorMessage = "login must consist of latin and russian letters")]
    public string Name { get; set; }

    [Range(0, 2)] public int Gender { get; set; }

    public DateTime? Birthday { get; set; }

    [Required] [DefaultValue(false)] public bool Admin { get; set; }

    [Required] public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? RevokedOn { get; set; }
    public string? RevokedBy { get; set; }
}