using System;
using System.Collections.Generic;

namespace PeopleDeskHomeWorkUsingSQL.Models.Data.Entity
{
    public partial class TblAllMenu
    {
        public long IntMenuId { get; set; }
        public string StrMenuName { get; set; } = null!;
        public string StrTo { get; set; } = null!;
        public long IntParentMenuId { get; set; }
        public long IntMenuLabelId { get; set; }
        public bool IsExpandable { get; set; }
        public bool IsActive { get; set; }
    }
}
