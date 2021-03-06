﻿using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

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
        public ResponseModel AddStoreUserPersonalDetail([FromBody] CustomStoreUserModel storeUser)
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
                int Result = userCaller.StoreUserPersonaldetail(new StoreUserService(_connectioSting), storeUser);

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
        public ResponseModel AddStoreUserProfileDetail([FromBody] CustomStoreUserModel storeUser)
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
                int Result = userCaller.StoreUserProfiledetail(new StoreUserService(_connectioSting), storeUser);

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
        /// Store User Mapping Claim Category
        /// </summary>
        /// <param name="storeUser"></param>
        [HttpPost]
        [Route("StoreUserMappingCategory")]
        public ResponseModel StoreUserMappingCategory([FromBody] CustomStoreUser storeUser)
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
                int Result = userCaller.StoreUserMapping(new StoreUserService(_connectioSting), storeUser);

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