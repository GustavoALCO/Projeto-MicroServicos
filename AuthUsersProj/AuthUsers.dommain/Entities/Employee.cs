using System.ComponentModel.DataAnnotations;

namespace AuthUsers.domain.Entities;

public class Employee
{
    [Key]
    public Guid IdEmployee {  get; set; }

    public required string Nome { get; set; }

    public required string Surnames { get; set; }

    public required string Login {  get; set; }

    public required string HashPassword { get; set; }

    public required string Position { get; set; }

    public required bool IsActive { get; set; }

}
