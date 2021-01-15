using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IKnowledge
    {

        /// <summary>
        /// Search By Category
        /// </summary>
        /// <param name="type_ID"></param>
        /// <param name="Category_ID"></param>
        /// <param name="SubCategory_ID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<KnowlegeBaseMaster> SearchByCategory(int type_ID, int Category_ID, int SubCategory_ID, int TenantId);

        /// <summary>
        /// Add KB
        /// </summary>
        /// <param name="knowlegeBaseMaster"></param>
        /// <returns></returns>
        int AddKB(KnowlegeBaseMaster knowlegeBaseMaster);

        /// <summary>
        /// Update KB
        /// </summary>
        /// <param name="knowlegeBaseMaster"></param>
        /// <returns></returns>
        int UpdateKB(KnowlegeBaseMaster knowlegeBaseMaster);

        /// <summary>
        /// Delete KB
        /// </summary>
        /// <param name="KBID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int DeleteKB(int KBID, int TenantId);

        /// <summary>
        /// Reject Approve KB
        /// </summary>
        /// <param name="knowlegeBaseMaster"></param>
        /// <returns></returns>
        int RejectApproveKB(KnowlegeBaseMaster knowlegeBaseMaster);

        /// <summary>
        /// Search KB
        /// </summary>
        /// <param name="Category_ID"></param>
        /// <param name="SubCategory_ID"></param>
        /// <param name="type_ID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        CustomKBList SearchKB( int Category_ID, int SubCategory_ID, int type_ID, int TenantId);

        /// <summary>
        /// KB List
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        CustomKBList KBList(int TenantId);
    }
}
