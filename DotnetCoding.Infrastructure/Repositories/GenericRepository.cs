using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly masterContext _dbContext;

        protected GenericRepository(masterContext context)
        {
            _dbContext = context;
        }

        public virtual async Task<bool> CreateProduct(T entity)
        {
            await _dbContext.AddAsync(entity);
            return true;
        }

       
        public virtual async Task<bool> DeleteProduct(int productId)
        {
            var entity = await _dbContext.Set<ProductDetails>().FindAsync(productId);
            if (entity == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public virtual async Task<IEnumerable<T>> GetAll(string productName, DateTime createdDate, decimal productPrice)
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<PendingApprovalQueue>> GetPendingApprovalProducts()
        {
            return await _dbContext.Set<PendingApprovalQueue>().ToListAsync();
        }

        public virtual async Task<bool> UpdateProduct(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
