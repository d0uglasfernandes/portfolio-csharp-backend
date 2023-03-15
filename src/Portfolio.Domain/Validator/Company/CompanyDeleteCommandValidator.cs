using Portfolio.Domain.Command.Company;
using Portfolio.Domain.Validator.Base;
using FluentValidation;

namespace Portfolio.Domain.Validator.Company
{
    public class CompanyDeleteCommandValidator : BaseValidator<CompanyDeleteCommand>
    {
        public CompanyDeleteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Company id cannot be null");
        }
    }
}