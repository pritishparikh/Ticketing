using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreUser
    {
        //int AddStoreUserPersonaldetail(CustomStoreUserModel storeUserModel);
        //int AddStoreUserProfiledetail(CustomStoreUserModel customStoreUserModel);


        
        int EditStoreUser(CustomStoreUserEdit customStoreUserEdit);

        int AddStoreUserPersonalDetail(StoreUserPersonalDetails personalDetails);

        int AddStoreUserProfileDetails(int tenantID,int userID, int BrandID, int storeID, int departmentId, string functionIDs, int designationID, int reporteeID, int CreatedBy);

        int AddStoreUserMappedCategory(StoreClaimCategory storeUserModel);

        int DeleteStoreUser(int tenantID, int UserId, bool IsStoreUser, int ModifiedBy);

        #region Profile Mapping

        List<StoreUserDepartmentList> BindDepartmentByBrandStore(int BrandID, int storeID);

        List<DesignationMaster> BindStoreReporteeDesignation(int DesignationID, int TenantID);


        List<CustomSearchTicketAgent> BindStoreReportToUser(int DesignationID, bool IsStoreUser, int TenantID);

        #endregion


        #region Claim Category mapping


        List<StoreClaimCategoryModel> GetClaimCategoryListByBrandID(int TenantID, string BrandIDs);

        List<StoreClaimSubCategoryModel> GetClaimSubCategoryByCategoryID(int TenantID, string CategoryIDs );

        List<StoreClaimIssueTypeModel> GetClaimIssueTypeListBySubCategoryID(int TenantID, string SubCategoryIDs);
        #endregion

    }
}
