using Application.Interfaces;
using backend.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessCardsController : ControllerBase
    {
        private readonly IBusinessCardService _businessCardService;

        public BusinessCardsController(IBusinessCardService businessCardService)
        {
            _businessCardService = businessCardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBusinessCards()
        {
            var businessCards = await _businessCardService.GetAllBusinessCardsAsync();
            return Ok(businessCards);
        }

        [HttpPost]
        public async Task<IActionResult> AddBusinessCard([FromForm] AddBusinessCardDto addBusinessCardDto)
        {
            try
            {
                var result = await _businessCardService.AddBusinessCardAsync(addBusinessCardDto);
                return Ok(result);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBusinessCard(int id)
        {
            try
            {
                var result = await _businessCardService.DeleteBusinessCardAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
