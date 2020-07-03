using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

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
        public ChatCustomerProfileModel GetChatCustomerProfileDetails(int TenantId, string ProgramCode, int CustomerID)
        {
            ChatCustomerProfileModel CustomerProfile = new ChatCustomerProfileModel();
            List<string> InsightList = new List<string>();
            try
            {
                // pass static value as will have to implement client api later

                CustomerProfile.CustomerID = CustomerID;
                CustomerProfile.CustomerName = "Rachel";
                CustomerProfile.CustomerTier = "Gold";
                CustomerProfile.CustomerMobileNo = "919837495849";
                CustomerProfile.CustomerEmailID = "rachelgreen@gmail.com";
                CustomerProfile.TotalPoints = 11278;
                CustomerProfile.LifetimeValue = 211278;
                CustomerProfile.VisitCount = 123;
                CustomerProfile.BillNumber = "CM2012190006034";
                CustomerProfile.BillAmount = 3567;
                CustomerProfile.StoreDetails = "MG Road Shopster";
                CustomerProfile.TransactionDate = "11 May 2020";

                InsightList.Add("Rahel has an ATV Rs.500 in last quarter");
                InsightList.Add("Rachel's favourite product category is Buzzer chappal");
                InsightList.Add("Rachel's basket size is reducing recommended products are: Soarf");

                CustomerProfile.Insights = InsightList;

                //---
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //if (conn != null)
                //{
                //    conn.Close();
                //}
                //if (ds != null)
                //{
                //    ds.Dispose();
                //}
            }

            return CustomerProfile;
        }
    }
}
