using MeatManager.API.Resources;
using MeatManager.Service.DTOs;
using MeatManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeatManager.API
{
    [ApiController]
    [Route("compradores")]
    public class BuyersController : ControllerBase
    {
        private readonly IBuyerService buyerService;

        public BuyersController(IBuyerService buyerService)
        {
            this.buyerService = buyerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await buyerService.GetAllAsync();
                if (!result.Success)
                    return BadRequest(result.Errors ?? new[] { result.Message });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Messages.UnexpectedError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await buyerService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create([FromBody] BuyerDto dto)
        {
            try
            {
                var result = await buyerService.CreateAsync(dto);
                if (!result.Success)
                    return BadRequest(result.Errors ?? new[] { result.Message });

                return StatusCode(StatusCodes.Status201Created, result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Messages.UnexpectedError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BuyerDto dto)
        {
            try
            {
                var result = await buyerService.UpdateAsync(id, dto);
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
                var result = await buyerService.DeleteAsync(id);
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
