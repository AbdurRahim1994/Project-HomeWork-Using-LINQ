namespace PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Item
{
    public class ItemInfoViewModel
    {
    }
    public class ItemViewModel
    {
        public string StrPart { get; set; }
        public long IntAutoId { get; set; }
        public long IntItemId { get; set; }
        public string StrItemName { get; set; } = null!;
        public decimal NumStockQuantity { get; set; }
        public decimal NumStockPrice { get; set; }
        public decimal NumTotalPrice { get; set; }
        public bool IsActive { get; set; }
        public long IntCreatedBy { get; set; }
        public DateTime DteCreatedAt { get; set; }
        public long? IntUpdatedBy { get; set; }
        public DateTime? DteUpdatedAt { get; set; }
    }
    public class ItemViewModelJson
    {
        public string StrPart { get; set; }
        public long IntAutoId { get; set; }
        //public long intItemId { get; set; }
        public string strItemName { get; set; } = null!;
        public decimal numStockQuantity { get; set; }
        public decimal numStockPrice { get; set; }
        public decimal numTotalPrice { get; set; }
        public bool isActive { get; set; }
        public long intCreatedBy { get; set; }
        public DateTime dteCreatedAt { get; set; }
        public long? intUpdatedBy { get; set; }
        public DateTime? dteUpdatedAt { get; set; }
    }
}
