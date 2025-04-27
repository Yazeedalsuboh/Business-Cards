using Application.DTOs;
using backend.Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBusinessCardService
    {
        Task<IEnumerable<BusinessCardDto>> GetAllBusinessCardsAsync();

        Task<BusinessCard> AddBusinessCardAsync(AddBusinessCardDto addBusinessCardDto);

        Task<BusinessCard> DeleteBusinessCardAsync(int id);

        Task<FileContentResult> ExportToCsvAsync(int id);
        Task<FileContentResult> ExportToXmlAsync(int id);

    }
}
