using Portfolio.Data.Context;
using Portfolio.Data.Repository;
using Portfolio.Domain.Entities.Company;
using Portfolio.Domain.Interfaces.DataModule;
using Portfolio.Domain.Interfaces.Repository;

namespace Portfolio.Data.DataModule
{
    public class DataModuleDBPortfolio: DataModule<PortfolioContext>, IDataModuleDBPortfolio
    {
        public DataModuleDBPortfolio(PortfolioContext dbContext)
            : base(dbContext) { }

        private IRepository<CompanyEntity> _companyRepository = null;
        public IRepository<CompanyEntity> CompanyRepository
        {
            get => _companyRepository ??= new BaseRepository<CompanyEntity>(CurrentContext);
        }
    }
}
