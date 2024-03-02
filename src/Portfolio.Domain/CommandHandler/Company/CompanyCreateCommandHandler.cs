using Portfolio.Domain.Command.Company;
using Portfolio.Domain.CommandHandler.Base;
using Portfolio.Domain.Entities.Company;
using Portfolio.Domain.Interfaces.DataModule;
using AutoMapper;
using FluentValidation;

namespace Portfolio.Domain.CommandHandler.Company
{
    public class CompanyCreateCommandHandler(
        IDataModuleDBPortfolio dataModule,
        IMapper mapper,
        IValidator<CompanyCreateCommand> validator)
                : CreateCommandHandlerBase<CompanyCreateCommand, CompanyEntity>(dataModule, mapper, validator, dataModule.CompanyRepository)
    {
    }
}