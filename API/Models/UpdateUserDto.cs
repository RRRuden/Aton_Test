using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class UpdateUserDto : PatchDtoBase
{
    [RegularExpression("^[a-zA-ZА-Яа-я]*$", ErrorMessage = "login must consist of latin and russian letters")]
    public string? Name { get; set; }

    [Range(0, 2)] public int Gender { get; set; }

    public DateTime? Birthday { get; set; }
}