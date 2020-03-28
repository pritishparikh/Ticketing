using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
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
        private readonly string connectioSting;
        private readonly string radisCacheServerAddress;
        #endregion

        #region Cunstructor
        public ModuleController(IConfiguration iConfig)
        {
            configuration = iConfig;
            connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Update  Modules Items
        /// <param name="ModuleID"></param>
        /// <param name="ModulesActive"></param>
        /// <param name="ModuleInactive"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyModuleItems")]
        public ResponseModel ModifyModuleItems(int ModuleID, string ModulesActive, string ModuleInactive)
        {
            int updatecount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                SettingsCaller _newModule = new SettingsCaller();

                updatecount = _newModule.UpdateModules(new ModuleService(connectioSting), authenticate.TenantId, ModuleID, ModulesActive, ModuleInactive,
                    authenticate.UserMasterID);

                StatusCode =
                updatecount == 0 ?
                     (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = updatecount;

            }
            catch (Exception )
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// View  Modules Items
        /// <param name="ModuleID"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetModulesItems")]

        public ResponseModel GetModulesItems(int ModuleID)
        {

            ResponseModel objResponseModel = new ResponseModel();
            List<ModuleItems> objresponseModel = new List<ModuleItems>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                SettingsCaller _newModule = new SettingsCaller();

                objresponseModel = _newModule.GetModulesItemList(new ModuleService(connectioSting), authenticate.TenantId, ModuleID);

                StatusCode = objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objresponseModel;

            }
            catch (Exception )
            {
                throw;
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
            List<ModulesModel> objresponseModel = new List<ModulesModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                SettingsCaller _newModule = new SettingsCaller();

                objresponseModel = _newModule.GetModulesList(new ModuleService(connectioSting), authenticate.TenantId);

                StatusCode = objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objresponseModel;

            }
            catch (Exception )
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion
    }
}
