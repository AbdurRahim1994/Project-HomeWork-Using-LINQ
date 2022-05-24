using System;
using System.Collections.Generic;

namespace PeopleDeskHomeWork.Models.Data.Entity
{
    public partial class TblPartner
    {
        public long IntPartnerId { get; set; }
        public string StrPartnerName { get; set; } = null!;
        public long IntPartnerTypeId { get; set; }
        public string? StrPartnerTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
