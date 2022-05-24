using System;
using System.Collections.Generic;

namespace PeopleDeskHomeWork.Models.Data.Entity
{
    public partial class TblSalesDetail
    {
        public long IntSalesDetailsId { get; set; }
        public long IntSalesId { get; set; }
        public long IntItemId { get; set; }
        public string StrItemName { get; set; } = null!;
        public decimal NumtemQuantity { get; set; }
        public decimal NumUnitPrice { get; set; }
        public decimal NumTotalPrice { get; set; }
        public bool IsActive { get; set; }
        public long IntCreatedBy { get; set; }
        public DateTime DteCreatedAt { get; set; }
        public long? IntUpdatedBy { get; set; }
        public DateTime? DteUpdatedAt { get; set; }
    }
}
