using AuthUsers.domain.Interfaces.Adress;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Adress.Handlers;

public class UpdateAdressHandlers : IRequestHandler<UpdateAdressCommands, Unit>
{
    private readonly IAdressRepositoryCommands _adressRepositoryCommands;
    private readonly IAdressRepositoryQuery _adressRepositoryQuery;
    private readonly ILogger<UpdateAdressHandlers> _logger;
    public UpdateAdressHandlers(IAdressRepositoryCommands adressRepositoryCommands, IAdressRepositoryQuery adressRepositoryQuery, ILogger<UpdateAdressHandlers> logger)
    {
        _adressRepositoryCommands = adressRepositoryCommands;
        _adressRepositoryQuery = adressRepositoryQuery;
        _logger = logger;
    }
    public async Task<Unit> Handle(UpdateAdressCommands request, CancellationToken cancellationToken)
    {
        var adress = await _adressRepositoryQuery.GetByIdAsync(request.Id);

        if (adress == null)
        {
            _logger.LogDebug("Endereço não encontrado com o ID: {Id}", request.Id);
            throw new Exception("Endereço não encontrado."); 
        }

        adress.HomeAdress = request.HomeAdress;
        adress.Number = request.Number;
        adress.Complement = request.Complement;
        adress.Cep = request.Cep;

        await _adressRepositoryCommands.UpdateAsync(adress);

        _logger.LogInformation("Endereço com ID {Id} atualizado com sucesso.", adress);

        return Unit.Value;
    }
}
