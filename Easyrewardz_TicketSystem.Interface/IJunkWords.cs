using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IJunkWords
    {
        int InsertJunkWords(JunkWordsMaster junkWordsMaster);
        int UpdateJunkWords(JunkWordsMaster junkWordsMaster);
        int DeleteJunkWords(int junkKeywordID, int userMasterID, int tenantId);
        List<JunkWordsMaster> ListJunkWords(int tenantId);

    }
}
