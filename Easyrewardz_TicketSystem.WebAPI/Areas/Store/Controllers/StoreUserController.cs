using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

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
        private readonly string ProfileImg_Resources;
        private readonly string StoreProfileImage;
        private readonly string rootPath;
        private readonly string BulkUpload;
        private readonly string UploadFiles;
        private readonly string DownloadFile;
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
            rootPath = configuration.GetValue<string>("APIURL");
            ProfileImg_Resources = configuration.GetValue<string>("ProfileImg_Resources");
            StoreProfileImage = configuration.GetValue<string>("StoreProfileImage");

            BulkUpload = configuration.GetValue<string>("BulkUpload");
            UploadFiles = configuration.GetValue<string>("Uploadfiles");
            DownloadFile = configuration.GetValue<string>("Downloadfile");
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
               StatusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : Result < 0 ? (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.InternalServerError;
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
            int Result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();

                if (userID > 0)
                {
                    Result = userCaller.CreateStoreUserProfiledetail(new StoreUserService(_connectioSting), authenticate.TenantId,
                     userID, BrandID, storeID, departmentId, functionIDs, designationID, reporteeID, authenticate.UserMasterID);

                    StatusCode =
                   Result == 0 ?
                          (int)EnumMaster.StatusCode.InternalServerError : (int)EnumMaster.StatusCode.Success;

                }
                else
                {
                    Result = (int)EnumMaster.StatusCode.RecordNotFound;
                }

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
        public ResponseModel AddStoreUserMappingCategory([FromBody] StoreClaimCategory storeUser)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int Result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();
                storeUser.CreatedBy = authenticate.UserMasterID;
                storeUser.TenantID = authenticate.TenantId;

                if (storeUser.UserID > 0)
                {
                    Result = userCaller.CreateStoreUserMapping(new StoreUserService(_connectioSting), storeUser);

                    StatusCode = Result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                }
                else
                {
                    Result = (int)EnumMaster.StatusCode.RecordNotFound;
                }

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
        [Route("ModifyStoreUser")]
        public ResponseModel ModifyStoreUser([FromBody] StoreUserDetailsModel editStoreUser)
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
                int Result = userCaller.ModifyStoreUser(new StoreUserService(_connectioSting), editStoreUser);

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
        /// Add Brand Store
        /// </summary>
        /// <param name="brandID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUserBrandStore")]
        public ResponseModel AddUserBrandStore(int brandID, int storeID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int Result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();
                Result = userCaller.AddBrandStore(new StoreUserService(_connectioSting), authenticate.TenantId, brandID, storeID, authenticate.UserMasterID);
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
        /// Update User Brand Store
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="brandID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateUserBrandStore")]
        public ResponseModel UpdateUserBrandStore(int UserID, int brandID, int storeID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int Result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();
                Result = userCaller.UpdateBrandStore(new StoreUserService(_connectioSting), authenticate.TenantId, brandID, storeID, authenticate.UserMasterID, UserID);
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

                deletecount = userCaller.DeleteStoreUser(new StoreUserService(_connectioSting), authenticate.TenantId, UserId, IsStoreUser, authenticate.UserMasterID);

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

                objUser = userCaller.GetStoreUserList(new StoreUserService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID);

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


        /// <summary>
        /// Get  Store User List on USerID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreUserDetailsByUserID")]
        public ResponseModel GetStoreUserDetailsByUserID(int UserID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            StoreUserListing objUser = new StoreUserListing();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();

                objUser = userCaller.GetStoreUserOnUserID(new StoreUserService(_connectioSting), authenticate.TenantId, UserID);

                StatusCode = objUser == null ?
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

        /// <summary>
        /// Get Store Report To User
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="IsStoreUser"></param>
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
        /// Bind Store Claim Category
        /// </summary>
        /// <param name="BrandIds"></param>
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
        /// <param name="CategoryIDs"></param>
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

        /// <summary>
        /// Bulk Upload User
        /// </summary>
        /// <param name="UserFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadStoreUser")]
        public ResponseModel BulkUploadStoreUser(int UserFor = 3)
        {
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false; bool successfilesaved = false;
            int count = 0;
            StoreCaller userCaller = new StoreCaller();
            StoreFileUploadCaller fileU = new StoreFileUploadCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = ""; string fileName = ""; string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            DataSet DataSetCSV = new DataSet();
            string[] filesName = null;
            List<string> CSVlist = new List<string>();
            string successfilename = string.Empty, errorfilename = string.Empty; string errorfilepath = string.Empty; string successfilepath = string.Empty;


            try
            {
                var files = Request.Form.Files;

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", timeStamp + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }
                var Keys = Request.Form;



                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));



                #region FilePath
                string Folderpath = Directory.GetCurrentDirectory();
                filesName = finalAttchment.Split(",");


                BulkUploadFilesPath = Path.Combine(Folderpath, BulkUpload, UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor));
                DownloadFilePath = Path.Combine(Folderpath, BulkUpload, DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor));


                if (!Directory.Exists(BulkUploadFilesPath))
                {
                    Directory.CreateDirectory(BulkUploadFilesPath);
                }



                if (files.Count > 0)
                {

                    for (int i = 0; i < files.Count; i++)
                    {
                        using (var ms = new MemoryStream())
                        {
                            files[i].CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            MemoryStream msfile = new MemoryStream(fileBytes);
                            FileStream docFile = new FileStream(Path.Combine(BulkUploadFilesPath, filesName[i]), FileMode.Create, FileAccess.Write);
                            msfile.WriteTo(docFile);
                            docFile.Close();
                            ms.Close();
                            msfile.Close();
                            string s = Convert.ToBase64String(fileBytes);
                            byte[] a = Convert.FromBase64String(s);
                            // act on the Base64 data

                        }
                    }
                }

                #endregion

                DataSetCSV = CommonService.csvToDataSet(Path.Combine(BulkUploadFilesPath, filesName[0]));
                CSVlist = userCaller.UserBulkUpload(new StoreService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, UserFor, DataSetCSV);


                #region Create Error and Success files and  Insert in FileUploadLog

                string SuccessFileName = "Store_UserSuccessFile_" + timeStamp + ".csv";
                string ErrorFileName = "Store_UserErrorFile_" + timeStamp + ".csv";



                //string SuccessFileUrl = !string.IsNullOrEmpty(CSVlist[0]) ?
                //  rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor) + "/Success/" + SuccessFileName : string.Empty;
                //string ErrorFileUrl = !string.IsNullOrEmpty(CSVlist[1]) ?
                //    rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor) + "/Error/" + ErrorFileName : string.Empty;

                if (!string.IsNullOrEmpty(CSVlist[0]))
                {
                    if (!CSVlist[0].ToLower().Contains("username"))
                        SuccessFileName = "Store_MappedCategorySuccessFile_" + timeStamp + ".csv";

                    successfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Success", SuccessFileName), CSVlist[0]);
                }
                if (!string.IsNullOrEmpty(CSVlist[1]))
                {
                    if (!CSVlist[1].ToLower().Contains("username"))
                        ErrorFileName = "Store_MappedCategoryErrorFile" + timeStamp + ".csv";

                    errorfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Error", ErrorFileName), CSVlist[1]);
                }


                string SuccessFileUrl = !string.IsNullOrEmpty(CSVlist[0]) ?
                  rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor) + "/Success/" + SuccessFileName : string.Empty;
                string ErrorFileUrl = !string.IsNullOrEmpty(CSVlist[1]) ?
                    rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor) + "/Error/" + ErrorFileName : string.Empty;

                count = fileU.CreateFileUploadLog(new StoreFileUploadService(_connectioSting), authenticate.TenantId, filesName[0], true,
                                 ErrorFileName, SuccessFileName, authenticate.UserMasterID, "Store_User", SuccessFileUrl, ErrorFileUrl, UserFor);
                #endregion



                StatusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = count;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;


        }

        /// <summary>
        /// Get Store User Profile Detail
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetStoreUserProfileDetail")]
        public ResponseModel GetStoreUserProfileDetail()
        {
            List<UpdateUserProfiledetailsModel> objUserList = new List<UpdateUserProfiledetailsModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                string url = configuration.GetValue<string>("APIURL") + ProfileImg_Resources + "/" + StoreProfileImage;
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                StoreUserCaller userCaller = new StoreUserCaller();
                objUserList = userCaller.GetUserProfileDetails(new StoreUserService(_connectioSting), authenticate.UserMasterID, url);
                StatusCode =
               objUserList.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUserList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// UpdateStoreUserProfileDetails
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("UpdateStoreUserProfileDetails")]
        public ResponseModel UpdateStoreUserProfileDetails(IFormFile File)
        {
            UpdateUserProfiledetailsModel UpdateUserProfiledetailsModel = new UpdateUserProfiledetailsModel();
            ProfileDetailsmodel profileDetailsmodel = new ProfileDetailsmodel();
            var Keys = Request.Form;
            UpdateUserProfiledetailsModel = JsonConvert.DeserializeObject<UpdateUserProfiledetailsModel>(Keys["UpdateUserProfiledetailsModel"]);
            var file = Request.Form.Files;
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            var folderName = Path.Combine(ProfileImg_Resources, StoreProfileImage);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                if (file.Count > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                    var fileName_Id = fileName.Replace(".", UpdateUserProfiledetailsModel.UserId + timeStamp + ".") + "";
                    var fullPath = Path.Combine(pathToSave, fileName_Id);
                    var dbPath = Path.Combine(folderName, fileName_Id);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file[0].CopyTo(stream);
                    }
                    UpdateUserProfiledetailsModel.ProfilePicture = fileName_Id;
                    string url = configuration.GetValue<string>("APIURL") + ProfileImg_Resources + "/" + StoreProfileImage + "/" + fileName_Id;
                    profileDetailsmodel.ProfilePath = url;
                }

          
                int StatusCode = 0;
                string statusMessage = "";

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();
                int Result = userCaller.UpdateUserProfileDetail(new StoreUserService(_connectioSting), UpdateUserProfiledetailsModel);

                profileDetailsmodel.Result = Result;

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = profileDetailsmodel;


            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel; 
        }

        /// <summary>
        /// Send Mail for changepassword
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="IsStoreUser"></param>
        [HttpPost]
        [Route("SendMailforchangepassword")]
        public ResponseModel SendMailforchangepassword(int userID, int IsStoreUser = 1)
        {
            CustomChangePassword customChangePassword = new CustomChangePassword();
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                string _data = "";
                string ProgramCode = authenticate.ProgramCode;
                RedisCacheService cacheService = new RedisCacheService(_radisCacheServerAddress);
                if (cacheService.Exists("Con" + ProgramCode))
                {
                    _data = cacheService.Get("Con" + ProgramCode);
                    _data = JsonConvert.DeserializeObject<string>(_data);
                }
                string X_Authorized_Domainname = Convert.ToString(Request.Headers["X-Authorized-Domainname"]);
                if (X_Authorized_Domainname != null)
                {
                    X_Authorized_Domainname = SecurityService.DecryptStringAES(X_Authorized_Domainname);
                }
                StoreUserCaller userCaller = new StoreUserCaller();

                customChangePassword = userCaller.GetStoreUserCredentails(new StoreUserService(_data), userID, authenticate.TenantId, IsStoreUser);
                if (customChangePassword.UserID > 0 && !string.IsNullOrEmpty(customChangePassword.Password) && !string.IsNullOrEmpty(customChangePassword.EmailID))
                {
                    MasterCaller masterCaller = new MasterCaller();
                    SMTPDetails sMTPDetails = masterCaller.GetSMTPDetails(new MasterServices(_data), authenticate.TenantId);
                    StoreSecurityCaller _securityCaller = new StoreSecurityCaller();
                    CommonService commonService = new CommonService();

                    EmailProgramCode emailProgramCode = new EmailProgramCode();
                    emailProgramCode.EmailID = customChangePassword.EmailID;
                    emailProgramCode.ProgramCode = ProgramCode;
                    string jsonData = JsonConvert.SerializeObject(emailProgramCode);

                    string encryptedEmailId = commonService.Encrypt(jsonData);
                    //string encryptedEmailId = SecurityService.Encrypt(jsonData);

                    string decriptedPassword = SecurityService.DecryptStringAES(customChangePassword.Password);
                    string url = X_Authorized_Domainname.TrimEnd('/') + "/StoreChangePassword";
                    string body = "Dear User, <br/>Please find the below details.  <br/><br/>" + "Your Email ID  : " + customChangePassword.EmailID + "<br/>" + "Your Password : " + decriptedPassword + "<br/><br/>" + "Click on Below link to change the Password <br/>" + url + "?Id:" + encryptedEmailId;
                    bool isUpdate = _securityCaller.sendMailForChangePassword(new StoreSecurityService(_data), sMTPDetails, customChangePassword.EmailID, body, authenticate.TenantId);
                    if (isUpdate)
                    {
                        objResponseModel.Status = true;
                        objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                        objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                        objResponseModel.ResponseData = "Mail sent successfully";
                    }
                    else
                    {
                        objResponseModel.Status = false;
                        objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                        objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                        objResponseModel.ResponseData = "Mail sent failure";
                    }
                }

                else
                {
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = (int)EnumMaster.StatusCode.RecordNotFound;
                    objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.RecordNotFound);
                    objResponseModel.ResponseData = "Sorry User does not exist or active";
                }
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }
        /// <summary>
        /// Delete Store User Profile
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [Route("DeleteStoreUserProfile")]
        public ResponseModel DeleteStoreUserProfile()
        {
            ResponseModel responseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreUserCaller userCaller = new StoreUserCaller();
                int Result = userCaller.DeleteProfilePicture(new StoreUserService(_connectioSting), authenticate.TenantId, authenticate.UserMasterID);

                StatusCode =
                Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                responseModel.Status = true;
                responseModel.StatusCode = StatusCode;
                responseModel.Message = statusMessage;
                responseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return responseModel;
        }
        /// <summary>
        /// StoreChangePassword
        /// </summary>
        /// <param name="customChangePassword"></param>
        /// 
        [AllowAnonymous]
        [HttpPost]
        [Route("StoreChangePassword")]
        public ResponseModel StoreChangePassword([FromBody] CustomChangePassword customChangePassword)
        {

            string _data = "";
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string programCode = "";
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                if (!string.IsNullOrEmpty(token))
                {
                    _data = _connectioSting;
                    authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                    programCode = authenticate.ProgramCode;
                }
                StoreSecurityCaller _securityCaller = new StoreSecurityCaller();
                CommonService commonService = new CommonService();

                EmailProgramCode bsObj = new EmailProgramCode();

                if (customChangePassword.ChangePasswordType.Equals("mail"))
                {
                    //customChangePassword.EmailID = SecurityService.DecryptStringAES(customChangePassword.EmailID);
                    //string cipherEmailId = SecurityService.DecryptStringAES(customChangePassword.EmailID);

                    string encryptedEmailId = commonService.Decrypt(customChangePassword.EmailID);
                    if (encryptedEmailId != null)
                    {
                        bsObj = JsonConvert.DeserializeObject<EmailProgramCode>(encryptedEmailId);
                    }
                    customChangePassword.EmailID = bsObj.EmailID;
                    customChangePassword.ProgramCode = bsObj.ProgramCode;
                    programCode = bsObj.ProgramCode;
                }

                if (programCode != null)
                {


                    RedisCacheService cacheService = new RedisCacheService(_radisCacheServerAddress);
                    if (cacheService.Exists("Con" + programCode))
                    {
                        _data = cacheService.Get("Con" + programCode);
                        _data = JsonConvert.DeserializeObject<string>(_data);
                    }
                }

                customChangePassword.Password = SecurityService.Encrypt(customChangePassword.Password);


                bool Result = _securityCaller.ChangePassword(new StoreSecurityService(_data), customChangePassword, authenticate.TenantId, authenticate.UserMasterID);

                StatusCode =
               Result == false ?
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
        /// GetStoreReportUserList
        /// </summary>
        /// <param name="RegionID"></param>
        /// <param name="ZoneID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreReportUser")]
        public ResponseModel GetStoreReportUser(int RegionID, int ZoneID)
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

                objUser = userCaller.GetStoreReportUser(new StoreUserService(_connectioSting), authenticate.TenantId, RegionID, ZoneID, authenticate.UserMasterID);

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


        /// <summary>
        /// GetStoreReportUsersList
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreReportUsersList")]
        public ResponseModel GetStoreReportUsersList()
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

                objUser = userCaller.GetStoreReportUsersList(new StoreUserService(_connectioSting), authenticate.TenantId);

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
        #endregion
    }
}