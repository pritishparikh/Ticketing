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

        public ChatCustomerProfileModel GetChatCustomerProfileDetails(ICustomerChat customerChat, int TenantID, string Programcode, int CustomerID, int UserID, string ClientAPIURL)
        {
            _customerChat = customerChat;
            return _customerChat.GetChatCustomerProfileDetails(TenantID, Programcode, CustomerID,  UserID,  ClientAPIURL);
        }

        public List<CustomerChatProductModel> GetChatCustomerProducts(ICustomerChat customerChat, int TenantID, string Programcode, int CustomerID, string MobileNo)
        {
            _customerChat = customerChat;
            return _customerChat.GetChatCustomerProducts(TenantID, Programcode, CustomerID, MobileNo);
        }

        public int RemoveProduct(ICustomerChat customerChat, int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCode)
        {
            _customerChat = customerChat;
            return _customerChat.RemoveProduct( TenantId,  ProgramCode,  CustomerID,  CustomerMobile, ItemCode);
        }


        public int AddProductsToShoppingBag(ICustomerChat customerChat, int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, bool IsFromRecommendation, int UserID)
        {
            _customerChat = customerChat;
            return _customerChat.AddProductsToShoppingBag(TenantId, ProgramCode, CustomerID, CustomerMobile, ItemCodes,  IsFromRecommendation,  UserID);
        }


        public int AddProductsToWishlist(ICustomerChat customerChat, int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, bool IsFromRecommendation, int UserID)
        {
            _customerChat = customerChat;
            return _customerChat.AddProductsToWishlist(TenantId, ProgramCode, CustomerID, CustomerMobile, ItemCodes, IsFromRecommendation , UserID);
        }

        public int BuyProductsOnChat(ICustomerChat customerChat, ChatCustomerBuyModel Buy, string ClientAPIURL)
        {
            _customerChat = customerChat;
            return _customerChat.BuyProductsOnChat(Buy, ClientAPIURL);
        }

        public int SendProductsOnChat(ICustomerChat customerChat, SendProductsToCustomer ProductDetails, string ClientAPIURL)
        {
            _customerChat = customerChat;
            return _customerChat.SendProductsOnChat(ProductDetails, ClientAPIURL);
        }


        #region Client Exposed API

        public int CustomerAddToShoppingBag(ICustomerChat customerChat, ClientChatAddProduct Item)
        {
            _customerChat = customerChat;
            return _customerChat.CustomerAddToShoppingBag( Item);
        }


        public int CustomerAddToWishlist(ICustomerChat customerChat, ClientChatAddProduct Item)
        {
            _customerChat = customerChat;
            return _customerChat.CustomerAddToWishlist(Item);
        }


        public int CustomerRemoveProduct(ICustomerChat customerChat, string ProgramCode, string CustomerMobile, string RemoveFrom, string ItemCode)
        {
            _customerChat = customerChat;
            return _customerChat.CustomerRemoveProduct( ProgramCode,  CustomerMobile, RemoveFrom,  ItemCode); 
        }

        #endregion

        #endregion
    }
}
