using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
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
    }
}
