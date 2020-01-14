using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreService : IStore
    {
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public StoreService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        /// <summary>
        /// Create Store
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int CreateStore(StoreMaster storeMaster, int TenantID, int UserID)
        {
            // MySqlCommand cmd = new MySqlCommand();
            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd  = new MySqlCommand("SP_InsertStore", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Brand_ID", storeMaster.BrandID);
                cmd.Parameters.AddWithValue("@Store_Code", storeMaster.StoreCode);
                cmd.Parameters.AddWithValue("@Store_Name", storeMaster.StoreName);
                cmd.Parameters.AddWithValue("@State_ID", storeMaster.StateID);
                cmd.Parameters.AddWithValue("@City_ID", storeMaster.CityID);
                cmd.Parameters.AddWithValue("@Pincode_ID", storeMaster.PincodeID);
                cmd.Parameters.AddWithValue("@Store_Address", storeMaster.Address);
                cmd.Parameters.AddWithValue("@Region_ID", storeMaster.RegionID);
                cmd.Parameters.AddWithValue("@Zone_ID", storeMaster.ZoneID);
                cmd.Parameters.AddWithValue("@StoreType_ID", storeMaster.StoreTypeID);
                cmd.Parameters.AddWithValue("@StoreEmail_ID", storeMaster.StoreEmailID);
                cmd.Parameters.AddWithValue("@StorePhone_No", storeMaster.StorePhoneNo);
                cmd.Parameters.AddWithValue("@Is_Active", storeMaster.IsActive);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                Success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return Success;
        }
        /// <summary>
        /// Delete Store 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int DeleteStore(int StoreID, int TenantID, int UserID)
        {

            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteStore", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Store_ID", StoreID);
                cmd.Parameters.AddWithValue("@tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

        /// <summary>
        /// Edit Store
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int EditStore(StoreMaster storeMaster, int StoreID, int TenantID, int UserID)
        {
            int Success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateStore", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Brand_ID", storeMaster.BrandID);
                cmd.Parameters.AddWithValue("@Store_Code", storeMaster.StoreCode);
                cmd.Parameters.AddWithValue("@Store_Name", storeMaster.StoreName);
                cmd.Parameters.AddWithValue("@State_ID", storeMaster.StateID);
                cmd.Parameters.AddWithValue("@City_ID", storeMaster.CityID);
                cmd.Parameters.AddWithValue("@Pincode_ID", storeMaster.PincodeID);
                cmd.Parameters.AddWithValue("@Store_Address", storeMaster.Address);
                cmd.Parameters.AddWithValue("@Region_ID", storeMaster.RegionID);
                cmd.Parameters.AddWithValue("@Zone_ID", storeMaster.ZoneID);
                cmd.Parameters.AddWithValue("@StoreType_ID", storeMaster.StoreTypeID);
                cmd.Parameters.AddWithValue("@StoreEmail_ID", storeMaster.StoreEmailID);
                cmd.Parameters.AddWithValue("@StorePhone_No", storeMaster.StorePhoneNo);
                cmd.Parameters.AddWithValue("@Is_Active", storeMaster.IsActive);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@Store_ID", StoreID);
                cmd.CommandType = CommandType.StoredProcedure;
                Success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return Success;
        }

        public List<StoreMaster> getStoreDetailByStorecodenPincode(string searchText, int tenantID)
        {
            List<StoreMaster> storeMaster = new List<StoreMaster>();
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getStoreSDetialwithStorenamenPincode", conn);
                cmd1.Parameters.AddWithValue("@searchText", searchText);
                cmd1.Parameters.AddWithValue("@Tenant_Id", tenantID);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreMaster store = new StoreMaster();
                        store.StoreCode = Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        store.StoreName = Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);
                        store.Pincode = Convert.ToString(ds.Tables[0].Rows[i]["Pincode"]);
                        store.StoreEmailID = Convert.ToString(ds.Tables[0].Rows[i]["StoreEmailID"]);
                        store.Address = Convert.ToString(ds.Tables[0].Rows[i]["Address"]);
                        store.StoreID = Convert.ToInt32(ds.Tables[0].Rows[i]["StoreID"]);
                        storeMaster.Add(store);
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
            return storeMaster;
        }
        /// <summary>
        /// Get list of the Stores
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<StoreMaster> getStores(string searchText, int tenantID)
        {
            List<StoreMaster> storeMaster = new List<StoreMaster>();
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getStores", conn);
                cmd1.Parameters.AddWithValue("@Tenant_Id", tenantID);
                cmd1.Parameters.AddWithValue("@searchText", searchText);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreMaster store = new StoreMaster();
                        store.StoreID = Convert.ToInt32(ds.Tables[0].Rows[i]["StoreID"]);
                        store.StoreName = Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);
                        store.Address = Convert.ToString(ds.Tables[0].Rows[i]["Address"]);

                        storeMaster.Add(store);
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
            return storeMaster;
        }
        #endregion
    }
}