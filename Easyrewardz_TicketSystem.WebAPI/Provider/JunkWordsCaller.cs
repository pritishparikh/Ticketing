using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class JunkWordsCaller
    {
        #region Variable declaration

        private IJunkWords _junkWords;
        #endregion

        #region Methods 
        public int InsertJunkWords(IJunkWords junkWords, JunkWordsMaster junkWordsMaster)
        {
            _junkWords = junkWords;

            return _junkWords.InsertJunkWords(junkWordsMaster);
        }
        public int UpdateJunkWords(IJunkWords junkWords, JunkWordsMaster junkWordsMaster)
        {
            _junkWords = junkWords;

            return _junkWords.UpdateJunkWords(junkWordsMaster);
        }
        public int DeleteJunkWords(IJunkWords junkWords, int blockEmailID, int UserMasterID, int TenantId)
        {
            _junkWords = junkWords;

            return _junkWords.DeleteJunkWords(blockEmailID, UserMasterID, TenantId);
        }
        public List<JunkWordsMaster> GetJunkWords(IJunkWords junkWords, int TenantId)
        {
            _junkWords = junkWords;

            return _junkWords.ListJunkWords(TenantId);
        }
        #endregion
    }
}
