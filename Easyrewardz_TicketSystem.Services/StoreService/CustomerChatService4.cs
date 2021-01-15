using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public async Task<ChatProfileOrder> ChatCustomerProfileOrderDetails(int TenantId, string ProgramCode, int CustomerID, int UserID)
        {

            ChatProfileOrder CustomerProfile = new ChatProfileOrder();
           // List<string> InsightList = new List<string>();
            //ClientChatProfileModel ClientRequest = new ClientChatProfileModel();
            string ClientAPIResponse = string.Empty; string JsonRequest = string.Empty;
            //CustomerpopupDetails CustomerATVDetails = new CustomerpopupDetails();
            DataTable schemaTable = new DataTable();
            try
            {


                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSChatCustomerProfileOrderDetails", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantId", TenantId);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                        cmd.Parameters.AddWithValue("@_AgentID", UserID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                schemaTable.Load(reader);
                                
                                if(schemaTable.Rows.Count > 0)
                                {
                                    CustomerProfile.CustomerID = CustomerID;
                                    CustomerProfile.CustomerName = schemaTable.Rows[0]["Customer_Name"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["Customer_Name"]).Trim();
                                    CustomerProfile.CustomerMobileNo = schemaTable.Rows[0]["Customer_MobileCC"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["Customer_MobileCC"]);
                                    CustomerProfile.OrderDelivered = schemaTable.Rows[0]["OrderDelivered"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["OrderDelivered"]);
                                    CustomerProfile.OrderShoppingBag = schemaTable.Rows[0]["OrderShoppingBag"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["OrderShoppingBag"]);
                                    CustomerProfile.OrderReadyToShip = schemaTable.Rows[0]["OrderReadyToShip"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["OrderReadyToShip"]);
                                    CustomerProfile.OrderReturns = schemaTable.Rows[0]["OrderReturns"] == DBNull.Value ? 0 : Convert.ToInt32(schemaTable.Rows[0]["OrderReturns"]);

                                   // ClientRequest.mobileNumber = schemaTable.Rows[0]["Customer_Mobile"] == DBNull.Value ? string.Empty : Convert.ToString(schemaTable.Rows[0]["Customer_Mobile"]);

                                }
                                
                            }
                        }
                    }
                }


                #region Other profile details commented

                //**
                /*
              // get ATV details of customer fromclient API
              ClientRequest.programCode = ProgramCode;
              JsonRequest = JsonConvert.SerializeObject(ClientRequest);
              try
              {
                  //ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetUserATVDetails", JsonRequest);
                  ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetUserATVDetails", JsonRequest);
              }
              catch (Exception)
              {
                  ClientAPIResponse = string.Empty;
              }

              if (!string.IsNullOrEmpty(ClientAPIResponse))
              {
                  CustomerATVDetails = JsonConvert.DeserializeObject<CustomerpopupDetails>(ClientAPIResponse);

                  if (CustomerATVDetails != null)
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
                  //ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetKeyInsight", JsonRequest);
                  ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetKeyInsight", JsonRequest);
              }
              catch (Exception)
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
                  //ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetLastTransactionDetails", JsonRequest);
                  ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetLastTransactionDetails", JsonRequest);
              }
              catch (Exception)
              {
                  ClientAPIResponse = string.Empty;
              }
              if (!string.IsNullOrEmpty(ClientAPIResponse))
              {
                  LastTransaction = JsonConvert.DeserializeObject<LastTransactionDetailsResponseModel>(ClientAPIResponse);

                  if (LastTransaction != null)
                  {
                      CustomerProfile.BillNumber = string.IsNullOrEmpty(LastTransaction.billNo) ? "" : LastTransaction.billNo;
                      CustomerProfile.BillAmount = string.IsNullOrEmpty(LastTransaction.amount) ? 0 : Convert.ToDecimal(CustomerATVDetails.lifeTimeValue);
                      CustomerProfile.StoreDetails = string.IsNullOrEmpty(LastTransaction.storeName) ? "" : LastTransaction.storeName;
                      CustomerProfile.TransactionDate = string.IsNullOrEmpty(LastTransaction.billDate) ? "" : Convert.ToDateTime(LastTransaction.billDate).ToString("dd MMM yyyy");
                      CustomerProfile.itemDetails = LastTransaction.itemDetails;
                  }

              }

              //**

  */
                #endregion

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
        public async Task<List<CustomerChatProductModel>> GetChatCustomerProducts(int TenantId, string ProgramCode, int CustomerID, string MobileNo, string ClientAPIURL)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<CustomerChatProductModel> CustomerProducts = new List<CustomerChatProductModel>();
            List<ClientReccomendedProductsModel> RecommendedList = new List<ClientReccomendedProductsModel>();
            List<CustomerChatProductModel> RecommendedCustomerProducts = new List<CustomerChatProductModel>();
            ClientChatProfileModel ClientRequest = new ClientChatProfileModel();

            string ClientAPIResponse = string.Empty;
            string CardItemsIds = string.Empty;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (conn)
                {
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

                    #region api call for getting shopping bag/wishlist products

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
                            #endregion

                        }

                        #region api call for getting recommendations products

                        ClientRequest.mobileNumber = MobileNo.Length.Equals(12) ? MobileNo.Substring(2, MobileNo.Length - 2) : MobileNo;
                        ClientRequest.programCode = ProgramCode;
                        string JsonRequest = JsonConvert.SerializeObject(ClientRequest);
                        
                        //ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetRecommendedList", JsonRequest);

                        ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL, JsonRequest);


                        if (!string.IsNullOrEmpty(ClientAPIResponse))
                        {
                            RecommendedList = JsonConvert.DeserializeObject<List<ClientReccomendedProductsModel>>(ClientAPIResponse);
                            List<string> RecomItemCode = RecommendedList.Select(x => x.itemCode).ToList();

                            if (RecommendedList.Count > 0)
                            {
                                foreach (CustomerChatProductModel custobj in CustomerProducts)
                                {

                                    custobj.IsRecommended = RecomItemCode.Contains(custobj.uniqueItemCode);

                                }

                                RecommendedCustomerProducts = RecommendedList.Where(r => !CustomerProducts.Select(c => c.uniqueItemCode).ToList().Contains(r.itemCode))
                                    .Select(x => new CustomerChatProductModel()
                                    {
                                        uniqueItemCode = string.IsNullOrEmpty(x.itemCode) ? string.Empty : x.itemCode,
                                        categoryName = string.IsNullOrEmpty(x.category) ? string.Empty : x.category,
                                        subCategoryName = string.IsNullOrEmpty(x.subCategory) ? string.Empty : x.subCategory,
                                        brandName = string.IsNullOrEmpty(x.brand) ? string.Empty : x.brand,
                                        color = string.IsNullOrEmpty(x.color) ? string.Empty : x.color,
                                        size = string.IsNullOrEmpty(x.size) ? string.Empty : x.size,
                                        price = string.IsNullOrEmpty(x.price) ? string.Empty : x.price,
                                        url = string.IsNullOrEmpty(x.url) ? string.Empty : x.url,
                                        imageURL = string.IsNullOrEmpty(x.imageURL) ? string.Empty : x.imageURL,
                                        IsShoppingBag = false,
                                        IsWishList = false,
                                        IsRecommended = true
                                    }).ToList();

                                if (RecommendedCustomerProducts.Count > 0)
                                    CustomerProducts.AddRange(RecommendedCustomerProducts);

                            }
                        }
                        #endregion

                        #region commented code for  item enable/disable requirement removal

                        /*
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
                        */
                        #endregion
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
        public async Task<int> RemoveProduct(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCode, string RemoveFrom, int UserID)
        {
            //MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {

                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSRemoveProduct", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                        cmd.Parameters.AddWithValue("@_MobileNo", CustomerMobile);
                        cmd.Parameters.AddWithValue("@_RemoveFrom", RemoveFrom.ToLower());

                        cmd.Parameters.AddWithValue("@_ItemCode", ItemCode);
                        cmd.Parameters.AddWithValue("@_UserID", UserID);

                        cmd.CommandType = CommandType.StoredProcedure;

                        Result = Convert.ToInt32(cmd.ExecuteScalar());
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

            return Result;
        }

        /// <summary>
        /// Add Products To ShoppingBag Or WishList
        /// <param name="tenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="CustomerID"></param>
        /// <param name="MobileNo"></param>
        /// <param name="ItemCodes"></param>
        /// <param name="Action"></param>
        /// <param name="UserID"></param>
        /// </summary>
        /// <returns></returns>
        public async Task<int> AddProductsToBagOrWishlist(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, string Action, int UserID)
        {
            // MySqlCommand cmd = new MySqlCommand();
            int Result = 0;
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSAddProductsToBagOrWishList", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                        cmd.Parameters.AddWithValue("@_MobileNo", CustomerMobile);
                        cmd.Parameters.AddWithValue("@_ItemCode", string.IsNullOrEmpty(ItemCodes) ? "" : ItemCodes.TrimEnd(','));
                        cmd.Parameters.AddWithValue("@_Action", string.IsNullOrEmpty(Action) ? "" : Action.ToLower());
                        cmd.Parameters.AddWithValue("@User_ID", UserID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        Result = Convert.ToInt32(cmd.ExecuteScalar());
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

            return Result;
        }

        public async Task<int> AddRecommendationToWishlist(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, string ClientAPIURL, int UserID)
        {
            //MySqlCommand cmd = new MySqlCommand();
            int Result = 0;
            string XmlText = string.Empty;
            XDocument RecommendedProducts = null;
            List<ClientReccomendedProductsModel> RecommendedList = new List<ClientReccomendedProductsModel>();
            string ClientAPIResponse = string.Empty;
            ClientChatProfileModel ClientRequest = new ClientChatProfileModel();
            List<string> ItemCodesList = new List<string>();
            try
            {
                using (conn)
                {
                    ItemCodesList = ItemCodes.TrimEnd(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    if (ItemCodesList.Count > 0)
                    {
                        #region to get Product details from recommendation api 


                        ClientRequest.mobileNumber = CustomerMobile.Length.Equals(12) ? CustomerMobile.Substring(2, CustomerMobile.Length - 2) : CustomerMobile;
                        ClientRequest.programCode = ProgramCode;
                        string JsonRequest = JsonConvert.SerializeObject(ClientRequest);
                      
                       // ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetRecommendedList", JsonRequest);

                        ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL, JsonRequest);

                        if (!string.IsNullOrEmpty(ClientAPIResponse))
                        {
                            RecommendedList = JsonConvert.DeserializeObject<List<ClientReccomendedProductsModel>>(ClientAPIResponse)
                                .Where(x => ItemCodesList.Contains(x.itemCode)).ToList();
                        }


                        #endregion
                    }
                    if (RecommendedList.Count > 0)
                    {
                        RecommendedProducts = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
                        new XElement("Recommendation",
                        from Products in RecommendedList
                        select new XElement("Products",
                        new XElement("itemCode", string.IsNullOrEmpty(Products.itemCode) ? "" : Products.itemCode),
                         new XElement("category", string.IsNullOrEmpty(Products.category) ? "" : Products.category),
                        new XElement("subCategory", string.IsNullOrEmpty(Products.subCategory) ? "" : Products.subCategory),
                         new XElement("brand", string.IsNullOrEmpty(Products.brand) ? "" : Products.brand),
                        new XElement("color", string.IsNullOrEmpty(Products.color) ? "" : Products.color),
                         new XElement("size", string.IsNullOrEmpty(Products.size) ? "" : Products.size),
                        new XElement("price", string.IsNullOrEmpty(Products.price) ? "" : Products.price),
                         new XElement("url", string.IsNullOrEmpty(Products.url) ? "" : Products.url),
                        new XElement("imageURL", string.IsNullOrEmpty(Products.imageURL) ? "" : Products.imageURL)
                        )));

                        XmlText = RecommendedProducts.ToString();
                    }

                    using (MySqlCommand cmd = new MySqlCommand
                           ("SP_HSAddRecommendationToWishlist", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                        cmd.Parameters.AddWithValue("@_MobileNo", CustomerMobile);
                        cmd.Parameters.AddWithValue("@_XmlItems", string.IsNullOrEmpty(XmlText) ? "" : XmlText);
                        cmd.Parameters.AddWithValue("@User_ID", UserID);

                        cmd.CommandType = CommandType.StoredProcedure;

                        Result = Convert.ToInt32(cmd.ExecuteScalar());
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

            return Result;
        }

        /// <summary>
        /// Buy Products On Chat from shopping Bag/WishList
        /// <param name="ChatCustomerBuyModel"></param>
        /// <param name="ClientAPIURL"></param>
        /// </summary>
        /// <returns></returns>
        public int BuyProductsOnChat(ChatCustomerBuyModel Buy, string ClientAPIURL)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            int Result = 0;
            string ClientAPIresponse = string.Empty;
            PhyAddOrderModel PhyOrder = new PhyAddOrderModel();
            List<ItemDetail> ItemDetails = new List<ItemDetail>();
            ClientChatProfileModel ClientRequest = new ClientChatProfileModel();
            List<ClientReccomendedProductsModel> RecommendedList = new List<ClientReccomendedProductsModel>();
            List<string> ItemCodesList = new List<string>();
            string Store_Code = string.Empty; string CustomerName = string.Empty;
            double TotalAmount = 0;
            try
            {
                ItemCodesList = Buy.ItemCodes.TrimEnd(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();


                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (conn)
                {

                    cmd = new MySqlCommand("SP_HSBuyProductsOnChat", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@_TenantID", Buy.TenantID);
                    cmd.Parameters.AddWithValue("@_ProgramCode", Buy.ProgramCode);
                    cmd.Parameters.AddWithValue("@_CustomerID", Buy.CustomerID);
                    cmd.Parameters.AddWithValue("@_MobileNo", Buy.CustomerMobile);
                    cmd.Parameters.AddWithValue("@_ItemCode", string.IsNullOrEmpty(Buy.ItemCodes) ? "" : Buy.ItemCodes.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@_IsDirectBuy", Convert.ToInt16(Buy.IsDirectBuy));
                    cmd.Parameters.AddWithValue("@_IsFromRecommendation", Convert.ToInt16(Buy.IsFromRecommendation));
                    cmd.Parameters.AddWithValue("@User_ID", Buy.UserID);

                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);


                    if (!Buy.IsFromRecommendation)
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
                                TotalAmount = ds.Tables[1].Rows[0]["TotalAmount"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[0]["TotalAmount"]);
                                Store_Code = ds.Tables[1].Rows[0]["Store_Code"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["Store_Code"]);
                                CustomerName = ds.Tables[1].Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["CustomerName"]);
                            }

                        }
                    }
                    else
                    {
                        if (ds != null && ds.Tables != null)
                        {
                            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                            {
                                Store_Code = ds.Tables[0].Rows[0]["Store_Code"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Store_Code"]);
                                CustomerName = ds.Tables[0].Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                            }
                        }


                        ClientRequest.mobileNumber = Buy.CustomerMobile.Length.Equals(12) ? Buy.CustomerMobile.Substring(2, Buy.CustomerMobile.Length - 2) : Buy.CustomerMobile;
                        ClientRequest.programCode = Buy.ProgramCode;
                        string JsonRequest = JsonConvert.SerializeObject(ClientRequest);
                        ClientAPIresponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetRecommendedList", JsonRequest);

                        if (!string.IsNullOrEmpty(ClientAPIresponse))
                        {
                            RecommendedList = JsonConvert.DeserializeObject<List<ClientReccomendedProductsModel>>(ClientAPIresponse)
                                .Where(x => ItemCodesList.Contains(x.itemCode)).ToList();

                            if (RecommendedList.Count > 0)
                            {
                                foreach (ClientReccomendedProductsModel recom in RecommendedList)
                                {

                                    TotalAmount = TotalAmount + (string.IsNullOrEmpty(recom.price) ? 0 : Convert.ToDouble(recom.price));

                                    ItemDetail item = new ItemDetail()
                                    {
                                        itemID = string.IsNullOrEmpty(recom.itemCode) ? string.Empty : recom.itemCode,
                                        itemName = string.Empty,// no item name recieved from API
                                        itemPrice = string.IsNullOrEmpty(recom.price) ? string.Empty : recom.price,
                                        quantity = "1",
                                        category = string.IsNullOrEmpty(recom.category) ? string.Empty : recom.category
                                    };

                                    ItemDetails.Add(item);
                                }
                            }
                        }

                    }

                    if (ItemDetails.Count > 0)
                    {
                        PhyOrder.itemDetails = ItemDetails;
                        PhyOrder.source = "BELL";
                        PhyOrder.progCode = Buy.ProgramCode;
                        PhyOrder.storeCode = Store_Code;
                        PhyOrder.billNo = "bell" + Store_Code + Convert.ToString(new Random().Next(0, 50));
                        PhyOrder.date = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                        PhyOrder.customerName = CustomerName;
                        PhyOrder.customerMobile = Buy.CustomerMobile;
                        PhyOrder.amount = TotalAmount;
                        PhyOrder.paymentCollected = "No";
                        PhyOrder.tender = "ER Phygital";
                        PhyOrder.tenderDetails = new List<TenderDetail>() { new TenderDetail() { tenderID = "1", tenderCode = "ER Phygital", tenderValue = Convert.ToString(TotalAmount) } };
                        PhyOrder.deleveryType = "Store Delivery";
                        PhyOrder.address = Buy.CustomerAddress;

                        string Json = JsonConvert.SerializeObject(PhyOrder);
                        ClientAPIresponse = CommonService.SendApiRequest(ClientAPIURL + "api/ShoppingBag/AddOrderinPhygital", Json);

                        if (!string.IsNullOrEmpty(ClientAPIresponse))
                        {
                            var Response = JsonConvert.DeserializeObject<Dictionary<string, string>>(ClientAPIresponse);

                            if (Response["statusCode"].Equals("200"))
                            {
                                Result = 1;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return Result;
        }


        /// <summary> 
        /// Buy Products On Chat from shopping Bag/WishList New API
        /// <param name="ChatCustomerBuyModel"></param>
        /// <param name="ClientAPIURL"></param>
        /// </summary>
        /// <returns></returns>
        public async Task<int> BuyProductsOnChatNew(ChatCustomerBuyModel Buy, string ClientAPIURL,string Recommended, string AddOrderinPhygital)
        {

            int Result = 0;
            string ClientAPIresponse = string.Empty;
            DataTable Datable1 = new DataTable(); DataTable Datable2 = new DataTable();
            PhyAddOrderModel PhyOrder = new PhyAddOrderModel();
            List<ItemDetail> ItemDetails = new List<ItemDetail>();
            ClientChatProfileModel ClientRequest = new ClientChatProfileModel();
            List<ClientReccomendedProductsModel> RecommendedList = new List<ClientReccomendedProductsModel>();
            List<string> ItemCodesList = new List<string>();
            string Store_Code = string.Empty; 
            double TotalAmount = 0;
            try
            {
                ItemCodesList = Buy.ItemCodes.TrimEnd(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();


                if (conn != null && conn.State == ConnectionState.Closed)
                {
                   await  conn.OpenAsync();
                }
                using (conn)
                {

                    using (MySqlCommand cmd = new MySqlCommand("SP_HSBuyProductsOnChat_New", conn))
                    {
                        cmd.Parameters.AddWithValue("@_TenantID", Buy.TenantID);
                        cmd.Parameters.AddWithValue("@_ProgramCode", Buy.ProgramCode);
                        cmd.Parameters.AddWithValue("@_CustomerID", Buy.CustomerID);
                        cmd.Parameters.AddWithValue("@_MobileNo", Buy.CustomerMobile);
                        cmd.Parameters.AddWithValue("@_ItemCode", string.IsNullOrEmpty(Buy.ItemCodes) ? "" : Buy.ItemCodes.TrimEnd(','));
                        cmd.Parameters.AddWithValue("@_IsFromRecommendation", Convert.ToInt16(Buy.IsFromRecommendation));
                        cmd.Parameters.AddWithValue("@User_ID", Buy.UserID);
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                Datable1.Load(reader);
                                if(!Buy.IsFromRecommendation)
                                {
                                    Datable2.Load(reader);
                                }
                            }

                            if (!Buy.IsFromRecommendation)
                            {
                                if (Datable1 != null && Datable1.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in Datable1.Rows)
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

                                if (Datable2 != null && Datable2.Rows.Count > 0)
                                {
                                    TotalAmount = Datable2.Rows[0]["TotalAmount"] == DBNull.Value ? 0 : Convert.ToDouble(Datable2.Rows[0]["TotalAmount"]);
                                    Store_Code = Datable2.Rows[0]["Store_Code"] == DBNull.Value ? string.Empty : Convert.ToString(Datable2.Rows[0]["Store_Code"]);
                                   
                                }
                            }
                            else
                            {

                                if (Datable1 != null && Datable1.Rows.Count > 0)
                                {
                                    Store_Code = Datable1.Rows[0]["Store_Code"] == DBNull.Value ? string.Empty : Convert.ToString(Datable1.Rows[0]["Store_Code"]);
                                  
                                }

                                ClientRequest.mobileNumber = Buy.CustomerMobile.Length.Equals(12) ? Buy.CustomerMobile.Substring(2, Buy.CustomerMobile.Length - 2) : Buy.CustomerMobile;
                                ClientRequest.programCode = Buy.ProgramCode;
                                string JsonRequest = JsonConvert.SerializeObject(ClientRequest);
                                // ClientAPIresponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetRecommendedList", JsonRequest);
                                 
                               // ClientAPIresponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/GetRecommendedList", JsonRequest); 

                               ClientAPIresponse = await APICall.SendApiRequest(ClientAPIURL + Recommended, JsonRequest);
                                if (!string.IsNullOrEmpty(ClientAPIresponse))
                                {
                                    RecommendedList = JsonConvert.DeserializeObject<List<ClientReccomendedProductsModel>>(ClientAPIresponse)
                                        .Where(x => ItemCodesList.Contains(x.itemCode)).ToList();

                                    if (RecommendedList.Count > 0)
                                    {
                                        foreach (ClientReccomendedProductsModel recom in RecommendedList)
                                        {

                                            TotalAmount = TotalAmount + (string.IsNullOrEmpty(recom.price) ? 0 : Convert.ToDouble(recom.price));

                                            ItemDetail item = new ItemDetail()
                                            {
                                                itemID = string.IsNullOrEmpty(recom.itemCode) ? string.Empty : recom.itemCode,
                                                itemName = string.Empty,// no item name recieved from API
                                                itemPrice = string.IsNullOrEmpty(recom.price) ? string.Empty : recom.price,
                                                quantity = "1",
                                                category = string.IsNullOrEmpty(recom.category) ? string.Empty : recom.category
                                            };

                                            ItemDetails.Add(item);
                                        }
                                    }
                                }
                            }
                        }


                    }
                    

                    if (ItemDetails.Count > 0)
                    {
                        PhyOrder.itemDetails = ItemDetails;
                        PhyOrder.source = "BELL";
                        PhyOrder.progCode = Buy.ProgramCode;
                        PhyOrder.storeCode = Store_Code;
                        PhyOrder.billNo = "bell" + Store_Code + Convert.ToString(new Random().Next(0, 50));
                        PhyOrder.date = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                        PhyOrder.customerName = Buy.CustomerName;
                        PhyOrder.customerMobile = Buy.CustomerMobile;
                        PhyOrder.amount = TotalAmount;
                        PhyOrder.paymentCollected = "No";
                        PhyOrder.tender = "ER Phygital";
                        PhyOrder.tenderDetails = new List<TenderDetail>() { new TenderDetail() { tenderID = "1", tenderCode = "ER Phygital", tenderValue = Convert.ToString(TotalAmount) } };
                        PhyOrder.deleveryType = "Store Delivery";
                        PhyOrder.address = Buy.CustomerAddress;

                        string Json = JsonConvert.SerializeObject(PhyOrder);

                        
                        ClientAPIresponse = await APICall.SendApiRequest(ClientAPIURL + AddOrderinPhygital, Json);  

                        if (!string.IsNullOrEmpty(ClientAPIresponse))
                        {
                            var Response = JsonConvert.DeserializeObject<Dictionary<string, string>>(ClientAPIresponse);

                            if (Response["statusCode"].Equals("200"))
                            {
                                Result = 1;
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
                if (Datable1 != null)
                {
                    Datable1.Dispose();
                }
                if (Datable2 != null)
                {
                    Datable2.Dispose();
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
        public async Task<int> SendProductsOnChat(SendProductsToCustomer ProductDetails, string ClientAPIURL)
        {
            int Result = 0;
            ClientCustomSendProductModel Details = new ClientCustomSendProductModel();
            string ClientAPIResponse = string.Empty;
            string Message = string.Empty;
            string CTAMessage = string.Empty;
            ChatSessionModel ChatSetting = new ChatSessionModel();
            List<CustomerChatProductModel> CustomerProducts = new List<CustomerChatProductModel>();
            bool ProductEnabled = false;
            try
            {


                if (ProductDetails.Products.Count > 0)
                {

                    ChatSetting = await GetChatSession(ProductDetails.TenantID, ProductDetails.ProgramCode);

                    if (ChatSetting != null)
                    {
                        ProductEnabled = ChatSetting.CustomerProduct;
                    }

                    if (ProductDetails.Products[0].IsCard)
                    {
                        CustomerProducts = await GetChatCustomerProducts(ProductDetails.TenantID, ProductDetails.ProgramCode, ProductDetails.CustomerID, ProductDetails.CustomerMobile, ClientAPIURL);

                    }

                    foreach (CustomerChatProductModel obj in ProductDetails.Products)
                    {

                        #region call client api for sending message to customer
                        CTAMessage = string.Empty;
                        if (ProductEnabled)
                        {

                            CTAMessage += !string.IsNullOrEmpty(obj.uniqueItemCode) ? obj.uniqueItemCode + "," : "N/A,";
                            CTAMessage += !string.IsNullOrEmpty(obj.productName) ? obj.productName + "," : "N/A,";
                            CTAMessage += !string.IsNullOrEmpty(obj.categoryName) ? obj.categoryName + "," : "N/A,";
                            CTAMessage += !string.IsNullOrEmpty(obj.subCategoryName) ? obj.subCategoryName + "," : "N/A,";
                            CTAMessage += !string.IsNullOrEmpty(obj.brandName) ? obj.brandName + "," : "N/A,";
                            CTAMessage += !string.IsNullOrEmpty(obj.price) ? obj.price + "," : "N/A,";
                            CTAMessage += !string.IsNullOrEmpty(obj.color) ? obj.color + "," : "N/A,";
                            CTAMessage += !string.IsNullOrEmpty(obj.size) ? obj.size + "," : "N/A,";
                            CTAMessage += !string.IsNullOrEmpty(obj.imageURL) ? obj.imageURL + "," : "N/A";

                            if (obj.IsCard)
                            {
                                Details.shoppingBag = CustomerProducts.Where(x => x.uniqueItemCode.Equals(obj.uniqueItemCode) && x.IsShoppingBag).ToList().Count() > 0 ? "1" : "0";
                                Details.like = CustomerProducts.Where(x => x.uniqueItemCode.Equals(obj.uniqueItemCode) && x.IsWishList).ToList().Count() > 0 ? "1" : "0";
                            }
                            else
                            {
                                Details.shoppingBag = obj.IsShoppingBag ? "1" : "0";
                                Details.like = obj.IsWishList ? "1" : "0";
                            }
                        }
                        else
                        {

                            CTAMessage += "ItemCode:" + (!string.IsNullOrEmpty(obj.uniqueItemCode) ? (obj.uniqueItemCode + ",") : " ,");
                            CTAMessage += "ItemName:" + (!string.IsNullOrEmpty(obj.productName) ? obj.productName + "," : " ,");
                            CTAMessage += "Category:" + (!string.IsNullOrEmpty(obj.categoryName) ? obj.categoryName + "," : " ,");
                            CTAMessage += "Subcategory:" + (!string.IsNullOrEmpty(obj.subCategoryName) ? obj.subCategoryName + "," : " ,");
                            CTAMessage += "Brand:" + (!string.IsNullOrEmpty(obj.brandName) ? obj.brandName + "," : " ,");
                            CTAMessage += "Price:" + (!string.IsNullOrEmpty(obj.price) ? obj.price + "," : " ,");
                            CTAMessage += "Color:" + (!string.IsNullOrEmpty(obj.color) ? obj.color + "," : " ,");
                            CTAMessage += "Size:" + (!string.IsNullOrEmpty(obj.size) ? obj.size + "," : " ,");
                            CTAMessage += "Image:" + (!string.IsNullOrEmpty(obj.imageURL) ? obj.imageURL + "," : "");


                            Details.shoppingBag = null;
                            Details.like = null;
                        }

                        Details.to = ProductDetails.CustomerMobile.Length > 10 ? ProductDetails.CustomerMobile : "91" + ProductDetails.CustomerMobile;
                        Details.textToReply = CTAMessage;
                        Details.programCode = ProductDetails.ProgramCode;
                        Details.imageUrl = obj.imageURL;


                        string JsonRequest = JsonConvert.SerializeObject(Details);

                        ClientAPIResponse = CommonService.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendCtaImage", JsonRequest);

                        #endregion

                        if (!obj.IsCard && ClientAPIResponse.Equals("true"))
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

        public async Task<int> SendProductsOnChatNew(SendProductsToCustomer ProductDetails, string ClientAPIURL, string SendImageMessage, string Recommended)
        {
            int Result = 0;
            ClientCustomSendProductModelNew Details = new ClientCustomSendProductModelNew();
            string ClientAPIResponse = string.Empty;
            string Message = string.Empty;
            string CTAMessage = string.Empty;
            ChatSessionModel ChatSetting = new ChatSessionModel();
            List<CustomerChatProductModel> CustomerProducts = new List<CustomerChatProductModel>();
            bool ProductEnabled = false;

            StringBuilder CTABuilder = new StringBuilder();
            StringBuilder HtmlBuilder = new StringBuilder();
            try
            {
                if (ProductDetails.Products.Count > 0)
                {
                    ChatSetting = await GetChatSession(ProductDetails.TenantID, ProductDetails.ProgramCode);

                    if (ChatSetting != null)
                    {
                        ProductEnabled = ChatSetting.CustomerProduct;
                    }
                    // if (ProductDetails.Source.Equals("cb") )
                    // {
                    if (ProductDetails.Products[0].IsCard)
                    {
                        CustomerProducts = await GetChatCustomerProducts(ProductDetails.TenantID, ProductDetails.ProgramCode,
                            ProductDetails.CustomerID, ProductDetails.CustomerMobile, ClientAPIURL + Recommended);
                    }
                  //  }
                    foreach (CustomerChatProductModel obj in ProductDetails.Products)
                    {
                       // if (ProductDetails.Source.Equals("cb"))
                      //  {
                            #region call client api for sending message to customer

                            CTABuilder = new StringBuilder();
                        HtmlBuilder = new StringBuilder();
                        if (ProductEnabled)
                        {
                           

                            if (ProductDetails.Source == "wb")
                            {
                                CTABuilder.Append(obj.uniqueItemCode);
                            }
                            else
                            {
                                CTABuilder.Append(!string.IsNullOrEmpty(obj.uniqueItemCode) ? obj.uniqueItemCode + "," : "N/A,");
                                CTABuilder.Append(!string.IsNullOrEmpty(obj.productName) ? obj.productName + "," : "N/A,");
                                CTABuilder.Append(!string.IsNullOrEmpty(obj.categoryName) ? obj.categoryName + "," : "N/A,");
                                CTABuilder.Append(!string.IsNullOrEmpty(obj.subCategoryName) ? obj.subCategoryName + "," : "N/A,");
                                CTABuilder.Append(!string.IsNullOrEmpty(obj.brandName) ? obj.brandName + "," : "N/A,");
                                CTABuilder.Append(!string.IsNullOrEmpty(obj.price) ? obj.price + "," : "N/A,");
                                CTABuilder.Append(!string.IsNullOrEmpty(obj.color) ? obj.color + "," : "N/A,");
                                CTABuilder.Append(!string.IsNullOrEmpty(obj.size) ? obj.size + "," : "N/A,");
                                CTABuilder.Append(!string.IsNullOrEmpty(obj.imageURL) ? obj.imageURL + "," : "N/A");
                            }


                            if (obj.IsCard)
                            {
                                Details.shoppingBag = CustomerProducts.Where(x => x.uniqueItemCode.Equals(obj.uniqueItemCode) && x.IsShoppingBag).ToList().Count() > 0 ? "1" : "0";
                                Details.like = CustomerProducts.Where(x => x.uniqueItemCode.Equals(obj.uniqueItemCode) && x.IsWishList).ToList().Count() > 0 ? "1" : "0";
                            }
                            else
                            {
                                Details.shoppingBag = obj.IsShoppingBag ? "1" : "0";
                                Details.like = obj.IsWishList ? "1" : "0";
                            }
                        }
                        else
                        {
                           
                            if (ProductDetails.Source == "wb")
                            {
                                CTABuilder.Append(obj.uniqueItemCode);
                            }
                            else
                            {
                                CTABuilder.Append("ItemCode:" + (!string.IsNullOrEmpty(obj.uniqueItemCode) ? (obj.uniqueItemCode + ",") : " ,"));
                                CTABuilder.Append("ItemName:" + (!string.IsNullOrEmpty(obj.productName) ? obj.productName + "," : " ,"));
                                CTABuilder.Append("Category:" + (!string.IsNullOrEmpty(obj.categoryName) ? obj.categoryName + "," : " ,"));
                                CTABuilder.Append("Subcategory:" + (!string.IsNullOrEmpty(obj.subCategoryName) ? obj.subCategoryName + "," : " ,"));
                                CTABuilder.Append("Brand:" + (!string.IsNullOrEmpty(obj.brandName) ? obj.brandName + "," : " ,"));
                                CTABuilder.Append("Price:" + (!string.IsNullOrEmpty(obj.price) ? obj.price + "," : " ,"));
                                CTABuilder.Append("Color:" + (!string.IsNullOrEmpty(obj.color) ? obj.color + "," : " ,"));
                                CTABuilder.Append("Size:" + (!string.IsNullOrEmpty(obj.size) ? obj.size + "," : " ,"));
                                CTABuilder.Append("Image:" + (!string.IsNullOrEmpty(obj.imageURL) ? obj.imageURL + "," : ""));
                            }

                            Details.shoppingBag = "";
                            Details.like = "";
                        }

                        Details.to = ProductDetails.CustomerMobile.Length > 10 ? ProductDetails.CustomerMobile : "91" + ProductDetails.CustomerMobile;
                        Details.textToReply = CTABuilder.ToString();
                        Details.programCode = ProductDetails.ProgramCode;
                        Details.imageUrl = obj.imageURL;
                        Details.source = ProductDetails.Source;


                        string JsonRequest = JsonConvert.SerializeObject(Details);

                       
                       // ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL + "api/ChatbotBell/SendImageMessage", JsonRequest);


                        ClientAPIResponse = await APICall.SendApiRequest(ClientAPIURL +SendImageMessage, JsonRequest);

                            #endregion
                      //  }
                      //  else
                      //  {
                      //      ClientAPIResponse = "true";
                      //  }

                        if (!obj.IsCard && ClientAPIResponse.Equals("true"))
                        {
                            //string Starthtml = "<div class=\"card-body position-relative\"><div class=\"row\" style=\"margin: 0px; align-items: flex-end;\"><div class=\"col-md-2\">";
                            string Starthtml = "<div class=\"card-body position-relative\"><div class=\"row\" style=\"margin: 0px;\"><div class=\"col-md-2\">";
                            string Midhtml = "<div class=\"col-md-10 bkcprdt\">";
                            string Endhtml = "</div></div></div>";

                            HtmlBuilder.Append(Starthtml);
                            HtmlBuilder.Append(!string.IsNullOrEmpty(obj.imageURL) ? "<img class=\"chat-product-img\" src=\"" + obj.imageURL + "\" alt=\"Product Image\" ></div>" : "</div>");
                            HtmlBuilder.Append( Midhtml);
                            HtmlBuilder.Append(!string.IsNullOrEmpty(obj.brandName) ? "<div><label class=\"chat-product-name\">Brand : " + obj.brandName + "</label></div>" : "");
                            HtmlBuilder.Append(!string.IsNullOrEmpty(obj.categoryName) ? "<div><label class=\"chat-product-code\">Category: " + obj.categoryName + "</label></div>" : "");
                            HtmlBuilder.Append(!string.IsNullOrEmpty(obj.subCategoryName) ? "<div><label class=\"chat-product-code\">SubCategory: " + obj.subCategoryName + "</label></div>" : "");
                            HtmlBuilder.Append(!string.IsNullOrEmpty(obj.color) ? "<div><label class=\"chat-product-code\">Color: " + obj.color + "</label></div>" : "");
                            HtmlBuilder.Append(!string.IsNullOrEmpty(obj.size) ? "<div><label class=\"chat-product-code\">Size: " + obj.size + "</label></div>" : "");
                            HtmlBuilder.Append(!string.IsNullOrEmpty(obj.uniqueItemCode) ? "<div><label class=\"chat-product-code\">Item Code: " + obj.uniqueItemCode + "</label></div>" : "");
                            HtmlBuilder.Append(!string.IsNullOrEmpty(obj.price) ? "<div><label class=\"chat-product-prize\"> Price : " + obj.price + "</label></div>" : "");
                            HtmlBuilder.Append(!string.IsNullOrEmpty(obj.url) ? "<div><a href=\"" + obj.url + "\" target=\"_blank\" class=\"chat-product-url\">" + obj.url + "</a></div>" : "");
                            HtmlBuilder.Append(Endhtml);

                        }
                        if (obj.IsCard  && ClientAPIResponse.Equals("true"))
                        {
                               HtmlBuilder.Append(obj.CardHtmlContent);
                               Result = Result + 1;
                        }

                        CustomerChatModel ChatMessageDetails = new CustomerChatModel();
                        ChatMessageDetails.ChatID = ProductDetails.ChatID;
                        ChatMessageDetails.Message = HtmlBuilder.ToString();
                        ChatMessageDetails.ByCustomer = false;
                        ChatMessageDetails.ChatStatus = 0;
                        ChatMessageDetails.StoreManagerId = ProductDetails.UserID;
                        ChatMessageDetails.CreatedBy = ProductDetails.UserID;

                        //Thread threadSaveMessage = new Thread(() => SaveChatMessages(ChatMessageDetails));
                        //threadSaveMessage.IsBackground = true;
                        //threadSaveMessage.Start();

                        Result = Result + SaveChatMessages(ChatMessageDetails);
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
        public async Task<int> CustomerAddToShoppingBag(ClientChatAddProduct Item)
        {
            //MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                       ("SP_HSCustomerAddToBagOrWishList", conn))
                    {
                        await conn.OpenAsync();
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

            return Result;
        }

        /// <summary>
        /// Add Products To ShoppingBag by Customer
        /// <param name="CustomerAddToShoppingBag"></param>
        /// </summary>
        /// <returns></returns>
        public async Task<int> CustomerAddToWishlist(ClientChatAddProduct Item)
        {
           // MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                       ("SP_HSCustomerAddToBagOrWishList", conn))
                    {
                        await conn.OpenAsync();
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
                        cmd.Parameters.AddWithValue("@_Action", "wishlist");

                        cmd.CommandType = CommandType.StoredProcedure;

                        Result = Convert.ToInt32(cmd.ExecuteScalar());
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
        public async Task<int> CustomerRemoveProduct(string ProgramCode, string CustomerMobile, string RemoveFrom, string ItemCode)
        {
            //MySqlCommand cmd = new MySqlCommand();
            int Result = 0;
            try
            {
                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand
                       ("SP_HSRemoveProductByCustomer", conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                        cmd.Parameters.AddWithValue("@_CustomerMobile", CustomerMobile);
                        cmd.Parameters.AddWithValue("@_RemoveFrom", RemoveFrom.ToLower());
                        cmd.Parameters.AddWithValue("@_ItemCode", ItemCode);

                        cmd.CommandType = CommandType.StoredProcedure;

                        Result = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                //        if (conn != null && conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                //using (conn)
                //{
                //    cmd = new MySqlCommand("SP_HSRemoveProductByCustomer", conn);
                //    cmd.Connection = conn;
                //    cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                //    cmd.Parameters.AddWithValue("@_CustomerMobile", CustomerMobile);
                //    cmd.Parameters.AddWithValue("@_RemoveFrom", RemoveFrom.ToLower());
                //    cmd.Parameters.AddWithValue("@_ItemCode", ItemCode);

                //    cmd.CommandType = CommandType.StoredProcedure;

                //    Result = Convert.ToInt32(cmd.ExecuteScalar());
                //}

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
