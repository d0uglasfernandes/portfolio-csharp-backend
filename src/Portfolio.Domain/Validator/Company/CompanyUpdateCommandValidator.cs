using Portfolio.Domain.Command.Company;
using Portfolio.Domain.Validator.Base;
using FluentValidation;

namespace Portfolio.Domain.Validator.Company
{
    public class CompanyUpdateCommandValidator : BaseValidator<CompanyUpdateCommand>
    {
        public CompanyUpdateCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Company id cannot be null");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code cannot be null.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be null.");
            RuleFor(x => x.FederalTaxIdentificationNumber).NotEmpty().WithMessage("Federal Tax Identification Number cannot be null.");
        }
    }
}