using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class KnowlegeBaseService : IKnowledge
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public KnowlegeBaseService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion
        /// Search By Category ,SubCategory and Type
        /// </summary>
        /// <param name="Category"></param>
        ///  <param name="SubCategory"></param>
        ///   <param name="type"></param>
        /// <returns></returns>
        public List<KnowlegeBaseMaster> SearchByCategory(int type_ID, int Category_ID, int SubCategory_ID,int TenantId)
        {
            DataSet ds = new DataSet();
            List<KnowlegeBaseMaster> listknowledge = new List<KnowlegeBaseMaster>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_SearchByTypeCategory", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@type_ID", type_ID);
                cmd.Parameters.AddWithValue("@Category_ID", Category_ID);
                cmd.Parameters.AddWithValue("@SubCategory_ID", SubCategory_ID);
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantId);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        KnowlegeBaseMaster knowlegeBase = new KnowlegeBaseMaster();
                        knowlegeBase.KBID = Convert.ToInt32(ds.Tables[0].Rows[i]["KBID"]);
                        knowlegeBase.Subject = Convert.ToString(ds.Tables[0].Rows[i]["Subject"]);
                        knowlegeBase.Description = Convert.ToString(ds.Tables[0].Rows[i]["Description"]);
                        listknowledge.Add(knowlegeBase);
                    }
                }
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
            return listknowledge;
        }

        public int AddKB(KnowlegeBaseMaster knowlegeBaseMaster)
        {

            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_InsertKnowlegeBase", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@KB_CODE", knowlegeBaseMaster.KBCODE);
                cmd1.Parameters.AddWithValue("@Tenant_ID", knowlegeBaseMaster.TenantID);
                cmd1.Parameters.AddWithValue("@Category_ID", knowlegeBaseMaster.CategoryID);
                cmd1.Parameters.AddWithValue("@SubCategory_ID", knowlegeBaseMaster.SubCategoryID);
                cmd1.Parameters.AddWithValue("@Is_Approved", knowlegeBaseMaster.IsApproved);
                cmd1.Parameters.AddWithValue("@Subject_", knowlegeBaseMaster.Subject);
                cmd1.Parameters.AddWithValue("@Description_", knowlegeBaseMaster.Description);
                cmd1.Parameters.AddWithValue("@Is_Active", knowlegeBaseMaster.IsActive);
                cmd1.Parameters.AddWithValue("@IssueType_ID", knowlegeBaseMaster.IssueTypeID);
                cmd1.Parameters.AddWithValue("@Created_By", knowlegeBaseMaster.CreatedBy);

                k = Convert.ToInt32(cmd1.ExecuteNonQuery());
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

            return k;

        }


        public int UpdateKB(KnowlegeBaseMaster knowlegeBaseMaster)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_UpdateKnowlegeBase", conn);
                cmd1.Parameters.AddWithValue("@KB_ID", knowlegeBaseMaster.KBID);
                cmd1.Parameters.AddWithValue("@KB_CODE", knowlegeBaseMaster.KBCODE);
                cmd1.Parameters.AddWithValue("@Category_ID", knowlegeBaseMaster.CategoryID);
                cmd1.Parameters.AddWithValue("@SubCategory_ID", knowlegeBaseMaster.SubCategoryID);
                cmd1.Parameters.AddWithValue("@IssueType_ID",knowlegeBaseMaster.IssueTypeID);
                cmd1.Parameters.AddWithValue("@Is_Approved", knowlegeBaseMaster.IsApproved);
                cmd1.Parameters.AddWithValue("@Subject_", knowlegeBaseMaster.Subject);
                cmd1.Parameters.AddWithValue("@Description_", knowlegeBaseMaster.Description);
                cmd1.Parameters.AddWithValue("@Is_Active", knowlegeBaseMaster.IsActive);
                cmd1.Parameters.AddWithValue("@Modify_By", knowlegeBaseMaster.ModifyBy);

                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteNonQuery());
                conn.Close();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
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


        public int DeleteKB(int KBID, int TenantId)
        {
            MySqlCommand cmd = new MySqlCommand();
            int k = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_DeleteKnowlegeBase", conn);
                cmd1.Parameters.AddWithValue("@KB_ID", KBID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;
                k = Convert.ToInt32(cmd1.ExecuteNonQuery());
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

            return k;
        }


        public List<KnowlegeBaseMaster> KBList(int TenantId)
        {

            List<KnowlegeBaseMaster> knowlegeBaseMasters = new List<KnowlegeBaseMaster>();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_KBList", conn);

                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();

                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        KnowlegeBaseMaster knowlegeBaseMaster = new KnowlegeBaseMaster();
                        knowlegeBaseMaster.KBID = Convert.ToInt32(dt.Rows[i]["KBID"]);
                        knowlegeBaseMaster.KBCODE = Convert.ToString(dt.Rows[i]["KBCODE"]);
                        knowlegeBaseMaster.CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]);
                        knowlegeBaseMaster.SubCategoryName = Convert.ToString(dt.Rows[i]["SubCategoryName"]);
                        knowlegeBaseMaster.IssueTypeName = Convert.ToString(dt.Rows[i]["IssueTypeName"]);
                        knowlegeBaseMaster.Subject = Convert.ToString(dt.Rows[i]["Subject"]);
                        knowlegeBaseMaster.Description = Convert.ToString(dt.Rows[i]["Description"]);
                        knowlegeBaseMaster.IsApproveStatus= Convert.ToString(dt.Rows[i]["IsApprove"]);
                        //knowlegeBaseMaster.Status = Convert.ToString(dt.Rows[i]["Status"]);
                        //knowlegeBaseMaster.CreatedName = Convert.ToString(dt.Rows[i]["createdby"]);
                       // knowlegeBaseMaster.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);
                        //knowlegeBaseMaster.ModifyName = Convert.ToString(dt.Rows[i]["modifyby"]);
                        //knowlegeBaseMaster.ModifyDate = Convert.ToDateTime(dt.Rows[i]["ModifyDate"]);


                        knowlegeBaseMasters.Add(knowlegeBaseMaster);
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

            return knowlegeBaseMasters;
        }

        public int RejectApproveKB(int KBID, int IsApprove, int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_ApproveRejectKB", conn);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.Parameters.AddWithValue("@KB_ID", KBID);
                cmd1.Parameters.AddWithValue("@Is_Approved", IsApprove);


                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteNonQuery());
                conn.Close();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
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

    }
}
