using MediatR;

namespace AuthUsers.Aplication.Query.Users;

public class GetUserEmailQuery : IRequest<domain.Entities.Users?>
{
    public string Email { get; set; }

}