using System.ComponentModel.DataAnnotations;

namespace AuthUsers.domain.Entities;

public class Users
{
    [Key]
    public Guid IdUser { get; set; }

    public required string Name { get; set; }

    public required string Surname { get; set; }

    public required string Cpf { get; set; }

    public required string[] PhoneNumber { get; set; }

    public required string Email { get; set; }

    public required string HashPassword { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool IsValid { get; set; }

}
