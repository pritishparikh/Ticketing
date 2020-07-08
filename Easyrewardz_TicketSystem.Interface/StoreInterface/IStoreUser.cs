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



        //int EditStoreUser(CustomStoreUserEdit customStoreUserEdit);

        /// <summary>
        /// Add Store User Personal Details
        /// </summary>
        /// <param name="personalDetails"></param>
        /// <returns></returns>
        int AddStoreUserPersonalDetails(StoreUserPersonalDetails personalDetails);

        /// <summary>
        /// Add Store User Profile Details
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="BrandID"></param>
        /// <param name="storeID"></param>
        /// <param name="departmentId"></param>
        /// <param name="functionIDs"></param>
        /// <param name="designationID"></param>
        /// <param name="reporteeID"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        int AddStoreUserProfileDetails(int tenantID,int userID, int BrandID, int storeID, int departmentId, string functionIDs, int designationID, int reporteeID, int CreatedBy);

        /// <summary>
        /// Add Store User Mapped Category
        /// </summary>
        /// <param name="storeUserModel"></param>
        /// <returns></returns>
        int AddStoreUserMappedCategory(StoreClaimCategory storeUserModel);

        /// <summary>
        /// Delete Store User
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserId"></param>
        /// <param name="IsStoreUser"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        int DeleteStoreUser(int tenantID, int UserId, bool IsStoreUser, int ModifiedBy);

        /// <summary>
        /// Add Brand Store
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="brandID"></param>
        /// <param name="storeID"></param>
        /// <param name="UserMasterID"></param>
        /// <returns></returns>
        int AddBrandStore(int tenantID, int brandID, int storeID, int UserMasterID);

        /// <summary>
        /// Update Brand Store
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="brandID"></param>
        /// <param name="storeID"></param>
        /// <param name="UserMasterID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        int UpdateBrandStore(int tenantID, int brandID, int storeID, int UserMasterID,int userID);

        /// <summary>
        /// Get Store User List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<StoreUserListing> GetStoreUserList(int tenantID);

        /// <summary>
        /// Get Store User On User ID
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        StoreUserListing GetStoreUserOnUserID(int tenantID, int UserID);

        /// <summary>
        /// Get User Profile Details
        /// </summary>
        /// <param name="UserMasterID"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        List<UpdateUserProfiledetailsModel> GetUserProfileDetails(int UserMasterID, string url);

        /// <summary>
        /// Update Store User
        /// </summary>
        /// <param name="userdetails"></param>
        /// <returns></returns>
        int UpdateStoreUser(StoreUserDetailsModel userdetails);

        /// <summary>
        /// Update User Profile Detail
        /// </summary>
        /// <param name="UpdateUserProfiledetailsModel"></param>
        /// <returns></returns>
        int UpdateUserProfileDetail(UpdateUserProfiledetailsModel UpdateUserProfiledetailsModel);

        /// <summary>
        /// Delete Profile Picture
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        int DeleteProfilePicture(int tenantID, int userID);

        /// <summary>
        /// Get Store User Credentails
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="TenantID"></param>
        /// <param name="IsStoreUser"></param>
        /// <returns></returns>
        CustomChangePassword GetStoreUserCredentails(int userID, int TenantID, int IsStoreUser);


        #region Profile Mapping

        /// <summary>
        /// Bind Department By Brand Store
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        List<StoreUserDepartmentList> BindDepartmentByBrandStore(int BrandID, int storeID);

        /// <summary>
        /// Bind Store Reportee Designation
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<DesignationMaster> BindStoreReporteeDesignation(int DesignationID, int TenantID);

        /// <summary>
        /// Bind Store Report To User
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="IsStoreUser"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<CustomSearchTicketAgent> BindStoreReportToUser(int DesignationID, bool IsStoreUser, int TenantID);

        #endregion


        #region Claim Category mapping

        /// <summary>
        /// Get Claim Category List By Brand ID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="BrandIDs"></param>
        /// <returns></returns>
        List<StoreClaimCategoryModel> GetClaimCategoryListByBrandID(int TenantID, string BrandIDs);

        /// <summary>
        /// Get Claim SubCategory By Category ID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CategoryIDs"></param>
        /// <returns></returns>
        List<StoreClaimSubCategoryModel> GetClaimSubCategoryByCategoryID(int TenantID, string CategoryIDs );

        /// <summary>
        /// Get Claim Issue Type List By Sub Category ID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="SubCategoryIDs"></param>
        /// <returns></returns>
        List<StoreClaimIssueTypeModel> GetClaimIssueTypeListBySubCategoryID(int TenantID, string SubCategoryIDs);
        #endregion


        List<StoreUserListing> GetStoreReportUserList(int tenantID, int RegionID, int ZoneID, int UserID);
    }
}
