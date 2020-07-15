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

        public int AddProductsToShoppingBag(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes)
        {
            MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_AddProductsToBagOrWishList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@_MobileNo", CustomerMobile);
                cmd.Parameters.AddWithValue("@_ItemCode", string.IsNullOrEmpty(ItemCodes) ? "" : ItemCodes);
                cmd.Parameters.AddWithValue("@_Action","shoppingbag");

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

        public int AddProductsToWishlist(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes)
        {
            MySqlCommand cmd = new MySqlCommand();
            int Result = 0;

            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_AddProductsToBagOrWishList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@_MobileNo", CustomerMobile);
                cmd.Parameters.AddWithValue("@_ItemCode", string.IsNullOrEmpty(ItemCodes) ? "" : ItemCodes);
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
    }
}
