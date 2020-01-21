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
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CRMRoleController: ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;

        #endregion

        #region Cunstructor
        public CRMRoleController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");

        }
        #endregion

        #region custom Methods

        /// <summary>
        /// Create /Update CRMRole
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateUpdateCRMRole")]
        public ResponseModel CreateUpdateCRMRole(int CRMRoleID, string RoleName, bool RoleisActive,string ModulesEnabled,string ModulesDisabled)
        {
            int count = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
               string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);


                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();

                   count = _newCRM.InsertUpdateCRMRole(new CRMRoleService(_connectioSting), CRMRoleID, authenticate.TenantId, RoleName, RoleisActive, authenticate.UserMasterID, ModulesEnabled, ModulesDisabled);

                StatusCode = count == 0 ?(int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = count;

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
        /// Delete CRMROLE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteCRMRole")]
        public ResponseModel CRMRol(int CRMRoleID)
        {
            int Deletecount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();
                Deletecount = _newCRM.DeleteCRMRole(new CRMRoleService(_connectioSting), authenticate.TenantId, CRMRoleID);

                StatusCode =
                Deletecount == 0 ? (int)EnumMaster.StatusCode.InternalServiceNotWorking : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Deletecount;

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
        /// View  CRMROLE
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCRMRoles")]
        public ResponseModel GetCRMRoles()
        {
          
            ResponseModel _objResponseModel = new ResponseModel();
            List<CRMRoleModel> _objresponseModel = new List<CRMRoleModel>();
           int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                SettingsCaller _newCRM = new SettingsCaller();
                _objresponseModel = _newCRM.CRMRoleList(new CRMRoleService(_connectioSting), authenticate.TenantId);

                StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

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

    }
}
