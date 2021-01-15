using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ReturnShipmentDetails> CreateShipmentAWB(int orderID, string itemIDs, int tenantID, int userID, string clientAPIURL, string programCode, int templateID, string phygitalClientAPIURL, WebBotContentRequest webBotcontentRequest)
        {
            bool isAWBGenerated = false;
            bool iSStoreDelivery = false;

            deliveryPartner objdeliveryPartner = new deliveryPartner();
            AWbdetailModel awbdetailModel = null;
            RequestCouriersPartnerAndAWBCode requestCouriersPartnerAndAWBCode = null;
            ResponseCouriersPartnerAndAWBCode responseCouriersPartnerAndAWBCode = new ResponseCouriersPartnerAndAWBCode
            {
                data = new Easyrewardz_TicketSystem.CustomModel.Data()
            };
            ResponseGeneratePickup responseGeneratePickup = new ResponseGeneratePickup();
            ResponseGenerateManifest responseGenerateManifest = new ResponseGenerateManifest();
            ReturnShipmentDetails obj = new ReturnShipmentDetails();
            DataTable schemaTable = new DataTable();
            try
            {
                // Code for gatting data from table for request AWB client API
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetOrderAWBDetails", conn)
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_UserID", userID);
                    cmd.Parameters.AddWithValue("@_OrderID", orderID);
                    cmd.Parameters.AddWithValue("@_TemplateID", templateID);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        if (dr.HasRows)
                        {
                            schemaTable.Load(dr);
                            if (schemaTable != null)
                            {
                                isAWBGenerated = schemaTable.Rows[0]["IsAwbNoGenerated"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["IsAwbNoGenerated"]);
                            }

                            schemaTable = new DataTable();
                            schemaTable.Load(dr);
                            if (schemaTable != null)
                            {
                                requestCouriersPartnerAndAWBCode = new RequestCouriersPartnerAndAWBCode
                                {
                                    pickup_postcode = schemaTable.Rows[0]["pickup_postcode"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["pickup_postcode"]),
                                    delivery_postcode = schemaTable.Rows[0]["delivery_postcode"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["delivery_postcode"]),
                                    weight = schemaTable.Rows[0]["weight"] == DBNull.Value ? 0 : Convert.ToDouble(schemaTable.Rows[0]["weight"]),
                                    countryCode = schemaTable.Rows[0]["CountryCode"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["CountryCode"]),
                                    tenantCode = schemaTable.Rows[0]["tenantCode"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["tenantCode"]),
                                    isCountryCodeRemove = schemaTable.Rows[0]["isCountryCodeRemove"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["isCountryCodeRemove"]),
                                    mobileNoLength = schemaTable.Rows[0]["MobileNoLength"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["MobileNoLength"]),
                                };
                            }

                            schemaTable = new DataTable();
                            schemaTable.Load(dr);
                            if (schemaTable != null)
                            {
                                requestCouriersPartnerAndAWBCode.orderDetails = new OrderDetails
                                {
                                    order_id = schemaTable.Rows[0]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["InvoiceNo"]),
                                    order_date = schemaTable.Rows[0]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["Date"]),
                                    pickup_location = schemaTable.Rows[0]["pickup_location"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["pickup_location"]),
                                    channel_id = schemaTable.Rows[0]["channel_id"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["channel_id"]),
                                    billing_customer_name = schemaTable.Rows[0]["billing_customer_name"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_customer_name"]),
                                    billing_last_name = schemaTable.Rows[0]["billing_last_name"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_last_name"]),
                                    billing_address = schemaTable.Rows[0]["billing_address"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_address"]),
                                    billing_address_2 = schemaTable.Rows[0]["billing_address_2"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_address_2"]),
                                    billing_city = schemaTable.Rows[0]["billing_city"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_city"]),
                                    billing_pincode = schemaTable.Rows[0]["billing_pincode"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_pincode"]),
                                    billing_state = schemaTable.Rows[0]["billing_state"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_state"]),
                                    billing_country = schemaTable.Rows[0]["billing_country"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_country"]),
                                    billing_email = schemaTable.Rows[0]["billing_email"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_email"]),
                                    billing_phone = schemaTable.Rows[0]["billing_phone"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_phone"]),
                                    billing_alternate_phone = schemaTable.Rows[0]["billing_alternate_phone"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_alternate_phone"]),
                                    shipping_is_billing = schemaTable.Rows[0]["shipping_is_billing"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["shipping_is_billing"]),
                                    shipping_customer_name = schemaTable.Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["CustomerName"]),
                                    shipping_last_name = schemaTable.Rows[0]["billing_last_name"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_last_name"]),
                                    shipping_address = schemaTable.Rows[0]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["ShippingAddress"]),
                                    shipping_address_2 = schemaTable.Rows[0]["billing_address_2"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["billing_address_2"]),
                                    shipping_city = schemaTable.Rows[0]["City"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["City"]),
                                    shipping_pincode = schemaTable.Rows[0]["delivery_postcode"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["delivery_postcode"]),
                                    shipping_country = schemaTable.Rows[0]["Country"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["Country"]),
                                    shipping_state = schemaTable.Rows[0]["State"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["State"]),
                                    shipping_email = schemaTable.Rows[0]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["EmailID"]),
                                    shipping_phone = schemaTable.Rows[0]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["MobileNumber"]),
                                    payment_method = "Prepaid",
                                    shipping_charges = 0,
                                    giftwrap_charges = 0,
                                    transaction_charges = 0,
                                    total_discount = 0,
                                    sub_total = schemaTable.Rows[0]["Amount"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["Amount"]),  // 10,
                                    length = schemaTable.Rows[0]["Length"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["Length"]),
                                    breadth = schemaTable.Rows[0]["Breath"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["Breath"]),
                                    height = schemaTable.Rows[0]["Height"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["Height"]),
                                    weight = schemaTable.Rows[0]["Weight"] == DBNull.Value ? 0 : Convert.ToDouble(schemaTable.Rows[0]["Weight"]),

                                    droplatitude = schemaTable.Rows[0]["latitude"] == DBNull.Value ? 0 : Convert.ToDouble(schemaTable.Rows[0]["latitude"]),
                                    droplongitude = schemaTable.Rows[0]["longitude"] == DBNull.Value ? 0 : Convert.ToDouble(schemaTable.Rows[0]["longitude"]),

                                    pickuplatitude = schemaTable.Rows[0]["storelatitude"] == DBNull.Value ? 0 : Convert.ToDouble(schemaTable.Rows[0]["storelatitude"]),
                                    pickuplongitude = schemaTable.Rows[0]["storelongitude"] == DBNull.Value ? 0 : Convert.ToDouble(schemaTable.Rows[0]["storelongitude"]),
                                    //store_code = schemaTable.Rows[0]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["StoreCode"]),
                                    collection_mode = schemaTable.Rows[0]["CollectionMode"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["CollectionMode"]),
                                };

                                requestCouriersPartnerAndAWBCode.StoreAddress = new StoreAddressOrder
                                {
                                    storeLat = schemaTable.Rows[0]["storelatitude"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["storelatitude"]),
                                    storeLong = schemaTable.Rows[0]["storelongitude"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["storelongitude"]),
                                    storeAddress = schemaTable.Rows[0]["StoreAddress"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["StoreAddress"]),
                                    storeCode = schemaTable.Rows[0]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["StoreCode"]),
                                    storeContactNo = schemaTable.Rows[0]["StorePhoneNo"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["StorePhoneNo"]),
                                    storeName = schemaTable.Rows[0]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["StoreName"])
                                };
                            }
                            schemaTable = new DataTable();
                            schemaTable.Load(dr);
                            if (schemaTable != null)
                            {
                                requestCouriersPartnerAndAWBCode.orderDetails.order_items = schemaTable.AsEnumerable().Select(x => new Orderitems()
                                {
                                    name = x.Field<object>("name") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("name")),
                                    sku = x.Field<object>("sku") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("sku")),
                                    units = x.Field<object>("units") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("units")),
                                    selling_price = x.Field<object>("selling_price") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("selling_price")),
                                    discount = x.Field<object>("discount") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("discount")),
                                    tax = x.Field<object>("tax") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("tax")),
                                    hsn = x.Field<object>("hsn") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("hsn")),
                                    RequestPayload = x.Field<object>("RequestPayload") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("RequestPayload")),
                                }).ToList();
                            }
                            schemaTable = new DataTable();
                            schemaTable.Load(dr);
                            if (schemaTable != null)
                            {
                                iSStoreDelivery = schemaTable.Rows[0]["StoreDelivery"] == DBNull.Value ? false : Convert.ToBoolean(schemaTable.Rows[0]["StoreDelivery"]);
                            }

                            schemaTable = new DataTable();
                            schemaTable.Load(dr);
                            if (schemaTable != null)
                            {
                                awbdetailModel = new AWbdetailModel
                                {
                                    InvoiceNo = schemaTable.Rows[0]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["InvoiceNo"]),
                                    AWBNumber = schemaTable.Rows[0]["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["AWBNo"]),
                                    ItemIDs = schemaTable.Rows[0]["OrderItemIDs"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["OrderItemIDs"]),
                                    Date = schemaTable.Rows[0]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["Date"]),
                                    CourierPartner = schemaTable.Rows[0]["CourierPartner"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["CourierPartner"]),
                                    ShipmentCharges = schemaTable.Rows[0]["ShippingCharges"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["ShippingCharges"]),
                                };
                            }

                            schemaTable = new DataTable();
                            schemaTable.Load(dr);
                            if (schemaTable != null)
                            {
                                if (schemaTable.Rows.Count > 0)
                                {
                                    objdeliveryPartner = new deliveryPartner
                                    {
                                        partnerID = schemaTable.Rows[0]["PartnerID"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["PartnerID"]),
                                        partnerName = schemaTable.Rows[0]["PartnerName"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["PartnerName"]),
                                        priority = schemaTable.Rows[0]["Priority"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["Priority"])
                                    };
                                }
                            }
                        }
                    }
                }
                if (isAWBGenerated == false)
                {
                    HSChkCourierAvailibilty hSChkCourierAvailibilty = new HSChkCourierAvailibilty
                    {
                        Pickup_postcode = requestCouriersPartnerAndAWBCode.pickup_postcode,
                        Delivery_postcode = requestCouriersPartnerAndAWBCode.delivery_postcode
                    };

                    ResponseCourierAvailibilty responseCourierAvailibilty = new ResponseCourierAvailibilty();
                    if (string.IsNullOrEmpty(objdeliveryPartner.partnerName))
                        responseCourierAvailibilty = await CheckClientPinCodeForCourierAvailibilty(hSChkCourierAvailibilty, tenantID, userID, clientAPIURL, phygitalClientAPIURL, requestCouriersPartnerAndAWBCode.orderDetails);
                    else
                    {
                        responseCourierAvailibilty.Available = "true";
                        responseCourierAvailibilty.Responsedata = new Courierdata
                        {
                            deliveryPartner = new deliveryPartner()
                        };
                        responseCourierAvailibilty.pickup_lat = requestCouriersPartnerAndAWBCode.orderDetails.pickuplatitude;
                        responseCourierAvailibilty.pickup_lng = requestCouriersPartnerAndAWBCode.orderDetails.pickuplongitude;
                        responseCourierAvailibilty.drop_lat = requestCouriersPartnerAndAWBCode.orderDetails.droplatitude;
                        responseCourierAvailibilty.drop_lng = requestCouriersPartnerAndAWBCode.orderDetails.droplongitude;

                        responseCourierAvailibilty.Responsedata.deliveryPartner = objdeliveryPartner;
                    }
                    if (responseCourierAvailibilty.Available == "false")
                    {
                        if (iSStoreDelivery == true)
                        {
                            //CouriersPartner =Store
                            responseCouriersPartnerAndAWBCode.data.courier_name = "Store";
                            responseCouriersPartnerAndAWBCode.deliveryPartner = new deliveryPartner
                            {
                                partnerID = 0
                            };

                            obj = await CreateShipment(orderID, itemIDs, tenantID, userID, responseCouriersPartnerAndAWBCode, true);
                        }
                        else
                        {
                            obj = new ReturnShipmentDetails
                            {
                                Status = false,
                                StatusMessge = "Service not available on entered Pincode"
                            };
                            int result = await SetOrderHasBeenReturn(tenantID, userID, orderID, "Shipment");
                        }
                    }
                    else if (responseCourierAvailibilty.Available == "true")
                    {
                        int updateAWB = 0;
                        requestCouriersPartnerAndAWBCode.deliveryPartner = responseCourierAvailibilty.Responsedata.deliveryPartner;
                        requestCouriersPartnerAndAWBCode.package_content = new List<string>();
                        if (requestCouriersPartnerAndAWBCode.orderDetails.order_items.Count > 0)
                        {
                      
                            foreach (Orderitems Orderitems in requestCouriersPartnerAndAWBCode.orderDetails.order_items)
                            {
                                string strobj = string.Empty;
                            
                                strobj = Orderitems.RequestPayload;
                                requestCouriersPartnerAndAWBCode.package_content.Add(strobj);
                            }
                        }
                        requestCouriersPartnerAndAWBCode.pickup_lat = responseCourierAvailibilty.pickup_lat;
                        requestCouriersPartnerAndAWBCode.pickup_lng = responseCourierAvailibilty.pickup_lng;
                        requestCouriersPartnerAndAWBCode.drop_lat = responseCourierAvailibilty.drop_lat;
                        requestCouriersPartnerAndAWBCode.drop_lng = responseCourierAvailibilty.drop_lng;


                        string apiReq = JsonConvert.SerializeObject(requestCouriersPartnerAndAWBCode);
                        
                        apiResponse = await _phygitalhttpclientservice.SendApiRequest(phygitalClientAPIURL + _OrderURLList.Createorder, apiReq);
                        responseCouriersPartnerAndAWBCode = JsonConvert.DeserializeObject<ResponseCouriersPartnerAndAWBCode>(apiResponse);
                        responseCouriersPartnerAndAWBCode.deliveryPartner = responseCourierAvailibilty.Responsedata.deliveryPartner;
                        if (responseCouriersPartnerAndAWBCode != null)
                        {
                            if (responseCouriersPartnerAndAWBCode.data == null)
                            {
                                updateAWB = 1;
                            }
                            else if (string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.awb_code) && string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.courier_name))
                            {
                                // insert in PHYOrderReturn table (return tab)
                                obj = new ReturnShipmentDetails
                                {
                                    Status = false,
                                    StatusMessge = "Service not available on entered Pincode"
                                };
                                int result = await SetOrderHasBeenReturn(tenantID, userID, orderID, "Shipment");
                                updateAWB = 2;
                            }
                            else if (!string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.awb_code) || !string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.courier_name))
                            {
                                updateAWB = 3;
                            }

                            if (updateAWB == 3)
                            {
                                if (responseCourierAvailibilty.Responsedata.deliveryPartner.partnerID == 2)
                                {
                                    responseCouriersPartnerAndAWBCode.data.courier_name = "Dunzo";
                                }
                                obj = await CreateShipment(orderID, itemIDs, tenantID, userID, responseCouriersPartnerAndAWBCode, false);

                                await SmsWhatsUpDataSend(tenantID, userID, programCode, orderID, clientAPIURL, "AWBAssigned", webBotcontentRequest);

                                if (responseCourierAvailibilty.Responsedata.deliveryPartner.partnerID == 1)
                                {
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
                                                   
                                                    apiResponse = await _phygitalhttpclientservice.SendApiRequest(phygitalClientAPIURL + _OrderURLList.Generatepickup, apiReq1);
                                                    responseGeneratePickup = JsonConvert.DeserializeObject<ResponseGeneratePickup>(apiResponse);

                                                    if (!string.IsNullOrEmpty(responseGeneratePickup.response.pickupTokenNumber))
                                                    {
                                                        int result = await UpdateGeneratePickupManifest(orderID, tenantID, userID, "Pickup", responseGeneratePickup);
                                                    }
                                                }
                                                catch (Exception)
                                                {

                                                }
                                                try
                                                {
                                                    //Code for GenerateManifest 
                                                    string apiReq2 = JsonConvert.SerializeObject(requestGeneratePickup);
                                                    
                                                    apiResponse = await _phygitalhttpclientservice.SendApiRequest(phygitalClientAPIURL + _OrderURLList.Generatemanifest, apiReq2);
                                                    responseGenerateManifest = JsonConvert.DeserializeObject<ResponseGenerateManifest>(apiResponse);                   
                                                    if (!string.IsNullOrEmpty(responseGenerateManifest.manifestUrl))
                                                    {
                                                        int success = await UpdateGeneratePickupManifest(orderID, tenantID, userID, "Manifest", responseGeneratePickup);
                                                    }
                                                }
                                                catch (Exception)
                                                {

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (updateAWB == 1)
                            {
                                obj = new ReturnShipmentDetails
                                {
                                    Status = false,
                                    StatusMessge = responseCouriersPartnerAndAWBCode.Message
                                };
                            }
                        }
                        else
                        {
                            obj = new ReturnShipmentDetails
                            {
                                Status = false,
                                StatusMessge = "Server not available"
                            };
                        }
                    }
                }
                else
                {

                    if (!string.IsNullOrEmpty(awbdetailModel.AWBNumber) || !string.IsNullOrEmpty(awbdetailModel.CourierPartner))
                    {
                        obj = new ReturnShipmentDetails
                        {
                            AWBNumber = awbdetailModel.AWBNumber,
                            InvoiceNo = awbdetailModel.InvoiceNo,
                            ItemIDs = awbdetailModel.ItemIDs,
                            ShipmentCharges = awbdetailModel.ShipmentCharges,
                        };
                    }

                    else
                    {
                        
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
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
        public async Task<OrdersItemDetails> GetItemDetailByOrderID(int orderID, int tenantID, int userID)
        {
            List<OrdersItem> lstOrdersItem = new List<OrdersItem>();
            OrdersItemDetails ordersItemDetails = new OrdersItemDetails();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand sqlcmd = new MySqlCommand("SP_PHYGetItemDetailByOrderID", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlcmd.Parameters.AddWithValue("order_ID", orderID);
                    sqlcmd.Parameters.AddWithValue("tenant_ID", tenantID);
                    sqlcmd.Parameters.AddWithValue("user_ID", userID);

                    using (var dr = await sqlcmd.ExecuteReaderAsync()) 
                    {
                        while (dr.Read())
                        {
                            OrdersItem ordersItems = new OrdersItem
                            {
                                ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                ItemID = dr["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemID"]),
                                ItemName = dr["ItemName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemName"]),
                                ItemPrice = dr["ItemPrice"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemPrice"]),
                                Quantity = dr["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Quantity"]),
                                OrderID = dr["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrderID"]),
                                Disable = dr["Disable"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Disable"]),
                                Checked = dr["Checked"] == DBNull.Value ? false : Convert.ToBoolean(dr["Checked"]),
                            };

                            lstOrdersItem.Add(ordersItems);
                        }
                        ordersItemDetails.OrdersItems = lstOrdersItem;
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                ordersItemDetails.InvoiceNumber = dr["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["InvoiceNo"]);
                            }
                        }
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                ordersItemDetails.ShowSelectCourierPartner = dr["ShowSelectCourierPartner"] == DBNull.Value ? false : Convert.ToBoolean(dr["ShowSelectCourierPartner"]);
                                ordersItemDetails.ShowTemplate = dr["ShowTemplate"] == DBNull.Value ? false : Convert.ToBoolean(dr["ShowTemplate"]);
                            }
                        }
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
            return ordersItemDetails;
        }

        /// <summary>
        /// GetAWBInvoicenoDetails
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<ReturnShipmentDetails>> GetAWBInvoicenoDetails(int orderID, int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            List<ReturnShipmentDetails> lstReturnShipmentDetails = new List<ReturnShipmentDetails>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand sqlcmd = new MySqlCommand("SP_PHYGetAWBInvoiceDetails", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlcmd.Parameters.AddWithValue("order_ID", orderID);
                    sqlcmd.Parameters.AddWithValue("tenant_ID", tenantID);
                    sqlcmd.Parameters.AddWithValue("user_ID", userID);

                    using (var dr = await sqlcmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {

                            ReturnShipmentDetails returnShipmentDetails = new ReturnShipmentDetails
                            {
                                InvoiceNo = dr["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["InvoiceNo"]),
                                ItemIDs = dr["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemID"]),
                                AWBNumber = dr["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AWBNo"]),
                                CourierPartner = dr["CourierPartner"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CourierPartner"]),
                                ShipmentCharges = dr["ShippingCharges"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ShippingCharges"]),
                                ShowShipmentCharges = dr["ShowShipmentCharges"] == DBNull.Value ? false : Convert.ToBoolean(dr["ShowShipmentCharges"]),
                            };

                            if (returnShipmentDetails.CourierPartner.Equals("Store"))
                            {
                                returnShipmentDetails.IsStoreDelivery = true;
                            }
                            lstReturnShipmentDetails.Add(returnShipmentDetails);
                        }
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
        public async Task<GenerateLinkResponse> GenerateLink(SentPaymentLink sentPaymentLink, string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, int tenantID, int userID, string programCode, string ClientAPIUrl, HSRequestGenerateToken hSRequestGenerateToken)
        {
            int result = 0;
            string apiReq1 = "";
            string URLGeneratePaymentLink = "";
            DataSet ds = new DataSet();
            //HSRequestResendPaymentLink hSRequestResendPaymentLink = null;
            HSRequestGeneratePaymentLink hSRequestGeneratePaymentLink = null;
            string APIResponse = string.Empty;

            GenerateLinkResponse Response = new GenerateLinkResponse();
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
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
                        OrderId = ds.Tables[0].Rows[0]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]),
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
                hSRequestGeneratePaymentLink.billDateTime = dateTime_billdatetime.ToString("dd-MMM-yyyy hh:mm:ss");
                HSResponseGeneratePaymentLink responseGeneratePaymentLink = new HSResponseGeneratePaymentLink();

                string apiReq = "Client_Id=" + hSRequestGenerateToken.Client_Id + "&Client_Secret=" + hSRequestGenerateToken.Client_Secret + "&Grant_Type=" + hSRequestGenerateToken.Grant_Type + "&Scope=" + hSRequestGenerateToken.Scope;

                //apiResponse = CommonService.SendApiRequestToken(clientAPIUrlForGenerateToken + "connect/token", apiReq);
                apiResponse = await _generateToken.SendApiRequest(clientAPIUrlForGenerateToken + _OrderURLList.Token, apiReq);
                HSResponseGenerateToken hSResponseGenerateToken = new HSResponseGenerateToken();
                hSResponseGenerateToken = JsonConvert.DeserializeObject<HSResponseGenerateToken>(apiResponse);
                
                if (!string.IsNullOrEmpty(hSResponseGenerateToken.access_token))
                {
                    #region commented code to remove resend payment link API call logic

                    //if (sentPaymentLink.SentPaymentLinkCount > 0)
                    //{
                    //    hSRequestResendPaymentLink = new HSRequestResendPaymentLink
                    //    {
                    //        programCode = programCode,
                    //        tokenId = null, 
                    //        storeCode = sentPaymentLink.StoreCode,
                    //        billDateTime = hSRequestGeneratePaymentLink.billDateTime,
                    //        terminalId = hSRequestGeneratePaymentLink.terminalId,
                    //        merchantTxnID = hSRequestGeneratePaymentLink.merchantTxnID,
                    //        mobile = hSRequestGeneratePaymentLink.mobile,
                    //        reason = "RequestResendPaymentLink"
                    //    };
                    //    apiReq1 = JsonConvert.SerializeObject(hSRequestResendPaymentLink);
                        
                    //    URLGeneratePaymentLink = clientAPIUrlForGeneratePaymentLink + _OrderURLList.Resendpaymentlink;
                    //}
                    //else
                    //{
                    //    apiReq1 = JsonConvert.SerializeObject(hSRequestGeneratePaymentLink);
                        
                    //    URLGeneratePaymentLink = clientAPIUrlForGeneratePaymentLink + _OrderURLList.Generatepaymentlink;
                    //}

                    #endregion

                    apiReq1 = JsonConvert.SerializeObject(hSRequestGeneratePaymentLink);

                    URLGeneratePaymentLink = clientAPIUrlForGeneratePaymentLink + _OrderURLList.Generatepaymentlink;

                    //apiResponse1 = CommonService.SendApiRequestMerchantApi(URLGeneratePaymentLink, apiReq1, hSResponseGenerateToken.access_token);
                    apiResponse1 = await _generatepaymentlinkhttpclientservice.SendApiRequest(URLGeneratePaymentLink, apiReq1, hSResponseGenerateToken.access_token);

                    responseGeneratePaymentLink = JsonConvert.DeserializeObject<HSResponseGeneratePaymentLink>(apiResponse1);

                    Response.ResponseMessage = responseGeneratePaymentLink.returnMessage;
                }
                else
                {
                    Response.ResponseCode = 0; Response.ResponseMessage = "Access Token Is Empty";
                }


                Response.ResponseCode = responseGeneratePaymentLink.returnCode.Equals("0") && responseGeneratePaymentLink.tokenStatus.Contains("Initiated") ? 1 : 0 ;

                        if (conn != null && conn.State == ConnectionState.Closed)
                        {
                            await conn.OpenAsync();
                        }
                        MySqlCommand cmd1 = new MySqlCommand("SP_PHYUpdateOrderDetailForPaymentLink", conn)
                        {
                            Connection = conn,
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd1.Parameters.AddWithValue("@Invoice_Number", sentPaymentLink.InvoiceNumber);
                        cmd1.Parameters.AddWithValue("@access_Token", responseGeneratePaymentLink.tokenId);
                        cmd1.Parameters.AddWithValue("@tenant_ID", tenantID);
                        cmd1.Parameters.AddWithValue("@user_ID", userID);

                        //added for logging payment link generation Request and Response
                        cmd1.Parameters.AddWithValue("@Is_UpdateOrder", Response.ResponseCode);
                        cmd1.Parameters.AddWithValue("@_PaymentLinkURL", URLGeneratePaymentLink);
                        cmd1.Parameters.AddWithValue("@_AccessToken", hSResponseGenerateToken.access_token);
                        cmd1.Parameters.AddWithValue("@_PaymentLinkRequest", apiReq1);
                        cmd1.Parameters.AddWithValue("@_PaymentLinkResponse", apiResponse1);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        result = Convert.ToInt32(await cmd1.ExecuteNonQueryAsync());
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
            return Response;
        }

        /// <summary>
        /// Check PinCode For Courier Availibilty
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public async Task<ResponseCourierAvailibilty> CheckPinCodeForCourierAvailibilty(HSChkCourierAvailibilty hSChkCourierAvailibilty, int tenantID, int userID, string clientAPIUrl, string PhygitalClientAPIURL)
        {
            ResponseCourierAvailibilty responseCourierAvailibilty = new ResponseCourierAvailibilty();
            Courierdata data = new Courierdata();
            try
            {
                PincodeCheck pincodeCheck = await CheckPincodeExists(tenantID, userID, Convert.ToString(hSChkCourierAvailibilty.Delivery_postcode));
                if (pincodeCheck != null)
                {
                    if (pincodeCheck.PincodeAvailable)
                    {

                        hSChkCourierAvailibilty.partnerList = new List<PartnerList>();
                        hSChkCourierAvailibilty.partnerList = await GetPHYOrderActivePartner(tenantID, userID);

                        OrderTabSetting orderTabSetting = new OrderTabSetting();
                        orderTabSetting = await GetOrderTabSettingDetails(tenantID, userID);

                        if (hSChkCourierAvailibilty.partnerList.Count < 1)
                        {
                            if (orderTabSetting.StoreDelivery)
                            {
                                responseCourierAvailibilty.StatusCode = "200";
                                responseCourierAvailibilty.Available = "true";
                                responseCourierAvailibilty.State = pincodeCheck.PincodeState;
                            }
                            else
                            {
                                responseCourierAvailibilty = new ResponseCourierAvailibilty
                                {
                                    StatusCode = "201",
                                    Available = "false"
                                };
                            }

                            return responseCourierAvailibilty;
                        }

                        GeocodeByAddressResponse geocodeByAddressResponse = new GeocodeByAddressResponse();
                        geocodeByAddressResponse = await GeocodeByAddress(Convert.ToString(hSChkCourierAvailibilty.Pickup_postcode), PhygitalClientAPIURL);
                        if (geocodeByAddressResponse != null)
                        {
                            if (geocodeByAddressResponse.data != null)
                            {
                                hSChkCourierAvailibilty.pickup_lat = geocodeByAddressResponse.data.latitude;
                                hSChkCourierAvailibilty.pickup_lng = geocodeByAddressResponse.data.longitude;
                            }
                        }
                        geocodeByAddressResponse = await GeocodeByAddress(Convert.ToString(hSChkCourierAvailibilty.Delivery_postcode), PhygitalClientAPIURL);
                        if (geocodeByAddressResponse != null)
                        {
                            if (geocodeByAddressResponse.data != null)
                            {
                                hSChkCourierAvailibilty.drop_lat = geocodeByAddressResponse.data.latitude;
                                hSChkCourierAvailibilty.drop_lng = geocodeByAddressResponse.data.longitude;
                            }
                        }

                        
                        hSChkCourierAvailibilty.Cod = 0;
                        hSChkCourierAvailibilty.Weight = 1;
                        string apiReq = JsonConvert.SerializeObject(hSChkCourierAvailibilty);
                        
                        apiResponse = await _phygitalhttpclientservice.SendApiRequest(PhygitalClientAPIURL + _OrderURLList.Chkcourieravailibilty, apiReq);
                        responseCourierAvailibilty = JsonConvert.DeserializeObject<ResponseCourierAvailibilty>(apiResponse);

                        if (responseCourierAvailibilty != null)
                        {
                            if (responseCourierAvailibilty.StatusCode != "200")
                            {
                                responseCourierAvailibilty = new ResponseCourierAvailibilty
                                {
                                    StatusCode = "201",
                                    Available = "false"
                                };
                            }
                            else
                            {
                                data = JsonConvert.DeserializeObject<Courierdata>(responseCourierAvailibilty.data.ToString());
                            }

                            if (data.isAvailable == false)
                            {
                                if (orderTabSetting.StoreDelivery)
                                {
                                    responseCourierAvailibilty.Available = "true";
                                    responseCourierAvailibilty.State = pincodeCheck.PincodeState;
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
                            else
                            {
                                responseCourierAvailibilty.Available = "true";
                                responseCourierAvailibilty.State = pincodeCheck.PincodeState;
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
                    else
                    {
                        responseCourierAvailibilty = new ResponseCourierAvailibilty
                        {
                            StatusCode = "301",
                            Available = "false"
                        };
                    }
                }
                else
                {
                    responseCourierAvailibilty = new ResponseCourierAvailibilty
                    {
                        StatusCode = "301",
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
        public async Task<string> GetStorePinCodeByUserID(int tenantID, int userID)
        {
            string pinCode = "";
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand sqlcmd = new MySqlCommand("SP_PHYGetStorePinCodeByUserID", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlcmd.Parameters.AddWithValue("tenant_ID", tenantID);
                    sqlcmd.Parameters.AddWithValue("user_ID", userID);

                    using (var dr = await sqlcmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            pinCode = dr["PincodeID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PincodeID"]);
                        }
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
            return pinCode;
        }

        /// <summary>
        /// Create Shipment
        /// </summary>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public async Task<ReturnShipmentDetails> CreateShipment(int orderID, string itemIDs, int tenantID, int userID, ResponseCouriersPartnerAndAWBCode responseCouriersPartnerAndAWBCode, bool IsStoreDelivery)
        {
            ReturnShipmentDetails returnShipmentDetails = null;
            DataTable schemaTable = new DataTable();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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
                    cmd.Parameters.AddWithValue("@_ShipmentCharges", string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.rate) ? "0" : responseCouriersPartnerAndAWBCode.data.rate);
                    cmd.Parameters.AddWithValue("@_EstimatedDeliveryDate", string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.etd) ? "" : responseCouriersPartnerAndAWBCode.data.etd);
                    cmd.Parameters.AddWithValue("@_DeliveryPartnerpartnerID", responseCouriersPartnerAndAWBCode.deliveryPartner.partnerID);
                    cmd.Parameters.AddWithValue("@_DeliveryPartnerpartnerName", string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.deliveryPartner.partnerName) ? "" : responseCouriersPartnerAndAWBCode.deliveryPartner.partnerName);
                    cmd.Parameters.AddWithValue("@_Drop_DateTime", string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.etd_Drop_DateTime) ? "" : responseCouriersPartnerAndAWBCode.data.etd_Drop_DateTime);
                    cmd.Parameters.AddWithValue("@_Pickup_DateTime", string.IsNullOrEmpty(responseCouriersPartnerAndAWBCode.data.etd_Pickup_DateTime) ? "" : responseCouriersPartnerAndAWBCode.data.etd_Pickup_DateTime);
                    cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                    cmd.Parameters.AddWithValue("@user_ID", userID);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            if (schemaTable != null)
                            {
                                foreach (DataRow dr in schemaTable.Rows)
                                {
                                    returnShipmentDetails = new ReturnShipmentDetails
                                    {
                                        InvoiceNo = dr["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["InvoiceNo"]),
                                        AWBNumber = dr["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AWBNo"]),
                                        ItemIDs = dr["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemID"]),
                                        Status = true,
                                        StatusMessge = "Success",
                                        IsStoreDelivery = IsStoreDelivery,
                                        ShipmentCharges = dr["ShipmentCharges"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ShipmentCharges"])
                                    };
                                }
                            }
                        }
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }
            return returnShipmentDetails;
        }

        /// <summary>
        /// Update Generate Pickup Manifest
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public async Task<int> UpdateGeneratePickupManifest(int orderID, int tenantID, int userID, string status, ResponseGeneratePickup responseGeneratePickup)
        {
            int result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYUpdateflagPickupManifest", conn)
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@order_ID", orderID);
                    cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                    cmd.Parameters.AddWithValue("@user_ID", userID);
                    cmd.Parameters.AddWithValue("@_status", status);
                    cmd.Parameters.AddWithValue("@_pickupScheduledDate", string.IsNullOrEmpty(responseGeneratePickup.response.pickupScheduledDate) ? "" : responseGeneratePickup.response.pickupScheduledDate);
                    result = await cmd.ExecuteNonQueryAsync();
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
        /// Check Client PinCode For Courier Availibilty
        /// </summary>
        /// <param name="hSChkCourierAvailibilty"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="clientAPIUrl"></param>
        /// <returns></returns>
        public async Task<ResponseCourierAvailibilty> CheckClientPinCodeForCourierAvailibilty(HSChkCourierAvailibilty hSChkCourierAvailibilty, int tenantID, int userID, string clientAPIUrl, string phygitalClientAPIURL, OrderDetails orderDetails)
        {
            ResponseCourierAvailibilty responseCourierAvailibilty = new ResponseCourierAvailibilty();
            Courierdata data = new Courierdata();
            try
            {


                hSChkCourierAvailibilty.partnerList = new List<PartnerList>();
                hSChkCourierAvailibilty.partnerList = await GetPHYOrderActivePartner(tenantID, userID);

                if (hSChkCourierAvailibilty.partnerList.Count < 1)
                {
                    responseCourierAvailibilty = new ResponseCourierAvailibilty
                    {
                        StatusCode = "201",
                        Available = "false"
                    };
                    return responseCourierAvailibilty;
                }

                GeocodeByAddressResponse geocodeByAddressResponse = new GeocodeByAddressResponse();

                if (orderDetails.pickuplatitude == 0 && orderDetails.pickuplongitude == 0)
                {
                    geocodeByAddressResponse = await GeocodeByAddress(orderDetails.billing_address + ", " + orderDetails.billing_city + ", " + orderDetails.billing_pincode
                        + ", " + orderDetails.billing_state + ", " + orderDetails.billing_country, phygitalClientAPIURL);
                    if (geocodeByAddressResponse != null)
                    {
                        if (geocodeByAddressResponse.data != null)
                        {
                            hSChkCourierAvailibilty.pickup_lat = geocodeByAddressResponse.data.latitude;
                            hSChkCourierAvailibilty.pickup_lng = geocodeByAddressResponse.data.longitude;
                        }
                    }
                }
                else
                {
                    hSChkCourierAvailibilty.pickup_lat = orderDetails.pickuplatitude;
                    hSChkCourierAvailibilty.pickup_lng = orderDetails.pickuplongitude;
                }

                if (orderDetails.droplatitude == 0 && orderDetails.droplongitude == 0)
                {
                    geocodeByAddressResponse = await GeocodeByAddress(orderDetails.shipping_address + ", " + orderDetails.shipping_city + ", " + orderDetails.shipping_pincode
                    + ", " + orderDetails.shipping_state + ", " + orderDetails.shipping_country, phygitalClientAPIURL);
                    if (geocodeByAddressResponse != null)
                    {
                        if (geocodeByAddressResponse.data != null)
                        {
                            hSChkCourierAvailibilty.drop_lat = geocodeByAddressResponse.data.latitude;
                            hSChkCourierAvailibilty.drop_lng = geocodeByAddressResponse.data.longitude;
                        }
                    }
                }
                else
                {
                    hSChkCourierAvailibilty.drop_lat = orderDetails.droplatitude;
                    hSChkCourierAvailibilty.drop_lng = orderDetails.droplongitude;
                }
                


                hSChkCourierAvailibilty.Cod = 0;
                hSChkCourierAvailibilty.Weight = 1;
                string apiReq = JsonConvert.SerializeObject(hSChkCourierAvailibilty);
                
                apiResponse = await _phygitalhttpclientservice.SendApiRequest(phygitalClientAPIURL + _OrderURLList.Chkcourieravailibilty, apiReq);
                responseCourierAvailibilty = JsonConvert.DeserializeObject<ResponseCourierAvailibilty>(apiResponse);

                if (responseCourierAvailibilty.StatusCode != "200")
                {
                    responseCourierAvailibilty = new ResponseCourierAvailibilty
                    {
                        StatusCode = "201",
                        Available = "false"
                    };
                }
                else
                {
                    data = JsonConvert.DeserializeObject<Courierdata>(responseCourierAvailibilty.data.ToString());
                    responseCourierAvailibilty.Responsedata = data;
                    responseCourierAvailibilty.pickup_lat = hSChkCourierAvailibilty.pickup_lat;
                    responseCourierAvailibilty.pickup_lng = hSChkCourierAvailibilty.pickup_lng;
                    responseCourierAvailibilty.drop_lat = hSChkCourierAvailibilty.drop_lat;
                    responseCourierAvailibilty.drop_lng = hSChkCourierAvailibilty.drop_lng;

                    responseCourierAvailibilty.Available = "true";
                }
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

        /// <summary>
        /// MakeShipmentAsStoreDelivery
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="itemIDs"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<ReturnShipmentDetails> MakeShipmentAsStoreDelivery(int orderID, string itemIDs, int tenantID, int userID, int templateID)
        {
            ReturnShipmentDetails returnShipmentDetails = null;
            DataSet ds = new DataSet();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYMakeShipmentAsStoreDelivery", conn)
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_orderID", orderID);
                    cmd.Parameters.AddWithValue("@_itemIDs", string.IsNullOrEmpty(itemIDs) ? "" : itemIDs.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@_courierPartner", "Store");
                    cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                    cmd.Parameters.AddWithValue("@_userID", userID);
                    cmd.Parameters.AddWithValue("@_templateID", templateID);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            returnShipmentDetails = new ReturnShipmentDetails
                            {
                                InvoiceNo = dr["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["InvoiceNo"]),
                                AWBNumber = dr["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AWBNo"]),
                                ItemIDs = dr["ItemID"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemID"]),
                                Status = true,
                                StatusMessge = "Success",
                                IsStoreDelivery = true,
                                ShipmentCharges = dr["ShipmentCharges"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ShipmentCharges"])
                            };
                        }
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
        /// GetPHYOrderActivePartner
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<PartnerList>> GetPHYOrderActivePartner(int tenantID, int userID)
        {
            DataTable schemaTable = new DataTable();
            List<OrdersItem> lstOrdersItem = new List<OrdersItem>();
            List<PartnerList> orderActivePartner = new List<PartnerList>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand sqlcmd = new MySqlCommand("SP_GetPHYOrderActivePartner", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlcmd.Parameters.AddWithValue("_TenantID", tenantID);
                    sqlcmd.Parameters.AddWithValue("_UserID", userID);

                    using (var reader = await sqlcmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            if (schemaTable != null)
                            {
                                foreach (DataRow dr in schemaTable.Rows)
                                {
                                    PartnerList ordersItems = new PartnerList
                                    {
                                        partnerID = dr["PartnerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PartnerID"]),
                                        partnerName = dr["PartnerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PartnerName"]),
                                        priority = dr["Priority"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Priority"]),
                                    };
                                    orderActivePartner.Add(ordersItems);
                                }
                            }
                        }
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
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }
            return orderActivePartner;
        }

        /// <summary>
        /// GeocodeByAddress
        /// </summary>
        /// <param name="Pickup_postcode"></param>
        /// <param name="PhygitalClientAPIURL"></param>
        /// <returns></returns>
        public async Task<GeocodeByAddressResponse> GeocodeByAddress(string Pickup_postcode, string PhygitalClientAPIURL)
        {
            GeocodeByAddressResponse geocodeByAddressResponse = new GeocodeByAddressResponse();
            try
            {
                GeocodeByAddress geocodeByAddress = new GeocodeByAddress
                {
                    address = Convert.ToString(Pickup_postcode)
                };
               
                apiResponse = await _phygitalhttpclientservice.SendApiRequest(PhygitalClientAPIURL + _OrderURLList.Getgeocodebyaddress, JsonConvert.SerializeObject(geocodeByAddress));
                geocodeByAddressResponse = JsonConvert.DeserializeObject<GeocodeByAddressResponse>(apiResponse);
            }
            catch (Exception)
            {
                throw;
            }

            return geocodeByAddressResponse;
        }

        /// <summary>
        /// CancelOrder
        /// </summary>
        /// <param name="orderCancelRequest"></param>
        /// <returns></returns>
        public async Task<OrderCancelResponse> CancelOrder(OrderCancelRequest orderCancelRequest)
        {
            OrderCancelResponse orderCancelResponse = new OrderCancelResponse();
            CancelOrderDetails cancelOrderDetails = new CancelOrderDetails();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand sqlcmd = new MySqlCommand("SP_PHYGetOrderForCancellation", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlcmd.Parameters.AddWithValue("_TenantID", orderCancelRequest.tenantID);
                    sqlcmd.Parameters.AddWithValue("_UserID", orderCancelRequest.userID);
                    sqlcmd.Parameters.AddWithValue("_OrderID", orderCancelRequest.orderID);

                    MySqlDataAdapter da = new MySqlDataAdapter
                    {
                        SelectCommand = sqlcmd
                    };

                    using (var dr = await sqlcmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            deliveryPartner ordersItems = new deliveryPartner
                            {
                                partnerID = dr["DeliveryPartnerpartnerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DeliveryPartnerpartnerID"]),
                                partnerName = dr["DeliveryPartnerpartnerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DeliveryPartnerpartnerName"]),
                                priority = dr["DeliveryPartnerPriority"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DeliveryPartnerPriority"]),
                            };
                            cancelOrderDetails.partner = ordersItems;
                            cancelOrderDetails.orderId = new OrderIds
                            {
                                ids = new List<int>()
                            };
                            if (ordersItems.partnerID == 1)
                            {
                                cancelOrderDetails.orderId.ids = new List<int>
                                {
                                    dr["CourierPartnerOrderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CourierPartnerOrderID"]),
                                };
                            }
                            cancelOrderDetails.task_id = dr["AWBNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AWBNo"]);
                        }

                        cancelOrderDetails.cancellation_reason = orderCancelRequest.cancellationReason;

                        string apiReq = JsonConvert.SerializeObject(cancelOrderDetails);                   
                        apiResponse = await _phygitalhttpclientservice.SendApiRequest(orderCancelRequest.phygitalClientAPIURL + _OrderURLList.Cancelorder, apiReq);
                        orderCancelResponse = JsonConvert.DeserializeObject<OrderCancelResponse>(apiResponse);
                    }

                    if (orderCancelResponse.StatusCode == "200")
                    {
                        sqlcmd = new MySqlCommand("SP_PHYUpdateOrderCancellation", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        sqlcmd.Parameters.AddWithValue("_TenantID", orderCancelRequest.tenantID);
                        sqlcmd.Parameters.AddWithValue("_UserID", orderCancelRequest.userID);
                        sqlcmd.Parameters.AddWithValue("_OrderID", orderCancelRequest.orderID);
                        sqlcmd.Parameters.AddWithValue("_cancellationReason", orderCancelRequest.cancellationReason);
                        sqlcmd.Parameters.AddWithValue("_cancellationResponseMessage", orderCancelResponse.Message);

                        await sqlcmd.ExecuteNonQueryAsync();
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
            return orderCancelResponse;
        }

        /// <summary>
        /// GetPODDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public async Task<OrderResponseDetails> PODDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            OrderResponseDetails objdetails = new OrderResponseDetails();
            List<Orders> orderlist = new List<Orders>();
            int TotalCount = 0;
            DataTable ordersTable = new DataTable();
            DataTable ordersItemTable = new DataTable();
            DataTable totalTable = new DataTable();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYGetPODdetails", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_SearchText", ordersDataRequest.SearchText);
                    cmd.Parameters.AddWithValue("@_pageno", ordersDataRequest.PageNo);
                    cmd.Parameters.AddWithValue("@_pagesize", ordersDataRequest.PageSize);
                    cmd.Parameters.AddWithValue("@_FilterStatus", ordersDataRequest.FilterStatus);
                    cmd.Parameters.AddWithValue("@_CourierPartner", ordersDataRequest.CourierPartner);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            ordersTable.Load(reader);
                            ordersItemTable.Load(reader);
                            totalTable.Load(reader);
                        }
                    }

                    if (ordersTable != null)
                    {
                        for (int i = 0; i < ordersTable.Rows.Count; i++)
                        {
                            Orders obj = new Orders
                            {
                                ID = ordersTable.Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ordersTable.Rows[i]["ID"]),
                                InvoiceNo = ordersTable.Rows[i]["InvoiceNo"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["InvoiceNo"]),
                                CustomerName = ordersTable.Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["CustomerName"]),
                                MobileNumber = ordersTable.Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["MobileNumber"]),
                                Amount = ordersTable.Rows[i]["Amount"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Amount"]),
                                Date = ordersTable.Rows[i]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Date"]),
                                Time = ordersTable.Rows[i]["Time"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["Time"]),
                                StatusName = ordersTable.Rows[i]["StatusName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["StatusName"]),
                                ShippingAddress = ordersTable.Rows[i]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ShippingAddress"]),
                                ActionTypeName = ordersTable.Rows[i]["ActionTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ActionTypeName"]),
                                IsShoppingBagConverted = ordersTable.Rows[i]["IsShoppingBagConverted"] == DBNull.Value ? false : Convert.ToBoolean(ordersTable.Rows[i]["IsShoppingBagConverted"]),
                                ShoppingID = ordersTable.Rows[i]["ShoppingID"] == DBNull.Value ? 0 : Convert.ToInt32(ordersTable.Rows[i]["ShoppingID"]),
                                //ShoppingBagNo = ordersTable.Rows[i]["ShoppingBagNo"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ShoppingBagNo"]),
                                DeliveryType = ordersTable.Rows[i]["DeliveryType"] == DBNull.Value ? 0 : Convert.ToInt32(ordersTable.Rows[i]["DeliveryType"]),
                                // DeliveryTypeName = ordersTable.Rows[i]["DeliveryTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["DeliveryTypeName"]),
                                PickupDate = ordersTable.Rows[i]["PickupDate"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PickupDate"]),
                                PickupTime = ordersTable.Rows[i]["PickupTime"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PickupTime"]),
                                CourierPartner = ordersTable.Rows[i]["CourierPartner"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["CourierPartner"]),
                                ShippingCharges = ordersTable.Rows[i]["ShippingCharges"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["ShippingCharges"]),
                                EstimatedDeliveryDate = ordersTable.Rows[i]["EstimatedDeliveryDate"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["EstimatedDeliveryDate"]),
                                PickupScheduledDate = ordersTable.Rows[i]["PickupScheduledDate"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PickupScheduledDate"]),
                                CancelButtonInShipment = ordersTable.Rows[i]["CancelButtonInShipment"] == DBNull.Value ? false : Convert.ToBoolean(ordersTable.Rows[i]["CancelButtonInShipment"]),
                                IsPODPaymentReceived = ordersTable.Rows[i]["IsPODPaymentReceived"] == DBNull.Value ? false : Convert.ToBoolean(ordersTable.Rows[i]["IsPODPaymentReceived"]),
                                PODPaymentReceivedOn = ordersTable.Rows[i]["PODPaymentReceivedOn"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PODPaymentReceivedOn"]),
                                PODPaymentComent = ordersTable.Rows[i]["PODPaymentComent"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["PODPaymentComent"]),
                                PODCommentBy = ordersTable.Rows[i]["CommentBy"] == DBNull.Value ? string.Empty : Convert.ToString(ordersTable.Rows[i]["CommentBy"]),
                                OrdersItemList = new List<OrdersItem>(),
                                ShoppingBagItemList = new List<ShoppingBagItem>()
                            };


                            obj.OrdersItemList = ordersItemTable.AsEnumerable().Where(x => (x.Field<int>("OrderID")).Equals(obj.ID)).Select(x => new OrdersItem()
                            {
                                ID = x.Field<object>("ID") == System.DBNull.Value ? 0 : Convert.ToInt32(x.Field<int>("ID")),
                                ItemID = x.Field<object>("ItemID") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemID")),
                                ItemName = x.Field<object>("ItemName") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemName")),
                                ItemPrice = x.Field<object>("ItemPrice") == System.DBNull.Value ? string.Empty : Convert.ToString(x.Field<string>("ItemPrice")),
                                Quantity = x.Field<object>("Quantity") == System.DBNull.Value ? 0 : x.Field<int>("Quantity")

                            }).ToList();

                            orderlist.Add(obj);
                        }
                    }

                    if (totalTable != null)
                    {
                        if (totalTable.Rows.Count > 0)
                        {
                            TotalCount = totalTable.Rows[0]["TotalOrder"] == DBNull.Value ? 0 : Convert.ToInt32(totalTable.Rows[0]["TotalOrder"]);
                        }
                    }

                    objdetails.OrdersList = orderlist;
                    objdetails.TotalCount = TotalCount;
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
                if (ordersTable != null)
                {
                    ordersTable.Dispose();
                }
                if (ordersItemTable != null)
                {
                    ordersItemTable.Dispose();
                }
                if (totalTable != null)
                {
                    totalTable.Dispose();
                }
            }
            return objdetails;
        }

        /// <summary>
        /// PaymentComment
        /// </summary>
        /// <param name="PaymentCommentModel"></param>
        /// <returns></returns>
        public async Task<int> PaymentComment(PaymentCommentModel paymentCommentModel)
        {

            MySqlCommand cmd = new MySqlCommand();
            int result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                cmd.Connection = conn;
                cmd = new MySqlCommand("SP_PHYPODPaymentComment", conn);

                cmd.Parameters.AddWithValue("@_UserID", paymentCommentModel.UserID);
                cmd.Parameters.AddWithValue("@_TenantID", paymentCommentModel.TenantID);
                cmd.Parameters.AddWithValue("@_OrderID", paymentCommentModel.OrderID);
                cmd.Parameters.AddWithValue("@_PODPaymentComent", paymentCommentModel.PODPaymentComent);

                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
        /// PaymentComment
        /// </summary>
        /// <param name="PaymentCommentModel"></param>
        /// <returns></returns>
        public async Task<List<DownloadReportResponse>> DownloadReport(DownloadReportRequest downloadReportRequest)
        {
            List<DownloadReportResponse> lstDownloadReportResponse = new List<DownloadReportResponse>();

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_PHYFilterReport", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", downloadReportRequest.TenantID);
                    cmd.Parameters.AddWithValue("@_UserID", downloadReportRequest.UserID);
                    cmd.Parameters.AddWithValue("@_Option", downloadReportRequest.Option);
                    cmd.Parameters.AddWithValue("@_FromDate", downloadReportRequest.FromDate);
                    cmd.Parameters.AddWithValue("@_Todate", downloadReportRequest.ToDate);
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            DownloadReportResponse obj = new DownloadReportResponse
                            {
                                OrderID = dr["InvoiceNo"] == DBNull.Value ? "" : Convert.ToString(dr["InvoiceNo"]),
                                CustomerName = dr["CustomerName"] == DBNull.Value ? "" : Convert.ToString(dr["CustomerName"]),
                                CustomerMobile = dr["MobileNumber"] == DBNull.Value ? "" : Convert.ToString(dr["MobileNumber"]),
                                CartAmount = dr["Amount"] == DBNull.Value ? "" : Convert.ToString(dr["Amount"]),
                                Partner = dr["CourierPartner"] == DBNull.Value ? "" : Convert.ToString(dr["CourierPartner"]),
                                PaymentStaus = dr["PaymentStatus"] == DBNull.Value ? "" : Convert.ToString(dr["PaymentStatus"])
                            };
                            lstDownloadReportResponse.Add(obj);
                        }
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

            return lstDownloadReportResponse;

        }
    }
}
