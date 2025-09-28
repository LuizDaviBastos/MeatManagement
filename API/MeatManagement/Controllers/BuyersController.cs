using MeatManager.Service.DTOs;
using MeatManager.Service.Interfaces;
using MeatManager.Service.Resources;
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
            var result = await buyerService.GetAllAsync();
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await buyerService.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BuyerDto dto)
        {
            var result = await buyerService.CreateAsync(dto);
            if (!result.Success)
                return BadRequest(result.Errors ?? new[] { result.Message });

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BuyerDto dto)
        {
            var result = await buyerService.UpdateAsync(id, dto);
            if (!result.Success)
                return BadRequest(result.Errors ?? new[] { result.Message });

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await buyerService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Errors ?? new[] { result.Message });

            return NoContent();
        }
    }
}
