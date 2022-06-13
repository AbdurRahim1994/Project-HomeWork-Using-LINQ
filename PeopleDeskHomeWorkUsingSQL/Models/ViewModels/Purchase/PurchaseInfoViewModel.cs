namespace PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Purchase
{
    public class PurchaseInfoViewModel
    {
    }
    public class PurchaseViewModel
    {
        public long IntPurchaseId { get; set; }
        public long IntSupplierId { get; set; }
        public string? StrSupplierName { get; set; }
        public DateTime DtePurchaseDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class PurchaseDetailsViewModel
    {
        public long IntPurchaseDetailsId { get; set; }
        public long IntPurchaseId { get; set; }
        public long IntItemId { get; set; }
        public string StrItemName { get; set; } = null!;
        public decimal NumItemQuantity { get; set; }
        public decimal NumUnitPrice { get; set; }
        public decimal NumTotalPrice { get; set; }
        public bool IsActive { get; set; }
        public long IntCreatedBy { get; set; }
        public DateTime DteCreatedAt { get; set; }
        public long? IntUpdatedBy { get; set; }
        public DateTime? DteUpdatedAt { get; set; }
    }
    public class PurchaseOrderCommonViewModel
    {
        public string StrPart { get; set; }
        public long AutoId { get; set; }
        public PurchaseViewModel? purchase { get; set; }
        public List<PurchaseDetailsViewModel>? purchaseDetails { get; set; }
    }
}
