using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class HSOrderService
    {
        /// <summary>
        /// CreateShipmentAWB
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="itemIDs"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public ReturnShipmentDetails CreateShipmentAWB(int orderID, string itemIDs, int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            ReturnShipmentDetails obj=null;
            Random generator = new Random();        
            var randomAWB= generator.Next(0, 1000000000).ToString("D10");
            string CourierPartner = "Blue Dart";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_PHYCreateShipmentAWB", conn)
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@order_ID", orderID);
                cmd.Parameters.AddWithValue("@item_IDs", string.IsNullOrEmpty(itemIDs) ? "" :itemIDs.TrimEnd(','));
                cmd.Parameters.AddWithValue("@random_AWB", randomAWB); 
                cmd.Parameters.AddWithValue("@_CourierPartner", CourierPartner);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@user_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                         obj = new ReturnShipmentDetails
                        {
                            InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            AWBNumber = ds.Tables[0].Rows[i]["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AWBNo"]),
                            ItemIDs = ds.Tables[0].Rows[i]["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemID"]),                            
                        };
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

            return obj;
        }
    }
}
