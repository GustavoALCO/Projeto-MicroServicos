using System.ComponentModel.DataAnnotations;
using System.Text;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthUsers.WebApi.Controllers;

public class EmployeeControllers : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger _logger;

    public EmployeeControllers(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("CreateEmployee")]
    public async Task<IActionResult> PostEmployee([FromBody] CreateEmployeeCommands createEmployee)
    {
        // Habilita o buffering para permitir múltiplas leituras do corpo da requisição
        HttpContext.Request.EnableBuffering();

        // Lê o corpo da requisição
        HttpContext.Request.Body.Position = 0;
        using var reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var rawJson = await reader.ReadToEndAsync();
        HttpContext.Request.Body.Position = 0; // Resetar a posição do stream

        createEmployee.json = rawJson;
        try
        {
            // Chamar o MediatR 
            var employee = await _mediator.Send(createEmployee);

            return Ok(employee);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar funcionário");
            return StatusCode(500, "Erro interno no servidor.");
        }
        
    }

}
