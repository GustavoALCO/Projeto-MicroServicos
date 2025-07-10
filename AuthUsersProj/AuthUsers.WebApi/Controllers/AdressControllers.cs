using AuthUsers.Aplication.Commands.Adress;
using AuthUsers.Aplication.Commands.Adress.Handlers;
using AuthUsers.Aplication.Query.Adress;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AuthUsers.WebApi.Controllers;

[Route("api/[controller]")]
public class AdressControllers : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AdressControllers> _logger;
    public AdressControllers(IMediator mediator, ILogger<AdressControllers> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [Authorize]
    [HttpPost("Adress")]
    public async Task<IActionResult> PostAdress([FromBody] CreateAdressCommands createAdress)
    {
        try
        {
            // Chamar o MediatR 
            var adress = await _mediator.Send(createAdress);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar endereço");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize]
    [HttpDelete("Adress")]
    public async Task<IActionResult> DeleteAdress([FromQuery] DeleteAdressHandler deleteAdressHandlers)
    {
        try
        {
            // Chamar o MediatR 
            var adress = await _mediator.Send(deleteAdressHandlers);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar endereço");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize]
    [HttpPut("Adress")]
    public async Task<IActionResult> PutAdress([FromBody] UpdateAdressCommands updateAdressCommands)
    {
        try
        {
            // Chamar o MediatR 
            var adress = await _mediator.Send(updateAdressCommands);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar endereço");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize]
    [HttpGet("BuscarID")]
    public async Task<IActionResult> GetAdressById([FromQuery] GetAdressIdQuery id)
    {
        try
        {
            // Chamar o MediatR 
            var adress = await _mediator.Send(id);
            return Ok(adress);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar endereço por ID");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [Authorize]
    [HttpGet("BuscarTodos")]
    public async Task<IActionResult> GetAllAdresses([FromQuery] GetAllAdressQuery getAllAdressesQuery)
    {
        try
        {
            // Chamar o MediatR 
            var adresses = await _mediator.Send(getAllAdressesQuery);
            return Ok(adresses);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar todos os endereços");
            return StatusCode(500, "Erro interno no servidor.");
        }
    }   
}
