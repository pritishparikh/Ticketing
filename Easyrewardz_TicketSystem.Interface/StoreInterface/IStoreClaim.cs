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
        /// Get list of the claim comments
        /// </summary>
        /// <param name="ClaimId">Id of the Claim</param>
        /// <returns></returns>
        List<UserComment> GetClaimComment(int ClaimID);
        List<CustomTaskMasterDetails> GetClaimList();
    }
}
