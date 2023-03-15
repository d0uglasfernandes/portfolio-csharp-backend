using System;

namespace Portfolio.Domain.Dto.Company
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FederalTaxIdentificationNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}