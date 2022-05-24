using Microsoft.EntityFrameworkCore;
using PeopleDeskHomeWork.Helper;
using PeopleDeskHomeWork.Models.Data;
using PeopleDeskHomeWork.Models.Data.Entity;
using PeopleDeskHomeWork.Models.ViewModel.Partner;
using PeopleDeskHomeWork.Services.Interfaces;

namespace PeopleDeskHomeWork.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly HomeWorkDbContext _context;
        public PartnerService(HomeWorkDbContext _context)
        {
            this._context = _context;
        }

        #region
        public async Task<MessageHelper> CreatePartnerType(PartnerTypeViewModel obj)
        {
            MessageHelper res = new MessageHelper();

            var isExists = await _context.TblPartnerTypes.Where(x => x.IsActive == true && x.IntPartnerTypeId != obj.IntPartnerTypeId && x.StrPartnerTypeName.ToLower().Trim() == obj.StrPartnerTypeName.ToLower().Trim()).FirstOrDefaultAsync();
            
            if (isExists != null)
            {
                res.Message = "Partner Type Already Exists";
            }
            else
            {
                TblPartnerType data = new TblPartnerType
                {
                    StrPartnerTypeName = obj.StrPartnerTypeName,
                    IsActive = true
                };

                if (obj.IntPartnerTypeId > 0)
                {
                    _context.Entry(data).State = EntityState.Modified;
                    _context.TblPartnerTypes.Update(data);
                    await _context.SaveChangesAsync();

                    res.Message = "Update Successfully";
                    res.AutoId = obj.IntPartnerTypeId;
                }
                else
                {
                    await _context.TblPartnerTypes.AddAsync(data);
                    await _context.SaveChangesAsync();

                    res.Message = "Create Successfully";
                    res.AutoId = obj.IntPartnerTypeId;
                }
            }
            return res;
            #endregion
        }
    }
}