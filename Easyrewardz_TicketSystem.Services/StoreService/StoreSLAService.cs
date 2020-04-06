using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreSLAService : IStoreSLA
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion
        MySqlConnection conn = new MySqlConnection();

        public StoreSLAService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }


        /// <summary>
        /// Bind issuetype 
        /// <param name="tenantID"></param>
        /// <param name="SearchText"></param>
        /// </summary>
        /// 
        public List<FunctionList> BindFunctionList(int tenantID, string SearchText)
        {
            List<FunctionList> objFunctionLst = new List<FunctionList>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetFunctionForStoreSLA", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd1.Parameters.AddWithValue("@Search_Text", string.IsNullOrEmpty(SearchText) ? "" : SearchText);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objFunctionLst = ds.Tables[0].AsEnumerable().Select(r => new FunctionList()
                        {
                            FunctionID = Convert.ToInt32(r.Field<object>("FunctionID")),
                            BrandID = Convert.ToInt32(r.Field<object>("BrandID")),
                            DepartmentID = Convert.ToInt32(r.Field<object>("DepartmentID")),
                            StoreID = Convert.ToInt32(r.Field<object>("StoreID")),

                            FunctionName = r.Field<object>("FunctionName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FunctionName")),
                            BrandName = r.Field<object>("BrandName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("BrandName")),
                            DepartmentName = r.Field<object>("DepartmentName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("DepartmentName")),
                            StoreName = r.Field<object>("StoreName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("StoreName")),

                            

                        }).ToList();
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


            return objFunctionLst;
        }

        /// <summary>
        /// Create SLA 
        /// <param name="SLAModel"></param>
        /// </summary>
        public int InsertStoreSLA(StoreSLAModel SLA)
        {

            List<int> ListSlaID = new List<int>();
            int SLATargetInsertCount = 0;
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertSLAMaster", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_tenantID", SLA.TenantID);
                cmd.Parameters.AddWithValue("@_createdBy", SLA.CreatedBy);
                cmd.Parameters.AddWithValue("@function_ID", string.IsNullOrEmpty(SLA.FunctionID) ? "": SLA.FunctionID.TrimEnd(','));
                cmd.Parameters.AddWithValue("@isSLAActive", Convert.ToInt16(SLA.isSLAActive));

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int slaID = ds.Tables[0].Rows[i]["SLAIDS"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SLAIDS"]);

                            if (slaID > 0)
                            {
                                ListSlaID.Add(slaID);
                            }
                        }

                    }

                    if (ListSlaID.Count > 0)
                    {
                        if (SLA.SLATarget.Count > 0)
                        {
                            for (int k = 0; k < ListSlaID.Count; k++)
                            {
                                for (int j = 0; j < SLA.SLATarget.Count; j++)
                                {
                                    MySqlCommand Targetcmd = new MySqlCommand("SP_InsertStoreSLATarget", conn);
                                    Targetcmd.Connection = conn;
                                    Targetcmd.Parameters.AddWithValue("@_slaID", ListSlaID[k]);
                                    Targetcmd.Parameters.AddWithValue("@_priorityID", SLA.SLATarget[j].PriorityID);
                                    Targetcmd.Parameters.AddWithValue("@_prioritySLABreach", SLA.SLATarget[j].SLABreachPercent);
                                    Targetcmd.Parameters.AddWithValue("@_priorityResolutionValue", SLA.SLATarget[j].PriorityResolutionValue);
                                    Targetcmd.Parameters.AddWithValue("@_priorityResolutionDuraton", SLA.SLATarget[j].PriorityResolutionDuration);
                                    Targetcmd.Parameters.AddWithValue("@_createdBy", SLA.CreatedBy);
                                    Targetcmd.CommandType = CommandType.StoredProcedure;
                                    SLATargetInsertCount += Targetcmd.ExecuteNonQuery();
                                }
                            }

                        }
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

            return SLATargetInsertCount;
        }


        /// <summary>
        /// Create SLA 
        /// <param name="SLAModel"></param>
        /// </summary>
        public int UpdateStoreSLA(StoreSLAModel SLA)
        {

            List<int> ListSlaID = new List<int>();
            int SLATargetUpdateCount = 0;
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateStoreSLAMaster", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tenant_ID", SLA.TenantID);
                cmd.Parameters.AddWithValue("@modified_By", SLA.CreatedBy);
                cmd.Parameters.AddWithValue("@SLA_ID", SLA.SlaID);
                cmd.Parameters.AddWithValue("@Is_Active", SLA.isSLAActive);
                cmd.ExecuteScalar();

                if (SLA != null)
                {
                    if (SLA.SLATarget.Count > 0)
                    {
                        for (int k = 0; k < SLA.SLATarget.Count; k++)
                        {
                            MySqlCommand cmdSLA = new MySqlCommand("SP_UpdateStoreSLATargetMaster", conn);
                            cmdSLA.Connection = conn;
                            cmdSLA.Parameters.AddWithValue("@SLATarget_ID", SLA.SLATarget[k].SLATargetID);
                            cmdSLA.Parameters.AddWithValue("@priority_ID", SLA.SLATarget[k].PriorityID);
                            cmdSLA.Parameters.AddWithValue("@priority_SLABreach", SLA.SLATarget[k].SLABreachPercent);
                            cmdSLA.Parameters.AddWithValue("@priority_ResolutionValue", SLA.SLATarget[k].PriorityResolutionValue);
                            cmdSLA.Parameters.AddWithValue("@priority_ResolutionType", SLA.SLATarget[k].PriorityResolutionDuration);
                            cmdSLA.Parameters.AddWithValue("@modified_By", SLA.CreatedBy);
                            cmdSLA.Parameters.AddWithValue("@tenant_ID", SLA.TenantID);
                            cmdSLA.Parameters.AddWithValue("@Sla_ID", SLA.SlaID);
                            cmdSLA.CommandType = CommandType.StoredProcedure;
                            SLATargetUpdateCount+=cmdSLA.ExecuteNonQuery();
                        }
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

            return SLATargetUpdateCount;
        }


        /// <summary>
        /// Delete SLA
        /// <param name="tenantID"></param>
        /// <param name="SLAID"></param>
        /// </summary>
        public int DeleteStoreSLA(int tenantID, int SLAID)
        {
            int deletecount = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteStoreSLA", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_slaID", SLAID);


                cmd.CommandType = CommandType.StoredProcedure;
                deletecount = Convert.ToInt32(cmd.ExecuteScalar());
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
            return deletecount;

        }


        /// <summary>
        /// GET SLA
        /// <param name="tenantID"></param>
        /// <param name="SLAFor"></param>
        /// </summary>
        public List<StoreSLAResponseModel> StoreSLAList(int tenantID)
        {
            List<StoreSLAResponseModel> objSLALst = new List<StoreSLAResponseModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreSLAList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objSLALst = ds.Tables[0].AsEnumerable().Select(r => new StoreSLAResponseModel()
                        {
                            SLAID = Convert.ToInt32(r.Field<object>("SlaId")),

                            FunctionID = r.Field<object>("FunctionID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("FunctionID")),
                            FunctionName = r.Field<object>("FunctionName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FunctionName")),
                           
                            BrandID = r.Field<object>("BrandID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("BrandID")),
                            BrandName = r.Field<object>("BrandName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("BrandName")),
                            StoreID = r.Field<object>("StoreID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("StoreID")),
                            StoreName = r.Field<object>("StoreName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("StoreName")),
                            DepartmentID = r.Field<object>("DepartmentID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("DepartmentID")),
                            DepartmentName = r.Field<object>("DepartmentName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("DepartmentName")),
                            isSLAActive = r.Field<object>("SLAStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("SLAStatus")),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),
                        }).ToList();
                    }

                    if (objSLALst.Count > 0)
                    {
                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < objSLALst.Count; i++)
                            {
                                objSLALst[i].SLATarget = ds.Tables[1].AsEnumerable().Where(r => r.Field<object>("SlaID") != System.DBNull.Value &&
                                    objSLALst[i].SLAID == Convert.ToInt32(r.Field<object>("SlaID"))).Select(r => new StoreSLATargetResponseModel()
                                    {
                                        SLATargetID = Convert.ToInt32(r.Field<object>("SLATargetID")),  
                                        PriorityID = Convert.ToInt32(r.Field<object>("PriorityID")),
                                        PriorityName = r.Field<object>("PriortyName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriortyName")),
                                        SLABreachPercent = r.Field<object>("PriorityBreach") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriorityBreach")),
                                        PriorityResolution = r.Field<object>("PriorityResolve") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("PriorityResolve")),
                                        PriorityResolutionDuration = r.Field<object>("PriorityResolutionDuration") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriorityResolutionDuration")),
                                    }).ToList();
                            }

                        }
                    }
                }

                
            }
            catch (Exception ex)
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


            return objSLALst;

        }

        #region ON EDIT CLICK

        /*
        /// <summary>
        /// Get Store SLA details for Edit using SLAID
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="SLAID"></param>
        /// <returns></returns>
        public StoreSLAResponseModel GetSLADetail(int tenantID, int SLAID)
        {
            StoreSLAResponseModel objSLADetail = new StoreSLAResponseModel();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreSLADetailsBySLAID", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd1.Parameters.AddWithValue("@SLA_ID", SLAID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objSLADetail.SLAID = Convert.ToInt16(ds.Tables[0].Rows[0]["SLAID"]);

                        objSLADetail.FunctionID = Convert.ToInt16(ds.Tables[0].Rows[0]["FunctionID"]);
                        objSLADetail.FunctionName = Convert.ToString(ds.Tables[0].Rows[0]["FunctionName"]);

                        objSLADetail.BrandID = Convert.ToInt16(ds.Tables[0].Rows[0]["BrandID"]);
                        objSLADetail.BrandName = Convert.ToString(ds.Tables[0].Rows[0]["BrandName"]);

                        objSLADetail.DepartmentID = Convert.ToInt16(ds.Tables[0].Rows[0]["DepartmentID"]);
                        objSLADetail.DepartmentName = Convert.ToString(ds.Tables[0].Rows[0]["DepartmentName"]);

                        objSLADetail.StoreID = Convert.ToInt16(ds.Tables[0].Rows[0]["StoreID"]);
                        objSLADetail.StoreName = Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]);

                        objSLADetail.isSLAActive = Convert.ToString(ds.Tables[0].Rows[0]["SubCategoryName"]);

                    }


                    List<StoreSLATargetResponseModel> sLATargetDetails = new List<StoreSLATargetResponseModel>();

                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            StoreSLATargetResponseModel sLATargetDetail = new StoreSLATargetResponseModel();
                            sLATargetDetail.SLATargetID = Convert.ToInt32(ds.Tables[1].Rows[i]["SLATargetID"]);
                            sLATargetDetail.PriorityID = Convert.ToInt32(ds.Tables[1].Rows[i]["PriorityID"]);
                            sLATargetDetail.PriorityName = Convert.ToString(ds.Tables[1].Rows[i]["PriortyName"]);
                        
                            sLATargetDetail.SLABreachPercent = Convert.ToInt32(ds.Tables[1].Rows[i]["SLABreachPercent"]);
                            sLATargetDetail.PriorityResolution = Convert.ToInt32(ds.Tables[1].Rows[i]["PriorityResolution"]);
                            sLATargetDetail.PriorityResolutionDuration = Convert.ToString(ds.Tables[1].Rows[i]["PriorityResolutionDuration"]);

                            sLATargetDetails.Add(sLATargetDetail);
                        }
                    }

                    objSLADetail.SLATarget = sLATargetDetails;
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

            return objSLADetail;

        }

        */

        #endregion
    }
}
