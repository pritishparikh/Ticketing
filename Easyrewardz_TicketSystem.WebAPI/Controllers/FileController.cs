﻿using Easyrewardz_TicketSystem.CustomModel;
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
    public class FileController : ControllerBase
    {


        #region Variable Declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor

        /// <summary>
        /// File Controller
        /// </summary>
        /// <param name="_iConfig"></param>
        public FileController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }

        #endregion

        #region Custom Methods 

        /// <summary>
        /// Get SLA Status list for the SLA dropdown
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetFileUploadLogs")]
        public ResponseModel GetFileUploadLogs()
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<FileUploadLogs> objresponseModel = new List<FileUploadLogs>();
            int statusCode = 0;
            string statusMessage = "";
            int fileuploadFor = 0;
            try
            {
                //Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                SettingsCaller newAlert = new SettingsCaller();
                objresponseModel = newAlert.GetFileUploadLogs(new FileUploadService(_connectioSting), authenticate.TenantId, fileuploadFor);
                statusCode = objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objresponseModel;

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
