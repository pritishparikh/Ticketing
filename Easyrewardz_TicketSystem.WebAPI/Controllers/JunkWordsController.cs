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
    public class JunkWordsController : ControllerBase
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
        public JunkWordsController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioString = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion
        #region Custom Methods

        /// <summary>
        /// Insert Junk words
        /// </summary>
        /// <param name="JunkWordsMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddJunkWords")]
        public ResponseModel AddJunkWords([FromBody] JunkWordsMaster junkWordsMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                JunkWordsCaller junkWordsCaller = new JunkWordsCaller();
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                junkWordsMaster.CreatedBy = authenticate.UserMasterID;
                junkWordsMaster.TenantID = authenticate.TenantId;

                int result = junkWordsCaller.InsertJunkWords(new JunkWordsService(_connectioString), junkWordsMaster);
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
        /// Update Email Block
        /// <param name="JunkWordsMaster"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateJunkWords")]
        public ResponseModel UpdateEmailBlock([FromBody] JunkWordsMaster junkWordsMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                JunkWordsCaller junkWordsCaller = new JunkWordsCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                junkWordsMaster.CreatedBy = authenticate.UserMasterID;
                junkWordsMaster.TenantID = authenticate.TenantId;

                int result = junkWordsCaller.UpdateJunkWords(new JunkWordsService(_connectioString), junkWordsMaster);
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
        /// Delete Junk Words
        /// <param name="junkKeywordID"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteJunkWords")]
        public ResponseModel DeleteEmailBlock(int junkKeywordID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                JunkWordsCaller junkWordsCaller = new JunkWordsCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = junkWordsCaller.DeleteJunkWords(new JunkWordsService(_connectioString), junkKeywordID, authenticate.UserMasterID, authenticate.TenantId);
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
        /// Get List of Junk Words
        /// <param name=""></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ListJunkWords")]
        public ResponseModel ListJunkWords()
        {
            List<JunkWordsMaster> listJunkWordsMaster = new List<JunkWordsMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                JunkWordsCaller junkWordsCaller = new JunkWordsCaller();

                listJunkWordsMaster = junkWordsCaller.GetJunkWords(new JunkWordsService(_connectioString), authenticate.TenantId);

                statusCode =
                listJunkWordsMaster.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = listJunkWordsMaster;

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