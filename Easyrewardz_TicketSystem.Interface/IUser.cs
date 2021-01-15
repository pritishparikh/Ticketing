using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IUser
    {
        /// <summary>
        /// Get User List
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<User> GetUserList(int TenantId ,int UserID);

        /// <summary>
        /// Add User Personal detail
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        int AddUserPersonaldetail(UserModel userModel);

        /// <summary>
        /// Edit User Personal detail
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        int EditUserPersonaldetail(UserModel userModel);

        /// <summary>
        /// Add User Profile detail
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="ReportTo"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="IsStoreUser"></param>
        /// <returns></returns>
        int AddUserProfiledetail(int DesignationID,int ReportTo ,int CreatedBy,int TenantID,int UserID,int IsStoreUser);

        /// <summary>
        /// Delete Profile Picture
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="isStoreUser"></param>
        /// <returns></returns>
        int DeleteProfilePicture(int tenantID, int userID, int isStoreUser);

        /// <summary>
        /// Mapped category
        /// </summary>
        /// <param name="customUserModel"></param>
        /// <returns></returns>
        int Mappedcategory(CustomUserModel customUserModel);

        /// <summary>
        /// Edit User
        /// </summary>
        /// <param name="customEditUserModel"></param>
        /// <returns></returns>
        int EditUser(CustomEditUserModel customEditUserModel);

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="TenantID"></param>
        /// <param name="Modifyby"></param>
        /// <param name="IsStoreUser"></param>
        /// <returns></returns>
        int DeleteUser(int userID,int TenantID, int Modifyby,int IsStoreUser);

        /// <summary>
        /// User List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="IsStoreUser"></param>
        /// <returns></returns>
        List<CustomUserList> UserList(int TenantID,int IsStoreUser);

        /// <summary>
        /// Get user Details By Id
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TenantID"></param>
        /// <param name="IsStoreUser"></param>
        /// <returns></returns>
        CustomUserList GetuserDetailsById(int UserID,int TenantID,int IsStoreUser);

        /// <summary>
        /// Bulk Upload User
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="UserFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> BulkUploadUser(int TenantID, int CreatedBy, int UserFor, DataSet DataSetCSV);

        /// <summary>
        /// Update User Profile Detail
        /// </summary>
        /// <param name="UpdateUserProfiledetailsModel"></param>
        /// <returns></returns>
        int UpdateUserProfileDetail(UpdateUserProfiledetailsModel UpdateUserProfiledetailsModel);

        /// <summary>
        /// Get User Profile Details
        /// </summary>
        /// <param name="UserMasterID"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        List<UpdateUserProfiledetailsModel> GetUserProfileDetails(int UserMasterID,string url);

        /// <summary>
        /// Send Mail for change password
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="TenantID"></param>
        /// <param name="IsStoreUser"></param>
        /// <returns></returns>
        CustomChangePassword SendMailforchangepassword(int userID, int TenantID, int IsStoreUser);

        /// <summary>
        /// validate User Exist
        /// </summary>
        /// <param name="UserEmailID"></param>
        /// <param name="UserMobile"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        string validateUserExist(string UserEmailID, string UserMobile, int TenantId);
    }
}
