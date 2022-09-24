using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionNet.Infrastructure.Responses
{
    public class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}
