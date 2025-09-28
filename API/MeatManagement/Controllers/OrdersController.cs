using AutoMapper;
using MeatManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeatManager.API.Controllers
{
    namespace MeatManager.API.Controllers
    {
        [ApiController]
        [Route("pedidos")]
        public class OrdersController : ControllerBase
        {
            private readonly IOrderService orderService;

            public OrdersController(IOrderService service, IMapper mapper)
            {
                this.orderService = service;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var orders = orderService.GetAllAsync();
                return Ok(orders);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var order = await orderService.GetByIdAsync(id);
                return Ok(order);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] object order)
            {
                var createResponse = await orderService.CreateAsync(order);
                return Ok(createResponse);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, [FromBody] object request)
            {
                return Ok();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                return Ok();
            }
        }
    }

}
