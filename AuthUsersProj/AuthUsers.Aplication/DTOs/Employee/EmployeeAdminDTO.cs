using AuthUsers.domain.Entities;

namespace AuthUsers.Aplication.DTOs.Employee;

public class EmployeeAdminDTO
{
    public Guid IdEmployee { get; set; }

    public string Nome { get; set; }

    public string Surnames { get; set; }

    public string Login { get; set; }

    public string Position { get; set; }

    public bool IsActive { get; set; }

    public IEnumerable<AuditLog> Audits { get; set; }
}
