using System;
using System.Collections.Generic;
using Portfolio.Domain.Dto.ValidationError;

namespace Portfolio.Domain.Exceptions
{
    public class ValidationException(List<ValidationErrorDto> errors) : Exception
    {
        public List<ValidationErrorDto> Errors { get; } = errors;
    }
}
