using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BusinessCardRepository : IBusinessCardRepository
    {
        private readonly AppDbContext _context;

        public BusinessCardRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BusinessCard>> GetAllAsync()
        {
            return await _context.businessCards.ToListAsync();
        }

        public async Task<BusinessCard> AddAsync(BusinessCard businessCard)
        {
            await _context.businessCards.AddAsync(businessCard);
            await _context.SaveChangesAsync();
            return businessCard;
        }
    }
}
