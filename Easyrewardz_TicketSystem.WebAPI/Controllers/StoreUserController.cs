using System;
using Microsoft.AspNetCore.Mvc;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Easyrewardz_TicketSystem.MySqlDBContext;

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
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// StoreUser Controller
        /// </summary>
        /// <param name="_iConfig"></param>
        public StoreUserController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;
        }
        #endregion

        #region Custom Methods 
        /// <summary>
        /// AddStoreUserPersonalDetail
        /// </summary>
        /// <param name="CustomStoreUserModel"></param>
        [HttpPost]
        [Route("AddStoreUserPersonalDetail")]
        public ResponseModel AddStoreUserPersonalDetail([FromBody] CustomStoreUserModel storeUser)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                StoreUserCaller userCaller = new StoreUserCaller();
                storeUser.CreatedBy = authenticate.UserMasterID;
                storeUser.TenantID = authenticate.TenantId;
                int Result = userCaller.StoreUserPersonaldetail(new StoreUserService(_Cache, Db), storeUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// AddStoreUserProfileDetail
        /// </summary>
        /// <param name="CustomStoreUserModel"></param>
        [HttpPost]
        [Route("AddStoreUserProfileDetail")]
        public ResponseModel AddStoreUserProfileDetail([FromBody] CustomStoreUserModel storeUser)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                StoreUserCaller userCaller = new StoreUserCaller();
                storeUser.CreatedBy = authenticate.UserMasterID;
                storeUser.TenantID = authenticate.TenantId;
                int Result = userCaller.StoreUserProfiledetail(new StoreUserService(_Cache, Db), storeUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Store User Mapping Claim Category
        /// </summary>
        /// <param name="CustomStoreUserModel"></param>
        [HttpPost]
        [Route("StoreUserMappingCategory")]
        public ResponseModel StoreUserMappingCategory([FromBody] CustomStoreUser storeUser)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                StoreUserCaller userCaller = new StoreUserCaller();
                storeUser.CreatedBy = authenticate.UserMasterID;
                storeUser.TenantID = authenticate.TenantId;
                int Result = userCaller.StoreUserMapping(new StoreUserService(_Cache, Db), storeUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Edit Store User
        /// </summary>
        /// <param name="CustomStoreUserModel"></param>
        [HttpPost]
        [Route("EditStoreUser")] 
        public ResponseModel EditStoreUser([FromBody] CustomStoreUserEdit editStoreUser)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                StoreUserCaller userCaller = new StoreUserCaller();
                editStoreUser.CreatedBy = authenticate.UserMasterID;
                editStoreUser.TenantID = authenticate.TenantId;
                int Result = userCaller.EditStoreUser(new StoreUserService(_Cache, Db), editStoreUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }

        #region Create Campaign Script
        /// <summary>
        /// Create Campaign script
        /// </summary>
        /// <param name="campaignScript"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCampaignScript")]
        public ResponseModel CreateCampaignScript([FromBody] CampaignScript campaignScript)
        {
            StoreCaller storeCaller = new StoreCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                //authenticate.TenantId = 0;
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                campaignScript.CreatedBy = authenticate.UserMasterID;
                campaignScript.TenantID = authenticate.TenantId;

                int result = storeCaller.CreateCampaignScript(new StoreService(_Cache, Db), campaignScript);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;

            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }
        #endregion

        #region Update Claim Attechment Setting
        /// <summary>
        /// Update Claim Attechment Setting
        /// </summary>
        /// <param name="claimAttechment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateClaimAttechmentSetting")]
        public ResponseModel UpdateClaimAttechmentSetting([FromBody] ClaimAttechment claimAttechment)
        {
            StoreCaller storeCaller = new StoreCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                //authenticate.TenantId = 0;
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                claimAttechment.CreatedBy = authenticate.UserMasterID;
                claimAttechment.TenantID = authenticate.TenantId;

                int result = storeCaller.UpdateClaimAttechmentSetting(new StoreService(_Cache, Db), claimAttechment);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;

            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }
        #endregion

        #endregion
    }
}