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
        public int ClaimApprove(IStoreClaim storeClaim, int claimID, double finalClaimAsked, bool IsApprove, int userMasterID,int tenantId)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.ClaimApprove(claimID, finalClaimAsked, IsApprove, userMasterID, tenantId);
        }
        public int AssignClaim(IStoreClaim storeClaim, int claimID,int assigneeID, int userMasterID, int tenantId)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.AssignClaim(claimID, assigneeID, userMasterID,tenantId);
        }
        public List<CustomStoreUserList> UserList(IStoreClaim storeClaim, int assignID, int tenantId)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetUserList(assignID, tenantId);
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
        public int AddClaimCommentByApprovel(IStoreClaim storeClaim, int ClaimID, string Comment, int UserID, bool iSRejectComment)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.AddClaimCommentByApprovel(ClaimID, Comment, UserID,iSRejectComment);
        }
        public List<CommentByApprovel> GetClaimCommentForApprovel(IStoreClaim storeClaim, int ClaimID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetClaimCommentForApprovel(ClaimID);
        }
        public List<CustomOrderwithCustomerDetails> GetOrderDetailByticketID(IStoreClaim storeClaim, int TicketID, int TenantID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetOrderDetailByTicketID(TicketID, TenantID);

        }
    }
}
