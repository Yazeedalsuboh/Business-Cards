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

        [HttpGet("export/csv/{id}")]
        public async Task<IActionResult> ExportToCsv(int id)
        {
            try
            {
                var fileResult = await _businessCardService.ExportToCsvAsync(id);
                return File(fileResult.FileContents, fileResult.ContentType, fileResult.FileDownloadName);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error exporting CSV: {ex.Message}");
            }
        }

        [HttpGet("export/xml/{id}")]
        public async Task<IActionResult> ExportToXml(int id)
        {
            try
            {
                var fileResult = await _businessCardService.ExportToXmlAsync(id);
                return File(fileResult.FileContents, fileResult.ContentType, fileResult.FileDownloadName);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error exporting XML: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetBusinessCardsByFilters([FromQuery] string term, [FromQuery] string searchString)
        {
            try
            {
                var businessCards = await _businessCardService.SearchBusinessCardsAsync(term, searchString);
                return Ok(businessCards);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
