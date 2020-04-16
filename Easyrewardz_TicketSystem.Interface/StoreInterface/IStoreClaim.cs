using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreClaim
    {
        int RaiseClaim(StoreClaimMaster storeClaimMaster, string finalAttchment);
        int AddClaimComment(int ClaimID, string Comment, int UserID);
        /// <summary>
        /// Get list of the claim comments from store
        /// </summary>
        /// <param name="ClaimId">Id of the Claim</param>
        /// <returns></returns>
        List<UserComment> GetClaimComment(int ClaimID);
        List<CustomClaimList> GetClaimList(int tabFor, int tenantID, int userID);
        CustomClaimByID GetClaimByID(int ClaimID, int tenantID, int userID,string url);
        int AddClaimCommentByApprovel(int ClaimID, string Comment, int UserID);
        /// <summary>
        /// GetClaimCommentForApprovel
        /// </summary>
        /// <param name="ClaimId">Id of the Claim</param>
        /// <returns></returns>
        List<CommentByApprovel> GetClaimCommentForApprovel(int ClaimID);
        int ClaimApprove(int claimID, double finalClaimAsked, bool IsApprove, int userMasterID, int tenantId);
        int AssignClaim(int claimID, int assigneeID, int userMasterID, int tenantId);
    }
}
