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
        //public long IntPurchaseDetailsId { get; set; }
        //public long IntPurchaseId { get; set; }
        public long intItemId { get; set; }
        public string strItemName { get; set; } = null!;
        public decimal numItemQuantity { get; set; }
        public decimal numUnitPrice { get; set; }
        public decimal numTotalPrice { get; set; }
        public bool isActive { get; set; }
        public long intCreatedBy { get; set; }
        public DateTime dteCreatedAt { get; set; }
        public long? intUpdatedBy { get; set; }
        public DateTime? dteUpdatedAt { get; set; }
    }
    public class PurchaseOrderCommonViewModel
    {
        public string StrPart { get; set; }
        public long AutoId { get; set; }
        public PurchaseViewModel? purchase { get; set; }
        public List<PurchaseDetailsViewModel>? purchaseDetails { get; set; }
    }
}
