using Microsoft.AspNetCore.Mvc;
using Shared;
using WebApi.Settings;

namespace WebApi.Services
{
    public interface ITokenService
    {
        SigningConfigurations SigningConfigurations { get; }
        TokenConfigurations TokenConfigurations { get; }

        JsonResult GenerateTokenJsonResult(User user);
        TokenResult GenerateTokenResult(User user);
    }
}