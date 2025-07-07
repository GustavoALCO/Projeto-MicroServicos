using AuthUsers.Aplication.DTOs.Users;
using MediatR;

namespace AuthUsers.Aplication.Query.Users;

public class GetUserIdQuery : IRequest<UsersDTO>
{
    public Guid Id { get; set; }
    
    public int Page  { get; set; }
}
