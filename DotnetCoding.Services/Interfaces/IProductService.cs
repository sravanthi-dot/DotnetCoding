using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetails>> GetAllProducts(string productName, DateTime requestedDate, decimal productPrice);
        Task<IEnumerable<PendingApprovalQueue>> GetPendingApprovalProducts();
        Task<bool> CreateProduct(ProductDetails productDetails);
        Task<bool> UpdateProduct(ProductDetails productDetails);
        Task<bool> DeleteProduct(int productId);

    }
}
