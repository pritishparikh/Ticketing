using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public class BlockEmailServices : IBlockEmail
    {
        MySqlConnection conn = new MySqlConnection();

        public BlockEmailServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        public int DeleteBlockEmail(int blockEmailID, int UserMasterID, int TenantId)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd  = new MySqlCommand("SP_DeleteBlockEmail", conn);
                cmd.Connection = conn;         
                cmd.Parameters.AddWithValue("@blockEmail_ID", blockEmailID);
                cmd.Parameters.AddWithValue("@Created_By", UserMasterID);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return success;
        }

        public int InsertBlockEmail(BlockEmailMaster blockEmailMaster)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddBlockEmail", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Email_ID", string.IsNullOrEmpty(blockEmailMaster.EmailID) ? "" :blockEmailMaster.EmailID.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_Reason", string.IsNullOrEmpty(blockEmailMaster.Reason) ? "" : blockEmailMaster.Reason);
                cmd.Parameters.AddWithValue("@Created_By", blockEmailMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@Tenant_Id", blockEmailMaster.TenantID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return success;
        }

        public List<BlockEmailMaster> ListBlockEmail(int TenantId)
        {
            DataSet ds = new DataSet();
            List<BlockEmailMaster> listBlockEmail = new List<BlockEmailMaster>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ListBlockEmail", conn)
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantId);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        BlockEmailMaster blockEmailMaster = new BlockEmailMaster();
                        blockEmailMaster.BlockEmailID =ds.Tables[0].Rows[i]["BlockEmailID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BlockEmailID"]);
                        blockEmailMaster.EmailID = ds.Tables[0].Rows[i]["EmailId"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["EmailId"]);
                        blockEmailMaster.Reason = ds.Tables[0].Rows[i]["Reason"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Reason"]);
                        blockEmailMaster.BlockedBy = ds.Tables[0].Rows[i]["BlockedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BlockedBy"]);
                        blockEmailMaster.BlockedDate = ds.Tables[0].Rows[i]["BlockedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BlockedDate"]);
                        blockEmailMaster.ModifyBy = ds.Tables[0].Rows[i]["ModifyBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyBy"]);
                        blockEmailMaster.ModifyDate = ds.Tables[0].Rows[i]["ModifyDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyDate"]);
                        listBlockEmail.Add(blockEmailMaster);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return listBlockEmail;
        }

        public int UpdateBlockEmail(BlockEmailMaster blockEmailMaster)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateBlockEmail", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@BlockEmail_ID", blockEmailMaster.BlockEmailID);
                cmd.Parameters.AddWithValue("@Email_ID", string.IsNullOrEmpty(blockEmailMaster.EmailID) ? "" : blockEmailMaster.EmailID.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_Reason", string.IsNullOrEmpty(blockEmailMaster.Reason) ? "" : blockEmailMaster.Reason);
                cmd.Parameters.AddWithValue("@Created_By", blockEmailMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@Tenant_Id", blockEmailMaster.TenantID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return success;
        }

    }
}
