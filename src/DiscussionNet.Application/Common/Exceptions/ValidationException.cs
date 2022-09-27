using DiscussionNet.Domain.Exceptions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionNet.Application.Common.Exceptions
{
    public class ValidationException : CustomException
    {

        public ValidationException(List<ValidationErrorItem> validationErrors) : base(null, false)
        {
            ValidationErrors = validationErrors;
        }

        public ValidationException(List<ValidationFailure> validationFailures) : base(null, false)
        {
            ValidationErrors = validationFailures.Select(m => new ValidationErrorItem
            {
                ErrorMessage = m.ErrorMessage,
                PropertyName = m.PropertyName
            }).ToList();
        }
        public List<ValidationErrorItem> ValidationErrors { get; }

        public class ValidationErrorItem
        {
            public string PropertyName { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
