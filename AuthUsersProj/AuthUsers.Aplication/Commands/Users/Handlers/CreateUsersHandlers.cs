using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.Aplication.Interfaces;
using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.Users;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AuthUsers.Aplication.Commands.Users.Handlers;

public class CreateUsersHandlers : IRequestHandler<CreateUsersCommands, Unit>
{

    private readonly ILogger<ChangeLoginUserHandlers> _logger;

    private readonly IUserRepositoryQuery _query;

    private readonly IUserRepositoryCommands _commands;

    private IValidator<CreateUsersCommands> _validator;

    private IPasswordHasher _passwordHasher;

    private IValidateCPF _validateCPF;

    public CreateUsersHandlers(IPasswordHasher passwordHasher, IValidator<CreateUsersCommands> validator, IUserRepositoryCommands commands, IUserRepositoryQuery query, ILogger<ChangeLoginUserHandlers> logger, IValidateCPF validateCPF)
    {
        _passwordHasher = passwordHasher;
        _validator = validator;
        _commands = commands;
        _query = query;
        _logger = logger;
        _validateCPF = validateCPF;
    }

    public async Task<Unit> Handle(CreateUsersCommands request, CancellationToken cancellationToken)
    {
        //Atribui os valores do requst para o Validator para fazer uma validacao
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
        {   //se nao for valido coleta os erros do Fluent Validation
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Funcionario: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            throw new ValidationException(validation.Errors);
        }

        //se caso o CPF for invalido ele lança a exceção 
        _ = _validateCPF.CPFIsValid(request.Cpf) is not true ? throw new Exception("CPF inválido") : true;

        //busca no banco de dados se ja existe usuario já criado
        var verifyUser = await _query.GetUserCPFAsync(request.Cpf);

        if (verifyUser != null)
            _logger.LogWarning("Tentativa de criar usuário com CPF já existente: {Cpf}", request.Cpf);


        //passa os atributos para o user
        var user = new AuthUsers.domain.Entities.Users
        {
            IdUser = Guid.NewGuid(),
            Name = request.Name?.Trim(),
            Surname = request.Surname?.Trim(),
            Cpf = request.Cpf?.Trim(),
            Email = request.Email?.Trim().ToLowerInvariant(),
            HashPassword = request.HashPassword, // assumindo que já veio criptografada
            IsValid = true,
            PhoneNumber = request.PhoneNumber?.Trim(),
            EmailConfirmed = false,
        };

        
        //Passa o hash para o user
        user.HashPassword = _passwordHasher.CreateHash(user, request.HashPassword);
        

        //Cria o Usuario
        await _commands.CreateUserAsync(user);

        return Unit.Value;
    }
}
