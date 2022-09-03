using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskisehirspor.Application.Common.Identity
{
    public class AuthenticatedUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
