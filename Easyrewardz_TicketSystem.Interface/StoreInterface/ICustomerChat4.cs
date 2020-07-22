﻿using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {
        ChatCustomerProfileModel GetChatCustomerProfileDetails(int TenantId, string ProgramCode, int CustomerID, int UserID, string ClientAPIURL);

        List<CustomerChatProductModel> GetChatCustomerProducts(int TenantId, string ProgramCode, int CustomerID, string MobileNo);

        int RemoveProduct(int TenantId, string ProgramCode, int CustomerID,string CustomerMobile, string ItemCode);

        int AddProductsToShoppingBag(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, bool IsFromRecommendation,int UserID);

        int AddProductsToWishlist(int TenantId, string ProgramCode, int CustomerID, string CustomerMobile, string ItemCodes, bool IsFromRecommendation, int UserID);


        int BuyProductsOnChat(ChatCustomerBuyModel Buy, string ClientAPIURL);


        #region Client Exposed API

        int CustomerAddToShoppingBag(ClientChatAddProduct Item);

        int CustomerAddToWishlist(ClientChatAddProduct Item);

        int CustomerRemoveProduct(string ProgramCode, string CustomerMobile, string StoreCode, string ItemCode);

        #endregion
    }
}