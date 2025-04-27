using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using backend.Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<FileContentResult> ExportToCsvAsync(int id)
        {
            var businessCard = await _businessCardRepository.GetByIdAsync(id);

            if (businessCard == null)
                throw new KeyNotFoundException($"Business card with ID {id} not found.");

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Name,Email,Phone,Gender,DateOfBirth,Address,Photo");

            csvBuilder.AppendLine($"{businessCard.Name}," +
                                   $"{businessCard.Email}," +
                                   $"{businessCard.Phone}," +
                                   $"{businessCard.Gender}," +
                                   $"{businessCard.DateOfBirth:yyyy-MM-dd}," +
                                   $"\"{businessCard.Address.Replace("\"", "\"\"")}\"," +
                                   $"{(businessCard.Photo != null ? businessCard.Photo : " ")}");

            var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            return new FileContentResult(csvBytes, "text/csv")
            {
                FileDownloadName = $"BusinessCard_{id}.csv"
            };
        }

        public async Task<FileContentResult> ExportToXmlAsync(int id)
        {
            var businessCard = await _businessCardRepository.GetByIdAsync(id);

            if (businessCard == null)
                throw new KeyNotFoundException($"Business card with ID {id} not found.");

            var xmlBuilder = new StringBuilder();
            xmlBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xmlBuilder.AppendLine("<BusinessCard>");
            xmlBuilder.AppendLine($"  <Name>{System.Security.SecurityElement.Escape(businessCard.Name)}</Name>");
            xmlBuilder.AppendLine($"  <Email>{System.Security.SecurityElement.Escape(businessCard.Email)}</Email>");
            xmlBuilder.AppendLine($"  <Phone>{System.Security.SecurityElement.Escape(businessCard.Phone)}</Phone>");
            xmlBuilder.AppendLine($"  <Gender>{System.Security.SecurityElement.Escape(businessCard.Gender)}</Gender>");
            xmlBuilder.AppendLine($"  <DateOfBirth>{businessCard.DateOfBirth:yyyy-MM-dd}</DateOfBirth>");
            xmlBuilder.AppendLine($"  <Address>{System.Security.SecurityElement.Escape(businessCard.Address)}</Address>");
            xmlBuilder.AppendLine($"  <Photo>{System.Security.SecurityElement.Escape(businessCard.Photo ?? "")}</Photo>");
            xmlBuilder.AppendLine("</BusinessCard>");

            var xmlBytes = Encoding.UTF8.GetBytes(xmlBuilder.ToString());

            return new FileContentResult(xmlBytes, "application/xml")
            {
                FileDownloadName = $"BusinessCard_{id}.xml"
            };
        }

    }
}
