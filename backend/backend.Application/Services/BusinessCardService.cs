using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using backend.Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BusinessCardService : IBusinessCardService
    {

        private readonly IBusinessCardRepository _businessCardRepository;
        private readonly IMapper _mapper;

        public BusinessCardService(IBusinessCardRepository businessCardRepository, IMapper mapper)
        {
            _businessCardRepository = businessCardRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BusinessCardDto>> GetAllBusinessCardsAsync()
        {
            return _mapper.Map<IEnumerable<BusinessCardDto>>(await _businessCardRepository.GetAllAsync());
        }

        public async Task<BusinessCard> AddBusinessCardAsync(AddBusinessCardDto addBusinessCardDto)
        {
            var businessCard = _mapper.Map<AddBusinessCardDto, BusinessCard>(addBusinessCardDto);

            if (addBusinessCardDto.Photo != null)
            {
                businessCard.Photo = EncodeImageToBase64(addBusinessCardDto.Photo);
            }

            return await _businessCardRepository.AddAsync(businessCard);
        }
        private string EncodeImageToBase64(IFormFile imageFile)
        {
            const long MaxFileSize = 1 * 1024 * 1024;

            if (imageFile == null || imageFile.Length > MaxFileSize)
            {
                return string.Empty;
            }

            using (var memoryStream = new MemoryStream())
            {
                imageFile.CopyTo(memoryStream);
                var imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public async Task<BusinessCard> DeleteBusinessCardAsync(int Id)
        {
            var businessCard = await _businessCardRepository.GetByIdAsync(Id);

            if (businessCard == null)
            {
                throw new KeyNotFoundException($"Business card with ID {Id} not found.");
            }

            return await _businessCardRepository.DeleteAsync(businessCard);
        }

    }
}
