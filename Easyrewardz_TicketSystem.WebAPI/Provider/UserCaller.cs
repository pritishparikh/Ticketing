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
    }
}
