using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class CustomerChatService : ICustomerChat
    {
        /// <summary>
        ///Get Chat Customer Profile Details
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="Programcode"></param>
        ///<param name="CustomerID"></param>
        /// <returns></returns>
        /// 
        public ChatCustomerProfileModel GetChatCustomerProfileDetails(int TenantId, string ProgramCode, int CustomerID, int UserID, string ClientAPIURL)
        {
            ChatCustomerProfileModel CustomerProfile = new ChatCustomerProfileModel();
            List<string> InsightList = new List<string>();
            string ClientAPIResponse = string.Empty; string JsonRequest = string.Empty;
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            ClientChatProfileModel ClientRequest = new ClientChatProfileModel();
            CustomerpopupDetails CustomerATVDetails = new CustomerpopupDetails();
            List<InsightsModel> CustomerInsights = new List<InsightsModel>();
            LastTransactionDetailsResponseModel LastTransaction = new LastTransactionDetailsResponseModel();
            try
            {

                // get order details of customer from SP

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSChatCustomerProfileOrderDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantId", TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@_AgentID", UserID);


                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {

                        CustomerProfile.CustomerID = CustomerID; 
                        CustomerProfile.CustomerName = ds.Tables[0].Rows[0]["Customer_Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Customer_Name"]).Trim();
                        CustomerProfile.CustomerMobileNo = ds.Tables[0].Rows[0]["Customer_MobileCC"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Customer_MobileCC"]);
                        CustomerProfile.OrderDelivered = ds.Tables[0].Rows[0]["OrderDelivered"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["OrderDelivered"]);
                        CustomerProfile.OrderShoppingBag = ds.Tables[0].Rows[0]["OrderShoppingBag"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["OrderShoppingBag"]);
                        CustomerProfile.OrderReadyToShip = ds.Tables[0].Rows[0]["OrderReadyToShip"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["OrderReadyToShip"]);
                        CustomerProfile.OrderReturns = ds.Tables[0].Rows[0]["OrderReturns"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["OrderReturns"]);

                        ClientRequest.mobileNumber= ds.Tables[0].Rows[0]["Customer_Mobile"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Customer_Mobile"]);
                    }
                }

                //**

                // get ATV details of customer fromclient API
                ClientRequest.programCode = ProgramCode;
                 JsonRequest = JsonConvert.SerializeObject(ClientRequest);
                try
                {
                    ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetUserATVDetails", JsonRequest);
                }
                catch (Exception)
                {
                    ClientAPIResponse = string.Empty;
                }

                if(!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    CustomerATVDetails= JsonConvert.DeserializeObject<CustomerpopupDetails>(ClientAPIResponse);

                    if(CustomerATVDetails!=null)
                    {
                        CustomerProfile.CustomerEmailID = string.IsNullOrEmpty(CustomerATVDetails.email) ? "" : CustomerATVDetails.email;
                        CustomerProfile.CustomerTier = string.IsNullOrEmpty(CustomerATVDetails.tiername) ? "" : CustomerATVDetails.tiername;
                        CustomerProfile.LifetimeValue = string.IsNullOrEmpty(CustomerATVDetails.lifeTimeValue) ? 0 : Convert.ToDouble(CustomerATVDetails.lifeTimeValue);
                        CustomerProfile.VisitCount = string.IsNullOrEmpty(CustomerATVDetails.visitCount) ? 0 : Convert.ToInt32(CustomerATVDetails.visitCount);
                        CustomerProfile.TotalPoints = string.IsNullOrEmpty(CustomerATVDetails.availablePoints) ? 0 : Convert.ToDouble(CustomerATVDetails.availablePoints);
                    }

                }

                //**

                // get Insigts details of customer from client API
                try
                {
                    ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetKeyInsight", JsonRequest);
                }
                catch(Exception)
                {
                    ClientAPIResponse = string.Empty;
                }
                
                if (!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    CustomerInsights = JsonConvert.DeserializeObject<List<InsightsModel>>(ClientAPIResponse);

                    if (CustomerInsights != null && CustomerInsights.Count > 0)
                    {
                        CustomerProfile.Insights = CustomerInsights;
                    }

                }

                //**

                // get Insigts details of customer from client API
                try
                {
                    ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetLastTransactionDetails", JsonRequest);
                }
                catch (Exception)
                {
                    ClientAPIResponse = string.Empty;
                }
                if (!string.IsNullOrEmpty(ClientAPIResponse))
                {
                    LastTransaction = JsonConvert.DeserializeObject<LastTransactionDetailsResponseModel>(ClientAPIResponse);

                    if (LastTransaction != null )
                    {
                        CustomerProfile.BillNumber = string.IsNullOrEmpty(LastTransaction.billNo) ? "" : LastTransaction.billNo;
                        CustomerProfile.BillAmount = string.IsNullOrEmpty(LastTransaction.amount) ? 0 : Convert.ToDecimal(CustomerATVDetails.lifeTimeValue);
                        CustomerProfile.StoreDetails = string.IsNullOrEmpty(LastTransaction.storeName) ? "" : LastTransaction.storeName;
                        CustomerProfile.TransactionDate = string.IsNullOrEmpty(LastTransaction.billDate) ? "" : Convert.ToDateTime(LastTransaction.billDate).ToString("dd MMM yyyy");
                    }

                }

                //**


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

            return CustomerProfile;
        }
    }
}
