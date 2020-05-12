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
        public List<HSSettingModel> GetStoreAgentList(int tenantID, int BrandID, string StoreCode)
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
                cmd.Parameters.AddWithValue("@Store_Code", (string.IsNullOrEmpty(StoreCode) ? string.Empty : StoreCode));

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
                        hSSettingModel.StoreCode = ds.Tables[0].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        hSSettingModel.Suggestion = ds.Tables[0].Rows[i]["Suggestion"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Suggestion"]);
                        hSSettingModel.FreeText = ds.Tables[0].Rows[i]["FreeText"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["FreeText"]);
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

                cmd1.CommandType = CommandType.StoredProcedure;
                i = cmd1.ExecuteNonQuery();
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
