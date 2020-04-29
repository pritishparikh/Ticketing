using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public class CustomerChatService: ICustomerChat
    {
        #region variable
        MySqlConnection conn = new MySqlConnection();
        #endregion
         
        public CustomerChatService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        public List<CustomerChatMaster> OngoingChat(int userMasterID, int tenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomerChatMaster> lstCustomerChatMaster = new List<CustomerChatMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_OngoingChat", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@userMaster_ID", userMasterID);
                cmd1.Parameters.AddWithValue("@tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomerChatMaster customerChatMaster = new CustomerChatMaster();
                        customerChatMaster.ChatID = Convert.ToInt32(ds.Tables[0].Rows[i]["ChatID"]);
                        customerChatMaster.StoreID = ds.Tables[0].Rows[i][""] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i][""]);
                        customerChatMaster.CustomerID = ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerID"]);
                        customerChatMaster.CumtomerName = ds.Tables[0].Rows[i][""] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i][""]);
                        customerChatMaster.ChatStatus = ds.Tables[0].Rows[i]["ChatStatus"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ChatStatus"]);
                        customerChatMaster.MobileNo = ds.Tables[0].Rows[i][""] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i][""]);
                        customerChatMaster.MessageCount= ds.Tables[0].Rows[i][""] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][""]);
                        customerChatMaster.TimeAgo = ds.Tables[0].Rows[i][""] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][""]);
                        lstCustomerChatMaster.Add(customerChatMaster);
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
            }
            return lstCustomerChatMaster;
        }
    }
}
