namespace PeopleDeskHomeWork.Models.ViewModel.Purchase
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
        public string? StrItemName { get; set; }
        public long IntItemQuantity { get; set; }
        public decimal NumUnitPrice { get; set; }
        public bool IsActive { get; set; }
    }
    public class ItemWiseDailyPurchaseReportViewModel
    {
        public long IntPurchaseId { get; set; }
        public long IntSupplierId { get; set; }
        public string StrSupplierName { get; set; }
        public DateTime DtePurchaseDate { get; set; }
        public bool IsActive { get; set; }
        public long IntPurchaseDetailsId { get; set; }
        //public long IntPurchaseId { get; set; }
        public long IntItemId { get; set; }
        public string StrItemName { set; get; }
        public decimal IntItemQuantity { get; set; }
        public decimal NumUnitPrice { get; set; }
        public decimal NumTotalPrice { get; set; }
        //public bool IsActive { get; set; }
        PurchaseInfoViewModel? purchase { get; set; }
        PurchaseDetailsViewModel? purchaseDetails { get; set; }
    }
    public class PurchaseOrderCommonViewModel
    {
        public PurchaseViewModel? purchase { get; set; }
        public List<PurchaseDetailsViewModel>? purchaseDetails { get; set; }
    }
}
