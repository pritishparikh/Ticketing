using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IStoreUser
    {
        int AddStoreUserPersonaldetail(CustomStoreUserModel storeUserModel);
        int AddStoreUserProfiledetail(CustomStoreUserModel customStoreUserModel);
        int StoreUserMappedCategory(CustomStoreUser customStoreUserModel);
        int EditStoreUser(CustomStoreUserEdit customStoreUserEdit);

    }
}
