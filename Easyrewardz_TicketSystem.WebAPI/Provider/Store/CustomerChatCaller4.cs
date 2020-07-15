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


        public int AddProductsToShoppingBag(ICustomerChat customerChat, int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes)
        {
            _customerChat = customerChat;
            return _customerChat.AddProductsToShoppingBag(TenantId, ProgramCode, CustomerID, CustomerMobile, ItemCodes);
        }


        public int AddProductsToWishlist(ICustomerChat customerChat, int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes)
        {
            _customerChat = customerChat;
            return _customerChat.AddProductsToWishlist(TenantId, ProgramCode, CustomerID, CustomerMobile, ItemCodes);
        }
        #endregion
    }
}
