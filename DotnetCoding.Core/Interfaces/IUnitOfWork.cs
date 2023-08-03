

using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        int Save();
    }
}
