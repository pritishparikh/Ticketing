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
        private IConfiguration Configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor

        public ModuleController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
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
            int updateCount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newModule = new SettingsCaller();

                updateCount = newModule.UpdateModules(new ModuleService(_Cache, Db), authenticate.TenantId, ModuleID, ModulesActive, ModuleInactive,
                    authenticate.UserMasterID);

                statusCode =
                updateCount == 0 ?
                     (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = updateCount;

            }
            catch (Exception)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }


        /// <summary>
        /// View  Modules Items
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetModulesItems")]
        public ResponseModel GetModulesItems(int ModuleID)
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<ModuleItems> objModuleItemResponseModel = new List<ModuleItems>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newModule = new SettingsCaller();

                objModuleItemResponseModel = newModule.GetModulesItemList(new ModuleService(_Cache, Db), authenticate.TenantId, ModuleID);

                statusCode = objModuleItemResponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objModuleItemResponseModel;

            }
            catch (Exception)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }


        /// <summary>
        /// View  Modules List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetModules")]
        public ResponseModel GetModules()
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<ModulesModel> objModuleItemResponseModel = new List<ModulesModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(token));

                SettingsCaller newModule = new SettingsCaller();

                objModuleItemResponseModel = newModule.GetModulesList(new ModuleService(_Cache, Db), authenticate.TenantId);

                StatusCode = objModuleItemResponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objModuleItemResponseModel;

            }
            catch (Exception)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }
        #endregion
    }
}
