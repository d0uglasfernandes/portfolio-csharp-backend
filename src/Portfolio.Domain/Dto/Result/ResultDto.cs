using System;
using System.Collections.Generic;
using Portfolio.Domain.Dto.ValidationError;

namespace Portfolio.Domain.Dto.Result
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public Object Result { get; set; }
        public bool IsFailure => !IsSuccess;
        public string ErrorType { get; set; }
        public string Error { get; set; }
        public List<ValidationErrorDto> ValidationErrors { get; set; } = [];
    }
}