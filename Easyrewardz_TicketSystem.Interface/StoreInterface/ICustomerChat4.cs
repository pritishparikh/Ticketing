using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {
        Task<ChatProfileOrder> ChatCustomerProfileOrderDetails(int TenantId, string ProgramCode, int CustomerID, int UserID);

        Task<List<CustomerChatProductModel>> GetChatCustomerProducts(int TenantId, string ProgramCode, int CustomerID, string MobileNo, string ClientAPIURL);

        Task<int> RemoveProduct(int TenantId, string ProgramCode, int CustomerID,string CustomerMobile, string ItemCode,string RemoveFrom, int UserID);

        Task<int> AddProductsToBagOrWishlist(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, string Action, int UserID);

        Task<int> AddRecommendationToWishlist(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, string ClientAPIURL, int UserID);

        int BuyProductsOnChat(ChatCustomerBuyModel Buy, string ClientAPIURL);

        Task<int> BuyProductsOnChatNew(ChatCustomerBuyModel Buy, string ClientAPIURL, string Recommended, string AddOrderinPhygital);

        Task<int> SendProductsOnChat(SendProductsToCustomer ProductDetails, string ClientAPIURL);

        Task<int> SendProductsOnChatNew(SendProductsToCustomer ProductDetails, string ClientAPIURL, string SendImageMessage, string Recommended);

        #region Client Exposed API

        Task<int> CustomerAddToShoppingBag(ClientChatAddProduct Item); 

        Task<int> CustomerAddToWishlist(ClientChatAddProduct Item);

        Task<int> CustomerRemoveProduct(string ProgramCode, string CustomerMobile, string RemoveFrom, string ItemCode);

        #endregion
    }
}
