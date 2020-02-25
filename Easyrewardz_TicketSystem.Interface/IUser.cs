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
        
        List<User> GetUserList(int TenantId ,int UserID);

        int AddUserPersonaldetail(UserModel userModel);
        int EditUserPersonaldetail(UserModel userModel);

        int AddUserProfiledetail(int DesignationID,int ReportTo ,int CreatedBy,int TenantID,int UserID,int IsStoreUser);

        int Mappedcategory(CustomUserModel customUserModel);

        int EditUser(CustomEditUserModel customEditUserModel);
        int DeleteUser(int userID,int TenantID, int Modifyby,int IsStoreUser);
        List<CustomUserList> UserList(int TenantID,int IsStoreUser);
        CustomUserList GetuserDetailsById(int UserID,int TenantID,int IsStoreUser);

        List<string> BulkUploadUser(int TenantID, int CreatedBy, int UserFor, DataSet DataSetCSV);
        int UpdateUserProfileDetail(UpdateUserProfiledetailsModel UpdateUserProfiledetailsModel);

        List<UpdateUserProfiledetailsModel> GetUserProfileDetails(int UserMasterID,string url);
        CustomChangePassword SendMailforchangepassword(int userID, int TenantID, int IsStoreUser);

        string validateUserExist(string UserEmailID, string UserMobile, int TenantId);
    }
}
