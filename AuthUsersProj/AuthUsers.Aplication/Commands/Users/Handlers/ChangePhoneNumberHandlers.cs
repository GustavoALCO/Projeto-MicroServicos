using AuthUsers.domain.Interfaces.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace AuthUsers.Aplication.Commands.Users.Handlers;

public class ChangePhoneNumberHandlers : IRequestHandler<ChangePhoneNumberCommands, Unit>
{
    private readonly ILogger _logger;

    private readonly IUserRepositoryCommands _commands;

    private readonly IUserRepositoryQuery _query;

    public ChangePhoneNumberHandlers(IUserRepositoryQuery query, IUserRepositoryCommands commands, ILogger logger)
    {
        _query = query;
        _commands = commands;
        _logger = logger;
    }
    public async Task<Unit> Handle(ChangePhoneNumberCommands request, CancellationToken cancellationToken)
    {
        var user = await _query.GetUserIdAsync(request.Id);

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        user.PhoneNumber = request.Number; 

        await _commands.UpdateUserAync(user);

        _logger.LogInformation("Numero do Usuario Atualizado com sucesso");

        return Unit.Value;
    }
}
