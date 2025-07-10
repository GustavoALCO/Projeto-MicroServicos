using AuthUsers.Aplication.DTOs.Adress;
using AuthUsers.Aplication.DTOs.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AuthUsers.Aplication.Query.Users.Handler;

public class GetUserIdHandler : IRequestHandler<GetUserIdQuery, UsersDTO>
{
    private readonly domain.Interfaces.Users.IUserRepositoryQuery _userRepositoryQuery;

    private readonly domain.Interfaces.Adress.IAdressRepositoryQuery _adressRepositoryQuery;

    private readonly ILogger<GetUserIdHandler> _logger;

    public GetUserIdHandler(domain.Interfaces.Users.IUserRepositoryQuery userRepositoryQuery, ILogger<GetUserIdHandler> logger, domain.Interfaces.Adress.IAdressRepositoryQuery adressRepositoryQuery)
    {
        _userRepositoryQuery = userRepositoryQuery;
        _logger = logger;
        _adressRepositoryQuery = adressRepositoryQuery;
    }
    public async Task<UsersDTO> Handle(GetUserIdQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepositoryQuery.GetUserIdAsync(request.Id);

        //Verificando se o usuário foi encontrado
        if (users == null)
        {
            _logger.LogWarning("User com o ID {Id} não encontrado.", request.Id);
            throw new Exception($"User com o ID {request.Id} não encontrado.");
        }

        var adress = await _adressRepositoryQuery.GetAllAsync(request.Id, 5);

        //Comunicando que o usuário foi encontrado com sucesso
        _logger.LogInformation("Usuario encontrado com o ID {Id}.", request.Id);

        //Verificando se o usuário tem endereço cadastrado
        List<AdressDTO> adressDTOs = new List<AdressDTO>();

        // Percorrendo os endereços do usuário
        foreach (var item in adress)
        {
            //Criando o DTO de endereço
            adressDTOs.Add(new AdressDTO
            {
                Id = item.Id,
                IdUser = item.IdUser,
                HomeAdress = item.HomeAdress,
                Number = item.Number,
                Complement = item.Complement,
                Cep = item.Cep             
            });
        }

        //Criando o DTO de usuário
        UsersDTO usersDTO = new UsersDTO
        {
            IdUser = users.IdUser,
            Name = users.Name,
            Surname = users.Surname,
            Email = users.Email,
            PhoneNumber = users.PhoneNumber,
            IsValid = users.IsValid,
            EmailConfirmed = users.EmailConfirmed,
            Adress = adressDTOs.ToList(),
        };

        //retorna o dados do usuário encontrado
        return usersDTO;
    }
}