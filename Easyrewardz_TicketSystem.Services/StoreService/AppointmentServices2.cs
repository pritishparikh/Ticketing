using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class AppointmentServices : IAppointment
    {
        /// <summary>
        /// GetBroadcastConfigurationResponses
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programcode"></param>
        /// <param name="storeCode"></param>
        /// <returns></returns>
        public StoreDetails GetStoreDetailsByStoreCode(int tenantID, int userID, string programcode, string storeCode)
        {
            DataSet ds = new DataSet();
            StoreDetails storeDetails = new StoreDetails();
            List<CampaignExecutionDetailsResponse> objList = new List<CampaignExecutionDetailsResponse>();
            BroadcastConfigurationResponse BroadcastConfigurationResponse = new BroadcastConfigurationResponse();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetStoreDetailsByStoreCode", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                cmd.Parameters.AddWithValue("@_UserID", userID);
                cmd.Parameters.AddWithValue("@_Programcode", programcode);
                cmd.Parameters.AddWithValue("@_StoreCode", storeCode);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        storeDetails = new StoreDetails()
                        {
                            StoreName = ds.Tables[0].Rows[0]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]),
                            Address = ds.Tables[0].Rows[0]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Address"]),
                            StorePhoneNo = ds.Tables[0].Rows[0]["StorePhoneNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StorePhoneNo"]),
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return storeDetails;

        }
    }
}
