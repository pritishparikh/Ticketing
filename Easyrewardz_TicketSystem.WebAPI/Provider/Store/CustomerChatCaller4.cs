using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class CustomerChatCaller
    {
        #region Methods 

        public async Task<ChatProfileOrder> ChatCustomerProfileOrderDetails(ICustomerChat customerChat, int TenantID, string Programcode, int CustomerID, int UserID)
        {
            _customerChat = customerChat;
            return await _customerChat.ChatCustomerProfileOrderDetails(TenantID, Programcode, CustomerID,  UserID);
        }

        public async Task<List<CustomerChatProductModel>> GetChatCustomerProducts(ICustomerChat customerChat, int TenantID, string Programcode, int CustomerID, string MobileNo, string ClientAPIURL)
        {
            _customerChat = customerChat;
            return await _customerChat.GetChatCustomerProducts(TenantID, Programcode, CustomerID, MobileNo,  ClientAPIURL);
        }

        public async Task<int> RemoveProduct(ICustomerChat customerChat, int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCode, string RemoveFrom, int UserID)
        {
            _customerChat = customerChat;
            return await _customerChat.RemoveProduct( TenantId,  ProgramCode,  CustomerID,  CustomerMobile, ItemCode,  RemoveFrom , UserID);
        }


        public async Task<int> AddProductsToBagOrWishlist(ICustomerChat customerChat, int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, string Action, int UserID)
        {
            _customerChat = customerChat;
            return await _customerChat.AddProductsToBagOrWishlist(TenantId, ProgramCode, CustomerID, CustomerMobile, ItemCodes, Action,  UserID);
        }


        public async Task<int> AddRecommendationToWishlist(ICustomerChat customerChat, int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, string ClientAPIURL ,int UserID)
        {
            _customerChat = customerChat;
            return await _customerChat.AddRecommendationToWishlist(TenantId, ProgramCode, CustomerID, CustomerMobile, ItemCodes , ClientAPIURL ,UserID);
        }

        public int BuyProductsOnChat(ICustomerChat customerChat, ChatCustomerBuyModel Buy, string ClientAPIURL)
        {
            _customerChat = customerChat;
            return _customerChat.BuyProductsOnChat(Buy, ClientAPIURL);
        }

        public async Task<int> BuyProductsOnChatNew(ICustomerChat customerChat, ChatCustomerBuyModel Buy, string ClientAPIURL, string Recommended, string AddOrderinPhygital)
        {
            _customerChat = customerChat;
            return await _customerChat.BuyProductsOnChatNew(Buy, ClientAPIURL, Recommended,  AddOrderinPhygital);
        }

        public async Task<int> SendProductsOnChat(ICustomerChat customerChat, SendProductsToCustomer ProductDetails, string ClientAPIURL)
        {
            _customerChat = customerChat;
            return await _customerChat.SendProductsOnChat(ProductDetails, ClientAPIURL);
        }

        public async Task<int> SendProductsOnChatNew(ICustomerChat customerChat, SendProductsToCustomer ProductDetails, string ClientAPIURL,string  SendImageMessage, string Recommended)
        {
            _customerChat = customerChat;
            return await _customerChat.SendProductsOnChatNew(ProductDetails, ClientAPIURL,   SendImageMessage,  Recommended);
        }


        #region Client Exposed API

        public async Task<int> CustomerAddToShoppingBag(ICustomerChat customerChat, ClientChatAddProduct Item)
        {
            _customerChat = customerChat;
            return await _customerChat.CustomerAddToShoppingBag( Item);
        }


        public async Task<int> CustomerAddToWishlist(ICustomerChat customerChat, ClientChatAddProduct Item)
        {
            _customerChat = customerChat;
            return await _customerChat.CustomerAddToWishlist(Item);
        }


        public async Task<int> CustomerRemoveProduct(ICustomerChat customerChat, string ProgramCode, string CustomerMobile, string RemoveFrom, string ItemCode)
        {
            _customerChat = customerChat;
            return await _customerChat.CustomerRemoveProduct( ProgramCode,  CustomerMobile, RemoveFrom,  ItemCode); 
        }

        #endregion

        #endregion
    }
}
