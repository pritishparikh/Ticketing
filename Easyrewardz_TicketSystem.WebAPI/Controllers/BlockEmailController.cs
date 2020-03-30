using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class BlockEmailController : ControllerBase
    {
        #region Variable Declaration
        private IConfiguration configuration;
        private readonly string _connectioString;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="_iConfig"></param>
        public BlockEmailController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioString = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion
        #region Custom Methods

        /// <summary>
        /// Add EmailBlock
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddEmailBlock")]
        public ResponseModel AddEmailBlock([FromBody] BlockEmailMaster blockEmailMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                BlockEmailCaller blockEmailCaller = new BlockEmailCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                blockEmailMaster.CreatedBy = authenticate.UserMasterID;
                blockEmailMaster.TenantID = authenticate.TenantId;

                int result = blockEmailCaller.InsertBlockEmail(new BlockEmailServices(_connectioString), blockEmailMaster);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;

            }
            return objResponseModel;
        }

        /// <summary>
        /// Update EmailBlock
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateEmailBlock")]
        public ResponseModel UpdateEmailBlock([FromBody] BlockEmailMaster blockEmailMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                BlockEmailCaller blockEmailCaller = new BlockEmailCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                blockEmailMaster.CreatedBy = authenticate.UserMasterID;
                blockEmailMaster.TenantID = authenticate.TenantId;

                int result = blockEmailCaller.UpdateBlockEmail(new BlockEmailServices(_connectioString), blockEmailMaster);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;

            }
            return objResponseModel;
        }

        /// <summary>
        /// Delete EmailBlock
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteEmailBlock")]
        public ResponseModel DeleteEmailBlock(int blockEmailID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                BlockEmailCaller blockEmailCaller = new BlockEmailCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = blockEmailCaller.DeleteBlockEmail(new BlockEmailServices(_connectioString), blockEmailID, authenticate.UserMasterID, authenticate.TenantId);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;

            }
            return objResponseModel;
        }

        /// <summary>
        /// Get List of Email Blocked
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ListEmailBlock")]
        public ResponseModel ListEmailBlock()
        {
            List<BlockEmailMaster> listblockEmailMasters = new List<BlockEmailMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                BlockEmailCaller blockEmailCaller = new BlockEmailCaller();

                listblockEmailMasters = blockEmailCaller.GetBlockEmail(new BlockEmailServices(_connectioString), authenticate.TenantId);

                statusCode =
                listblockEmailMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = listblockEmailMasters;

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