using Portfolio.Domain.Command.Company;
using Portfolio.Domain.CommandHandler.Base;
using Portfolio.Domain.Entities.Company;
using Portfolio.Domain.Interfaces.DataModule;
using AutoMapper;
using FluentValidation;

namespace Portfolio.Domain.CommandHandler.Company
{
    public class CompanyDeleteCommandHandler 
        : DeleteCommandHandlerBase<CompanyDeleteCommand, CompanyEntity>
    {
        public CompanyDeleteCommandHandler(
            IDataModuleDBPortfolio dataModule,
            IMapper mapper,
            IValidator<CompanyDeleteCommand> validator)
        : base(dataModule, mapper, validator, dataModule.CompanyRepository)
        { }
    }
}