using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class CreateUserDto
{
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
}