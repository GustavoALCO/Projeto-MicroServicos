using AuthUsers.Aplication.DTOs.Employee;
using MediatR;

namespace AuthUsers.Aplication.Query.Employee;

public class GetAllEmployeeQuery : IRequest<IEnumerable<EmployeeDTO>>
{
    public required int Page { get; set; }
}
