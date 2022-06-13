using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using PeopleDeskHomeWorkUsingSQL.Helper;
using PeopleDeskHomeWorkUsingSQL.Models.Data;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Purchase;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Purchase;
using System.Data;

namespace PeopleDeskHomeWorkUsingSQL.Services.Purchase
{
    public class PurchaseService:IPurchaseService
    {
        private readonly HomeWorkDbContext _context;
        DataTable dt = new DataTable();
        MessageHelper res = new MessageHelper();
        public PurchaseService(HomeWorkDbContext _context)
        {
            this._context = _context;
        }

        public async Task<MessageHelper> PurchaseOrder(PurchaseOrderCommonViewModel obj)
        {
            try
            {
                using(SqlConnection connection=new SqlConnection(Connection.iPEOPLE_HCM))
                {
                    string sql = "dbo.sprPurchaseCRUD";
                    using(SqlCommand cmd=new SqlCommand(sql, connection))
                    {
                        string jsonString = JsonConvert.SerializeObject(obj.purchaseDetails);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@strPart", obj.StrPart);
                        cmd.Parameters.AddWithValue("@AutoId", obj.AutoId);
                        cmd.Parameters.AddWithValue("@intSupplierId", obj.purchase.IntSupplierId);
                        cmd.Parameters.AddWithValue("@strSupplierName", obj.purchase.StrSupplierName);
                        cmd.Parameters.AddWithValue("@dtePurchaseDate", obj.purchase.DtePurchaseDate);
                        cmd.Parameters.AddWithValue("@isActive", obj.purchase.IsActive);
                        cmd.Parameters.AddWithValue("@jsonRowString", jsonString);

                        connection.Open();
                        using(SqlDataAdapter adapter=new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                        connection.Close();

                        res.StatusCode = Convert.ToInt32(dt.Rows[0]["statusCode"]);
                        res.Message = Convert.ToString(dt.Rows[0]["returnMessage"]);
                        return res;
                    }
                }
            }
            catch (Exception ex)
            {

                res.Message = ex.Message;
                res.StatusCode = 500;
                return res;
            }
        }
    }
}
