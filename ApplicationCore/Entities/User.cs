namespace ApplicationCore.Entities;

public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Email { get; set; }
    public string? HashedPassword { get; set; }
    public string? Salt { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? TwoFactorEnabled { get; set; }
    public DateTime? LockoutEndDate { get; set; }
    public DateTime? LastLoginDateTime { get; set; }
    public bool? IsLocked { get; set; }
    public int? AccessFailedCount { get; set; }
    
    public List<Purchase> PurchasesOfUser { get; set; }
    public List<Favorite> FavoritesOfUser { get; set; }
    public List<Review> ReviewsOfUser { get; set; }
    public List<UserRole> RolesOfUser { get; set; }
    
}