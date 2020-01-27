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

        public CustomKBList SearchKB(int Category_ID, int SubCategory_ID, int type_ID, int TenantId)
        {

            DataSet ds = new DataSet();
            CustomKBList customKBLists = new CustomKBList();
            List<KBisApproved> kBisApproveds = new List<KBisApproved>();
            List<KBisNotApproved> kBisNotApproveds = new List<KBisNotApproved>();

            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_SearchKB", conn);

                cmd1.Parameters.AddWithValue("@type_ID", type_ID);
                cmd1.Parameters.AddWithValue("@Category_ID", Category_ID);
                cmd1.Parameters.AddWithValue("@SubCategory_ID", SubCategory_ID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        KBisApproved approved = new KBisApproved();
                        approved.KBID = Convert.ToInt32(ds.Tables[0].Rows[i]["KBID"]);
                        approved.KBCODE = ds.Tables[0].Rows[i]["KBCODE"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["KBCODE"]);
                        approved.CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        approved.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        approved.IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        approved.Subject = ds.Tables[0].Rows[i]["Subject"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Subject"]);
                        approved.Description = ds.Tables[0].Rows[i]["Description"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Description"]);
                        approved.IsApproveStatus = ds.Tables[0].Rows[i]["IsApprove"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IsApprove"]);




                        kBisApproveds.Add(approved);

                    }

                    customKBLists.Approved = kBisApproveds;
                }


                if (ds != null && ds.Tables[1] != null)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        KBisNotApproved notApproved = new KBisNotApproved();
                        notApproved.KBID = Convert.ToInt32(ds.Tables[1].Rows[i]["KBID"]);
                        notApproved.KBCODE = ds.Tables[0].Rows[i]["KBCODE"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["KBCODE"]);
                        notApproved.CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        notApproved.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        notApproved.IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        notApproved.Subject = ds.Tables[0].Rows[i]["Subject"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Subject"]);
                        notApproved.Description = ds.Tables[0].Rows[i]["Description"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Description"]);
                        notApproved.IsApproveStatus = ds.Tables[0].Rows[i]["IsApprove"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IsApprove"]);

                        kBisNotApproveds.Add(notApproved);
                    }

                    customKBLists.NotApproved = kBisNotApproveds;
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

            return customKBLists;
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
                cmd1.Parameters.AddWithValue("@Tenant_ID", knowlegeBaseMaster.TenantID);
                cmd1.Parameters.AddWithValue("@KB_ID", knowlegeBaseMaster.KBID);
                cmd1.Parameters.AddWithValue("@KB_CODE", knowlegeBaseMaster.KBCODE);
                cmd1.Parameters.AddWithValue("@Category_ID", knowlegeBaseMaster.CategoryID);
                cmd1.Parameters.AddWithValue("@SubCategory_ID", knowlegeBaseMaster.SubCategoryID);
                cmd1.Parameters.AddWithValue("@IssueType_ID",knowlegeBaseMaster.IssueTypeID);
                cmd1.Parameters.AddWithValue("@Subject_", knowlegeBaseMaster.Subject);
                cmd1.Parameters.AddWithValue("@Description_", knowlegeBaseMaster.Description);
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


        public CustomKBList KBList(int TenantId)
        {
            DataSet ds = new DataSet();
            CustomKBList customKBLists = new CustomKBList();
            List<KBisApproved> kBisApproveds = new List<KBisApproved>();
            List<KBisNotApproved> kBisNotApproveds = new List<KBisNotApproved>();

            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_KBList", conn);

                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                       
                        KBisApproved approved = new KBisApproved();
                        approved.KBID = Convert.ToInt32(ds.Tables[0].Rows[i]["KBID"]);
                        approved.KBCODE = ds.Tables[0].Rows[i]["KBCODE"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["KBCODE"]);
                        approved.CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        approved.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        approved.IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        approved.Subject = ds.Tables[0].Rows[i]["Subject"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Subject"]);
                        approved.Description = ds.Tables[0].Rows[i]["Description"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Description"]);
                        approved.IsApproveStatus = ds.Tables[0].Rows[i]["IsApprove"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IsApprove"]);




                        kBisApproveds.Add(approved);

                    }

                    customKBLists.Approved=kBisApproveds;
                }


                if (ds != null && ds.Tables[1] != null)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            KBisNotApproved notApproved = new KBisNotApproved();
                            notApproved.KBID = Convert.ToInt32(ds.Tables[1].Rows[i]["KBID"]);
                        notApproved.KBCODE = ds.Tables[0].Rows[i]["KBCODE"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["KBCODE"]);
                        notApproved.CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        notApproved.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        notApproved.IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        notApproved.Subject = ds.Tables[0].Rows[i]["Subject"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Subject"]);
                        notApproved.Description = ds.Tables[0].Rows[i]["Description"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Description"]);
                        notApproved.IsApproveStatus = ds.Tables[0].Rows[i]["IsApprove"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IsApprove"]);

                        kBisNotApproveds.Add(notApproved);
                        }

                    customKBLists.NotApproved = kBisNotApproveds;
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

            return customKBLists;
        }

        public int RejectApproveKB(KnowlegeBaseMaster knowlegeBaseMaster)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {

                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_ApproveRejectKB", conn);
                cmd1.Parameters.AddWithValue("@Tenant_ID", knowlegeBaseMaster.TenantID);
                cmd1.Parameters.AddWithValue("@KB_ID", knowlegeBaseMaster.KBID);
                cmd1.Parameters.AddWithValue("@Is_Approved", knowlegeBaseMaster.IsApproved);
                cmd1.Parameters.AddWithValue("@Category_ID", knowlegeBaseMaster.CategoryID);
                cmd1.Parameters.AddWithValue("@SubCategory_ID", knowlegeBaseMaster.SubCategoryID);
                cmd1.Parameters.AddWithValue("@IssueType_ID", knowlegeBaseMaster.IssueTypeID);
                cmd1.Parameters.AddWithValue("@Subject_", knowlegeBaseMaster.Subject);
                cmd1.Parameters.AddWithValue("@Description_", knowlegeBaseMaster.Description);
                

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
