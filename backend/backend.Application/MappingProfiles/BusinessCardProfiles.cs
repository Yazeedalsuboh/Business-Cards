using AutoMapper;
using backend.Application.DTOs;
using Domain.Entities;

namespace backend.Application.MappingProfiles
{
    class BusinessCardProfiles : Profile
    {
        public BusinessCardProfiles()
        {
            CreateMap<BusinessCard, BusinessCardDto>();

            CreateMap<AddBusinessCardDto, BusinessCard>();

            CreateMap<BusinessCardCsvXmlDto, BusinessCard>();


        }
    }
}
