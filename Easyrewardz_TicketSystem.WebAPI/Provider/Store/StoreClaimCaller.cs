using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreClaimCaller
    {
        #region Variable
        public IStoreClaim _ClaimRepository;
        #endregion

        public int InsertRaiseClaim(IStoreClaim storeClaim, StoreClaimMaster storeClaimMaster,string finalAttchment)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.RaiseClaim(storeClaimMaster, finalAttchment);
        }
        public int AddClaimComment(IStoreClaim storeClaim,int ClaimID, string Comment, int UserID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.AddClaimComment(ClaimID, Comment, UserID);
        }
        public List<UserComment> GetClaimComment(IStoreClaim storeClaim , int ClaimID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetClaimComment(ClaimID);
        }
        public List<CustomClaimList> GetClaimList(IStoreClaim storeClaim,int tabFor,int tenantID,int userID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetClaimList(tabFor, tenantID, userID);
        }
        public CustomClaimByID GetClaimByID(IStoreClaim storeClaim, int ClaimID, int tenantID, int userID,string url)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetClaimByID(ClaimID, tenantID, userID, url);
        }
        public int AddClaimCommentByApprovel(IStoreClaim storeClaim, int ClaimID, string Comment, int UserID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.AddClaimCommentByApprovel(ClaimID, Comment, UserID);
        }
        public List<CommentByApprovel> GetClaimCommentForApprovel(IStoreClaim storeClaim, int ClaimID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetClaimCommentForApprovel(ClaimID);
        }
    }
}
