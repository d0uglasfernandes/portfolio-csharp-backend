using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Portfolio.Domain.Interfaces.DataModule
{
    public interface IDataModule
    {
        IDbContextTransaction CurrentTransaction { get; set; }
        Task StartTransactionAsync();
        Task CommitDataAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
