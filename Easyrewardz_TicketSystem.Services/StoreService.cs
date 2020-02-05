﻿using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreService : IStore
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public StoreService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        public int AttachStore(string StoreId, int TicketId, int CreatedBy)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_BulkTicketStoreMapping", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Ticket_Id", TicketId);
                cmd.Parameters.AddWithValue("@StoreIds", StoreId);
                cmd.Parameters.AddWithValue("@Created_By", CreatedBy);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());
                conn.Close();
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

        /// <summary>
        /// Create Store
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int CreateStore(StoreMaster storeMaster, int TenantID, int UserID)
        {
            // MySqlCommand cmd = new MySqlCommand();
            int storeId = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertStore", conn);
                cmd.Connection = conn;
                //cmd.Parameters.AddWithValue("@Brand_ID", storeMaster.BrandID);
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
                cmd.Parameters.AddWithValue("@BrandIDs", storeMaster.BrandIDs);
                cmd.CommandType = CommandType.StoredProcedure;
                storeId = Convert.ToInt32(cmd.ExecuteScalar());

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

            return storeId;
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
                //cmd.Parameters.AddWithValue("@Brand_ID", storeMaster.BrandID);
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
                cmd.Parameters.AddWithValue("@BrandIDs", storeMaster.BrandIDs);
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

        public List<StoreMaster> SearchStore(int StateID, int PinCode, string Area, bool IsCountry)
        {
            List<StoreMaster> storeMaster = new List<StoreMaster>();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_SearchStore", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@State_ID", StateID);
                cmd.Parameters.AddWithValue("@Pin_Code", PinCode);
                cmd.Parameters.AddWithValue("@Store_Area", Area);
                cmd.Parameters.AddWithValue("@Is_Country", IsCountry);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreMaster store = new StoreMaster();
                        store.StoreID = Convert.ToInt32(ds.Tables[0].Rows[i]["StoreID"]);
                        store.StoreName = Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);
                        store.StoreCode = Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        store.StorePhoneNo = Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        //store.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i][""]);
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
        /// Get list of Stores
        /// </summary>
        /// <param name="TenantID">Id of the Tenant</param>
        /// <returns></returns>
        public List<CustomStoreList> StoreList(int TenantID)
        {
            List<CustomStoreList> storeMaster = new List<CustomStoreList>();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomStoreList store = new CustomStoreList();
                        store.StoreID = ds.Tables[0].Rows[i]["StoreID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StoreID"]);
                        store.StoreName = ds.Tables[0].Rows[i]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);
                        store.StoreCode = ds.Tables[0].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        //store.BranName = Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        store.CityName = ds.Tables[0].Rows[i]["CityName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CityName"]);
                        store.StateName = ds.Tables[0].Rows[i]["StateName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StateName"]);
                        store.strPinCode = ds.Tables[0].Rows[i]["PincodeID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PincodeID"]);
                        store.Status = ds.Tables[0].Rows[i]["StoreStatus"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreStatus"]);

                        store.Address = ds.Tables[0].Rows[i]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Address"]); 
                        store.PhoneNumber = ds.Tables[0].Rows[i]["StorePhoneNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StorePhoneNo"]);
                        store.Email= ds.Tables[0].Rows[i]["StoreEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreEmailID"]);

                        store.CityID = ds.Tables[0].Rows[i]["CityID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CityID"]);
                        store.StateID = ds.Tables[0].Rows[i]["StateID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StateID"]);
                        store.RegionID = ds.Tables[0].Rows[i]["RegionID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["RegionID"]);
                        store.ZoneID = ds.Tables[0].Rows[i]["ZoneID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ZoneID"]);
                        store.StoreTypeID = ds.Tables[0].Rows[i]["StoreTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StoreTypeID"]);
                        store.StatusID = Convert.ToBoolean(ds.Tables[0].Rows[i]["StatusID"]);
                        store.BrandIDs = ds.Tables[0].Rows[i]["BrandIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandIDs"]);
                        store.BrandNames = ds.Tables[0].Rows[i]["BrandNames"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandNames"]);
                        store.Brand_Names = ds.Tables[0].Rows[i]["Brand_Names"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Brand_Names"]);

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
        /// Get list of Stores
        /// </summary>
        /// <param name="TicketId">Id of the Ticket</param>
        /// <returns></returns>
        public List<StoreMaster> getSelectedStoreByTicketId(int TicketId)
        {
            List<StoreMaster> storeMaster = new List<StoreMaster>();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetSelectedStoresByTicketID", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Ticket_ID", TicketId);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand = cmd;
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

        public int BulkUploadStore(int TenantID, int CreatedBy, DataSet DataSetCSV)
        {
            int uploadcount = 0;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                if (DataSetCSV != null && DataSetCSV.Tables.Count > 0)
                {
                    if (DataSetCSV.Tables[0] != null && DataSetCSV.Tables[0].Rows.Count > 0)
                    {

                        xmlDoc.LoadXml(DataSetCSV.GetXml());

                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("", conn);
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                        cmd.Parameters.AddWithValue("@_node", Xpath);
                        cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                        cmd.Parameters.AddWithValue("@_createdBy", CreatedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
                        uploadcount = cmd.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (DataSetCSV != null)
                {
                    DataSetCSV.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return uploadcount;
        }

        #endregion
    }
}