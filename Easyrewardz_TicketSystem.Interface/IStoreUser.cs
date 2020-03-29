using Easyrewardz_TicketSystem.CustomModel;

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
