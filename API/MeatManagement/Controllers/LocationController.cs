using MeatManager.API.Resources;
using MeatManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeatManager.API
{
    [ApiController]
    [Route("estados")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await locationService.GetStatesAsync();
                if (!result.Success)
                    return BadRequest(result.Errors ?? new[] { result.Message });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Messages.UnexpectedError);
            }
        }

        [HttpGet("{stateId}/cidades")]
        public async Task<IActionResult> GetById(Guid stateId)
        {
            try
            {
                var result = await locationService.GetCitiesByStateAsync(stateId);
                if (!result.Success)
                {
                    return result.ErrorCode switch
                    {
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
    }
}
