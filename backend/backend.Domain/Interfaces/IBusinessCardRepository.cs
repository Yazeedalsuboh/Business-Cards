using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBusinessCardRepository
    {
        Task<IEnumerable<BusinessCard>> GetAllAsync();

        Task<BusinessCard> AddAsync(BusinessCard businessCard);

        Task<BusinessCard?> GetByIdAsync(int id);
        Task<BusinessCard> DeleteAsync(BusinessCard businessCard);

        Task<IEnumerable<BusinessCard>> SearchAsync(string term, string searchString);


    }
}
