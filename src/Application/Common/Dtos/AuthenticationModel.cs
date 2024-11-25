using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Dtos
{
    public class AuthenticationModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public int ExpirationDuration { get; set; }
        public string RefreshToken { get; set; }
    }
}