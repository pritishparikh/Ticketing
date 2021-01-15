using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class HSSettingService : IHSSetting
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public HSSettingService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        #region Custom Methods

        /// <summary>
        /// Get Store Agent List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="BrandID"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public List<HSSettingModel> GetStoreAgentList(int tenantID, int BrandID, int StoreID)
        {
            DataSet ds = new DataSet();
            List<HSSettingModel> listHierarchy = new List<HSSettingModel>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetSoreAgent", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@Brand_ID", BrandID);
                cmd.Parameters.AddWithValue("@Store_ID", StoreID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        HSSettingModel hSSettingModel = new HSSettingModel();

                        hSSettingModel.AgentID = ds.Tables[0].Rows[i]["AgentID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["AgentID"]);
                        hSSettingModel.AgentName = ds.Tables[0].Rows[i]["AgentName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AgentName"]);
                        hSSettingModel.EmailID = ds.Tables[0].Rows[i]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["EmailID"]);
                        hSSettingModel.TenantID = ds.Tables[0].Rows[i]["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TenantID"]);
                        hSSettingModel.BrandID = ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        hSSettingModel.StoreID = ds.Tables[0].Rows[i]["StoreID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StoreID"]);
                        hSSettingModel.StoreCode = ds.Tables[0].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        hSSettingModel.Suggestion = ds.Tables[0].Rows[i]["Suggestion"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Suggestion"]);
                        hSSettingModel.FreeText = ds.Tables[0].Rows[i]["FreeText"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["FreeText"]);
                        hSSettingModel.Attachment = ds.Tables[0].Rows[i]["Attachment"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Attachment"]);
                        //hSSettingModel.GrammarlyCheck = ds.Tables[0].Rows[i]["GrammarlyCheck"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["GrammarlyCheck"]);
                        listHierarchy.Add(hSSettingModel);
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
            return listHierarchy;
        }

        /// <summary>
        /// Insert And Update Agent Details
        /// </summary>
        /// <param name="hSSettingModel"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int InsertUpdateAgentDetails(HSSettingModel hSSettingModel, int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSInsertStoreAgentDetails", conn);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.Parameters.AddWithValue("@Brand_ID", hSSettingModel.BrandID);
                cmd1.Parameters.AddWithValue("@Store_Code", hSSettingModel.StoreCode);
                cmd1.Parameters.AddWithValue("@StoreAgent_ID", hSSettingModel.AgentID);
                cmd1.Parameters.AddWithValue("@Suggestion", hSSettingModel.Suggestion);
                cmd1.Parameters.AddWithValue("@Free_Text", hSSettingModel.FreeText);
                cmd1.Parameters.AddWithValue("@_Attachment", hSSettingModel.Attachment);
               // cmd1.Parameters.AddWithValue("@_GrammarlyCheck", hSSettingModel.GrammarlyCheck);

                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();
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

            return i;
        }

        /// <summary>
        /// Get Store Agent Details By Id
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        public List<HSSettingModel> GetStoreAgentDetailsById(int tenantID, int AgentID)
        {
            DataSet ds = new DataSet();
            List<HSSettingModel> listHierarchy = new List<HSSettingModel>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetAgentByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@Agent_ID", AgentID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        HSSettingModel hSSettingModel = new HSSettingModel();
                        hSSettingModel.AgentID = ds.Tables[0].Rows[i]["AgentID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["AgentID"]);
                        hSSettingModel.Suggestion = ds.Tables[0].Rows[i]["Suggestion"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Suggestion"]);
                        hSSettingModel.FreeText = ds.Tables[0].Rows[i]["FreeText"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["FreeText"]);
                        hSSettingModel.Attachment = ds.Tables[0].Rows[i]["Attachment"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Attachment"]);
                       // hSSettingModel.GrammarlyCheck = ds.Tables[0].Rows[i]["GrammarlyCheck"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["GrammarlyCheck"]);
                        listHierarchy.Add(hSSettingModel);
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
            return listHierarchy;
        }
        #endregion
    }
}
