using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class PurchaseRequestModel
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDateTime { get; set; }

    public static PurchaseRequestModel FromEntity(Purchase purchase)
    {
        var purchaseModel = new PurchaseRequestModel
        {
            UserId = purchase.UserId,
            MovieId = purchase.MovieId,
            TotalPrice = purchase.TotalPrice,
            PurchaseDateTime = purchase.PurchaseDateTime
        };

        return purchaseModel;
    }
}