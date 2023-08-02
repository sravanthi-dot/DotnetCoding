﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly masterContext _dbContext;
        public IProductRepository Products { get; }
        public IPendingApprovalRepository PendingApprovalQueue { get; }


        public UnitOfWork(masterContext dbContext,
                            IProductRepository productRepository,
                            IPendingApprovalRepository pendingApprovalQueue)
        {
            _dbContext = dbContext;
            Products = productRepository;
            PendingApprovalQueue = pendingApprovalQueue;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
