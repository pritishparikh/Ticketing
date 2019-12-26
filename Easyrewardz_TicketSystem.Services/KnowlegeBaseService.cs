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
        public List<KnowlegeBaseMaster> SearchByCategory(int type_ID, int Category_ID, int SubCategory_ID)
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
    }
}
