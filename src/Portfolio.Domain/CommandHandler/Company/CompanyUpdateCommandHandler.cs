using Portfolio.Domain.Command.Company;
using Portfolio.Domain.CommandHandler.Base;
using Portfolio.Domain.Entities.Company;
using Portfolio.Domain.Interfaces.DataModule;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Domain.CommandHandler.Company
{
    public class CompanyUpdateCommandHandler
        : UpdateCommandHandlerBase<CompanyUpdateCommand, CompanyEntity>
    {
        public CompanyUpdateCommandHandler(
            IDataModuleDBPortfolio dataModule,
            IMapper mapper,
            IValidator<CompanyUpdateCommand> validator)
        : base(dataModule, 
              mapper,
              validator,
              dataModule.CompanyRepository)
        {

            OnRequestRepositoryData += async (CompanyUpdateCommand request) =>
            {
                var dbData = await repository.DataSet.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

                var dbEntity = _mapper.Map<CompanyEntity>(request);
                dbEntity.Id = dbData.Id;

                await repository.UpdateAsync(dbEntity);

                return dbData;
            };
        }
    }
}