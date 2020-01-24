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
        public List<KnowlegeBaseMaster> SearchByCategory(IKnowledge Knowledge, int type_ID, int Category_ID, int SubCategory_ID,int TenantId)
        {
            _KnowledgeRepository = Knowledge;
            return _KnowledgeRepository.SearchByCategory(type_ID, Category_ID, SubCategory_ID, TenantId);
        }

        public int AddKB(IKnowledge Knowledge, KnowlegeBaseMaster knowlegeBaseMaster)
        {

            _KnowledgeRepository = Knowledge;
            return _KnowledgeRepository.AddKB(knowlegeBaseMaster);
        }

        public int UpdateKB(IKnowledge Knowledge, KnowlegeBaseMaster knowlegeBaseMaster)
        {
            _KnowledgeRepository = Knowledge;
            return _KnowledgeRepository.UpdateKB(knowlegeBaseMaster);
        }

        public int DeleteKB(IKnowledge Knowledge, int KBID, int TenantId)
        {
            _KnowledgeRepository = Knowledge;
            return _KnowledgeRepository.DeleteKB(KBID, TenantId);
        }

        public int RejectApproveKB(IKnowledge Knowledge, KnowlegeBaseMaster knowlegeBaseMaster)
        {
            _KnowledgeRepository = Knowledge;
            return _KnowledgeRepository.RejectApproveKB(knowlegeBaseMaster);
        }
        public CustomKBList KBList(IKnowledge Knowledge, int TenantId)
        {
            _KnowledgeRepository = Knowledge;
            return _KnowledgeRepository.KBList( TenantId);
        }
    }
}
