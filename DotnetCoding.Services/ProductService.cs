using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateProduct(ProductDetails productDetails)
        {
            var product = await _unitOfWork.Products.CreateProduct(productDetails);
            return product;
        }

        public Task<bool> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts(long productID, DateTime requestedDate, decimal productPrice)
        {
            var productDetailsList = await _unitOfWork.Products.GetAll(productID, requestedDate, productPrice);
            return productDetailsList;
        }

        public async Task<IEnumerable<PendingApprovalQueue>> GetPendingApprovalProducts()
        {
            var productDetailsList = await _unitOfWork.PendingApprovalQueue.GetPendingApprovalProducts();
            return productDetailsList;
        }

        public Task<bool> UpdateProduct(ProductDetails productDetails)
        {
            throw new NotImplementedException();
        }
    }
}
