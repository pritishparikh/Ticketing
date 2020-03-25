using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
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
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Brand Controller
        /// </summary>
        /// <param name="_iConfig"></param>
        public FileController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
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
            List<FileUploadLogs> objFileUploadModel = new List<FileUploadLogs>();
            int statusCode = 0;
            string statusMessage = "";
            int fileUploadFor = 0;
            try
            {
                //Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newAlert = new SettingsCaller();

                objFileUploadModel = newAlert.GetFileUploadLogs(new FileUploadService(Cache, Db), authenticate.TenantId, fileUploadFor);
                statusCode = objFileUploadModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objFileUploadModel;

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
