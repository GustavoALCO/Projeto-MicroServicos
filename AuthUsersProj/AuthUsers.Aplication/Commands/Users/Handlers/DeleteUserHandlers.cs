using AuthUsers.domain.Interfaces.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Users.Handlers;

public class DeleteUserHandlers : IRequestHandler<DeleteUserCommands, Unit>
{
    private readonly IUserRepositoryQuery _query;

    private readonly IUserRepositoryCommands _command;

    private readonly ILogger<DeleteUserHandlers> _logger;

    public DeleteUserHandlers(ILogger<DeleteUserHandlers> logger, IUserRepositoryQuery query, IUserRepositoryCommands command)
    {
        _logger = logger;
        _query = query;
        _command = command;
    }

    public async Task<Unit> Handle(DeleteUserCommands request, CancellationToken cancellationToken)
    {
        var user = await _query.GetUserIdAsync(request.Id);

        if (user == null)
            throw new Exception("Id se usuario não encontrado");

        await _command.DeleteUserAsync(user);

        return Unit.Value;
    }
}
