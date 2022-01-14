using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Servies;

public interface IAccountService
{
    Task<bool> Register(UserRegisterRequestModel model);
  
    Task<UserLoginResponseModel> Validate(string email, string password);
    
}