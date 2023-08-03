using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductDetails>, IProductRepository
    {
        private readonly masterContext _context;
        //protected readonly ILogger _logger;

        public ProductRepository(masterContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            //_logger = logger;
        }

        public override async Task<IEnumerable<ProductDetails>> GetAll(string productName, DateTime createdDate, decimal productPrice)
        {
            try
            {
                var products = await _context.MasProducts
                    .Where(c => c.IsActive == true && c.ProductPrice == productPrice 
                            && c.CreatedDate == createdDate && c.ProductName == productName)
                    .OrderByDescending(a => a.CreatedDate).ToListAsync();
            }
            catch (Exception ex)
            {
                ////_logger.LogError(ex, "{Repo} All function error", typeof(ProductRepository));
            }
            return new List<ProductDetails>();

        }
        //2.	User can search using Product name, Price range and posted date range. 
        public override async Task<IEnumerable<PendingApprovalQueue>> GetPendingApprovalProducts()
        {
            List<PendingApprovalQueue> result = new List<PendingApprovalQueue>();
            try
            {
                var products = await _context.QueueApprovals
                    .Join(_context.MasProducts,
                c => c.ProductId,
                cm => cm.ProductId,
                (c, cm) => new
                {
                    ProductId = cm.ProductId,
                    ProductName = cm.ProductName,
                    ProductPrice = cm.ProductPrice,
                    Status = c.Status,
                    RequestedDate = c.RequestedDate,
                    RequestedReason = c.RequestedReason,

                })
                .Where(a => a.Status == "W" )
                .OrderByDescending(a => a.RequestedDate)
                .ToListAsync();

                
                PendingApprovalQueue pendingApprovalQueue = new PendingApprovalQueue();
                foreach (var p in products)
                {

                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetPendingApprovalProducts() function error", typeof(ProductRepository));
            }
            return result;

        }
        public override async Task<bool> CreateProduct(ProductDetails entity)
        {
            try
            {
                bool sendtoQueue = false;
                var existingProduct = await _context.MasProducts.Where(x => x.ProductName == entity.ProductName)
                                                    .FirstOrDefaultAsync();
                //Validations
                if (existingProduct != null)
                {
                    return false;
                }

                //3.	Product creation is not allowed when its price is more than 10000 dollars.
                if (entity.ProductPrice > 10000)
                {
                    return false;
                }
                //5.Any product should be pushed to approval queue if its price is more than 50% of its previous price.
                if (existingProduct.ProductPrice < (entity.ProductPrice / 2))
                {
                    sendtoQueue = true;
                }
                //4.Any product should be pushed to approval queue
                //if its price is more than 5000 dollars at the time of creation and update. 
                if (entity.ProductPrice > 5000)
                {
                    sendtoQueue = true;
                }
                if (sendtoQueue == false)
                {
                    entity.IsActive = true;
                }
                else { existingProduct.IsActive = false; }
                MasProduct product = new()
                {
                    ProductName = entity.ProductName,
                    ProductPrice = entity.ProductPrice,
                    ProductDescription = entity.ProductDescription,
                    CreatedBy = entity.CreatedBy,
                    IsActive = entity.IsActive,
                };
                var createdproduct = await _context.MasProducts.Where(x => x.ProductName == entity.ProductName)
                                    .FirstOrDefaultAsync();
                if (sendtoQueue == false)
                {
                    QueueApproval queueApproval = new()
                    {
                        RequestedBy = entity.CreatedBy,
                        Status = "W",
                        ProductId = createdproduct.ProductId,
                        RequestedDate = DateTime.Now,
                    };
                }
                _context.MasProducts.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Create method error", typeof(ProductRepository));
                return false;
            }
        }

        public override async Task<bool> UpdateProduct(ProductDetails entity)
        {
            try
            {
                bool sendtoQueue = false;
                var existingProduct = await _context.MasProducts.Where(x => x.ProductName == entity.ProductName)
                                                    .FirstOrDefaultAsync();
                //Validations

                //4.Any product should be pushed to approval queue
                //if its price is more than 5000 dollars at the time of creation and update. 
                if (entity.ProductPrice > 5000)
                {
                    sendtoQueue = true;
                }
                if (sendtoQueue == false)
                {
                    entity.IsActive = true;
                }
                else { existingProduct.IsActive = false; }
                MasProduct product = new()
                {
                    ProductName = entity.ProductName,
                    ProductPrice = entity.ProductPrice,
                    ProductDescription = entity.ProductDescription,
                    CreatedBy = entity.CreatedBy,
                    IsActive = entity.IsActive,
                };

                if (sendtoQueue == false)
                {
                    QueueApproval queueApproval = new()
                    {
                        RequestedBy = entity.CreatedBy,
                        Status = "W",
                        ProductId = existingProduct.ProductId,
                        RequestedDate = DateTime.Now,
                    };
                }
                _context.MasProducts.Update(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Update method error", typeof(ProductRepository));
                return false;
            }
        }

        public override async Task<bool> DeleteProduct(int ProductId)
        {
            try
            {
                var existingProduct = await _context.MasProducts.Where(x => x.ProductId == ProductId)
                                                    .FirstOrDefaultAsync();
                MasProduct product = new()
                {
                    ProductName = existingProduct.ProductName,
                    ProductPrice = existingProduct.ProductPrice,
                    ProductDescription = existingProduct.ProductDescription,
                    CreatedBy = existingProduct.CreatedBy,
                    IsActive = false,
                };

                QueueApproval queueApproval = new()
                {
                    RequestedBy = existingProduct.CreatedBy,
                    Status = "W",
                    ProductId = existingProduct.ProductId,
                    RequestedDate = DateTime.Now,
                };

                _context.MasProducts.Update(product);
                _context.QueueApprovals.Add(queueApproval);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Delete function error", typeof(ProductRepository));
                return false;
            }
        }

    }
}
