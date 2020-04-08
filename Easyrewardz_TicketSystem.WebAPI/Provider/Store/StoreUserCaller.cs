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

        #region Store User

        public int CreateStoreUserMapping(IStoreUser Users, StoreClaimCategory storeUser)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.AddStoreUserMappedCategory(storeUser);
        }

      //  public int EditStoreUser(IStoreUser Users, CustomStoreUserEdit storeUser)
        //{
        //    _StoreUserRepository = Users;
        //    return _StoreUserRepository.EditStoreUser(storeUser);
        //}

        

        public int CreateStoreUserPersonaldetail(IStoreUser Users, StoreUserPersonalDetails personalDetails)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.AddStoreUserPersonalDetails(personalDetails);
        }

        public int CreateStoreUserProfiledetail(IStoreUser Users,int tenantID, int userID, int BrandID, int storeID, int departmentId, string functionIDs, int designationID, int reporteeID, int CreatedBy)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.AddStoreUserProfileDetails(tenantID, userID, BrandID,  storeID,  departmentId,  functionIDs,  designationID,  reporteeID,  CreatedBy);
        }


        public int DeleteStoreUser(IStoreUser Users, int tenantID, int UserId, bool IsStoreUser, int ModifiedBy)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.DeleteStoreUser( tenantID,  UserId,  IsStoreUser,  ModifiedBy);
        }

        public int ModifyStoreUser(IStoreUser Users, StoreUserDetailsModel userdetails)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.UpdateStoreUser(userdetails);
        }

        public List<StoreUserListing> GetStoreUserList(IStoreUser Users, int tenantID)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.GetStoreUserList(tenantID);
        }

        #region Profile Mapping

        public List<StoreUserDepartmentList> GetDepartmentByBrandStore(IStoreUser Users,int BrandID, int storeID)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.BindDepartmentByBrandStore(BrandID, storeID);  
        }


        public List<DesignationMaster> GetStoreReporteeDesignation(IStoreUser Users, int DesignationID,  int TenantID)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.BindStoreReporteeDesignation(DesignationID,  TenantID);
        }

        public List<CustomSearchTicketAgent> GetStoreReportToUser(IStoreUser Users, int DesignationID, bool IsStoreUser,int TenantID)
        {
            _StoreUserRepository = Users;
            return _StoreUserRepository.BindStoreReportToUser(DesignationID, IsStoreUser,TenantID);
        }


        #endregion


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
