using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
   public class JunkWordsService : IJunkWords
    {
        MySqlConnection conn = new MySqlConnection();

        public JunkWordsService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        /// <summary>
        /// Delete Junk Words
        /// </summary>
        /// <param name="junkKeywordID"></param>
        /// <param name="userMasterID"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public int DeleteJunkWords(int junkKeywordID, int userMasterID, int tenantId)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteJunkKeywords", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@junkKeyword_ID", junkKeywordID);
                cmd.Parameters.AddWithValue("@Created_By", userMasterID);
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantId);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());
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
        /// <summary>
        /// Insert Junk Words
        /// </summary>
        /// <param name="JunkWordsMaster"></param>
        /// <returns></returns>
        public int InsertJunkWords(JunkWordsMaster junkWordsMaster)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertJunkKeywords", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Junk_Keyword", string.IsNullOrEmpty(junkWordsMaster.JunkKeyword) ? "" : junkWordsMaster.JunkKeyword.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_Reason", string.IsNullOrEmpty(junkWordsMaster.Reason) ? "" : junkWordsMaster.Reason);
                cmd.Parameters.AddWithValue("@Created_By", junkWordsMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@Tenant_Id", junkWordsMaster.TenantID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
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
        /// <summary>
        /// List JunkWords
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<JunkWordsMaster> ListJunkWords(int tenantId)
        {
            DataSet ds = new DataSet();
            List<JunkWordsMaster> listJunkWords  = new List<JunkWordsMaster>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ListJunkKeywords", conn)
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_Id", tenantId);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        JunkWordsMaster junkWordsMaster = new JunkWordsMaster();
                        junkWordsMaster.JunkKeywordID = ds.Tables[0].Rows[i]["JunkKeywordID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["JunkKeywordID"]);
                        junkWordsMaster.JunkKeyword = ds.Tables[0].Rows[i]["JunkKeywords"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["JunkKeywords"]);
                        junkWordsMaster.Reason = ds.Tables[0].Rows[i]["Reason"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Reason"]);
                        junkWordsMaster.EnteredBy = ds.Tables[0].Rows[i]["EnteredBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["EnteredBy"]);
                        junkWordsMaster.EnteredDate = ds.Tables[0].Rows[i]["EnteredDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["EnteredDate"]);
                        junkWordsMaster.ModifyBy = ds.Tables[0].Rows[i]["ModifyBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyBy"]);
                        junkWordsMaster.ModifyDate = ds.Tables[0].Rows[i]["ModifyDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyDate"]);
                        listJunkWords.Add(junkWordsMaster);
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
            }

            return listJunkWords;
        }
        /// <summary>
        /// Update JunkWords
        /// </summary>
        /// <param name="JunkWordsMaster"></param>
        /// <returns></returns>
        public int UpdateJunkWords(JunkWordsMaster junkWordsMaster)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateJunkKeywords", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@JunkKeyword_ID", junkWordsMaster.JunkKeywordID);
                cmd.Parameters.AddWithValue("@Junk_Keyword", string.IsNullOrEmpty(junkWordsMaster.JunkKeyword) ? "" : junkWordsMaster.JunkKeyword.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_Reason", string.IsNullOrEmpty(junkWordsMaster.Reason) ? "" : junkWordsMaster.Reason);
                cmd.Parameters.AddWithValue("@Created_By", junkWordsMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@Tenant_Id", junkWordsMaster.TenantID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
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
    }
}
