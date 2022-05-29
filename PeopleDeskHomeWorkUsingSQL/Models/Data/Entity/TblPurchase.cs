using System;
using System.Collections.Generic;

namespace PeopleDeskHomeWorkUsingSQL.Models.Data.Entity
{
    public partial class TblPurchase
    {
        public long IntPurchaseId { get; set; }
        public long IntSupplierId { get; set; }
        public string? StrSupplierName { get; set; }
        public DateTime DtePurchaseDate { get; set; }
        public bool IsActive { get; set; }
    }
}
