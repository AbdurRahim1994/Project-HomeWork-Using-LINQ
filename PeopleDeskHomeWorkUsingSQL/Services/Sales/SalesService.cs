using Microsoft.Data.SqlClient;
using PeopleDeskHomeWorkUsingSQL.Helper;
using PeopleDeskHomeWorkUsingSQL.Models.Data;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Sales;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Sales;
using System.Data;

namespace PeopleDeskHomeWorkUsingSQL.Services.Sales
{
    public class SalesService:ISalesService
    {
        private readonly HomeWorkDbContext _context;
        MessageHelper res = new MessageHelper();
        DataTable dt = new DataTable();
        public SalesService(HomeWorkDbContext _context)
        {
            this._context = _context;
        }

        public async Task<MessageHelper> CreateSalesOrder(SalesOrderCommonViewModel obj)
        {
            try
            {
                if (obj == null || obj.sales == null || obj.salesDetails.Count <= 0)
                {
                    res.Message = "Invalid Data";
                    res.StatusCode = 500;
                    return res;
                }
                else
                {
                    using(SqlConnection connection=new SqlConnection(Connection.iPEOPLE_HCM))
                    {
                        string sql = "dbo.sprSalesCRUD";
                        string jsonString = System.Text.Json.JsonSerializer.Serialize(obj.salesDetails);
                        using(SqlCommand cmd=new SqlCommand(sql, connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@strPart", obj.sales.StrPart);
                            cmd.Parameters.AddWithValue("@AutoId", obj.sales.AutoId);
                            cmd.Parameters.AddWithValue("@strCustomerName", obj.sales.StrCustomerName);
                            cmd.Parameters.AddWithValue("@dteSalesDate", obj.sales.DteSalesDate);
                            cmd.Parameters.AddWithValue("@isActive", obj.sales.IsActive);
                            cmd.Parameters.AddWithValue("@intCustomerId", obj.sales.IntCustomerId);
                            cmd.Parameters.AddWithValue("@jsonRowString", jsonString);

                            connection.Open();
                            using(SqlDataAdapter adapter=new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(dt);
                            }
                            connection.Close();

                            res.Message = Convert.ToString(dt.Rows[0]["returnMessage"]);
                            res.StatusCode = Convert.ToInt32(dt.Rows[0]["returnStatus"]);
                            return res;
                        }
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
