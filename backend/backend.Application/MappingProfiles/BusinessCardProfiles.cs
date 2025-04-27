using Application.DTOs;
using AutoMapper;
using backend.Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
    class BusinessCardProfiles : Profile
    {
        public BusinessCardProfiles()
        {
            CreateMap<BusinessCard, BusinessCardDto>();

            CreateMap<AddBusinessCardDto, BusinessCard>();
        }
    }
}
