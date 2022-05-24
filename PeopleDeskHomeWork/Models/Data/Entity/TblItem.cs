using System;
using System.Collections.Generic;

namespace PeopleDeskHomeWork.Models.Data.Entity
{
    public partial class TblItem
    {
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
}
