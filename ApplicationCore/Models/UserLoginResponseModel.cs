using System.Runtime.InteropServices.ComTypes;
using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class UserLoginResponseModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public List<string> Roles { get; set; }

    public static UserLoginResponseModel FromEntity(User user)
    {
        return new UserLoginResponseModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth
        };
    }
    
}