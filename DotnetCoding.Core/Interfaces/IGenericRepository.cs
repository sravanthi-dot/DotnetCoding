using DotnetCoding.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(string productName, DateTime requestedDate, decimal productPrice);
        Task<IEnumerable<PendingApprovalQueue>> GetPendingApprovalProducts();

        Task<bool> CreateProduct(T entity);

        Task<bool> UpdateProduct(T entity);

        Task<bool> DeleteProduct(int productId);

    }
}
