using Application.DTOs;
using backend.Application.DTOs;
using Domain.Entities;
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

    }
}
