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
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        [Route("Cast/{id:int}")]
        public async Task<IActionResult> GetCastDetails(int id)
        {
            var details = await _castService.GetCastDetails(id);
            
            if (details == null)
            {
                return Unauthorized("No Movies Found By Cast");
            }
            return Ok(details);
        }
    }

    
}