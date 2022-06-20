namespace PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Sales
{
    public class SalesInfoViewModel
    {
    }
    public class SalesViewModel
    {
        public string StrPart { get; set; }
        public long AutoId { get; set; }
        public long IntSalesId { get; set; }
        public long IntCustomerId { get; set; }
        public string? StrCustomerName { get; set; }
        public DateTime DteSalesDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class SalesDetailsViewModel
    {
        //public long IntSalesDetailsId { get; set; }
        //public long IntSalesId { get; set; }
        public long intItemId { get; set; }
        public string strItemName { get; set; } = null!;
        public decimal numtemQuantity { get; set; }
        public decimal numUnitPrice { get; set; }
        public decimal numTotalPrice { get; set; }
        public bool isActive { get; set; }
        public long intCreatedBy { get; set; }
        public DateTime dteCreatedAt { get; set; }
        public long? intUpdatedBy { get; set; }
        public DateTime? dteUpdatedAt { get; set; }
    }
    public class SalesOrderCommonViewModel
    {
        public SalesViewModel? sales { get; set; }
        public List<SalesDetailsViewModel>? salesDetails { get; set; }
    }
}
