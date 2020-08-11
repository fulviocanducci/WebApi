using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Threading.Tasks;
using WebApi.DataServices;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public RepositoryUserImplementation Repository { get; }
        public ITokenService TokenService { get; }

        public LoginController(RepositoryUserImplementation repository, ITokenService tokenService)
        {
            Repository = repository;
            TokenService = tokenService;
        }


        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Authenticate(User user)
        {
            await UserCreateDefaultAsync();
            if (ModelState.IsValid)
            {                
                if (!CheckLogin(ref user))
                {
                    return NotFound(new { message = "User no found" });
                }
                return TokenService.GenerateTokenJsonResult(user);
            }
            return BadRequest(ModelState);
        }

        [NonAction]
        protected bool CheckLogin(ref User user)
        {
            string email = user.Email;
            string password = user.Password;
            user = Repository.GetFirst(email, password);
            return user != null;
        }

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
