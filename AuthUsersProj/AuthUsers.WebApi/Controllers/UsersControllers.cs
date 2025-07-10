using AuthUsers.Aplication.Commands.Users;
using AuthUsers.Aplication.Query.Employee;
using AuthUsers.Aplication.Query.Users;
using AuthUsers.Aplication.Query.Users.Handler;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AuthUsers.WebApi.Controllers;

[Route("api/[controller]")]
public class UsersControllers : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<UsersControllers> _logger;

    public UsersControllers(IMediator mediator, ILogger<UsersControllers> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [Authorize(Roles = "Admin,Manager,Viewer")]
    [HttpGet("BuscarTodosUsers")]
    public async Task<IActionResult> GetUsers([FromQuery] GetAllUsersQuery getAllUsersHandler)
    {
        try
        {
            // Chamar o MediatR para obter a lista de usuários
            var users = await _mediator.Send(getAllUsersHandler);
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter usuários");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("BuscarPorId")]
    public async Task<IActionResult> GetUserById([FromQuery] GetUserIdQuery getUserByIdQuery)
    {
        try
        {
            // Chamar o MediatR para obter o usuário por ID
            var user = await _mediator.Send(getUserByIdQuery);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter usuário por ID");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("BuscarPeloEmail")]
    public async Task<IActionResult> GetUserByEmail([FromQuery] GetUserEmailQuery getUserByEmailQuery)
    {
        try
        {
            // Chamar o MediatR para obter o usuário por email
            var user = await _mediator.Send(getUserByEmailQuery);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter usuário por email");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [HttpPost("CriarUsuario")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUsersCommands createUserCommand)
    {
        try
        {
            // Chamar o MediatR para criar um novo usuário
            var result = await _mediator.Send(createUserCommand);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar usuário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize(Roles = "Admin,Manager,User")]
    [HttpPatch("MudarLogin")]
    public async Task<IActionResult> ChangeLogin([FromBody] ChangeLoginUserCommands changeLoginUserCommands)
    {
        try
        {
            // Chamar o MediatR para alterar o login do usuário
            var result = await _mediator.Send(changeLoginUserCommands);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao alterar login do usuário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize(Roles = "Admin,Manager,User")]
    [HttpPatch("MudarNome")]
    public async Task<IActionResult> ChangeName([FromBody] ChangeNameUserCommands changeNameUserCommands)
    {
        try
        {
            // Chamar o MediatR para alterar o nome do usuário
            var result = await _mediator.Send(changeNameUserCommands);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao alterar nome do usuário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize(Roles = "Admin,Manager,User")]
    [HttpPatch("MudarNumero")]
    public async Task<IActionResult> ChangeNumber([FromBody] ChangePhoneNumberCommands changeNumberUserCommands)
    {
        try
        {
            // Chamar o MediatR para alterar o número do usuário
            var result = await _mediator.Send(changeNumberUserCommands);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao alterar número do usuário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpDelete("DeletarUsuario")]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommands deleteUserCommands)
    {
        try
        {
            // Chamar o MediatR para deletar o usuário
            var result = await _mediator.Send(deleteUserCommands);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar usuário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [HttpPatch("EmailConfirm")]
    public async Task<IActionResult> ConfirmEmail([FromBody] EmailConfirmedCommands confirmEmailUserCommands)
    {
        try
        {
            // Chamar o MediatR para confirmar o email do usuário
            var result = await _mediator.Send(confirmEmailUserCommands);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao confirmar email do usuário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommands loginUserCommands)
    {
        try
        {
            // Chamar o MediatR para realizar o login do usuário
            var result = await _mediator.Send(loginUserCommands);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao realizar login do usuário");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }
}