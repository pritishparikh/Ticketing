using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
            ReturnShipmentDetails obj = null;
            Random generator = new Random();
            var randomAWB = generator.Next(0, 1000000000).ToString("D10");
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
                cmd.Parameters.AddWithValue("@item_IDs", string.IsNullOrEmpty(itemIDs) ? "" : itemIDs.TrimEnd(','));
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

        /// <summary>
        /// GetItemDetailByOrderID
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public OrdersItemDetails GetItemDetailByOrderID(int orderID, int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<OrdersItem> lstOrdersItem = new List<OrdersItem>();
            OrdersItemDetails ordersItemDetails = new OrdersItemDetails();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_PHYGetItemDetailByOrderID", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("order_ID", orderID);
                sqlcmd.Parameters.AddWithValue("tenant_ID", tenantID);
                sqlcmd.Parameters.AddWithValue("user_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        OrdersItem ordersItems = new OrdersItem
                        {
                            ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            ItemID = ds.Tables[0].Rows[i]["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemID"]),
                            ItemName = ds.Tables[0].Rows[i]["ItemName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]),
                            ItemPrice = ds.Tables[0].Rows[i]["ItemPrice"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemPrice"]),
                            Quantity = ds.Tables[0].Rows[i]["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]),
                            OrderID = ds.Tables[0].Rows[i]["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"]),
                            Disable = ds.Tables[0].Rows[i]["Disable"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Disable"]),
                            Checked = ds.Tables[0].Rows[i]["Checked"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["Checked"]),
                        };

                        lstOrdersItem.Add(ordersItems);
                    }
                    ordersItemDetails.OrdersItems = lstOrdersItem;
                }
                if (ds != null && ds.Tables[1] != null)
                {
                    ordersItemDetails.InvoiceNumber = ds.Tables[1].Rows[0]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["InvoiceNo"]);
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
            return ordersItemDetails;
        }

        /// <summary>
        /// GetAWBInvoicenoDetails
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public List<ReturnShipmentDetails> GetAWBInvoicenoDetails(int orderID, int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<ReturnShipmentDetails> lstReturnShipmentDetails = new List<ReturnShipmentDetails>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_PHYGetAWBInvoiceDetails", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("order_ID", orderID);
                sqlcmd.Parameters.AddWithValue("tenant_ID", tenantID);
                sqlcmd.Parameters.AddWithValue("user_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ReturnShipmentDetails returnShipmentDetails = new ReturnShipmentDetails
                        {
                            InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            ItemIDs = ds.Tables[0].Rows[i]["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemID"]),
                            AWBNumber = ds.Tables[0].Rows[i]["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AWBNo"]),
                        };

                        lstReturnShipmentDetails.Add(returnShipmentDetails);
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
            return lstReturnShipmentDetails;
        }

        /// <summary>
        ///Generate Link
        /// </summary>
        /// <param name="sentPaymentLink"></param>
        /// <param name="clientAPIUrlForGenerateToken"></param>
        /// <param name="clientAPIUrlForGeneratePaymentLink"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public int GenerateLink(SentPaymentLink sentPaymentLink, string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, int tenantID, int userID, string programCode)
        {
            int result = 0;
            string apiReq1 = "";
            string URLGeneratePaymentLink = "";
            DataSet ds = new DataSet();
            HSRequestResendPaymentLink hSRequestResendPaymentLink = null;
            HSRequestGeneratePaymentLink hSRequestGeneratePaymentLink = null;
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderDetailForPaymentLink", conn)
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Invoice_Number", sentPaymentLink.InvoiceNumber);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@user_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                        hSRequestGeneratePaymentLink = new HSRequestGeneratePaymentLink
                        {
                            merchantTxnID = ds.Tables[0].Rows[0]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]),
                            billDateTime = ds.Tables[0].Rows[0]["billDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["billDateTime"]),
                            terminalId = ds.Tables[0].Rows[0]["TerminalId"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["TerminalId"]),
                            name = ds.Tables[0].Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]),
                            email = ds.Tables[0].Rows[0]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]),
                            mobile = ds.Tables[0].Rows[0]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["MobileNumber"]),
                            amount = ds.Tables[0].Rows[0]["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"])
                        };
                }
                hSRequestGeneratePaymentLink.programCode = programCode;
                hSRequestGeneratePaymentLink.storeCode = sentPaymentLink.StoreCode;
                DateTime dateTime_billdatetime = Convert.ToDateTime(hSRequestGeneratePaymentLink.billDateTime);
                var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
                var json = JsonConvert.SerializeObject(dateTime_billdatetime, settings);
              var newdate=  JsonConvert.DeserializeObject<string>(json);
                hSRequestGeneratePaymentLink.billDateTime = newdate;
                HSResponseGeneratePaymentLink responseGeneratePaymentLink = new HSResponseGeneratePaymentLink();

                HSRequestGenerateToken hSRequestGenerateToken = new HSRequestGenerateToken();
                string apiReq = JsonConvert.SerializeObject(hSRequestGenerateToken);
                apiResponse = CommonService.SendApiRequestToken(clientAPIUrlForGenerateToken + "connect/token", apiReq);
                HSResponseGenerateToken hSResponseGenerateToken = new HSResponseGenerateToken();
                hSResponseGenerateToken = JsonConvert.DeserializeObject<HSResponseGenerateToken>(apiResponse);

                if (!string.IsNullOrEmpty(hSResponseGenerateToken.access_Token))
                {
                    if (sentPaymentLink.SentPaymentLinkCount > 0)
                    {
                        hSRequestResendPaymentLink = new HSRequestResendPaymentLink
                        {
                            programCode = programCode,
                            tokenId = hSResponseGenerateToken.access_Token,
                            storeCode = sentPaymentLink.StoreCode,
                            billDateTime = hSRequestGeneratePaymentLink.billDateTime,
                            terminalId = hSRequestGeneratePaymentLink.terminalId,
                            merchantTxnID = hSRequestGeneratePaymentLink.merchantTxnID,
                            mobile = hSRequestGeneratePaymentLink.mobile,
                            reason = "ABCD"
                        };
                         apiReq1 = JsonConvert.SerializeObject(hSRequestResendPaymentLink);
                        URLGeneratePaymentLink = clientAPIUrlForGeneratePaymentLink + "api/ResendPaymentLink";
                    }
                    else
                    {
                        apiReq1 = JsonConvert.SerializeObject(hSRequestGeneratePaymentLink);
                        URLGeneratePaymentLink = clientAPIUrlForGeneratePaymentLink + "api/GeneratePaymentLink";
                    }
                    apiResponse1 = CommonService.SendApiRequestMerchantApi(URLGeneratePaymentLink, apiReq1, hSResponseGenerateToken.access_Token);

                    responseGeneratePaymentLink = JsonConvert.DeserializeObject<HSResponseGeneratePaymentLink>(apiResponse1);
                }

                if (responseGeneratePaymentLink.returnMessage == "Success")
                {
                    if (conn != null && conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    MySqlCommand cmd1 = new MySqlCommand("SP_PHYUpdateOrderDetailForPaymentLink", conn)
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd1.Parameters.AddWithValue("@Invoice_Number", sentPaymentLink.InvoiceNumber);
                    cmd1.Parameters.AddWithValue("@access_Token", hSResponseGenerateToken.access_Token);
                    cmd1.Parameters.AddWithValue("@tenant_ID", tenantID);
                    cmd1.Parameters.AddWithValue("@user_ID", userID);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    result = Convert.ToInt32(cmd1.ExecuteNonQuery());
                    conn.Close();
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
            return result;
        }

        /// <summary>
        /// CheckPinCodeForCourierAvailibilty
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public ResponseCourierAvailibilty CheckPinCodeForCourierAvailibilty(HSChkCourierAvailibilty hSChkCourierAvailibilty, int tenantID, int userID, string clientAPIUrl)
        {
            ResponseCourierAvailibilty responseCourierAvailibilty = new ResponseCourierAvailibilty();
            try
            {
                hSChkCourierAvailibilty.Cod = 0;
                hSChkCourierAvailibilty.Weight = 1;
                string apiReq = JsonConvert.SerializeObject(hSChkCourierAvailibilty);
                apiResponse = CommonService.SendApiRequest(clientAPIUrl + "api/ShoppingBag/ChkCourierAvailibilty", apiReq);
                responseCourierAvailibilty = JsonConvert.DeserializeObject<ResponseCourierAvailibilty>(apiResponse);
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
            return responseCourierAvailibilty;
        }

        /// <summary>
        /// Get Store PinCode By UserID
        /// </summary>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public string GetStorePinCodeByUserID(int tenantID, int userID)
        {
            string pinCode = "";
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_PHYGetStorePinCodeByUserID", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("tenant_ID", tenantID);
                sqlcmd.Parameters.AddWithValue("user_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    pinCode = ds.Tables[0].Rows[0]["PincodeID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["PincodeID"]);
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
            return pinCode;
        }
    }
}
