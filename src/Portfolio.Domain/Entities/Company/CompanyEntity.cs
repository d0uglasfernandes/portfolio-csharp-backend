using System;
using Portfolio.Domain.Entities.Base;

namespace Portfolio.Domain.Entities.Company
{
    public class CompanyEntity : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string FederalTaxIdentificationNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}