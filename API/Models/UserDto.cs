namespace API.Models;

public class UserDto
{
    public string? Name { get; set; }
    public int Gender { get; set; }
    public DateTime? Birthday { get; set; }
    public bool Active { get; set; }
}