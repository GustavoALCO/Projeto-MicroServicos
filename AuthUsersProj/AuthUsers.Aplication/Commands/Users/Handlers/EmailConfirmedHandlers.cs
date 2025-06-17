using AuthUsers.domain.Interfaces.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Users.Handlers;

public class EmailConfirmedHandlers : IRequestHandler<EmailConfirmedCommands, Unit>
{
    private readonly IUserRepositoryQuery _query;

    private readonly IUserRepositoryCommands _command;

    private readonly ILogger<EmailConfirmedHandlers> _logger;

    public EmailConfirmedHandlers(ILogger<EmailConfirmedHandlers> logger, IUserRepositoryQuery query, IUserRepositoryCommands command)
    {
        _logger = logger;
        _query = query;
        _command = command;
    }

    public async Task<Unit> Handle(EmailConfirmedCommands request, CancellationToken cancellationToken)
    {
        var user = await _query.GetUserIdAsync(request.Id);

        if (user == null) 
            throw new Exception("Id se usuario não encontrado");

        user.EmailConfirmed = true;

        await _command.UpdateUserAync(user);

        return Unit.Value;
    }
}
