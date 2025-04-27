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

        public async Task<IEnumerable<BusinessCard>> SearchAsync(string term, string searchString)
        {
            if (string.IsNullOrWhiteSpace(term) || string.IsNullOrWhiteSpace(searchString))
                throw new ArgumentException("Search term and search string must be provided.");

            var query = _context.businessCards.AsQueryable();
            var loweredSearchString = searchString.ToLower();

            switch (term.ToLower())
            {
                case "name":
                    query = query.Where(e => e.Name != null && e.Name.ToLower().Contains(loweredSearchString));
                    break;

                case "dateofbirth":
                    if (DateOnly.TryParse(searchString, out var parsedDate))
                    {
                        query = query.Where(e => e.DateOfBirth == parsedDate);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid date format for DateOfBirth search.");
                    }
                    break;

                case "phone":
                    query = query.Where(e => e.Phone != null && e.Phone.ToLower().Contains(loweredSearchString));
                    break;

                case "gender":
                    query = query.Where(e => e.Gender != null && e.Gender.ToLower() == loweredSearchString);
                    break;

                case "email":
                    query = query.Where(e => e.Email != null && e.Email.ToLower().Contains(loweredSearchString));
                    break;

                default:
                    throw new ArgumentException($"Invalid search term: {term}. Supported terms are Name, DateOfBirth, Phone, Gender, Email.");
            }

            return await query.ToListAsync();
        }



    }
}
