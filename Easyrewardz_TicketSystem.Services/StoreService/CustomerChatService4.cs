using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        /// <summary>
        ///   Get Chat Customer Products Details
        /// <param name="tenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// </summary>
        /// <returns></returns>
        public List<CustomerChatProductModel> GetChatCustomerProducts(int TenantId, string ProgramCode, int CustomerID, string MobileNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<CustomerChatProductModel> CustomerProducts = new List<CustomerChatProductModel>();
            string CardItemsIds = string.Empty;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSChatCustomerProductsList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@_MobileNo", MobileNo);


                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            CustomerChatProductModel Obj = new CustomerChatProductModel()
                            {
                                ProductID = dr["ProductID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ProductID"]),
                                uniqueItemCode = dr["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemCode"]),
                                productName = dr["ItemName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemName"]),
                                categoryName = dr["Category"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Category"]),
                                subCategoryName = dr["SubCategory"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SubCategory"]),
                                color = dr["Color"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Color"]),
                                colorCode = dr["ColorCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ColorCode"]),
                                brandName = dr["Brand"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Brand"]),
                                price = dr["Price"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Price"]),
                                discount = dr["Discount"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Discount"]),
                                url = dr["Url"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Url"]),
                                imageURL = dr["ImageURL"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ImageURL"]),
                                size = dr["Size"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Size"]),

                                StoreID = dr["StoreId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreId"]),
                                StoreCode = dr["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreCode"]),
                                IsShoppingBag = dr["IsShoppingBag"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsShoppingBag"]),
                                IsWishList = dr["IsWishList"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsWishList"]),
                                IsRecommended = dr["IsRecommended"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsRecommended"]),
                               
                            };


                            CustomerProducts.Add(Obj);
                        }

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            CardItemsIds = ds.Tables[1].Rows[0]["CardItemID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["CardItemID"]);
                        }

                        if (!string.IsNullOrEmpty(CardItemsIds))
                        {
                            string[] CardItemsIDArr = CardItemsIds.Split(new char[] { ',' });

                            if (CardItemsIDArr.Contains("1"))
                                CustomerProducts = CustomerProducts.Select(x => { x.uniqueItemCode = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("2"))
                                CustomerProducts = CustomerProducts.Select(x => { x.categoryName = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("3"))
                                CustomerProducts = CustomerProducts.Select(x => { x.subCategoryName = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("4"))
                                CustomerProducts = CustomerProducts.Select(x => { x.brandName = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("5"))
                                CustomerProducts = CustomerProducts.Select(x => { x.color = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("6"))
                                CustomerProducts = CustomerProducts.Select(x => { x.size = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("7"))
                                CustomerProducts = CustomerProducts.Select(x => { x.price = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("8"))
                                CustomerProducts = CustomerProducts.Select(x => { x.url = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("9"))
                                CustomerProducts = CustomerProducts.Select(x => { x.imageURL = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("10"))
                                CustomerProducts = CustomerProducts.Select(x => { x.productName = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("11"))
                                CustomerProducts = CustomerProducts.Select(x => { x.discount = ""; return x; }).ToList();

                            if (CardItemsIDArr.Contains("12"))
                                CustomerProducts = CustomerProducts.Select(x => { x.colorCode = ""; return x; }).ToList();


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

            return CustomerProducts;
        }


        /// <summary>
        ///  Remove Item from Wishlist /Shopping Bag
        /// <param name="tenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ItemCode"></param>
        /// </summary>
        /// <returns></returns>
        public int RemoveProduct(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCode)
        {
            MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSRemoveProduct", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@_MobileNo", CustomerMobile);
                cmd.Parameters.AddWithValue("@_ItemCode", ItemCode);

                cmd.CommandType = CommandType.StoredProcedure;

                Result = Convert.ToInt32(cmd.ExecuteScalar());


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

            return Result;
        }

        /// <summary>
        /// Add Products To ShoppingBag
        /// <param name="tenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ItemCodes"></param>
        /// </summary>
        /// <returns></returns>
        public int AddProductsToShoppingBag(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, bool IsFromRecommendation,int UserID)
        {
            MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSAddProductsToBagOrWishList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@_MobileNo", CustomerMobile);
                cmd.Parameters.AddWithValue("@_ItemCode", string.IsNullOrEmpty(ItemCodes) ? "" : ItemCodes.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_Action","shoppingbag");
                cmd.Parameters.AddWithValue("@_IsFromRecommendation", Convert.ToInt16(IsFromRecommendation));
                cmd.Parameters.AddWithValue("@User_ID", UserID);

                cmd.CommandType = CommandType.StoredProcedure;

                Result = Convert.ToInt32(cmd.ExecuteScalar());


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

            return Result;
        }

        /// <summary>
        /// Add Products To Wishlist
        /// <param name="tenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ItemCodes"></param>
        /// </summary>
        /// <returns></returns>
        public int AddProductsToWishlist(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, bool IsFromRecommendation, int UserID)
        {
            MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSAddProductsToBagOrWishList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@_MobileNo", CustomerMobile);
                cmd.Parameters.AddWithValue("@_ItemCode", string.IsNullOrEmpty(ItemCodes) ? "" : ItemCodes.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_Action", "wishlist");
                cmd.Parameters.AddWithValue("@_IsFromRecommendation", Convert.ToInt16(IsFromRecommendation));
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;

                Result = Convert.ToInt32(cmd.ExecuteScalar());


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

            return Result;
        }

        /// <summary>
        /// Buy Products On Chat from shopping Bag/WishList
        /// <param name="ChatCustomerBuyModel"></param>
        /// <param name="ClientAPIURL"></param>
        /// </summary>
        /// <returns></returns>
        public int BuyProductsOnChat(ChatCustomerBuyModel Buy,string ClientAPIURL)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            int Result = 0;
            string ClientAPIresponse = string.Empty;
            PhyAddOrderModel PhyOrder = new PhyAddOrderModel();
            List<ItemDetail> ItemDetails = new List<ItemDetail>();
            string Store_Code = string.Empty; string CustomerName = string.Empty;
            double TotalAmount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                cmd = new MySqlCommand("SP_HSBuyProductsOnChat", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", Buy.TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", Buy.ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", Buy.CustomerID);
                cmd.Parameters.AddWithValue("@_MobileNo", Buy.CustomerMobile);
                cmd.Parameters.AddWithValue("@_ItemCode", string.IsNullOrEmpty(Buy.ItemCodes) ? "" : Buy.ItemCodes.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_IsDirectBuy",   Convert.ToInt16(Buy.IsDirectBuy) );
                cmd.Parameters.AddWithValue("@_IsFromRecommendation", Convert.ToInt16(Buy.IsFromRecommendation));
                cmd.Parameters.AddWithValue("@User_ID", Buy.UserID);
                
                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (!Buy.IsDirectBuy)
                {
                    if (ds != null && ds.Tables != null)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                ItemDetail obj = new ItemDetail()
                                {
                                    itemID = dr["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemCode"]),
                                    itemName = dr["ItemName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemName"]),
                                    itemPrice = dr["Price"] == DBNull.Value ? "0" : Convert.ToString(dr["Price"]),
                                    quantity = "1",
                                    category = dr["Category"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Category"]),
                                };

                                ItemDetails.Add(obj);
                            }
                        }

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            TotalAmount=ds.Tables[1].Rows[0]["TotalAmount"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[0]["TotalAmount"]);
                            Store_Code = ds.Tables[1].Rows[0]["Store_Code"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["Store_Code"]);
                            CustomerName = ds.Tables[1].Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["CustomerName"]);
                        }
                       
                    }

                    if(ItemDetails.Count > 0)
                    {
                        PhyOrder.itemDetails = ItemDetails;
                        PhyOrder.source = "BELL";
                        PhyOrder.progCode = Buy.ProgramCode;
                        PhyOrder.storeCode = Store_Code;
                        PhyOrder.billNo = "";
                        PhyOrder.date = DateTime.Now.ToString("dd MMM yyyy hh:mm:ss");
                        PhyOrder.customerName  = CustomerName;
                        PhyOrder.customerMobile = Buy.CustomerMobile;
                        PhyOrder.amount = TotalAmount;
                        PhyOrder.paymentCollected = "No";
                        PhyOrder.tender = "ER Phygital";
                        PhyOrder.deleveryType = "Store Delivery";
                        PhyOrder.address = Buy.CustomerAddress;

                        string Json = JsonConvert.SerializeObject(PhyOrder);
                        ClientAPIresponse = CommonService.SendApiRequest(ClientAPIURL+ "api/ShoppingBag/AddOrderinPhygital", Json);

                        if(!string.IsNullOrEmpty(ClientAPIresponse))
                        {
                            var Response = JsonConvert.DeserializeObject<Dictionary<string, string>>(ClientAPIresponse);

                            if (Response["statusCode"].Equals("200"))
                            {
                                Result = 1;
                            }

                        }
                    }
                }
                else
                {
                    Result = 1;
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

            return Result;
        }

        /// <summary>
        /// Buy Products On Chat from shopping Bag/WishList
        /// <param name="ChatCustomerBuyModel"></param>
        /// <param name="ClientAPIURL"></param>
        /// </summary>
        /// <returns></returns>
        public int SendProductsOnChat(SendProductsToCustomer ProductDetails, string ClientAPIURL)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            int Result = 0;
            ClientCustomSendProductModel Details = new ClientCustomSendProductModel();
            string ClientAPIResponse = string.Empty;
            string Message = string.Empty;
            string CTAMessage = string.Empty;
            try
            {
               

                if(ProductDetails.Products.Count > 0)
                {
                    foreach(CustomerChatProductModel obj in ProductDetails.Products)
                    {

                        #region call client api for sending message to customer

                        CTAMessage += !string.IsNullOrEmpty(obj.uniqueItemCode) ? obj.uniqueItemCode : " " + ",";
                        CTAMessage += !string.IsNullOrEmpty(obj.productName) ? obj.productName : " " + ",";
                        CTAMessage += !string.IsNullOrEmpty(obj.categoryName) ? obj.categoryName : " " + ",";
                        CTAMessage += !string.IsNullOrEmpty(obj.subCategoryName) ? obj.subCategoryName : " " + ",";
                        CTAMessage += !string.IsNullOrEmpty(obj.brandName) ? obj.brandName : " " + ",";
                        CTAMessage += !string.IsNullOrEmpty(obj.price) ? obj.price : " " + ",";
                        CTAMessage += !string.IsNullOrEmpty(obj.color) ? obj.color : " " + ",";
                        CTAMessage += !string.IsNullOrEmpty(obj.size) ? obj.size : " " + ",";
                        CTAMessage += !string.IsNullOrEmpty(obj.imageURL) ? obj.imageURL : " " + ",";


                        Details.to = ProductDetails.CustomerMobile.Length > 10 ? ProductDetails.CustomerMobile : "91" + ProductDetails.CustomerMobile;
                        Details.textToReply = Message;
                        Details.programCode = ProductDetails.ProgramCode;
                        Details.imageUrl = obj.imageURL;
                        Details.shoppingBag = obj.IsCard ? "0" : obj.IsShoppingBag ? "1" : "0";
                        Details.like = obj.IsCard ? "0" : obj.IsWishList ? "1" : "0";

                        string JsonRequest = JsonConvert.SerializeObject(Details);

                        ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendCtaImage", JsonRequest);

                        #endregion

                        if(!obj.IsCard)
                        {
                            //string Starthtml = "<div class=\"card-body position-relative\"><div class=\"row\" style=\"margin: 0px; align-items: flex-end;\"><div class=\"col-md-2\">";
                            string Starthtml = "<div class=\"card-body position-relative\"><div class=\"row\" style=\"margin: 0px;\"><div class=\"col-md-2\">";
                            string Midhtml = "<div class=\"col-md-10 bkcprdt\">";
                            var Endhtml = "</div></div></div>";

                            Starthtml += !string.IsNullOrEmpty(obj.imageURL) ? "<img class=\"chat-product-img\" src=\"" + obj.imageURL + "\" alt=\"Product Image\" ></div>" : "</div>";
                            Starthtml += Midhtml;

                            Starthtml += !string.IsNullOrEmpty(obj.brandName) ? "<div><label class=\"chat-product-name\">Brand : " + obj.brandName + "</label></div>" : "";
                            Starthtml += !string.IsNullOrEmpty(obj.categoryName) ? "<div><label class=\"chat-product-code\">Category: " + obj.categoryName + "</label></div>" : "";
                            Starthtml += !string.IsNullOrEmpty(obj.subCategoryName) ? "<div><label class=\"chat-product-code\">SubCategory: " + obj.subCategoryName + "</label></div>" : "";
                            Starthtml += !string.IsNullOrEmpty(obj.color) ? "<div><label class=\"chat-product-code\">Color: " + obj.color + "</label></div>" : "";
                            Starthtml += !string.IsNullOrEmpty(obj.size) ? "<div><label class=\"chat-product-code\">Size: " + obj.size + "</label></div>" : "";
                            Starthtml += !string.IsNullOrEmpty(obj.uniqueItemCode) ? "<div><label class=\"chat-product-code\">Item Code: " + obj.uniqueItemCode + "</label></div>" : "";
                            Starthtml += !string.IsNullOrEmpty(obj.price) ? "<div><label class=\"chat-product-prize\"> Price : " + obj.price + "</label></div>" : "";
                            Starthtml += !string.IsNullOrEmpty(obj.url) ? "<div><a href=\"" + obj.url + "\" target=\"_blank\" class=\"chat-product-url\">" + obj.url + "</a></div>" : "";
                            Starthtml += Endhtml;


                            CustomerChatModel ChatMessageDetails = new CustomerChatModel();
                            ChatMessageDetails.ChatID = ProductDetails.ChatID;
                            ChatMessageDetails.Message = Starthtml;
                            ChatMessageDetails.ByCustomer = false;
                            ChatMessageDetails.ChatStatus = 0;
                            ChatMessageDetails.StoreManagerId = ProductDetails.UserID;
                            ChatMessageDetails.CreatedBy = ProductDetails.UserID;

                            Result = Result + SaveChatMessages(ChatMessageDetails);

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
                
            }

            return Result;
        }

        #region Client Exposed API

        /// <summary>
        /// Add Products To ShoppingBag by Customer
        /// <param name="CustomerAddToShoppingBag"></param>
        /// </summary>
        /// <returns></returns>
        public int CustomerAddToShoppingBag(ClientChatAddProduct Item)
        {
            MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed) 
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSCustomerAddToBagOrWishList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_ProgramCode", Item.ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerMobile", Item.CustomerMobile);
                cmd.Parameters.AddWithValue("@_ItemCode", string.IsNullOrEmpty(Item.ProductDetails.uniqueItemCode) ? "" : Item.ProductDetails.uniqueItemCode);
                cmd.Parameters.AddWithValue("@_ItemName", string.IsNullOrEmpty(Item.ProductDetails.productName) ? "" : Item.ProductDetails.productName);
                cmd.Parameters.AddWithValue("@_Category", string.IsNullOrEmpty(Item.ProductDetails.categoryName) ? "" : Item.ProductDetails.categoryName);
                cmd.Parameters.AddWithValue("@_SubCategory", string.IsNullOrEmpty(Item.ProductDetails.subCategoryName) ? "" : Item.ProductDetails.subCategoryName);
                cmd.Parameters.AddWithValue("@_Brand", string.IsNullOrEmpty(Item.ProductDetails.brandName) ? "" : Item.ProductDetails.brandName);
                cmd.Parameters.AddWithValue("@_Color", string.IsNullOrEmpty(Item.ProductDetails.color) ? "" : Item.ProductDetails.color);
                cmd.Parameters.AddWithValue("@_ColorCode", string.IsNullOrEmpty(Item.ProductDetails.colorCode) ? "" : Item.ProductDetails.colorCode);
                cmd.Parameters.AddWithValue("@_Size", string.IsNullOrEmpty(Item.ProductDetails.size) ? "" : Item.ProductDetails.size);
                cmd.Parameters.AddWithValue("@_Price", string.IsNullOrEmpty(Item.ProductDetails.price) ? "" : Item.ProductDetails.price);
                cmd.Parameters.AddWithValue("@_Discount", string.IsNullOrEmpty(Item.ProductDetails.discount) ? "" : Item.ProductDetails.discount);
                cmd.Parameters.AddWithValue("@_Url", string.IsNullOrEmpty(Item.ProductDetails.url) ? "" : Item.ProductDetails.url);
                cmd.Parameters.AddWithValue("@_ImageURL", string.IsNullOrEmpty(Item.ProductDetails.imageURL) ? "" : Item.ProductDetails.imageURL);
                cmd.Parameters.AddWithValue("@_Action", "shoppingbag");

                cmd.CommandType = CommandType.StoredProcedure;

                Result = Convert.ToInt32(cmd.ExecuteScalar());


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

            return Result;
        }

        /// <summary>
        /// Add Products To ShoppingBag by Customer
        /// <param name="CustomerAddToShoppingBag"></param>
        /// </summary>
        /// <returns></returns>
        public int CustomerAddToWishlist(ClientChatAddProduct Item)
        {
            MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSCustomerAddToBagOrWishList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_ProgramCode", Item.ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerMobile",  Item.CustomerMobile);
                cmd.Parameters.AddWithValue("@_ItemCode", string.IsNullOrEmpty(Item.ProductDetails.uniqueItemCode) ? "" : Item.ProductDetails.uniqueItemCode);
                cmd.Parameters.AddWithValue("@_ItemName", string.IsNullOrEmpty(Item.ProductDetails.productName) ? "" : Item.ProductDetails.productName);
                cmd.Parameters.AddWithValue("@_Category", string.IsNullOrEmpty(Item.ProductDetails.categoryName) ? "" : Item.ProductDetails.categoryName);
                cmd.Parameters.AddWithValue("@_SubCategory", string.IsNullOrEmpty(Item.ProductDetails.subCategoryName) ? "" : Item.ProductDetails.subCategoryName);
                cmd.Parameters.AddWithValue("@_Brand", string.IsNullOrEmpty(Item.ProductDetails.brandName) ? "" : Item.ProductDetails.brandName);
                cmd.Parameters.AddWithValue("@_Color", string.IsNullOrEmpty(Item.ProductDetails.color) ? "" : Item.ProductDetails.color);
                cmd.Parameters.AddWithValue("@_ColorCode", string.IsNullOrEmpty(Item.ProductDetails.colorCode) ? "" : Item.ProductDetails.colorCode);
                cmd.Parameters.AddWithValue("@_Size", string.IsNullOrEmpty(Item.ProductDetails.size) ? "" : Item.ProductDetails.size);
                cmd.Parameters.AddWithValue("@_Price", string.IsNullOrEmpty(Item.ProductDetails.price) ? "" : Item.ProductDetails.price);
                cmd.Parameters.AddWithValue("@_Discount", string.IsNullOrEmpty(Item.ProductDetails.discount) ? "" : Item.ProductDetails.discount);
                cmd.Parameters.AddWithValue("@_Url", string.IsNullOrEmpty(Item.ProductDetails.url) ? "" : Item.ProductDetails.url);
                cmd.Parameters.AddWithValue("@_ImageURL", string.IsNullOrEmpty(Item.ProductDetails.imageURL) ? "" : Item.ProductDetails.imageURL);
                cmd.Parameters.AddWithValue("@_Action", "wishlist");

                cmd.CommandType = CommandType.StoredProcedure;

                Result = Convert.ToInt32(cmd.ExecuteScalar());


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

            return Result;
        }

        /// <summary>
        /// Remove Products To Wishlist/ShoppingBag by Customer
        /// <param name="ProgramCode"></param>
        /// <param name="CustomerMobile"></param>
        /// <param name="StoreCode"></param>
        /// <param name="ItemCode"></param>
        /// </summary>
        /// <returns></returns>
        public int CustomerRemoveProduct(string ProgramCode, string CustomerMobile, string RemoveFrom, string ItemCode)
        {
            MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSRemoveProductByCustomer", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerMobile", CustomerMobile);
                cmd.Parameters.AddWithValue("@_RemoveFrom", RemoveFrom.ToLower());
                cmd.Parameters.AddWithValue("@_ItemCode", ItemCode);

                cmd.CommandType = CommandType.StoredProcedure;

                Result = Convert.ToInt32(cmd.ExecuteScalar());


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

            return Result;
        }

        #endregion
    }
}
