using backend.Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IBusinessCardService
    {
        Task<IEnumerable<BusinessCardDto>> GetAllBusinessCardsAsync();

        Task<BusinessCard> AddBusinessCardAsync(AddBusinessCardDto addBusinessCardDto);

        Task<BusinessCard> DeleteBusinessCardAsync(int id);

        Task<FileContentResult> ExportToCsvAsync(int id);
        Task<FileContentResult> ExportToXmlAsync(int id);

        Task<List<BusinessCard>> SearchBusinessCardsAsync(string term, string searchString);

        Task<(bool Succeeded, string Message)> ImportFromCsvAsync(IFormFile file);

        Task<(bool Succeeded, string Message)> ImportFromXmlAsync(IFormFile file);

    }
}
