﻿using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.Extensions.Caching.Distributed;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class TemplateService : ITemplate
    {
        #region variable
        private readonly IDistributedCache Cache;
        public TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public TemplateService(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            Cache = cache;
        }
        #endregion
        public List<Template> getTemplateForNote(int IssueTypeId, int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<Template> objtemplate = new List<Template>();

            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getTemplateList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@IssueType_ID", IssueTypeId);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Template template = new Template();
                        template.TenantID = Convert.ToInt32(ds.Tables[0].Rows[i]["TenantID"]);
                        template.TemplateID = Convert.ToInt32(ds.Tables[0].Rows[i]["TemplateID"]);
                        template.TemplateName = Convert.ToString(ds.Tables[0].Rows[i]["TemplateName"]);
                        //template.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        objtemplate.Add(template);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return objtemplate;
        }

        public Template getTemplateContent(int TemplateId,int TenantId)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            Template objtemplate = new Template();

            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getTemplateContent", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Template_ID", TemplateId);
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantId);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objtemplate.TenantID = Convert.ToInt32(ds.Tables[0].Rows[i]["TenantID"]);
                        objtemplate.TemplateID = Convert.ToInt32(ds.Tables[0].Rows[i]["TemplateID"]);
                        objtemplate.TemplateName = Convert.ToString(ds.Tables[0].Rows[i]["TemplateName"]);
                        objtemplate.TemplateSubject = Convert.ToString(ds.Tables[0].Rows[i]["TemplateSubject"]);
                        objtemplate.TemplateBody = Convert.ToString(ds.Tables[0].Rows[i]["TemplateBody"]);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return objtemplate;
        }


        #region Settings Template

        /// <summary>
        /// InsertTemplate 
        /// </summary>
        public int InsertTemplate(int tenantId, string TemplateName, string TemplatSubject, string TemplatBody, string issueTypes, bool isTemplateActive, int createdBy)
        {
            int insertcount = 0;
            try
            {

                conn = Db.Connection;
                MySqlCommand cmd = new MySqlCommand("SP_InsertTemplate", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd.Parameters.AddWithValue("@_temaplateName", TemplateName);
                cmd.Parameters.AddWithValue("@_templatesubject", TemplatSubject);
                cmd.Parameters.AddWithValue("@_templatebody", TemplatBody);
                cmd.Parameters.AddWithValue("@_issueTypes", issueTypes);
                cmd.Parameters.AddWithValue("@_isTemplateActive", Convert.ToInt16(isTemplateActive));

                cmd.Parameters.AddWithValue("@_createdBy", createdBy);


                insertcount = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                throw ex;
            }
            
            return insertcount;
        }


        /// <summary>
        ///DeleteTemplate
        /// </summary>
        public int DeleteTemplate(int tenantID, int TemplateID)
        {
            int deleteCount = 0;
            try
            {
                conn = Db.Connection;
                MySqlCommand cmd = new MySqlCommand("SP_DeleteTemplate", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantId", tenantID);
                cmd.Parameters.AddWithValue("@_templateId", TemplateID);


                cmd.CommandType = CommandType.StoredProcedure;
                deleteCount = Convert.ToInt32(cmd.ExecuteScalar());
            }

            catch (Exception ex)
            {

                throw ex;
            }
            
            return deleteCount;
        }


        /// <summary>
        /// UpdateTemplate
        /// </summary>
        public int UpdateTemplate(int tenantId,int TemplateID ,string TemplateName, string issueType, bool isTemplateActive,int ModifiedBy, string templateSubject, string templateContent)
        {
            int updateCount = 0;

            try
            {
                conn = Db.Connection;
                MySqlCommand cmd = new MySqlCommand("SP_UpdateTemplate", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd.Parameters.AddWithValue("@_templateID", TemplateID);
                cmd.Parameters.AddWithValue("@_templateName", TemplateName);

                cmd.Parameters.AddWithValue("@_issueTypeID", issueType);
                cmd.Parameters.AddWithValue("@_isTemplateActive", Convert.ToInt16(isTemplateActive));
                cmd.Parameters.AddWithValue("@_modifiedBy", ModifiedBy);
                cmd.Parameters.AddWithValue("@_templateContent", string.IsNullOrEmpty(templateContent) ? "" : templateContent);
                cmd.Parameters.AddWithValue("@_templateSubject", string.IsNullOrEmpty(templateSubject) ? "" : templateSubject);
                cmd.CommandType = CommandType.StoredProcedure;
                updateCount = cmd.ExecuteNonQuery();
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

            return updateCount;
        }

        /// <summary>
        /// GetTemplates
        /// </summary>
        public  List<TemplateModel> GetTemplates(int tenantId)
        {
            List<TemplateModel> objTempLst = new List<TemplateModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetTemplates", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
       
                cmd1.Parameters.AddWithValue("@_tenantID", tenantId);
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objTempLst = ds.Tables[0].AsEnumerable().Select(r => new TemplateModel()
                        {
                            TemplateID = Convert.ToInt32(r.Field<object>("TemplateID")),

                            TemplateName = r.Field<object>("TemplateName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TemplateName")),
                            IssueTypeCount = r.Field<object>("IssueTypeCount") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IssueTypeCount")),
                            IssueTypeID = r.Field<object>("IssueTypeIDs") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("IssueTypeIDs")),
                            IssueTypeName = r.Field<object>("IssueTypeName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("IssueTypeName")),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),
                            TemplateStatus= r.Field<object>("TemplateStatus") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TemplateStatus")),

                            TemplateContent = r.Field<object>("TemplateBody") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TemplateBody")),
                            TemplateSubject = r.Field<object>("TemplateSubject") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TemplateSubject")),

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
                if (ds != null) ds.Dispose();
            }

            return objTempLst;
        }

        /// <summary>
        /// GetTemplates
        /// </summary>
        public List<MailParameterModel> GetMailParameter(int tenantId, int AlertID)
        {
            List<MailParameterModel> objTempLst = new List<MailParameterModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = Db.Connection;
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetMailParameter", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantId", tenantId);
                cmd1.Parameters.AddWithValue("@_AlertID", AlertID);
                //cmd1.Parameters.AddWithValue("@_tenantID", tenantId);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objTempLst = ds.Tables[0].AsEnumerable().Select(r => new MailParameterModel()
                        {
                            MailParameterID = Convert.ToInt32(r.Field<object>("MailParameterID")),
                            ParameterName = r.Field<object>("ParameterName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ParameterName")),
                            Description = r.Field<object>("Description") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("Description")),

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
                if (ds != null) ds.Dispose();
            }

            return objTempLst;
        }

        #endregion

    }
}
