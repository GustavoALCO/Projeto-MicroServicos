using AuthUsers.Aplication.Interfaces;
using AuthUsers.domain.Interfaces.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace AuthUsers.Aplication.Commands.Users.Handlers;

public class ChangeLoginUserHandlers : IRequestHandler<ChangeLoginUserCommands, Unit>
{
    private readonly ILogger _logger;

    private readonly IUserRepositoryCommands _commands;

    private readonly IUserRepositoryQuery _query;

    private readonly IPasswordHasher _passwordHasher;

    public ChangeLoginUserHandlers(IPasswordHasher passwordHasher, IUserRepositoryQuery query, IUserRepositoryCommands commands, ILogger logger)
    {
        _passwordHasher = passwordHasher;
        _query = query;
        _commands = commands;
        _logger = logger;
    }

    public async Task<Unit> Handle(ChangeLoginUserCommands request, CancellationToken cancellationToken)
    {
        var user = await _query.GetUserEmailAsync(request.Email);
        //verifica se o email existe no banco de dados
        if (user == null)
            throw new Exception($"Usuario com o email {request.Email} não foi encontrado");
       
        //verifica se exite um email para substituir
        if (!string.IsNullOrEmpty(request.ChangeEmail))
        {
            _logger.LogInformation("Email do Usuario Alterado");
            user.Email = request.ChangeEmail;
        }

        //verifica se exite uma nova senha para substituir
        if (!string.IsNullOrEmpty(request.Password))
        {
            _logger.LogInformation("Alterando senha do Usuario");
            user.HashPassword = _passwordHasher.CreateHash(user, request.Password);
        }

        await _commands.UpdateUserAync(user);

        return Unit.Value;
    }
}
