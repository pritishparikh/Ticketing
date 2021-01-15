using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreClaim
    {
        /// <summary>
        /// Raise Claim
        /// </summary>
        /// <param name="storeClaimMaster"></param>
        /// <param name="finalAttchment"></param>
        /// <returns></returns>
        int RaiseClaim(StoreClaimMaster storeClaimMaster, string finalAttchment);
        /// <summary>
        /// Add Claim Comment
        /// </summary> 
        /// <param name="claimID"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        /// <param name="oldAssignID"></param>
        /// <param name="newAssignID"></param>
        /// <returns></returns>
        int AddClaimComment(int ClaimID, string comment, int userID, int oldAssignID, int newAssignID, bool iSTicketingComment);
        /// <summary>
        /// Get Claim Comment
        /// </summary>
        /// <param name="claimID"></param>
        /// <returns></returns>
        List<UserComment> GetClaimComment(int claimID);
        /// <summary>
        /// Get Claim List
        /// </summary>
        /// <param name="tabFor"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<CustomClaimList> GetClaimList(int tabFor, int tenantID, int userID);
        /// <summary>
        /// Get Claim By ID
        /// </summary>
        /// <param name="claimID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        CustomClaimByID GetClaimByID(int claimID, int tenantID, int userID,string url);
        /// <summary>
        /// Add Claim Comment By Approvel
        /// </summary>
        /// <param name="claimID"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        /// <param name="iSRejectComment"></param>
        /// <returns></returns>
        int AddClaimCommentByApprovel(int claimID, string comment, int userID, bool iSRejectComment);
        /// <summary>
        /// Get Claim Comment For Approval
        /// </summary>
        /// <param name="claimID">Id of the Claim</param>
        /// <returns></returns>
        List<CommentByApprovel> GetClaimCommentForApprovel(int claimID);
        /// <summary>
        /// Claim Approve or Reject
        /// </summary>
        /// <param name="claimID"></param>
        /// <param name="finalClaimAsked"></param>
        /// <param name="IsApprove"></param>
        /// <param name="userMasterID"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        int ClaimApprove(int claimID, double finalClaimAsked, bool IsApprove, int userMasterID, int tenantId);
        /// <summary>
        /// Re Assign Claim
        /// </summary>
        /// <param name="claimID"></param>
        /// <param name="assigneeID"></param>
        /// <param name="userMasterID"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        int AssignClaim(int claimID, int assigneeID, int userMasterID, int tenantId);
        /// <summary>
        /// Get Order Detail By ticketID
        /// </summary>
        /// <param name="ticketID"></param>
        ///  <param name="tenantID"></param>
        /// <returns></returns>
        List<CustomOrderwithCustomerDetails> GetOrderDetailByTicketID(int ticketID, int tenantID);
        /// <summary>
        /// User List for Dropdown for reasssgn
        /// </summary>
        /// <param name="storeClaim"></param>
        /// <param name="assignID"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        List<CustomStoreUserList> GetUserList(int assignID, int tenantId);
    }
}
