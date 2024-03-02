using Portfolio.Domain.Command.Company;
using Portfolio.Domain.CommandHandler.Base;
using Portfolio.Domain.Entities.Company;
using Portfolio.Domain.Interfaces.DataModule;
using AutoMapper;
using FluentValidation;

namespace Portfolio.Domain.CommandHandler.Company
{
    public class CompanyDeleteCommandHandler(
        IDataModuleDBPortfolio dataModule,
        IMapper mapper,
        IValidator<CompanyDeleteCommand> validator)
                : DeleteCommandHandlerBase<CompanyDeleteCommand, CompanyEntity>(dataModule, mapper, validator, dataModule.CompanyRepository)
    {
    }
}