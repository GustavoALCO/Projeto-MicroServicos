using AdsService.Aplication.Commands.Image;
using AdsService.Aplication.Commands.Product;
using AdsService.Aplication.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdsService.WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductControlller : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<ProductControlller> _logger;

    public ProductControlller(IMediator mediator, ILogger<ProductControlller> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }


    [HttpGet("BuscarTodos")]
    public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsQuery getAllProducts)
    {
        try
        {
            var products = await _mediator.Send(getAllProducts);

            _logger.LogInformation($"{products}");

            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [HttpGet("ID")]
    public async Task<IActionResult> GetProductById([FromQuery] GetByIdAsyncQuery getByIdAsync)
    {
        try
        {
            var products = await _mediator.Send(getByIdAsync);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [HttpGet("AnunciosUser")]
    public async Task<IActionResult> GetAllPrductsUser([FromQuery] GetByIdAsyncQuery getByIdAsync)
    {
        try
        {
            var product = await _mediator.Send(getByIdAsync);
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostProducts([FromBody]
                                            CreateProductCommands createProduct)
    {
        try
        {
            await _mediator.Send(createProduct);

            _logger.LogInformation("Produto criado com sucesso: {Title}", createProduct.Title);

            return Ok("Produto criado com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [Authorize]
    [HttpPatch("Products")]
    public async Task<IActionResult> UpdateProduct([FromBody] PathProductsCommands pathProducts)
    {
        try
        {
            await _mediator.Send(pathProducts);
            _logger.LogInformation("Produto atualizado com sucesso: {Title}", pathProducts.Title);
            return Ok("Produto atualizado com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [Authorize]
    [HttpPatch("Deactivate")]
    public async Task<IActionResult> DeactivateProduct([FromQuery] DeactivateProductsCommands deactivateProducts)
    {
        try
        {
            await _mediator.Send(deactivateProducts);
            
            return Ok("Produto atualizado com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [Authorize]
    [HttpPatch("Activate")]
    public async Task<IActionResult> UpdateProduct([FromQuery] ActivateProductsCommands activateProducts)
    {
        try
        {
            await _mediator.Send(activateProducts);
           
            return Ok("Produto atualizado com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [Authorize]
    [HttpPatch("Images")]
    public async Task<IActionResult> UpdateImages([FromBody] PathImagesCommands pathImages)
    {
        try
        {
            await _mediator.Send(pathImages);
            _logger.LogInformation("Imagens atualizadas com sucesso: {Title}", pathImages.FileName);
            return Ok("Imagens atualizadas com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromQuery] DeleteProductsCommands deleteProducts)
    {
        try
        {
            await _mediator.Send(deleteProducts);
            _logger.LogInformation("Produto deletado com sucesso: {Id}", deleteProducts.IdProduct);
            return Ok("Produto deletado com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }


}