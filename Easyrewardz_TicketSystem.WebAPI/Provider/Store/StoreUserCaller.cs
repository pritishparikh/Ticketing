using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreUserCaller
    {
        #region Variable
        public IStoreUser _StoreUserRepository;
        #endregion

        //public int StoreUserPersonaldetail(IStoreUser Users, CustomStoreUserModel  storeUserModel)
        //{
        //    _StoreUserRepository = Users;
        //    return _StoreUserRepository.AddStoreUserPersonaldetail(storeUserModel);
        //}
        //public int StoreUserProfiledetail(IStoreUser Users, CustomStoreUserModel storeUserModel)
        //{
        //    _StoreUserRepository = Users;
        //    return _StoreUserRepository.AddStoreUserProfiledetail(storeUserModel);
        //}
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

        #region Store User

        public int CreateStoreUserPersonaldetail(IStoreUser Users, StoreUserPersonalDetails personalDetails)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.AddStoreUserPersonalDetail(personalDetails);
        }

        public int CreateStoreUserProfiledetail(IStoreUser Users,int tenantID, int userID, int BrandID, int storeID, int departmentId, string functionIDs, int designationID, int reporteeID, int CreatedBy)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.AddStoreUserProfileDetails(tenantID, userID, BrandID,  storeID,  departmentId,  functionIDs,  designationID,  reporteeID,  CreatedBy);
        }




        #region Claim CategoryMapping

        public List<StoreClaimCategoryModel> GetClaimCategoryListByBrandID(IStoreUser Users, int tenantID, string BrandIDs)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.GetClaimCategoryListByBrandID(tenantID,  BrandIDs);
        }

        public List<StoreClaimSubCategoryModel> GetClaimSubCategoryByCategoryID(IStoreUser Users, int tenantID, string CategoryIDs)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.GetClaimSubCategoryByCategoryID(tenantID,  CategoryIDs);
        }

        public List<StoreClaimIssueTypeModel> GetClaimIssueTypeListBySubCategoryID(IStoreUser Users, int tenantID, string SubCategoryIDs)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.GetClaimIssueTypeListBySubCategoryID(tenantID,  SubCategoryIDs);
        }

        #endregion

        #endregion
    }
}
