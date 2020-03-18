using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class ModuleController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor

        public ModuleController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;

        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Update module items
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="ModulesActive"></param>
        /// <param name="ModuleInactive"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyModuleItems")]
        public ResponseModel ModifyModuleItems(int ModuleID, string ModulesActive, string ModuleInactive)
        {
            int updatecount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newModule = new SettingsCaller();

                updatecount = _newModule.UpdateModules(new ModuleService(_Cache, Db), authenticate.TenantId, ModuleID, ModulesActive, ModuleInactive,
                    authenticate.UserMasterID);

                StatusCode =
                updatecount == 0 ?
                     (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = updatecount;

            }
            catch (Exception)
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
        /// View  Modules Items
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetModulesItems")]
        public ResponseModel GetModulesItems(int ModuleID)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<ModuleItems> _objresponseModel = new List<ModuleItems>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newModule = new SettingsCaller();

                _objresponseModel = _newModule.GetModulesItemList(new ModuleService(_Cache, Db), authenticate.TenantId, ModuleID);

                StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

            }
            catch (Exception)
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
        /// View  Modules List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetModules")]
        public ResponseModel GetModules()
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<ModulesModel> _objresponseModel = new List<ModulesModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newModule = new SettingsCaller();

                _objresponseModel = _newModule.GetModulesList(new ModuleService(_Cache, Db), authenticate.TenantId);

                StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

            }
            catch (Exception)
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
    }
}
