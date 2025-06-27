using AuthUsers.domain.Entities;

namespace AuthUsers.Aplication.DTOs.Employee;

public class EmployeeDTO
{
    public Guid IdEmployee { get; set; }

    public required string Nome { get; set; }

    public required string Surnames { get; set; }

    public required string Login { get; set; }

    public required string Position { get; set; }

    public required bool IsActive { get; set; }

}
