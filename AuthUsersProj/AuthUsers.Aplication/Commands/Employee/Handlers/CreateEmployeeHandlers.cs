using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.Aplication.Interfaces;
using AuthUsers.Aplication.Validator.Employee;
using AuthUsers.domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class CreateEmployeeHandlers : IRequestHandler<CreateEmployeeCommands, Unit>
{
    private readonly IEmployeeRepositoryCommands _commands;

    private readonly IEmployeeRepositoryQuery _query;

    private readonly ILogger<CreateEmployeeHandlers> _logger;

    private IValidator<CreateEmployeeCommands> _validator;

    private IPasswordHasher _passwordHasher;

    public CreateEmployeeHandlers(IEmployeeRepositoryCommands commands, IEmployeeRepositoryQuery query, ILogger<CreateEmployeeHandlers> logger, IValidator<CreateEmployeeCommands> validator, IPasswordHasher passwordHasher)
    {
        _commands = commands;
        _query = query;
        _logger = logger;
        _validator = validator;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(CreateEmployeeCommands request, CancellationToken cancellationToken)
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


        //Monta o login do usuario
        string createlogin = $"{request.Nome}.{request.Surnames.Replace(" ", "")}";

        //Busca no banco de dados se Exite ja algum funcionario com o mesmo login
        var existingLogins = await _query.GetEmployeesLoginAsync(createlogin);

        //Se caso Tiver ja um com o mesmologin ele entra no If 
        if (existingLogins.Any())
        {
            //Buscar os numeros dos funcionarios existentes 
            var numbers = existingLogins
            .Select(l =>
            {
                //Busca a quantidade de caracteres que possui os loguins 
                var suffix = l.Login.Substring(createlogin.Length);
                //retorna os numeros ou 0 se nao possuir
                return int.TryParse(suffix, out int n) ? n : 0;
            })
            .ToList();

            //passa apenas o numero maior para maxNumber
            int maxNumber = numbers.Max();

            //define para createlogin o maior numero +1
            createlogin += maxNumber + 1;
        }

        //Passa as variaveis para criar um Usuario
        var user = new domain.Entities.Employee
        {
            IdEmployee = Guid.NewGuid(),
            Nome = request.Nome,
            Surnames = request.Surnames,
            Login = createlogin,
            HashPassword = request.HashPassword,
            IsActive = true,
            Position = request.Position,
            Audits = new List<AuditLog>
            {
                new AuditLog
                {
                    Id = request.CreateById,
                    Action = "Create",
                    PerformedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)),
                    ChangesJson = request.json
                }
            }
        };

        user.HashPassword = _passwordHasher.CreateHash(user, request.HashPassword);

        //Chama a Interface para adicionar o funcionario no banco de dados
        await _commands.CreateEmployeeAsync(user);

        return Unit.Value;
    }
}
