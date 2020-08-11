using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Threading.Tasks;
using WebApi.DataServices;
using WebApi.Services;

namespace WebApi.Controllers
{
    /// <summary>
    /// Login Controller
    /// </summary>
    [Route("api/login")]
    [ApiController]    
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public RepositoryUserImplementation Repository { get; }

        /// <summary>
        /// 
        /// </summary>
        public ITokenService TokenService { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="tokenService"></param>
        public LoginController(RepositoryUserImplementation repository, ITokenService tokenService)
        {
            Repository = repository;
            TokenService = tokenService;
        }

        /// <summary>
        /// Login - Authentication
        /// </summary>
        /// <param name="login">Login class</param>
        /// <returns>Json Result</returns>        
        [HttpPost]
        [Route("auth", Name = "Auth Name")]   
        [ProducesResponseType(200, Type = typeof(JsonResult))]
        public async Task<IActionResult> Authenticate(Login login)
        {
            await UserCreateDefaultAsync();
            if (ModelState.IsValid)
            {
                var user = CheckLogin(login);
                if (user == null)
                {
                    return NotFound(new { message = "User no found" });
                }
                return TokenService.GenerateTokenJsonResult(user);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [NonAction]
        protected User CheckLogin(Login login)
        {
            string email = login.Email;
            string password = login.Password;
            var user = Repository.GetFirst(email, password);
            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected async Task UserCreateDefaultAsync()
        {
            if ((await Repository.CountAsync()) == 0)
            {
                var user = new Shared.User() { 
                    Email = "fulviocanducci@hotmail.com", 
                    Password = "123456@@" 
                };
                await Repository.AddAsync(user);
            }
        }
    }
}
