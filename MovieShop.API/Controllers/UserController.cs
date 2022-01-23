using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Servies;
using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Route("{id:int}/purchases")]
        public async Task<IActionResult> GetAllUserPurchases(int id)
        {
            var purchases = await _userService.GetAllPurchasesForUser(id);
            if (purchases == null)
            {
                return NotFound();
            }

            return Ok(purchases);
        }
        
        [HttpGet]
        [Route("{id:int}/favorites")]
        public async Task<IActionResult> GetAllUserFavorites(int id)
        {
            var favorites = await _userService.GetAllFavoritesForUser(id);
            if (favorites == null)
            {
                return NotFound();
            }

            return Ok(favorites);
        }
        
        [HttpGet]
        [Route("{id:int}/movie/{movieId:int}/favorites")]
        public async Task<IActionResult> GetAllUserFavorites(int id, int movieId)
        {
            var favorite = await _userService.FavoriteExists(id, movieId);
            
            return Ok(favorite);
        }
        
        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetAllUserReviews(int id)
        {
            var reviews = await _userService.GetAllReviewsByUser(id);
            if (!reviews.Reviews.Any())
            {
                return NotFound();
            }
            
            return Ok(reviews.Reviews);
        }
        
        
    }
  
}