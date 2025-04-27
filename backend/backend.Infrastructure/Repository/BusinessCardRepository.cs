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

        public async Task<BusinessCard?> GetByIdAsync(int id)
        {
            return await _context.businessCards.FindAsync(id);
        }

        public async Task<BusinessCard> DeleteAsync(BusinessCard businessCard)
        {
            if (businessCard == null) throw new ArgumentNullException(nameof(businessCard));

            _context.Remove(businessCard);
            await _context.SaveChangesAsync();
            return businessCard;
        }

    }
}
