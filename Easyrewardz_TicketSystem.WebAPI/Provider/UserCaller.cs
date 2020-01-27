using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class UserCaller
    {
        #region Variable
        public IUser _UserRepository;
        #endregion

        public int AddUserPersonaldetail(IUser User, UserModel userModel,int TenantID)
        {
            _UserRepository = User;
            return _UserRepository.AddUserPersonaldetail(userModel, TenantID);
        }
        public int AddUserProfiledetail(IUser User, int DesignationID, int ReportTo, int CreatedBy, int TenantID, int UserID)
        {
            _UserRepository = User;
            return _UserRepository.AddUserProfiledetail(DesignationID, ReportTo, CreatedBy, TenantID, UserID);
        }
        public int Mappedcategory(IUser User, CustomUserModel customUserModel)
        {
            _UserRepository = User;
            return _UserRepository.Mappedcategory(customUserModel);
        }
        public int EditUserDetail(IUser User, CustomEditUserModel customEditUserModel)
        {
            _UserRepository = User;
            return _UserRepository.EditUser(customEditUserModel);
        }
        public int DeleteUser(IUser User, int userID, int TenantID, int Modifyby)
        {
            _UserRepository = User;
            return _UserRepository.DeleteUser(userID,TenantID, Modifyby);
        }
        public List<CustomUserList> UserList(IUser User,int TenantID)
        {
            _UserRepository = User;
            return _UserRepository.UserList(TenantID);
        }
        public CustomUserList GetuserDetailsById(IUser User, int UserID, int TenantID)
        {
            _UserRepository = User;
            return _UserRepository.GetuserDetailsById(UserID, TenantID);
        }
    }
}
