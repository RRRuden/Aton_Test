using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[Index(nameof(NewLogin), IsUnique = true)]
public class UpdateLoginDto
{
    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "login must consist of latin letters and numbers")]
    public string OldLogin { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "login must consist of latin letters and numbers")]
    public string NewLogin { get; set; }
}