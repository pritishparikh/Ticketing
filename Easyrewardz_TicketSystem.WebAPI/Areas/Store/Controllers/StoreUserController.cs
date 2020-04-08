using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    /// <summary>
    /// Store User controller to manage Store Users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreUserController : ControllerBase
    {
        #region  Variable Declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion
        #region Constructor
        /// <summary>
        /// StoreUser Controller
        /// </summary>
        /// <param name="_iConfig"></param>
        public StoreUserController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods 
        /// <summary>
        /// AddStoreUserPersonalDetail
        /// </summary>
        /// <param name="CustomStoreUserModel"></param>
        [HttpPost]
        [Route("AddStoreUserPersonalDetail")]
        public ResponseModel AddStoreUserPersonalDetail([FromBody] StoreUserPersonalDetails storeUser)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();
                storeUser.CreatedBy = authenticate.UserMasterID;
                storeUser.TenantID = authenticate.TenantId;
                int Result = userCaller.CreateStoreUserPersonaldetail(new StoreUserService(_connectioSting), storeUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// AddStoreUserProfileDetail
        /// </summary>
        /// <param name="storeUser"></param>
        [HttpPost]
        [Route("AddStoreUserProfileDetail")]
        public ResponseModel AddStoreUserProfileDetail(int userID, int BrandID, int storeID, int departmentId, string functionIDs, int designationID, int reporteeID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();
              
                int Result = userCaller.CreateStoreUserProfiledetail(new StoreUserService(_connectioSting), authenticate.TenantId,
                     userID,  BrandID,  storeID,  departmentId,  functionIDs,  designationID,  reporteeID, authenticate.UserMasterID);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.InternalServerError : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Store User Mapping Claim Category
        /// </summary>
        /// <param name="storeUser"></param>
        [HttpPost]
        [Route("AddStoreUserMappingCategory")]
        public ResponseModel StoreUserMappingCategory([FromBody] StoreClaimCategory storeUser)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();
                storeUser.CreatedBy = authenticate.UserMasterID;
                storeUser.TenantID = authenticate.TenantId;
                int Result = userCaller.CreateStoreUserMapping(new StoreUserService(_connectioSting), storeUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Edit Store User
        /// </summary>
        /// <param name="editStoreUser"></param>
        [HttpPost]
        [Route("EditStoreUser")] 
        public ResponseModel EditStoreUser([FromBody] CustomStoreUserEdit editStoreUser)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();
                editStoreUser.CreatedBy = authenticate.UserMasterID;
                editStoreUser.TenantID = authenticate.TenantId;
                int Result = userCaller.EditStoreUser(new StoreUserService(_connectioSting), editStoreUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Delete Store User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="IsStoreUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteStoreUser")]
        public ResponseModel DeleteStoreUser(int UserId, bool IsStoreUser)
        {
            ResponseModel objResponseModel = new ResponseModel();

            int StatusCode = 0;
            int deletecount = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();

                deletecount = userCaller.DeleteStoreUser(new StoreUserService(_connectioSting),authenticate.TenantId, UserId, IsStoreUser,authenticate.UserMasterID);

                StatusCode =
               deletecount == 0 ?
                      (int)EnumMaster.StatusCode.InternalServerError : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode); 

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = deletecount;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Get  Store User List
        /// </summary>
        /// <param name="tenantID"></param>
        
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreUsers")]
        public ResponseModel GetStoreUsers()
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<StoreUserListing> objUser = new List<StoreUserListing>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();

                objUser = userCaller.GetStoreUserList(new StoreUserService(_connectioSting), authenticate.TenantId);

                StatusCode =
               objUser.Count == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUser;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        #region Profile Mapping

        /// <summary>
        /// Get  Department List by  brandID and store ID
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BindDepartmentByBrandAndStore")]
        public ResponseModel BindDepartmentByBrandAndStore(int BrandID, int storeID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<StoreUserDepartmentList> objDepartmentModel = new List<StoreUserDepartmentList>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();

                objDepartmentModel = userCaller.GetDepartmentByBrandStore(new StoreUserService(_connectioSting), BrandID, storeID);

                StatusCode =
               objDepartmentModel.Count == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objDepartmentModel;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Get  reportee designation on designation id
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BindStoreReporteeDesignation")]
        public ResponseModel BindStoreReporteeDesignation(int DesignationID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<DesignationMaster> objdesignationModel = new List<DesignationMaster>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();

                objdesignationModel = userCaller.GetStoreReporteeDesignation(new StoreUserService(_connectioSting), DesignationID, authenticate.TenantId);

                StatusCode =
               objdesignationModel.Count == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objdesignationModel;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        // <summary>
        /// Get Store Report To User
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="IsStoreUser"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BindStoreReportToUser")]
        public ResponseModel BindStoreReportToUser(int DesignationID, bool IsStoreUser)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<CustomSearchTicketAgent> objuserModel = new List<CustomSearchTicketAgent>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();

                objuserModel = userCaller.GetStoreReportToUser(new StoreUserService(_connectioSting), DesignationID, IsStoreUser, authenticate.TenantId);

                StatusCode =
               objuserModel.Count == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objuserModel;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        #endregion

        #region Bind Claim 

        /// <summary>
        /// Get Claim Category List by muliptle brandID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("BindStoreClaimCategory")]
        public ResponseModel BindStoreClaimCategory(string BrandIds)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<StoreClaimCategoryModel> objClaimModel = new List<StoreClaimCategoryModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();

                 objClaimModel = userCaller.GetClaimCategoryListByBrandID(new StoreUserService(_connectioSting), authenticate.TenantId, BrandIds);

                StatusCode =
               objClaimModel.Count == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objClaimModel;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Get Claim sub Category List by muliptle CategoryID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BindStoreClaimSubCategory")]
        public ResponseModel BindStoreClaimSubCategory(string CategoryIDs)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<StoreClaimSubCategoryModel> objClaimModel = new List<StoreClaimSubCategoryModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();

                objClaimModel = userCaller.GetClaimSubCategoryByCategoryID(new StoreUserService(_connectioSting), authenticate.TenantId, CategoryIDs);

                StatusCode =
               objClaimModel.Count == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objClaimModel;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Get Claim Issue Type List by multiple subcat Id
        /// </summary>
        /// <param name="TenantID">Tenant Id</param>
        /// <param name="SubCategoryID">SubCategory ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("BindStoreClaimIssueType")]
        public ResponseModel BindStoreClaimIssueType(string subCategoryIDs)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<StoreClaimIssueTypeModel> objClaimModel = new List<StoreClaimIssueTypeModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();

                objClaimModel = userCaller.GetClaimIssueTypeListBySubCategoryID(new StoreUserService(_connectioSting), authenticate.TenantId, subCategoryIDs);

                StatusCode =
               objClaimModel.Count == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objClaimModel;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        #endregion


        #region Create Campaign Script

        /// <summary>
        /// Create Campaign Script
        /// </summary>
        /// <param name="campaignScript"></param>
        [HttpPost]
        [Route("CreateCampaignScript")]
        public ResponseModel CreateCampaignScript([FromBody] CampaignScript campaignScript)
        {
            StoreCaller storeCaller = new StoreCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                campaignScript.CreatedBy = authenticate.UserMasterID;
                campaignScript.TenantID = authenticate.TenantId;

                int result = storeCaller.CreateCampaignScript(new StoreService(_connectioSting), campaignScript);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion

        #region Update Claim Attechment Setting

        /// <summary>
        /// Update Claim Attechment Setting
        /// </summary>
        /// <param name="claimAttechment"></param>
        [HttpPost]
        [Route("UpdateClaimAttechmentSetting")]
        public ResponseModel UpdateClaimAttechmentSetting([FromBody] ClaimAttechment claimAttechment)
        {
            StoreCaller storeCaller = new StoreCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                claimAttechment.CreatedBy = authenticate.UserMasterID;
                claimAttechment.TenantID = authenticate.TenantId;

                int result = storeCaller.UpdateClaimAttechmentSetting(new StoreService(_connectioSting), claimAttechment);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion

        #endregion
    }
}