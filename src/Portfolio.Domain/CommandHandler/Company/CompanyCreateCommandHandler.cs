using Portfolio.Domain.Command.Company;
using Portfolio.Domain.CommandHandler.Base;
using Portfolio.Domain.Entities.Company;
using Portfolio.Domain.Interfaces.DataModule;
using AutoMapper;
using FluentValidation;

namespace Portfolio.Domain.CommandHandler.Company
{
    public class CompanyCreateCommandHandler 
        : CreateCommandHandlerBase<CompanyCreateCommand, CompanyEntity>
    {
        public CompanyCreateCommandHandler(
            IDataModuleDBPortfolio dataModule,
            IMapper mapper,
            IValidator<CompanyCreateCommand> validator)
        : base(dataModule, mapper, validator, dataModule.CompanyRepository) { }
    }
}