using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public sealed class TokenResult : ITokenResult
    {
        public string Token { get; }
        public DateTime Expires { get; }
        public DateTime Created { get; }
        public bool Status { get; }
        public TokenResult(string token, DateTime created, DateTime expires, bool status)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            Created = created;
            Expires = expires;
            Status = status;
        }
    }
}
