namespace MovieShop.API.Helpers;

public interface ICurrentLoginUserService
{
    int UserId { get; }
    string Email { get; }
    string FullName { get; }
    List<String> Roles { get; set; }
    bool IsAdmin { get; }
}