using MeatManager.API.Resources;
using MeatManager.Service.DTOs;
using MeatManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeatManager.API.Controllers
{
    [ApiController]
    [Route("pedidos")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await orderService.GetAllAsync();
                if (!result.Success)
                    return BadRequest(result.Errors ?? new[] { result.Message });

                return Ok(result.Data);
            }
            catch
            {
                return StatusCode(500, Messages.UnexpectedError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await orderService.GetByIdAsync(id);
                if (!result.Success)
                {
                    return result.ErrorCode switch
                    {
                        ServiceError.NotFound => NotFound(result.Message),
                        _ => BadRequest(result.Errors ?? new[] { result.Message })
                    };
                }
                return Ok(result.Data);
            }
            catch
            {
                return StatusCode(500, Messages.UnexpectedError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDto dto)
        {
            try
            {
                var result = await orderService.CreateAsync(dto);
                if (!result.Success)
                    return BadRequest(result.Errors ?? new[] { result.Message });

                return StatusCode(StatusCodes.Status201Created, result.Data);
            }
            catch
            {
                return StatusCode(500, Messages.UnexpectedError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderDto dto)
        {
            try
            {
                var result = await orderService.UpdateAsync(id, dto);
                if (!result.Success)
                    return BadRequest(result.Errors ?? new[] { result.Message });

                return Ok(result.Data);
            }
            catch
            {
                return StatusCode(500, Messages.UnexpectedError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await orderService.DeleteAsync(id);
                if (!result.Success)
                {
                    return result.ErrorCode switch
                    {
                        ServiceError.Conflict => Conflict(result.Message),
                        ServiceError.NotFound => NotFound(result.Message),
                        _ => BadRequest(result.Errors ?? new[] { result.Message })
                    };
                }
                return NoContent();
            }
            catch
            {
                return StatusCode(500, Messages.UnexpectedError);
            }
        }
    }
}
