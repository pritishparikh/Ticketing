using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.CustomModel;
using System.Xml;

namespace Easyrewardz_TicketSystem.Services
{
    public class SLAServices : ISLA
    {
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion
        MySqlConnection conn = new MySqlConnection();

        public SLAServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<SLAStatus> GetSLAStatusList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SLAStatus> slas = new List<SLAStatus>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetSLAStatusList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SLAStatus sla = new SLAStatus();
                        sla.SLAId = Convert.ToInt32(ds.Tables[0].Rows[i]["SlaId"]);
                        sla.SLATargetId = Convert.ToInt32(ds.Tables[0].Rows[i]["SLATargetId"]);
                        sla.TenatID = Convert.ToInt32(ds.Tables[0].Rows[i]["TenantId"]);
                        sla.SLARequestResponse = Convert.ToString(ds.Tables[0].Rows[i]["Respond"]) + "/" + Convert.ToString(ds.Tables[0].Rows[i]["Resolution"]);

                        slas.Add(sla);
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

            return slas;
        }

        /// <summary>
        /// Create SLA 
        /// </summary>
        public int InsertSLA(SLAModel SLA)
        {
            
            List<int> ListSlaID= new List<int>();
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
                cmd.Parameters.AddWithValue("@_issueType", SLA.IssueTypeID);
                cmd.Parameters.AddWithValue("@isSLAActive", Convert.ToInt16(SLA.isSLAActive));
                cmd.Parameters.AddWithValue("@_SLAFor", SLA.SLAFor); 

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for(int i=0;i< ds.Tables[0].Rows.Count; i++)
                        {
                            int slaID = ds.Tables[0].Rows[i]["SLAIDS"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SLAIDS"]);

                            if(slaID > 0)
                            {
                                ListSlaID.Add(slaID);
                            }
                        }

                    }

                    if(ListSlaID.Count > 0 )
                    {
                        if(SLA.SLATarget.Count > 0)
                        {
                            for(int k=0; k< ListSlaID.Count;k++)
                            {
                                for (int j = 0; j < SLA.SLATarget.Count; j++)
                                {
                                    MySqlCommand Targetcmd = new MySqlCommand("SP_InsertSLATarget", conn);
                                    Targetcmd.Connection = conn;
                                    Targetcmd.Parameters.AddWithValue("@_slaID", ListSlaID[k]);
                                    Targetcmd.Parameters.AddWithValue("@_priorityID", SLA.SLATarget[j].PriorityID);
                                    Targetcmd.Parameters.AddWithValue("@_prioritySLABreach", SLA.SLATarget[j].SLABreachPercent);
                                    Targetcmd.Parameters.AddWithValue("@_priorityRespondValue", SLA.SLATarget[j].PriorityRespondValue);
                                    Targetcmd.Parameters.AddWithValue("@_priorityRespondDuraton", SLA.SLATarget[j].PriorityRespondDuration);
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

            return SLATargetInsertCount;
        }

        /// <summary>
        /// Update SLA
        /// </summary>
        public int UpdateSLA(int SLAID, int tenantID, int IssuetypeID,  bool isActive, int modifiedBy)
        {
            int updatecount = 0; 

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateSLA", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_slaID", SLAID);
                cmd.Parameters.AddWithValue("@_issueType", IssuetypeID);
               
                cmd.Parameters.AddWithValue("@_isSLAActive", Convert.ToInt16(isActive));
                cmd.Parameters.AddWithValue("@_modifiedBy", modifiedBy);
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
        /// Delete SLA
        /// </summary>
        public int DeleteSLA(int tenantID, int SLAID)
        {
            int deletecount = 0;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteSLA", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_slaID", SLAID);


                cmd.CommandType = CommandType.StoredProcedure;
                deletecount = Convert.ToInt32(cmd.ExecuteScalar());
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
            return deletecount;

        }



        /// <summary>
        /// GET SLA
        /// </summary>

            public List<SLAResponseModel> SLAList(int tenantID,int SLAFor)
        {
            List<SLAResponseModel> objSLALst = new List<SLAResponseModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                //int rowStart = (pageNo - 1) * PageSize;
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetSLAList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@_tenantID", 1);
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd1.Parameters.AddWithValue("@_SLAFor", SLAFor);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objSLALst = ds.Tables[0].AsEnumerable().Select(r => new SLAResponseModel()
                        {
                            SLAID = Convert.ToInt32(r.Field<object>("SlaId")),

                            IssueTpeID = r.Field<object>("IssueTypeID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IssueTypeID")),
                            IssueTpeName = r.Field<object>("IssueTypeName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("IssueTypeName")),
                            isSLAActive = r.Field<object>("SLAStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("SLAStatus")),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy= r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
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
                                    objSLALst[i].SLAID == Convert.ToInt32(r.Field<object>("SlaID"))).Select(r => new SLATargetResponseModel()
                                    {
                                        SLATargetID = Convert.ToInt32(r.Field<object>("SLATargetID")),
                                        PriorityID = Convert.ToInt32(r.Field<object>("PriorityID")),
                                        PriorityName = r.Field<object>("PriortyName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriortyName")),
                                        SLABreachPercent = r.Field<object>("PriorityBreach") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriorityBreach")),
                                        PriorityRespond = r.Field<object>("PriorityRespond") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriorityRespond")),
                                        PriorityResolution = r.Field<object>("PriorityResolve") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("PriorityResolve")),

                                    }).ToList();
                            }

                        }
                    }
                }

                //paging here
                //if (PageSize > 0 && objSLALst.Count > 0)
                //    objSLALst[0].totalpages = objSLALst.Count > PageSize ? Math.Round(Convert.ToDouble(objSLALst.Count / PageSize)) : 1;

                //objSLALst = objSLALst.Skip(rowStart).Take(PageSize).ToList();


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

           
            return objSLALst;

        }


        /// <summary>
        /// Bind issuetype 
        /// </summary>
        /// 
       public List<IssueTypeList> BindIssueTypeList(int tenantID, string SearchText)
        {
            List<IssueTypeList> objIssueTypeLst = new List<IssueTypeList>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {

                conn.Open();
            cmd.Connection = conn;

            MySqlCommand cmd1 = new MySqlCommand("SP_GetIssueTypeForSLACreation", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            //cmd1.Parameters.AddWithValue("@_tenantID", 1);
            cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
            cmd1.Parameters.AddWithValue("@Search_Text", string.IsNullOrEmpty(SearchText) ? "" : SearchText);
                MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd1;
            da.Fill(ds);

            if (ds != null && ds.Tables != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                        objIssueTypeLst = ds.Tables[0].AsEnumerable().Select(r => new IssueTypeList()
                    {
                            IssueTypeID = Convert.ToInt32(r.Field<object>("IssueTypeID")),

                            IssueTypeName = r.Field<object>("IssueTypeName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("IssueTypeName")),
                            CategoryID = r.Field<object>("CategoryID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("CategoryID")),
                            CategoryName = r.Field<object>("CategoryName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CategoryName")),
                            SubCategoryID = r.Field<object>("SubCategoryID") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("SubCategoryID")),
                            SubCategoryName = r.Field<object>("SubCategoryName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("SubCategoryName")),
                       
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

           
            return objIssueTypeLst;
        }

        /// <summary>
        /// BulkUploadSLA
        /// </summary>
        /// 
        public int BulkUploadSLA(int TenantID, int CreatedBy, DataSet DataSetCSV)
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

        public List<IssueTypeList> SearchIssueType(int tenantID, string SearchText)
        {
            List<IssueTypeList> objIssueTypeLst = new List<IssueTypeList>();
            DataSet ds = new DataSet();
            try
            {

                conn.Open();          
                MySqlCommand cmd1 = new MySqlCommand("SP_SearchIssueType", conn);
                cmd1.Connection = conn;
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd1.Parameters.AddWithValue("@Search_Text", SearchText);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objIssueTypeLst = ds.Tables[0].AsEnumerable().Select(r => new IssueTypeList()
                        {
                            IssueTypeID = Convert.ToInt32(r.Field<object>("IssueTypeID")),
                            IssueTypeName = r.Field<object>("IssueTypeName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("IssueTypeName")),

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


            return objIssueTypeLst;
        }
    }
}
