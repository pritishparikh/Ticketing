using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public class MasterServices : IMasterInterface
    {
        #region
        MySqlConnection conn = new MySqlConnection();
        public MasterServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion


        /// <summary>
        /// Get Channel Of Purchase List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<ChannelOfPurchase> GetChannelOfPurchaseList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<ChannelOfPurchase> objChannel = new List<ChannelOfPurchase>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetChannelOfPurchaseList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@TenantID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ChannelOfPurchase channel = new ChannelOfPurchase();
                        channel.ChannelOfPurchaseID = Convert.ToInt32(ds.Tables[0].Rows[i]["ChannelOfPurchaseID"]);
                        channel.TenantID = Convert.ToInt32(ds.Tables[0].Rows[i]["TenantID"]);
                        channel.NameOfChannel = Convert.ToString(ds.Tables[0].Rows[i]["NameOfChannel"]);
                        channel.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        //brand.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["dd"]);

                        objChannel.Add(channel);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objChannel;
        }
    }
}
