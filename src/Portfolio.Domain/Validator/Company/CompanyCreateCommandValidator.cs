using Portfolio.Domain.Command.Company;
using Portfolio.Domain.Validator.Base;
using FluentValidation;

namespace Portfolio.Domain.Validator.Company
{
    public class CompanyCreateCommandValidator : BaseValidator<CompanyCreateCommand>
    {
        public CompanyCreateCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code cannot be null.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be null.");
            RuleFor(x => x.FederalTaxIdentificationNumber).NotEmpty().WithMessage("Federal Tax Identification Number cannot be null.");
        }
    }
}