using AuthUsers.Aplication.DTOs.Employee;
using MediatR;

namespace AuthUsers.Aplication.Query.Employee;

public class GetAllEmployeeAdminQuery : IRequest<IEnumerable<EmployeeAdminDTO>>
{
    public required int Page {  get; set; }
}
