using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Portfolio.Data.Context
{
    public class SQLServerContextFactory : IDesignTimeDbContextFactory<PortfolioContext>
    {
        public PortfolioContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost\\SQL2019; Database=DBPortfolio; User Id=sa; Password=SIMERP;";
            var optionsBuilder = new DbContextOptionsBuilder<PortfolioContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new PortfolioContext(optionsBuilder.Options);
        }
    }
}