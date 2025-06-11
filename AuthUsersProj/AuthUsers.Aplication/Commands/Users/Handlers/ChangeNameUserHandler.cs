using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.domain.Interfaces.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Users.Handlers;

public class ChangeNameUserHandler : IRequestHandler<ChangeNameUserCommands, Unit>
{

    private readonly ILogger _logger;

    private readonly IUserRepositoryCommands _commands;

    private readonly IUserRepositoryQuery _query;

    public ChangeNameUserHandler(IUserRepositoryQuery query, IUserRepositoryCommands commands, ILogger logger)
    {
        _query = query;
        _commands = commands;
        _logger = logger;
    }

    public async Task<Unit> Handle(ChangeNameUserCommands request, CancellationToken cancellationToken)
    {
        var user = await _query.GetUserIdAsync(request.Id);

        if (user == null) 
            throw new ArgumentNullException(nameof(user));

        user.Name = request.Name;

        user.Surname = request.Surname;

        await _commands.UpdateUserAync(user);

        _logger.LogInformation("Nome do Usuario Atualizado com sucesso");

        return Unit.Value;
    }
}
