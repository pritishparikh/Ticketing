﻿using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static Easyrewardz_TicketSystem.CustomModel.EnumMaster;

namespace Easyrewardz_TicketSystem.Services.StoreServices
{
    public class MasterService: IMaster
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public MasterService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        #endregion

        /// <summary>
        /// Get Store User List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<StoreUser> GetStoreUserList(int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreUser> users = new List<StoreUser>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreUserFullName", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@User_ID", UserID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreUser user = new StoreUser();
                        user.UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        user.FullName = Convert.ToString(ds.Tables[0].Rows[i]["FullName"]);
                        user.ReporteeID = Convert.ToInt32(ds.Tables[0].Rows[i]["ReporteeID"]);
                        user.RoleID = Convert.ToInt32(ds.Tables[0].Rows[i]["RoleID"]);
                        user.RoleName = Convert.ToString(ds.Tables[0].Rows[i]["RoleName"]);

                        users.Add(user);
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

            return users;
        }

        /// <summary>
        /// Get Store Function List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<StoreFunctionModel> GetStoreFunctionList(int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreFunctionModel> users = new List<StoreFunctionModel>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreUserFullName", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@User_ID", UserID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreFunctionModel user = new StoreFunctionModel();
                        user.FunctionID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        user.FuncationName = Convert.ToString(ds.Tables[0].Rows[i]["FullName"]);
                        user.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);

                        users.Add(user);
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

            return users;
        }

        /// <summary>
        /// Get Region List
        /// </summary>
        /// <returns></returns>
        public RegionZoneList GetRegionlist(int UserID)
        {

            DataSet ds = new DataSet();
            List<RegionMaster> regionMaster = new List<RegionMaster>();
            RegionZoneList regionZoneList = new RegionZoneList();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetRegionZoneByUserID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_UserID", UserID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        RegionMaster region = new RegionMaster
                        {
                            RegionID = Convert.ToInt32(ds.Tables[0].Rows[i]["RegionID"]),
                            RegionName = Convert.ToString(ds.Tables[0].Rows[i]["RegionName"])
                        };
                        regionMaster.Add(region);
                    }
                    regionZoneList.regionMasters = regionMaster;
                }

                List<EnumModel> enumModel = ((Zones[])Enum.GetValues(typeof(Zones))).Select(c => new EnumModel() { Value = (int)c, Name = c.ToString() }).ToList();
                regionZoneList.enumModels = enumModel;
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
            return regionZoneList;
        }
    }

    
}
