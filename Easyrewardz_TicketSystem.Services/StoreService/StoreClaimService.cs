using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreClaimService: IStoreClaim
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public StoreClaimService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        public int AddClaimComment(int ClaimID, string Comment, int UserID)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SP_AddStoreClaimComment", conn)
                {
                    Connection = conn
                };
                cmd1.Parameters.AddWithValue("@Claim_ID", ClaimID);
                cmd1.Parameters.AddWithValue("@_Comments", Comment);
                cmd1.Parameters.AddWithValue("@User_ID", UserID);
                cmd1.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd1.ExecuteNonQuery());

            }
            catch (Exception)
            {
                throw;
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

        public List<UserComment> GetClaimComment(int ClaimID)
        {
            DataSet ds = new DataSet();
            List<UserComment> lstClaimComment = new List<UserComment>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetClaimCommentByClaimId", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Claim_ID", ClaimID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        UserComment userComment = new UserComment();
                        userComment.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                        userComment.Comment = Convert.ToString(ds.Tables[0].Rows[i]["Comment"]);
                        userComment.datetime = Convert.ToString(ds.Tables[0].Rows[i]["datetime"]);
                        lstClaimComment.Add(userComment);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return lstClaimComment;
        }

        public List<CustomTaskMasterDetails> GetClaimList()
        {
            throw new NotImplementedException();
        }

        public int RaiseClaim(StoreClaimMaster storeClaimMaster, string finalAttchment)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertRaiseClaim", conn);
                cmd.Parameters.AddWithValue("@Tenant_ID", storeClaimMaster.TenantID);
                cmd.Parameters.AddWithValue("@Created_By", storeClaimMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@fileName", string.IsNullOrEmpty(finalAttchment) ? "" : finalAttchment);
                cmd.Parameters.AddWithValue("@Brand_ID", storeClaimMaster.BrandID);
                cmd.Parameters.AddWithValue("@Category_ID", storeClaimMaster.CategoryID);
                cmd.Parameters.AddWithValue("@SubCategory_ID", storeClaimMaster.SubCategoryID);
                cmd.Parameters.AddWithValue("@IssueType_ID", storeClaimMaster.IssueTypeID);
                cmd.Parameters.AddWithValue("@Order_IDs", string.IsNullOrEmpty(storeClaimMaster.OrderIDs) ? "" : storeClaimMaster.OrderIDs);
                cmd.Parameters.AddWithValue("@Claim_Percent", storeClaimMaster.ClaimPercent);
                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return result;
        }
        #endregion
    }
}
