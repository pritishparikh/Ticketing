using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.Extensions.Caching.Distributed;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public class IssueTypeServices : IIssueType
    {
        #region variable
        private readonly IDistributedCache _Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        #region Constructor 
        MySqlConnection conn = new MySqlConnection();
        public IssueTypeServices(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            _Cache = cache;
        }
        #endregion

        #region Method
        /// <summary>
        /// Get Issue Type List
        /// </summary>
        /// <param name="TenantID">Tenant Id</param>
        /// <param name="SubCategoryID">SubCategory ID</param>
        /// <returns></returns>
        public List<IssueType> GetIssueTypeList(int TenantID,int SubCategoryID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<IssueType> objIssueType = new List<IssueType>();

            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetIssueTypeList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@SubCategory_ID", SubCategoryID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IssueType issueType = new IssueType();
                        issueType.IssueTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        issueType.TenantID = Convert.ToInt32(ds.Tables[0].Rows[i]["TenantID"]);
                        issueType.IssueTypeName = Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        issueType.SubCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        issueType.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        //brand.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["dd"]);

                        objIssueType.Add(issueType);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return objIssueType;
        }

        /// <summary>
        /// Add Issue Type
        /// </summary>
        /// <param name="SubcategoryID"></param>
        /// <param name="IssuetypeName"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int AddIssueType(int SubcategoryID, string IssuetypeName, int TenantID, int UserID)
        {
            int Success = 0;
            try
            {
                conn = Db.Connection;
                MySqlCommand cmd = new MySqlCommand("SP_InsertIssueType", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubCategory_ID", SubcategoryID);
                cmd.Parameters.AddWithValue("@Issuetype_Name", IssuetypeName);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Created_By", UserID);
                Success = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return Success;
        }

        public List<IssueType> GetIssueTypeListByMultiSubCategoryID(int TenantID, string SubCategoryIDs)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<IssueType> objIssueType = new List<IssueType>();

            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetIssueTypeListByMultiSubCatagoryID", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd1.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IssueType issueType = new IssueType();
                        issueType.IssueTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        issueType.IssueTypeName = Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);;
                        //brand.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["dd"]);
                        objIssueType.Add(issueType);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return objIssueType;
        }
        #endregion
    }
}
