namespace PeopleDeskHomeWorkUsingSQL.Models.ViewModels.MenuPermission
{
    public class MenuInfoViewModel
    {
    }
    public class FirstLabelMenuViewModel
    {
        public long IntMenuId { get; set; }
        public string StrMenuName { get; set; } = null!;
        public string StrTo { get; set; } = null!;
        public long IntParentMenuId { get; set; }
        public long IntMenuLabelId { get; set; }
        public bool IsExpandable { get; set; }
        public bool IsActive { get; set; }
        public List<SecondLabelMenuViewModel> secondLabelMenu { get; set; }
    }
    public class SecondLabelMenuViewModel
    {
        public long IntMenuId { get; set; }
        public string StrMenuName { get; set; } = null!;
        public string StrTo { get; set; } = null!;
        public long IntParentMenuId { get; set; }
        public long IntMenuLabelId { get; set; }
        public bool IsExpandable { get; set; }
        public bool IsActive { get; set; }
        public List<ThirdLabelMenuViewModel> thirdLabelMenu { get; set; }
    }
    public class ThirdLabelMenuViewModel
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
