using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateAccount([FromBody] UserRegisterRequestModel model)
        {
            var success = await _accountService.Register(model);

            return Ok(success);
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

            var token = GenerateJWT(user);
            return Ok(new {token = token});
        }

        private string GenerateJWT(UserLoginResponseModel user)
        {
            // store some claims in the token 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.DateOfBirth?.ToShortDateString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("language", "english")
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            
            // we need to genereate the token using secret key and we need to specify details of the token
            // such as who is the issuer of the token, who is the Audience of the token, expiration time for the token
            
            // best way to store any secret information, any configuration information in
            // Azure KeyVault

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["PrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expiration = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expiration,
                SigningCredentials = credentials,
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"]
            };

            var encodedJwt = tokenHandler.CreateToken(tokenDetails);

            return tokenHandler.WriteToken(encodedJwt);
        }
    }

    
}