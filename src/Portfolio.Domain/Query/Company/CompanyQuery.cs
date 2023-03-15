using System;
using System.Collections.Generic;
using Portfolio.Domain.Dto.Company;
using Portfolio.Domain.Query.Base;
using MediatR;

namespace Portfolio.Domain.Query.Company
{
    public class CompanyQuery : BaseQuery, IRequest<List<CompanyDto>>
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FederalTaxIdentificationNumber { get; set; }
    }
}