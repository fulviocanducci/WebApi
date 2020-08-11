using Microsoft.AspNetCore.Mvc;
using Shared;
using WebApi.Settings;

namespace WebApi.Services
{
    /// <summary>
    /// ITokenService interface
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// 
        /// </summary>
        SigningConfigurations SigningConfigurations { get; }
        /// <summary>
        /// 
        /// </summary>
        TokenConfigurations TokenConfigurations { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        JsonResult GenerateTokenJsonResult(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        TokenResult GenerateTokenResult(User user);
    }
}