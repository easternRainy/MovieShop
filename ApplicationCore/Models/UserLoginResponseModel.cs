namespace ApplicationCore.Models;

public class UserLoginResponseModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string DateOfBirth { get; set; }
    public List<string> Roles { get; set; } 
    
}