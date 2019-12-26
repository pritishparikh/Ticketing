using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{

    public class KnowledgeCaller
    {
        #region Variable
        public IKnowledge _KnowledgeRepository;
        #endregion
        /// <summary>
        /// Search By Category
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<KnowlegeBaseMaster> SearchByCategory(IKnowledge Knowledge, int type_ID, int Category_ID, int SubCategory_ID)
        {
            _KnowledgeRepository = Knowledge;
            return _KnowledgeRepository.SearchByCategory(type_ID, Category_ID, SubCategory_ID);
        }
    }
}
