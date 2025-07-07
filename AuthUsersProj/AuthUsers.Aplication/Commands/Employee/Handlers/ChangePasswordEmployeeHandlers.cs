using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.Aplication.Interfaces;
using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.AuditLogs;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class ChangePasswordEmployeeHandlers : IRequestHandler<ChangePasswordCommands, Unit>
{

    private readonly IPasswordHasher _passwordHasher;

    private readonly IEmployeeRepositoryQuery _query;

    private readonly IEmployeeRepositoryCommands _command;

    private readonly IAuditlogsRepositoryCommands _commandsLogs;

    private readonly IValidator<ChangePasswordCommands> _validator;

    private readonly ILogger<ActivateEmployeeHandler> _logger;

    public ChangePasswordEmployeeHandlers(IEmployeeRepositoryCommands command, IEmployeeRepositoryQuery query, IPasswordHasher passwordHasher, IValidator<ChangePasswordCommands> validator, ILogger<ActivateEmployeeHandler> logger, IAuditlogsRepositoryCommands commandsLogs)
    {
        _command = command;
        _query = query;
        _passwordHasher = passwordHasher;
        _validator = validator;
        _logger = logger;
        _commandsLogs = commandsLogs;
    }

    public async Task<Unit> Handle(ChangePasswordCommands request, CancellationToken cancellationToken)
    {
        //Atribui os valores do request para o Validator para fazer uma validacao
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
        {   //se nao for valido coleta os erros do Fluent Validation
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Funcionario: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna NULL 
            throw new ValidationException(validation.Errors);
        }

        //Busco no bando de dados para verificar se existe no banco de dados
        var employee = await _query.GetEmployeeIdAsync(request.Id);

        if (employee == null) 
            throw new ArgumentNullException("Usuario nao Encontrado");

        //gerando uma nova senha hash para o usuario 
        employee.HashPassword = _passwordHasher.CreateHash(employee, request.Password);

        //passando as informacoes do request para ser armazenado
        var log = new AuditLog
        {
            IdLog = Guid.NewGuid(),
            TableName = "Path",
            RecordId = employee.IdEmployee,
            Action = "Create",
            DateLog = DateTimeOffset.UtcNow, // Ajuste para o fuso horário de Brasília
            PerformeBy = $"{request.UpdatebyId} | {employee.Nome} {employee.Surnames}",
            ChangesJson = $"Indisponivel Pela ação do Endpoint"
        };

        //Atualizando o usuario no banco de dados
        await _command.UpdateEmployeeProfile(employee);

        //Criando o log de auditoria no banco de dados
        await _commandsLogs.CreateAuditLog(log);

        return Unit.Value;
    }
}
