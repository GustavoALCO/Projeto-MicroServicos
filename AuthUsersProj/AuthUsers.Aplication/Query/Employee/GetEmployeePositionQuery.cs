using MediatR;

namespace AuthUsers.Aplication.Query.Employee;

public class GetEmployeePositionQuery : IRequest<IEnumerable<AuthUsers.domain.Entities.Employee?>>
{
    public string Position { get; set; }
    public int Page { get; set; }
    
}