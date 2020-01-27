using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IUser
    {
        List<User> GetUserList(int TenantId ,int UserID);

        int AddUserPersonaldetail(UserModel userModel,int TenantID);

        int AddUserProfiledetail(int DesignationID,int ReportTo ,int CreatedBy,int TenantID,int UserID);

        int Mappedcategory(CustomUserModel customUserModel);

        int EditUser(CustomEditUserModel customEditUserModel);
        int DeleteUser(int userID,int TenantID, int Modifyby);
        List<CustomUserList> UserList(int TenantID);
        CustomUserList GetuserDetailsById(int UserID,int TenantID);
    }
}
