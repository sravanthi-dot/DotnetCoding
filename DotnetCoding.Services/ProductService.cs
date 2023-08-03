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

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _unitOfWork.Products.DeleteProduct(productId);
            return product;
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts(string productName, DateTime requestedDate, decimal productPrice)
        {
            var productDetailsList = await _unitOfWork.Products.GetAll(productName, requestedDate, productPrice);
            return productDetailsList;
        }

        public async Task<IEnumerable<PendingApprovalQueue>> GetPendingApprovalProducts()
        {
            var productDetailsList = await _unitOfWork.Products.GetPendingApprovalProducts();
            return productDetailsList;
        }

        public async Task<bool> UpdateProduct(ProductDetails productDetails)
        {
            var product = await _unitOfWork.Products.UpdateProduct(productDetails);
            return product;
        }
    }
}
