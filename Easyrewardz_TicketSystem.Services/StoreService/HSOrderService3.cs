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
        public ReturnShipmentDetails CreateShipmentAWB(int orderID, string itemIDs, int tenantID, int userID, string clientAPIURL, string ProgramCode)
        {

            bool isAWBGenerated = false;
            bool iSStoreDelivery = false;
            DataSet ds = new DataSet();
            AWbdetailModel awbdetailModel = null;
            RequestCouriersPartnerAndAWBCode requestCouriersPartnerAndAWBCode = null;
            ResponseCouriersPartnerAndAWBCode responseCouriersPartnerAndAWBCode = new ResponseCouriersPartnerAndAWBCode();
            responseCouriersPartnerAndAWBCode.data = new Data();
            ResponseGeneratePickup responseGeneratePickup = new ResponseGeneratePickup();
            ResponseGenerateManifest responseGenerateManifest = new ResponseGenerateManifest();
            ReturnShipmentDetails obj = new ReturnShipmentDetails();
            try
            {
                // Code for gatting data from table for request AWB client API
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderAWBDetails", conn)
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                cmd.Parameters.AddWithValue("@_UserID", userID);
                cmd.Parameters.AddWithValue("@_OrderID", orderID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    isAWBGenerated = ds.Tables[0].Rows[0]["IsAwbNoGenerated"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAwbNoGenerated"]);
                }
                if (ds != null && ds.Tables[1] != null)
                {
                    requestCouriersPartnerAndAWBCode = new RequestCouriersPartnerAndAWBCode
                    {
                        pickup_postcode = ds.Tables[1].Rows[0]["pickup_postcode"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["pickup_postcode"]),
                        delivery_postcode = ds.Tables[1].Rows[0]["delivery_postcode"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["delivery_postcode"]),
                        weight = ds.Tables[1].Rows[0]["weight"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["weight"])
                    };
                }

                if (ds != null && ds.Tables[2] != null)
                {
                    requestCouriersPartnerAndAWBCode.orderDetails = new OrderDetails
                    {
                        order_id = ds.Tables[2].Rows[0]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["InvoiceNo"]),
                        order_date = ds.Tables[2].Rows[0]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["Date"]),
                        pickup_location = ds.Tables[2].Rows[0]["pickup_location"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["pickup_location"]),
                        channel_id = ds.Tables[2].Rows[0]["channel_id"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["channel_id"]),
                        billing_customer_name = ds.Tables[2].Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["CustomerName"]),
                        billing_last_name = ds.Tables[2].Rows[0]["billing_last_name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["billing_last_name"]),
                        billing_address = ds.Tables[2].Rows[0]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["ShippingAddress"]),
                        billing_address_2 = ds.Tables[2].Rows[0]["billing_address_2"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["billing_address_2"]),
                        billing_city = ds.Tables[2].Rows[0]["City"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["City"]),
                        billing_pincode = ds.Tables[2].Rows[0]["delivery_postcode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["delivery_postcode"]),
                        billing_state = ds.Tables[2].Rows[0]["State"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["State"]),
                        billing_country = ds.Tables[2].Rows[0]["Country"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["Country"]),
                        billing_email = ds.Tables[2].Rows[0]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["EmailID"]),
                        billing_phone = ds.Tables[2].Rows[0]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["MobileNumber"]),
                        billing_alternate_phone = ds.Tables[2].Rows[0]["billing_alternate_phone"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["billing_alternate_phone"]),
                        shipping_is_billing = ds.Tables[2].Rows[0]["shipping_is_billing"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[2].Rows[0]["shipping_is_billing"]),
                        shipping_customer_name = ds.Tables[2].Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["CustomerName"]),
                        shipping_last_name = ds.Tables[2].Rows[0]["billing_last_name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["billing_last_name"]),
                        shipping_address = ds.Tables[2].Rows[0]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["ShippingAddress"]),
                        shipping_address_2 = ds.Tables[2].Rows[0]["billing_address_2"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["billing_address_2"]),
                        shipping_city = ds.Tables[2].Rows[0]["City"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["City"]),
                        shipping_pincode = ds.Tables[2].Rows[0]["delivery_postcode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["delivery_postcode"]),
                        shipping_country = ds.Tables[2].Rows[0]["Country"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["Country"]),
                        shipping_state = ds.Tables[2].Rows[0]["State"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["State"]),
                        shipping_email = ds.Tables[2].Rows[0]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["EmailID"]),
                        shipping_phone = ds.Tables[2].Rows[0]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["MobileNumber"]),
                        payment_method = "Prepaid",
                        shipping_charges = 0,
                        giftwrap_charges = 0,
                        transaction_charges = 0,
                        total_discount = 0,
                        sub_total =  ds.Tables[2].Rows[0]["Amount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[2].Rows[0]["Amount"]),  // 10,
                        length = 10,
                        breadth = 10,
                        height = 2,
                        weight = 1.4,
                        //orderitems = new List<Orderitems>(),
                    };
                }

                if (ds != null && ds.Tables[3] != null)
                {
                    requestCouriersPartnerAndAWBCode.orderDetails.order_items = ds.Tables[3].AsEnumerable().Select(x => new Orderitems()
                    {
                        name = x.Field<object>("name") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("name")),
                        sku = x.Field<object>("sku") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("sku")),
                        units = x.Field<object>("units") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("units")),
                        selling_price = x.Field<object>("selling_price") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("selling_price")),
                        discount = x.Field<object>("discount") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("discount")),
                        tax = x.Field<object>("tax") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("tax")),
                        hsn = x.Field<object>("hsn") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("hsn")),
                    }).ToList();
                }
                if (ds != null && ds.Tables[4] != null)
                {
                    iSStoreDelivery = ds.Tables[4].Rows[0]["StoreDelivery"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[4].Rows[0]["StoreDelivery"]);
                }
                if (ds != null && ds.Tables[5] != null)
                {
                    awbdetailModel = new AWbdetailModel
                    {
                        InvoiceNo = ds.Tables[5].Rows[0]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[5].Rows[0]["InvoiceNo"]),
                        AWBNumber = ds.Tables[5].Rows[0]["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[5].Rows[0]["AWBNo"]),
                        ItemIDs = ds.Tables[5].Rows[0]["OrderItemIDs"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[5].Rows[0]["OrderItemIDs"]),
                        Date = ds.Tables[5].Rows[0]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[5].Rows[0]["Date"]),
                        CourierPartner = ds.Tables[5].Rows[0]["CourierPartner"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[5].Rows[0]["CourierPartner"]),
                    };
                }

                if (isAWBGenerated == false)
                {
                    HSChkCourierAvailibilty hSChkCourierAvailibilty = new HSChkCourierAvailibilty
                    {
                        Pickup_postcode = requestCouriersPartnerAndAWBCode.pickup_postcode,
                        Delivery_postcode = requestCouriersPartnerAndAWBCode.delivery_postcode
                    };

                    ResponseCourierAvailibilty responseCourierAvailibilty = new ResponseCourierAvailibilty();
                    responseCourierAvailibilty = CheckClientPinCodeForCourierAvailibilty(hSChkCourierAvailibilty, tenantID,userID, clientAPIURL);

                    if(responseCourierAvailibilty.Available == "false")
                    {
                        if (iSStoreDelivery == true)
                        {
                            //CouriersPartner =Store
                            responseCouriersPartnerAndAWBCode.data.courier_name = "Store";
                            obj = CreateShipment(orderID, itemIDs, tenantID, userID, responseCouriersPartnerAndAWBCode);
                        }
                        else
                        {
                            obj = new ReturnShipmentDetails
                            {
                                Status = false,
                                StatusMessge = "Delivery Not Available."
                            };
                            int result = SetOrderHasBeenReturn(tenantID, userID, orderID, "Shipment");
                        }
                    }
                    else if (responseCourierAvailibilty.Available == "true")
                    {

                        string apiReq = JsonConvert.SerializeObject(requestCouriersPartnerAndAWBCode);
                        apiResponse = CommonService.SendApiRequest(clientAPIURL + "api/ShoppingBag/GetCouriersPartnerAndAWBCode", apiReq);
                        responseCouriersPartnerAndAWBCode = JsonConvert.DeserializeObject<ResponseCouriersPartnerAndAWBCode>(apiResponse);
                        if (string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.awb_code) && string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.courier_name))
                        {
                            // insert in PHYOrderReturn table (return tab)
                            obj = new ReturnShipmentDetails
                            {
                                Status = false,
                                StatusMessge = "Delivery Not Available."
                            };
                            int result = SetOrderHasBeenReturn(tenantID, userID, orderID, "Shipment");
                        }
                        else if (!string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.awb_code) || !string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.courier_name))
                        {

                            obj = CreateShipment(orderID, itemIDs, tenantID, userID, responseCouriersPartnerAndAWBCode);

                            SmsWhatsUpDataSend(tenantID, userID, ProgramCode, orderID, clientAPIURL, "AWBAssigned");
                            //Code for GeneratePickup 
                            // { "statusCode":"200","data":{ "awb_code":"141123201505566","order_id":"41363502","shipment_id":"41079500","courier_company_id":"51","courier_name":"Xpressbees Surface","rate":100,"is_custom_rate":"0","cod_multiplier":"0","cod_charges":"0","freight_charge":"100","rto_charges":"92","min_weight":"0.5","etd_hours":"112","etd":"Jun 19, 2020","estimated_delivery_days":"5"} }

                            if (responseCouriersPartnerAndAWBCode != null)
                            {
                                if (responseCouriersPartnerAndAWBCode.data != null)
                                {
                                    if (responseCouriersPartnerAndAWBCode.data.shipment_id != null)
                                    {
                                        RequestGeneratePickup requestGeneratePickup = new RequestGeneratePickup
                                        {
                                            shipmentId = new List<int>
                                        {
                                            Convert.ToInt32(responseCouriersPartnerAndAWBCode.data.shipment_id)
                                        }
                                        };

                                        try
                                        {
                                            string apiReq1 = JsonConvert.SerializeObject(requestGeneratePickup);
                                            apiResponse = CommonService.SendApiRequest(clientAPIURL + "api/ShoppingBag/GeneratePickup", apiReq1);
                                            responseGeneratePickup = JsonConvert.DeserializeObject<ResponseGeneratePickup>(apiResponse);

                                            // need to write Code for update Status shipment pickup

                                            if (!string.IsNullOrEmpty(responseGeneratePickup.response.pickupTokenNumber))
                                            {
                                                int result = UpdateGeneratePickupManifest(orderID, tenantID, userID, "Pickup");
                                            }
                                            //end //Code for GeneratePickup 
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        try
                                        {
                                            //Code for GenerateManifest need to move the code 
                                            string apiReq2 = JsonConvert.SerializeObject(requestGeneratePickup);
                                            apiResponse = CommonService.SendApiRequest(clientAPIURL + "api/ShoppingBag/GenerateManifest", apiReq2);
                                            responseGenerateManifest = JsonConvert.DeserializeObject<ResponseGenerateManifest>(apiResponse);

                                            // need to write Code for update Status manifest created
                                            //end Code for GenerateManifest
                                            //}                     
                                            if (!string.IsNullOrEmpty(responseGenerateManifest.manifestUrl))
                                            {
                                                int success = UpdateGeneratePickupManifest(orderID, tenantID, userID, "Manifest");
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // need to write code if isAWBGenerated flag is true

                    //  code for return awb ,InvoiceNo.itemIDs need to write code (need to ask)

                    //if (responseCouriersPartnerAndAWBCode.data.awb_code != null)
                    //{
                    //    if (iSStoreDelivery == false)
                    //    {
                    //        // call rabit MQ  and update the AWB in table
                    //    }
                    //}
                    if (!string.IsNullOrEmpty(awbdetailModel.AWBNumber) || !string.IsNullOrEmpty(awbdetailModel.CourierPartner))
                    {
                        obj = new ReturnShipmentDetails
                        {
                            AWBNumber = awbdetailModel.AWBNumber,
                            InvoiceNo = awbdetailModel.InvoiceNo,
                            ItemIDs = awbdetailModel.ItemIDs,
                        };
                    }

                    else
                    {
                        //    if (iSStoreDelivery == false)
                        //    {
                        //        // call rabit MQ  and update the AWB in table
                        //    }
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
                if (ds != null)
                {
                    ds.Dispose();
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
                if (ds != null)
                {
                    ds.Dispose();
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
        public int GenerateLink(SentPaymentLink sentPaymentLink, string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, int tenantID, int userID, string programCode, string ClientAPIUrl)
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
                        OrderId = ds.Tables[0].Rows[0]["Id"] == DBNull.Value ? 0 : Convert.ToInt16(ds.Tables[0].Rows[0]["Id"]),
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
                var newdate = JsonConvert.DeserializeObject<string>(json);
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
                    if (result > 0)
                    {
                        SmsWhatsUpDataSend(tenantID, userID, programCode, hSRequestGeneratePaymentLink.OrderId, ClientAPIUrl, "ShoppingBagConvertToOrder");
                    }
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
                if (ds != null)
                {
                    ds.Dispose();
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

                if (responseCourierAvailibilty != null)
                {
                    if (responseCourierAvailibilty.Available.ToLower() == "false")
                    {
                        OrderTabSetting orderTabSetting = new OrderTabSetting();
                        orderTabSetting = GetOrderTabSettingDetails(tenantID, userID);
                        if (orderTabSetting.StoreDelivery)
                        {
                            responseCourierAvailibilty.Available = "true";
                        }
                    }
                }
                else
                {
                    responseCourierAvailibilty = new ResponseCourierAvailibilty
                    {
                        StatusCode = "201",
                        Available = "false"
                    };
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return pinCode;
        }

        /// <summary>
        /// Get Store PinCode By UserID
        /// </summary>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public ReturnShipmentDetails CreateShipment(int orderID, string itemIDs, int tenantID, int userID, ResponseCouriersPartnerAndAWBCode responseCouriersPartnerAndAWBCode)
        {
            ReturnShipmentDetails returnShipmentDetails = null;
            DataSet ds = new DataSet();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SP_PHYCreateShipmentAWB", conn)
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@order_ID", orderID);
                cmd.Parameters.AddWithValue("@item_IDs", string.IsNullOrEmpty(itemIDs) ? "" : itemIDs.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_Awb_code", string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.awb_code) ? "" : responseCouriersPartnerAndAWBCode.data.awb_code);
                cmd.Parameters.AddWithValue("@_CourierPartner", string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.courier_name) ? "" : responseCouriersPartnerAndAWBCode.data.courier_name);
                cmd.Parameters.AddWithValue("@_CourierPartnerID", string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.courier_company_id) ? "" : responseCouriersPartnerAndAWBCode.data.courier_company_id);
                cmd.Parameters.AddWithValue("@_CourierPartnerOrderID", string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.order_id) ? "" : responseCouriersPartnerAndAWBCode.data.order_id);
                cmd.Parameters.AddWithValue("@_CourierPartnerShipmentID", string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.shipment_id) ? "" : responseCouriersPartnerAndAWBCode.data.shipment_id);
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
                        returnShipmentDetails = new ReturnShipmentDetails
                        {
                            InvoiceNo = ds.Tables[0].Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNo"]),
                            AWBNumber = ds.Tables[0].Rows[i]["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AWBNo"]),
                            ItemIDs = ds.Tables[0].Rows[i]["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemID"]),
                            Status = true,
                            StatusMessge = "Success"
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
            return returnShipmentDetails;
        }

        /// <summary>
        /// UpdateGeneratePickupManifest
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public int UpdateGeneratePickupManifest(int orderID,int tenantID, int userID,string status)
        {
            int result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateflagPickupManifest", conn)
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@order_ID", orderID);              
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@user_ID", userID);
                cmd.Parameters.AddWithValue("@_status", status);
                result = cmd.ExecuteNonQuery();
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
        /// CheckClientPinCodeForCourierAvailibilty
        /// </summary>
        /// <param name="hSChkCourierAvailibilty"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="clientAPIUrl"></param>
        /// <returns></returns>
        public ResponseCourierAvailibilty CheckClientPinCodeForCourierAvailibilty(HSChkCourierAvailibilty hSChkCourierAvailibilty, int tenantID, int userID, string clientAPIUrl)
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
                responseCourierAvailibilty = new ResponseCourierAvailibilty
                {
                    StatusCode = "201",
                    Available = "false"
                };
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

    }
}
