using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IBlockEmail
    {
        /// <summary>
        /// Insert Block Email
        /// </summary>
        /// <param name="blockEmailMaster"></param>
        /// <returns></returns>
        int InsertBlockEmail(BlockEmailMaster blockEmailMaster);

        /// <summary>
        /// Update Block Email
        /// </summary>
        /// <param name="blockEmailMaster"></param>
        /// <returns></returns>
        int UpdateBlockEmail(BlockEmailMaster blockEmailMaster);

        /// <summary>
        /// Delete Block Email
        /// </summary>
        /// <param name="blockEmailID"></param>
        /// <param name="UserMasterID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int DeleteBlockEmail(int blockEmailID, int UserMasterID, int TenantId);

        /// <summary>
        /// List Block Email
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<BlockEmailMaster> ListBlockEmail(int TenantId);
    }
}
