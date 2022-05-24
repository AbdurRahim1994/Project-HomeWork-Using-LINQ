namespace PeopleDeskHomeWork.Models.ViewModel.Sales
{
    public class SalesInfoViewModel
    {
    }
    public class SalesViewModel
    {
        public long IntSalesId { get; set; }
        public long IntCustomerId { get; set; }
        public string? StrCustomerName { get; set; }
        public DateTime DteSalesDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class SalesDetailsViewModel
    {
        public long IntSalesDetailsId { get; set; }
        public long IntSalesId { get; set; }
        public long IntItemId { get; set; }
        public string StrItemName { get; set; }
        public long IntItemQuantity { get; set; }
        public decimal NumUnitPrice { get; set; }
        public bool IsActive { get; set; }
    }
    public class ItemWiseMonthlySalesReportViewModel
    {
        public long IntSalesId { get; set; }
        public long IntCustomerId { get; set; }
        public string? StrCustomerName { get; set; }
        public DateTime DteSalesDate { get; set; }
        public bool IsActive { get; set; }
        public long IntSalesDetailsId { get; set; }
        //public long IntSalesId { get; set; }
        public long IntItemId { get; set; }
        public string? StrItemName { get; set; }
        public decimal IntItemQuantity { get; set; }
        public decimal NumUnitPrice { get; set; }
        public decimal NumTotalPrice { get; set; }
        //public bool IsActive { get; set; }
        public SalesInfoViewModel? sales { get; set; }
        public SalesDetailsViewModel? salesDetails { get; set; }
    }
    public class SalesOrderCommonViewModel
    {
        public SalesViewModel? sales { get; set; }
        public List<SalesDetailsViewModel>? salesDetails { get; set; }
    }
}
