using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public async Task CreateAccount([FromBody] UserRegisterRequestModel model)
        {
            var success = await _accountService.Register(model);
            
        }
        
        [HttpGet]
        [Route("Account/check-email")]
        public async Task<IActionResult> GetEmailExists(string email)
        {
            var user = await _accountService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var user = await _accountService.Validate(model.Email, model.Password);

            if (user == null)
            {
                return Unauthorized("Wrong Email/Password");
            }
            return Ok(user);
        }
    }

    
}