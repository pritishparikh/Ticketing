using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class TemplateService : ITemplate
    {
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public TemplateService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion
        public List<Template> getTemplateForNote(int IssueTypeId, int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<Template> objtemplate = new List<Template>();

            try
            {
                conn.Open();
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
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return objtemplate;
        }

    }
}
