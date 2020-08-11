using System;

namespace WebApi.Services
{
    public interface ITokenResult
    {
        DateTime Created { get; }
        DateTime Expires { get; }
        bool Status { get; }
        string Token { get; }
    }
}