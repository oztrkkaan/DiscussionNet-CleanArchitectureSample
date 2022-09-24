using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiscussionNet.Application.Common.Exceptions.ValidationException;

namespace DiscussionNet.Infrastructure.Responses
{
    public class ValidationExceptionResponse
    {
        public int StatusCode { get; set; }
        public List<ValidationErrorItem> ValitationErrors { get; set; }
    }
}
