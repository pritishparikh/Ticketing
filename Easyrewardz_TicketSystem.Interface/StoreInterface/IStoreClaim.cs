using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreClaim
    {
        int RaiseClaim(StoreClaimMaster storeClaimMaster, string finalAttchment);
        int AddClaimComment(int ClaimID, string Comment, int UserID, int oldAssignID, int newAssignID);
        /// <summary>
        /// Get list of the claim comments from store
        /// </summary>
        /// <param name="ClaimId">Id of the Claim</param>
        /// <returns></returns>
        List<UserComment> GetClaimComment(int ClaimID);
        List<CustomClaimList> GetClaimList(int tabFor, int tenantID, int userID);
        /// <summary>
        /// Get Claim By ID
        /// </summary>
        /// <param name="ClaimID"></param>
        /// <returns></returns>
        CustomClaimByID GetClaimByID(int ClaimID, int tenantID, int userID,string url);
        /// <summary>
        /// Store Claim Comment By Approvel
        /// </summary>
        /// <param name="CommentForId"></param>
        ///    <param name="ID"></param>
        ///   <param name="Comment"></param>
        /// <returns></returns>
        int AddClaimCommentByApprovel(int claimID, string comment, int UserID, bool iSRejectComment);
        /// <summary>
        /// GetClaimCommentForApprovel
        /// </summary>
        /// <param name="ClaimId">Id of the Claim</param>
        /// <returns></returns>
        List<CommentByApprovel> GetClaimCommentForApprovel(int ClaimID);
        /// <summary>
        /// Claim Approve Or Reject
        /// </summary>
        /// <param name="claimID"></param>
        ///    <param name="finalClaimAsked"></param>
        ///   <param name="IsApprove"></param>
        /// <returns></returns>
        int ClaimApprove(int claimID, double finalClaimAsked, bool IsApprove, int userMasterID, int tenantId);
        /// <summary>
        /// Claim Re Assign
        /// </summary>
        /// <param name=""></param>
        ///    <param name="claimID"></param>
        ///   <param name="Comment"></param>
        /// <returns></returns>
        int AssignClaim(int claimID, int assigneeID, int userMasterID, int tenantId);
        /// <summary>
        /// Get Order and Customer Detail By TicketID
        /// </summary>
        /// <param name=""></param>
        ///    <param name="claimID"></param>
        ///   <param name="Comment"></param>
        /// <returns></returns>
        List<CustomOrderwithCustomerDetails> GetOrderDetailByTicketID(int TicketID, int TenantID);
        List<CustomStoreUserList> GetUserList(int TenantID, int TaskID);
    }
}
