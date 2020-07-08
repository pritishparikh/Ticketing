using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class CustomerChatService : ICustomerChat
    {

        /// <summary>
        /// Save Chat messages
        /// </summary>
        /// <param name="CustomerChatMaster"></param>
        /// <returns></returns>
        public int SaveReInitiateChatMessages(CustomerChatMaster customerChatMaster,int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int resultCount = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSInsertReInitiateChat", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_StoreID", string.IsNullOrEmpty(customerChatMaster.StoreID) ? "" : customerChatMaster.StoreID);
                cmd.Parameters.AddWithValue("@_ProgramCode", string.IsNullOrEmpty(customerChatMaster.ProgramCode) ? "" : customerChatMaster.ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", string.IsNullOrEmpty(customerChatMaster.CustomerID) ? "" : customerChatMaster.CustomerID);
                cmd.Parameters.AddWithValue("@_FirstName", string.IsNullOrEmpty(customerChatMaster.FirstName) ? "" : customerChatMaster.FirstName);
                cmd.Parameters.AddWithValue("@_LastName", string.IsNullOrEmpty(customerChatMaster.LastName) ? "" : customerChatMaster.LastName);
                cmd.Parameters.AddWithValue("@_CustomerMobileNumber", string.IsNullOrEmpty(customerChatMaster.MobileNo) ? "" : customerChatMaster.MobileNo);
                cmd.Parameters.AddWithValue("@_StoreManagerId", customerChatMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@_ChatTicketID", customerChatMaster.ChatTicketID);
                
                cmd.CommandType = CommandType.StoredProcedure;
                resultCount = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return resultCount;
        }
    }
}
