using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class UpdatePasswordDto
{
    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "login must consist of latin letters and numbers")]
    public string Login { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "login must consist of latin letters and numbers")]
    public string Password { get; set; }
}