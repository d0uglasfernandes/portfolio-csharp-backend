using System;
using Portfolio.Domain.Command.Base;

namespace Portfolio.Domain.Command.Company
{
    public class CompanyCreateCommand : BaseCreateCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string FederalTaxIdentificationNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}