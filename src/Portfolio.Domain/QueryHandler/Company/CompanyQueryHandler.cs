using System.Collections.Generic;
using System.Linq;
using Portfolio.Domain.Dto.Company;
using Portfolio.Domain.Entities.Company;
using Portfolio.Domain.Interfaces.DataModule;
using Portfolio.Domain.Query.Base;
using Portfolio.Domain.Query.Company;
using Portfolio.Domain.QueryHandler.Base;
using AutoMapper;
using FluentValidation;

namespace Portfolio.Domain.QueryHandler.Company
{
    public class CompanyQueryHandler : QueryHandlerBase<CompanyEntity, CompanyQuery, List<CompanyDto>>
    {
        public CompanyQueryHandler(
            IDataModuleDBPortfolio dataModule,
            IMapper mapper,
            IValidator<CompanyQuery> validator)
            : base(dataModule, mapper, validator)
        {

            OnRequestData += (BaseQuery rqt) =>
            {
                var request = (rqt as CompanyQuery);

                return dataModule.CompanyRepository
                .ListNoTracking(x =>
                    ((!request.Id.HasValue || x.Id.Equals(request.Id))
                    && (string.IsNullOrEmpty(request.Code) || x.Code.Contains(request.Code))
                    && (string.IsNullOrEmpty(request.Name) || x.Name.Contains(request.Name))
                    && (string.IsNullOrEmpty(request.FederalTaxIdentificationNumber) || x.FederalTaxIdentificationNumber.Contains(request.FederalTaxIdentificationNumber)))
                )
                .OrderBy(x => x.Code);
            };

        }
    }
}