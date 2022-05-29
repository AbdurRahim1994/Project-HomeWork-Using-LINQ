using Microsoft.Data.SqlClient;
using PeopleDeskHomeWorkUsingSQL.Helper;
using PeopleDeskHomeWorkUsingSQL.Models.Data;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Partner;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Partner;
using System.Data;

namespace PeopleDeskHomeWorkUsingSQL.Services.Partner
{
    public class PartnerService:IPartnerService
    {
        DataTable dt = new DataTable();
        private readonly HomeWorkDbContext _context;
        public PartnerService(HomeWorkDbContext _context)
        {
            this._context = _context;
        }

        #region Partner Type
        public async Task<MessageHelper> CreatePartnerType(PartnerTypeViewModel obj)
        {
            try
            {
                using(var connection=new SqlConnection(Connection.iPEOPLE_HCM))
                {
                    string sql = "dbo.PartnerCRUD";
                    using(SqlCommand sqlCmd=new SqlCommand(sql, connection))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@strPartName", obj.StrPartName);
                        sqlCmd.Parameters.AddWithValue("@intPartnerTypeId", obj.IntPartnerTypeId);
                        sqlCmd.Parameters.AddWithValue("@strPartnerTypeName", obj.StrPartnerTypeName);
                        sqlCmd.Parameters.AddWithValue("@isActive", obj.IsActive);
                        sqlCmd.Parameters.AddWithValue("@intAutoId", obj.IntAutoId);

                        connection.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                        connection.Close();

                        var msg = new MessageHelper();
                        msg.StatusCode = Convert.ToInt32(dt.Rows[0]["returnStatus"]);
                        msg.Message = Convert.ToString(dt.Rows[0]["resMsg"]);
                        msg.AutoId = string.IsNullOrEmpty(dt.Rows[0]["AutoId"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["AutoId"]);
                        msg.AutoName = string.IsNullOrEmpty(dt.Rows[0]["AutoName"].ToString()) ? "" : Convert.ToString(dt.Rows[0]["AutoName"]);
                        return msg;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
