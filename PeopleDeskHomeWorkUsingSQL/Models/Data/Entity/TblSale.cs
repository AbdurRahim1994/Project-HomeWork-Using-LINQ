using System;
using System.Collections.Generic;

namespace PeopleDeskHomeWorkUsingSQL.Models.Data.Entity
{
    public partial class TblSale
    {
        public long IntSalesId { get; set; }
        public long IntCustomerId { get; set; }
        public string? StrCustomerName { get; set; }
        public DateTime DteSalesDate { get; set; }
        public bool IsActive { get; set; }
    }
}
