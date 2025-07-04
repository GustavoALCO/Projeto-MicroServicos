using AuthUsers.domain.Interfaces.Adress;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Adress.Handlers;

public class DeleteAdressHandler : IRequestHandler<DeleteAdressCommands, Unit>
{
    private readonly IAdressRepositoryQuery _adressRepositoryQuery;
    private readonly IAdressRepositoryCommands _adressRepositoryCommands;
    private readonly ILogger<DeleteAdressHandler> _logger;
    public DeleteAdressHandler(IAdressRepositoryQuery adressRepositoryQuery, IAdressRepositoryCommands adressRepositoryCommands, ILogger<DeleteAdressHandler> logger)
    {
        _adressRepositoryQuery = adressRepositoryQuery;
        _adressRepositoryCommands = adressRepositoryCommands;
        _logger = logger;
    }
    public async Task<Unit> Handle(DeleteAdressCommands request, CancellationToken cancellationToken)
    {
        var adress = await _adressRepositoryQuery.GetByIdAsync(request.IdAdress);

        if (adress is null)
        {
            _logger.LogDebug("Endereço não encontrado");
            throw new Exception("Endereço não encontrado");
        } 

        await _adressRepositoryCommands.DeleteAsync(adress);

        _logger.LogInformation($"Endereço com ID {request.IdAdress} foi deletado com sucesso.");

        return Unit.Value;
    }
}
