using Portfolio.Domain.Entities.Company;
using Portfolio.Domain.Interfaces.Repository;

namespace Portfolio.Domain.Interfaces.DataModule
{
    public interface IDataModuleDBPortfolio : IDataModule
    {
        IRepository<CompanyEntity> CompanyRepository { get; }
    }
}
