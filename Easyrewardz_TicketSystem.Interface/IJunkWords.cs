using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IJunkWords
    {
        /// <summary>
        /// Insert Junk Words
        /// </summary>
        /// <param name="junkWordsMaster"></param>
        /// <returns></returns>
        int InsertJunkWords(JunkWordsMaster junkWordsMaster);

        /// <summary>
        /// Update Junk Words
        /// </summary>
        /// <param name="junkWordsMaster"></param>
        /// <returns></returns>
        int UpdateJunkWords(JunkWordsMaster junkWordsMaster);

        /// <summary>
        /// Delete Junk Words
        /// </summary>
        /// <param name="junkKeywordID"></param>
        /// <param name="userMasterID"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        int DeleteJunkWords(int junkKeywordID, int userMasterID, int tenantId);

        /// <summary>
        /// List Junk Words
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        List<JunkWordsMaster> ListJunkWords(int tenantId);

    }
}
