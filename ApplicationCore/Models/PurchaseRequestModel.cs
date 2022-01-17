namespace ApplicationCore.Models;

public class PurchaseRequestModel
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public decimal TotalPrice { get; set; }
}