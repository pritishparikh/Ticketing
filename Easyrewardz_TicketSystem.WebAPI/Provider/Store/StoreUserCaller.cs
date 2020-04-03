using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreUserCaller
    {
        #region Variable
        public IStoreUser _StoreUserRepository;
        #endregion

        public int StoreUserPersonaldetail(IStoreUser Users, CustomStoreUserModel  storeUserModel)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.AddStoreUserPersonaldetail(storeUserModel);
        }
        public int StoreUserProfiledetail(IStoreUser Users, CustomStoreUserModel storeUserModel)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.AddStoreUserProfiledetail(storeUserModel);
        }
        public int StoreUserMapping(IStoreUser Users, CustomStoreUser storeUser)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.StoreUserMappedCategory(storeUser);
        }
        public int EditStoreUser(IStoreUser Users, CustomStoreUserEdit storeUser)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.EditStoreUser(storeUser);
        }
    }
}
