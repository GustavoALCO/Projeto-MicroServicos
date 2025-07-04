using MediatR;

namespace AuthUsers.Aplication.Query.Users;

public class GetUserIdQuery : IRequest<domain.Entities.Users?>
{
    public Guid Id { get; set; }
    
}
