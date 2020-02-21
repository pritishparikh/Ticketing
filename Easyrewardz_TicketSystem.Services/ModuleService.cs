﻿using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class ModuleService : IModules
    {

        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();

        public ModuleService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        #endregion

        #region custom methods

        /// <summary>
        /// UpdateModules
        /// </summary>
        public int UpdateModules(int tenantID, int ModuleID, string ModulesActive, string ModuleInactive, int ModifiedBy)
        {
            int updatecount = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Sp_UpdateModuleItems", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_ModuleID", ModuleID);
                cmd.Parameters.AddWithValue("@_modifiedBy", ModifiedBy);

                cmd.Parameters.AddWithValue("@_ActiveModuleItems",!string.IsNullOrEmpty(ModulesActive)? ModulesActive: "");
                cmd.Parameters.AddWithValue("@_InactiveModuleItems", !string.IsNullOrEmpty(ModuleInactive) ? ModuleInactive : "" );
        
                cmd.CommandType = CommandType.StoredProcedure;
                updatecount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return updatecount;
        }





        /// <summary>
        /// GetModulesItemList
        /// </summary>
        public List<ModuleItems> GetModulesItemList(int tenantID, int ModuleID)
        {
            List<ModuleItems> objModuleItemLst = new List<ModuleItems>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetModuleItemList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd1.Parameters.AddWithValue("@_moduleID", ModuleID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objModuleItemLst = ds.Tables[0].AsEnumerable().Select(r => new ModuleItems()
                        {
                            ModuleID = ModuleID,
                            ModuleItemID = r.Field<object>("ModuleItemID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("ModuleItemID")),
                            ModuleItemName   = r.Field<object>("ModuleItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ModuleItemName")),
                            ModuleItemisActive = r.Field<object>("IsActive") == System.DBNull.Value ? false: Convert.ToBoolean(Convert.ToInt16(r.Field<object>("IsActive"))),
                           
                        }).ToList();

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
                if (ds != null) ds.Dispose(); conn.Close();
            }

            return objModuleItemLst;
        }


        // <summary>
        /// GetModulesList
        /// </summary>
        public List<ModulesModel> GetModulesList(int tenantID)
        {
            List<ModulesModel> objModuleLst = new List<ModulesModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_ModuleList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objModuleLst = ds.Tables[0].AsEnumerable().Select(r => new ModulesModel()
                        {
                            ModuleID = Convert.ToInt32(r.Field<object>("ModuleID")),

                            ModuleName = r.Field<object>("ModuleName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ModuleName")),
                            ModuleisActive = r.Field<object>("IsActive") == System.DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt16(r.Field<object>("IsActive"))),


                        }).ToList();

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
                if (ds != null) ds.Dispose(); conn.Close();
            }

            return objModuleLst;
        }
        #endregion
    }
}
