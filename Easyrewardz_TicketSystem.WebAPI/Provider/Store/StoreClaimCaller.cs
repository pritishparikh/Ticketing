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
        /// <summary>
        /// InsertRaiseClaim
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="storeClaimMaster"></param>
        /// <param name="finalAttchment"></param>
        /// <returns></returns>
        public int InsertRaiseClaim(IStoreClaim storeClaim, StoreClaimMaster storeClaimMaster,string finalAttchment)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.RaiseClaim(storeClaimMaster, finalAttchment);
        }
        /// <summary>
        /// Add Claim Comment
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="claimID"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        /// <param name="oldAssignID"></param>
        /// <param name="newAssignID"></param>
        /// <returns></returns>
        public int AddClaimComment(IStoreClaim storeClaim, int claimID, string comment, int userID, int oldAssignID, int newAssignID, bool iSTicketingComment)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.AddClaimComment(claimID, comment, userID, oldAssignID, newAssignID, iSTicketingComment);
        }
        /// <summary>
        /// Claim Approve
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="claimID"></param>
        /// <param name="finalClaimAsked"></param>
        /// <param name="IsApprove"></param>
        /// <param name="userMasterID"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public int ClaimApprove(IStoreClaim storeClaim, int claimID, double finalClaimAsked, bool IsApprove, int userMasterID,int tenantId)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.ClaimApprove(claimID, finalClaimAsked, IsApprove, userMasterID, tenantId);
        }
        /// <summary>
        /// Assign Claim
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="claimID"></param>
        /// <param name="assigneeID"></param>
        /// <param name="userMasterID"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public int AssignClaim(IStoreClaim storeClaim, int claimID,int assigneeID, int userMasterID, int tenantId)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.AssignClaim(claimID, assigneeID, userMasterID,tenantId);
        }
        /// <summary>
        /// User List
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="assignID"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<CustomStoreUserList> UserList(IStoreClaim storeClaim, int assignID, int tenantId)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetUserList(assignID, tenantId);
        }
        /// <summary>
        /// Get Claim Comment
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="claimID"></param>
        /// <returns></returns>
        public List<UserComment> GetClaimComment(IStoreClaim storeClaim , int claimID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetClaimComment(claimID);
        }
        /// <summary>
        /// Get Claim List
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="tabFor"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<CustomClaimList> GetClaimList(IStoreClaim storeClaim,int tabFor,int tenantID,int userID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetClaimList(tabFor, tenantID, userID);
        }
        /// <summary>
        /// Get Claim By ID
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="claimID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public CustomClaimByID GetClaimByID(IStoreClaim storeClaim, int claimID, int tenantID, int userID,string url)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetClaimByID(claimID, tenantID, userID, url);
        }
        /// <summary>
        /// Add Claim Comment By Approvel
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="claimID"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        /// <param name="iSRejectComment"></param>
        /// <returns></returns>
        public int AddClaimCommentByApprovel(IStoreClaim storeClaim, int claimID, string comment, int userID, bool iSRejectComment)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.AddClaimCommentByApprovel(claimID, comment, userID, iSRejectComment);
        }
        /// <summary>
        /// Get Claim Comment For Approvel
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="claimID"></param>
        /// <returns></returns>
        public List<CommentByApprovel> GetClaimCommentForApprovel(IStoreClaim storeClaim, int claimID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetClaimCommentForApprovel(claimID);
        }
        /// <summary>
        /// Get Order Detail By ticketID
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="ticketID"></param>
        ///  <param name="tenantID"></param>
        /// <returns></returns>
        public List<CustomOrderwithCustomerDetails> GetOrderDetailByticketID(IStoreClaim storeClaim, int ticketID, int tenantID)
        {
            _ClaimRepository = storeClaim;
            return _ClaimRepository.GetOrderDetailByTicketID(ticketID, tenantID);

        }
    }
}
