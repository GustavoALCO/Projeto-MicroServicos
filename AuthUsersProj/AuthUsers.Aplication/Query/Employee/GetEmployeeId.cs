using MediatR;

namespace AuthUsers.Aplication.Query.Employee;

public class GetEmployeeId : IRequest<AuthUsers.domain.Entities.Employee?>
{
    public required Guid Id { get; set; }
}   