using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskisehirspor.Domain.Entities
{
    public class TokenUser
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
