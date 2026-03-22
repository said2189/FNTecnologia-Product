using Application.Commands;
using Application.DTOs;
using Application.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FnTecnologia.Heiner.Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(new CreateProductCommand(dto));

                if (!result.IsSuccess)
                    return BadRequest(new { message = result.Error });

                return Ok(new { product = result.Value, message = "Producto creado correctamente." });
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.LogWarning("Errores de validación al crear producto: {Errors}", string.Join(", ", errors));
                return BadRequest(new { errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear producto");
                return StatusCode(500, new { message = "Error en el sistema, inténtelo más tarde." });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(new UpdateProductCommand(id, dto));

                if (!result.IsSuccess)
                    return NotFound(new { message = result.Error });

                return Ok(new { product = result.Value, message = "Producto actualizado correctamente." });
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.LogWarning("Errores de validación al actualizar producto {Id}: {Errors}", id, string.Join(", ", errors));
                return BadRequest(new { errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar producto {Id}", id);
                return StatusCode(500, new { message = "Error en el sistema, inténtelo más tarde." });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteProductCommand(id));

                if (!result.IsSuccess)
                    return NotFound(new { message = result.Error });

                return Ok(new { deleted = true, message = "Producto eliminado correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar producto {Id}", id);
                return StatusCode(500, new { message = "Error en el sistema, inténtelo más tarde." });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new GetAllProductsQuery());

                if (!result.IsSuccess)
                    return BadRequest(new { message = result.Error });

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al consultar lista de productos");
                return StatusCode(500, new { message = "Error en el sistema, inténtelo más tarde." });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetProductByIdQuery(id));

                if (!result.IsSuccess)
                    return NotFound(new { message = result.Error });

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al consultar producto {Id}", id);
                return StatusCode(500, new { message = "Error en el sistema, inténtelo más tarde." });
            }
        }
    }
}
