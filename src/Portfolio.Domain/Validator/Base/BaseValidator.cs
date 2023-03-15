using FluentValidation;

namespace Portfolio.Domain.Validator.Base
{
    public class BaseValidator<BaseClass> : AbstractValidator<BaseClass>
    {        
        public BaseValidator()
        {
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        }
    }
}
