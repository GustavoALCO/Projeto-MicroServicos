using System.ComponentModel.DataAnnotations;
using System.Text;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.Aplication.Commands.Employee.Handlers;
using AuthUsers.Aplication.Query.Employee;
using AuthUsers.domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthUsers.WebApi.Controllers;

public class EmployeeControllers : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<EmployeeControllers> _logger;

    public EmployeeControllers(IMediator mediator, ILogger<EmployeeControllers> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Employee")]
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

            return Ok();
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

    [Authorize(Roles = "Admin")]
    [HttpDelete("Employee")]
    public async Task<IActionResult> DeleteEmployee([FromBody]
                                                    DeleteEmployeeHandlers
                                                    deleteEmployeeHandlers)
    {

        try
        {
            // Chamar o MediatR 
            var employee = await _mediator.Send(deleteEmployeeHandlers);

            return Ok();
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

    [Authorize(Roles = "Admin")]
    [HttpPatch("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommands changePasswordCommands)
    {
        try
        {
            // Chamar o MediatR 
            var employee = await _mediator.Send(changePasswordCommands);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao alterar senha do funcionário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("ChangeRole")]
    public async Task<IActionResult> PatchChangeRole([FromBody]
                                                      ChangeRoleEmployeeCommands changeRoleEmployee)
    {
        try
        {
            var employee = await _mediator.Send(changeRoleEmployee);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao alterar Cargo do funcionário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("Deactivate")]
    public async Task<IActionResult> DeactivateEmployee([FromBody]DeactivateEmployeeCommands deactivateEmployee)
    {
        try
        {
            var employee = await _mediator.Send(deactivateEmployee);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao alterar Desativar funcionário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("Activate")]
    public async Task<IActionResult> ActivateEmployee([FromBody] ActivateEmployeeCommands activateEmployee)
    {
        try
        {
            var employee = await _mediator.Send(activateEmployee);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao alterar Ativar funcionário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginEmployee([FromBody] LoginEmployeeCommands login)
    {
        try
        {
            var employee = await _mediator.Send(login);
            return Ok(employee);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao fazer o login do funcionário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize]
    [HttpGet("BuscarTodos")]
    public async Task<IActionResult> BuscarTodos([FromBody] GetAllEmployeeQuery getall)
    {
        try
        {
            var employee = await _mediator.Send(getall);
            return Ok(employee);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao Buscar os funcionários");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("BuscarTodosAdmin")]
    public async Task<IActionResult> BuscarTodosAdmin([FromBody] GetAllEmployeeAdminQuery GetAllAdmin)
    {
        try
        {
            var employee = await _mediator.Send(GetAllAdmin);
            return Ok(employee);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao Buscar os funcionário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }


    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("BuscarPorId")]
    public async Task<IActionResult> BuscarPorId([FromBody] GetEmployeeId GetEmployee)
    {
        try
        {
            var employee = await _mediator.Send(GetEmployee);
            return Ok(employee);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao Buscar o funcionário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

}
