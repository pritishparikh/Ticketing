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
using System.IO;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreDashboardController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        private readonly string _UploadedBulkFile;
        private readonly string _profileImg_Resources;
        private readonly string StoreProfileImage;
        private readonly string _API_Url;
        #endregion

        #region Constructor
        public StoreDashboardController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _UploadedBulkFile = configuration.GetValue<string>("FileUploadLocation");
            rootPath = configuration.GetValue<string>("APIURL");
            _API_Url = configuration.GetValue<string>("APIURL");
            _profileImg_Resources = configuration.GetValue<string>("ProfileImg_Resources");
            StoreProfileImage = configuration.GetValue<string>("StoreProfileImage");
        }
        #endregion

        /// <summary>
        /// Get Stroe Dashboard Data
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetstoreDashboardList")]
        public ResponseModel GetstoreDashboardList([FromBody] StoreDashboardModel dasbhboardmodel)
        {

            List<StoreDashboardResponseModel> objStoreDash = new List<StoreDashboardResponseModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreDashboard newstoreDash = new StoreDashboard();

                objStoreDash = newstoreDash.getStoreDashboardTaskList(new StoreDashboardService(_connectioSting), dasbhboardmodel);

                StatusCode =
                objStoreDash.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStoreDash;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }




        /// <summary>
        /// Get Stroe Dashboard Data
        /// </summary>
        /// <param name=StoreDashboardClaimModel></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getstoreDashboardListClaim")]
        public ResponseModel getstoreDashboardListClaim([FromBody] StoreDashboardClaimModel ClaimSearchModel)
        {

            List<StoreDashboardClaimResponseModel> ClaimSearchResponse = new List<StoreDashboardClaimResponseModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                ClaimSearchModel.tenantID = authenticate.TenantId;
                StoreDashboardCaller dashBoardcaller = new StoreDashboardCaller();

                ClaimSearchResponse = dashBoardcaller.getStoreDashboardClaimList(new StoreDashboardService(_connectioSting), ClaimSearchModel);

                StatusCode = ClaimSearchResponse.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ClaimSearchResponse; 
            }
            catch (Exception )

            { throw ; }
            return objResponseModel;
        }

        /// <summary>
        /// Store Logged In Account Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("StoreLoggedInAccountDetails")]
        public ResponseModel StoreLoggedInAccountDetails()
        {
            LoggedInAgentModel loggedinAccInfo = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0; string statusMessage = "";
            StoreDashboard storeDashboard = new StoreDashboard();
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                var folderName = Path.Combine(_profileImg_Resources, StoreProfileImage);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                loggedinAccInfo = storeDashboard.GetLogginAccountInfo(new StoreDashboardService(_connectioSting),
                    authenticate.TenantId, authenticate.UserMasterID, pathToSave);

                statusCode = loggedinAccInfo != null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = loggedinAccInfo != null ? loggedinAccInfo : null;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


    }
}