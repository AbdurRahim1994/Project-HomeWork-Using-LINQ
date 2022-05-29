namespace PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Partner
{
    public class PartnerInfoViewModel
    {
    }
    public class PartnerTypeViewModel
    {
        public string StrPartName { get; set; }
        public long IntAutoId { get; set; }
        public long IntPartnerTypeId { get; set; }
        public string StrPartnerTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
