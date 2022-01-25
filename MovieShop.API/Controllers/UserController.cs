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

        // [HttpGet]
        // [Route("details")]
        // public async Task<IActionResult> GetUserDetails(int userId)
        // {
        //     var userDetails = _userService.
        // }
        
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
        
        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> Purchase([FromBody] PurchaseRequestModel model)
        {
            var purchase = await _userService.PurchaseMovie(model);

            return Ok(purchase);
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

        [HttpPost]
        [Route("favorite")]
        public async Task AddFavorite([FromBody] FavoriteRequestModel model)
        {
            await _userService.AddFavorite(model);
        }
        
        [HttpPost]
        [Route("un-favorite")]
        public async Task RemoveFavorite([FromBody] FavoriteRequestModel model)
        {
            await _userService.RemoveFavorite(model);
        }

        // [HttpGet]
        // [Route("check-movie-favorite/{movieId:int}")]
        // public async Task<IActionResult> CheckMovieFavoritedByUser(int movieId)
        // {
        //     var userId = _userService
        // }
        
        
        
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

        [HttpPost]
        [Route("add-review")]
        public async Task AddReview([FromBody] ReviewRequestModel model)
        {
            await _userService.AddMovieReview(model);
        }

        [HttpPut]
        [Route("edit-review")]
        public async Task PutReview([FromBody] ReviewRequestModel model)
        {
            await _userService.PutMovieReview(model);
        }
        
        // delete-review NOT Implement

        [HttpDelete]
        [Route("{userId:int}/movie/{movieId:int}")]
        public async Task DeleteUserMovie(int userId, int movieId)
        {
            var unfavorite = new FavoriteRequestModel
            {
                UserId = userId,
                MovieId = movieId
            };

            var unpurchase = new PurchaseRequestModel
            {
                UserId = userId,
                MovieId = movieId
            };
            
            await _userService.DeleteMovieReview(userId, movieId);
            await _userService.RemoveFavorite(unfavorite);
            await _userService.DeletePurchase(unpurchase);

        }
        
        
    }
  
}