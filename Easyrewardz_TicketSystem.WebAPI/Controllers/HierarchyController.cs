using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Easyrewardz_TicketSystem.WebAPI.Filters;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class HierarchyController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _radisCacheServerAddress;
        private readonly string _connectionSting;
        #endregion

        #region Cunstructor
        public HierarchyController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// CreateHierarchy
        /// </summary>
        /// <param name="TaskMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateHierarchy")]
        public ResponseModel CreateHierarchy([FromBody] CustomHierarchymodel customHierarchymodel)
        {
            HierarchyCaller _Hierarchy = new HierarchyCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                customHierarchymodel.TenantID = authenticate.TenantId;
                customHierarchymodel.CreatedBy = authenticate.UserMasterID;
                int result = _Hierarchy.CreateHierarchy(new HierarchyService(_connectionSting), customHierarchymodel);

                if (customHierarchymodel.Deleteflag == 1)
                {
                    if (result == 0)
                    {
                        statusMessage = "Record in use";
                    }
                    else
                    {
                        statusMessage = "Record deleted successfully ";
                    }

                    StatusCode = 
                        result == 0 ? (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

                }
                else
                {
                    StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                }

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;

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
        /// List Hierarchy
        /// </summary>
        /// <returns></returns>
        [Route("ListHierarchy")]
        public ResponseModel ListHierarchy(int HierarchyFor = 1)
        {
            List<CustomHierarchymodel> customHierarchymodels = new List<CustomHierarchymodel>();
            HierarchyCaller _Hierarchy = new HierarchyCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                customHierarchymodels = _Hierarchy.ListofHierarchy(new HierarchyService(_connectionSting), authenticate.TenantId, HierarchyFor);
                StatusCode =
                   customHierarchymodels.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = customHierarchymodels;

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