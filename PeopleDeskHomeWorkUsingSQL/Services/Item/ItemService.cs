using Microsoft.Data.SqlClient;
using PeopleDeskHomeWorkUsingSQL.Helper;
using PeopleDeskHomeWorkUsingSQL.Models.Data;
using PeopleDeskHomeWorkUsingSQL.Models.ViewModels.Item;
using PeopleDeskHomeWorkUsingSQL.Services.Interfaces.Item;
using System.Data;
using System.Text.Json;

namespace PeopleDeskHomeWorkUsingSQL.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly HomeWorkDbContext _context;
        DataTable dt = new DataTable();
        MessageHelper res = new MessageHelper();
        public ItemService(HomeWorkDbContext _context)
        {
            this._context = _context;
        }
        public async Task<MessageHelper> CreateItem(ItemViewModel obj)
        {
            try
            {
                using(SqlConnection connection=new SqlConnection(Connection.iPEOPLE_HCM))
                {
                    string sql = "dbo.sprItemCRUD";
                    using(SqlCommand cmd=new SqlCommand(sql, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@strPart", obj.StrPart);
                        cmd.Parameters.AddWithValue("@AutoId", obj.IntAutoId);
                        cmd.Parameters.AddWithValue("@intItemId", obj.IntItemId);
                        cmd.Parameters.AddWithValue("@strItemName", obj.StrItemName);
                        cmd.Parameters.AddWithValue("@numStockQuantity", obj.NumStockQuantity);
                        cmd.Parameters.AddWithValue("@numStockPrice", obj.NumStockPrice);
                        cmd.Parameters.AddWithValue("@numTotalPrice", obj.NumTotalPrice);
                        cmd.Parameters.AddWithValue("@isActive", obj.IsActive);
                        cmd.Parameters.AddWithValue("@intCreatedBy", obj.IntCreatedBy);
                        cmd.Parameters.AddWithValue("@dteCreatedAt", obj.DteCreatedAt);
                        cmd.Parameters.AddWithValue("@intUpdatedBy", obj.IntUpdatedBy);
                        cmd.Parameters.AddWithValue("@dteUpdatedAt", obj.DteUpdatedAt);

                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                        connection.Close();

                        res.Message = Convert.ToString(dt.Rows[0]["returnMessage"]);
                        res.StatusCode = Convert.ToInt32(dt.Rows[0]["returnStatus"]);
                        res.AutoId = string.IsNullOrEmpty(dt.Rows[0]["AutoId"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["AutoId"]);
                        res.AutoName = string.IsNullOrEmpty(dt.Rows[0]["AutoName"].ToString()) ? "" : Convert.ToString(dt.Rows[0]["AutoName"]);
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

        public async Task<MessageHelper> CreateItemList(List<ItemViewModel> obj)
        {
            try
            {
                using(SqlConnection connection=new SqlConnection(Connection.iPEOPLE_HCM))
                {
                    string jsonString = JsonSerializer.Serialize(obj);
                    string sql = "dbo.sprItemListCRUD";
                    using(SqlCommand cmd=new SqlCommand(sql, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@strPart", obj.Select(x => x.StrPart).FirstOrDefault());
                        //cmd.Parameters.AddWithValue("@AutoId", obj.Select(x => x.IntAutoId).FirstOrDefault());
                        cmd.Parameters.AddWithValue("@jsonString", jsonString);

                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                        connection.Close();

                        res.Message = Convert.ToString(dt.Rows[0]["returnMessage"]);
                        res.StatusCode = Convert.ToInt32(dt.Rows[0]["returnStatus"]);
                        res.AutoId = string.IsNullOrEmpty(dt.Rows[0]["AutoId"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["AutoId"]);
                        res.AutoName = string.IsNullOrEmpty(dt.Rows[0]["AutoName"].ToString()) ? "" : Convert.ToString(dt.Rows[0]["AutoName"]);
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
