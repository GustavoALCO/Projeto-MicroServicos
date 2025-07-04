using AuthUsers.domain.Interfaces.Adress;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Adress.Handlers;

public class CreateAdressHandlers : IRequestHandler<CreateAdressCommands, Unit>
{
    private readonly IAdressRepositoryCommands _adressRepositoryCommands;
    private readonly ILogger<CreateAdressHandlers> _logger;

    public CreateAdressHandlers(IAdressRepositoryCommands adressRepositoryCommands, ILogger<CreateAdressHandlers> logger)
    {
        _adressRepositoryCommands = adressRepositoryCommands;
        _logger = logger;
    }
    public async Task<Unit> Handle(CreateAdressCommands request, CancellationToken cancellationToken)
    {
        var adress = new domain.Entities.Adress
        {
            IdUser = request.IdUser,
            HomeAdress = request.HomeAdress,
            Number = request.Number,
            Complement = request.Complement,
            Cep = request.Cep
        };

        await _adressRepositoryCommands.AddAsync(adress);

        _logger.LogInformation("Endereço criado com sucesso para o usuário com ID {IdUser}.", adress);

        return Unit.Value;
    }
}

