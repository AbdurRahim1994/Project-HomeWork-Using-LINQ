namespace PeopleDeskHomeWork.Models.ViewModel.Item
{
    public class ItemInfoViewModel
    {
    }
    public class ItemViewModel
    {
        public long IntItemId { get; set; }
        public string StrItemName { get; set; } = null!;
        public decimal NumStockQuantity { get; set; }
        public decimal NumUnitPrice { get; set; }
        public decimal NumTotalPrice { get; set; }
        public bool IsActive { get; set; }
    }
    public class ItemCommonViewModel
    {
        public List<ItemViewModel>? items { get; set; }
    }
}
