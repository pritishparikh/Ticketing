using System;
using System.Collections.Generic;
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

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class DesignationController : ControllerBase
    {
        #region Variable Declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Designation 
        /// </summary>
        /// <param name="_iConfig"></param>
        public DesignationController(IConfiguration _iConfig,IDistributedCache cache, TicketDBContext db)
        {
            Configuration = _iConfig;
            Cache = cache;
            Db = db;
        }

        #endregion

        #region Custom Methods 

        /// <summary>
        /// Get designation list for the Designation dropdown
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDesignationList")]
        public ResponseModel GetDesignationList()
        {
            List<DesignationMaster> designationMasters = new List<DesignationMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                DesignationCaller designationCaller = new DesignationCaller();
                designationMasters = designationCaller.GetDesignations(new DesignationService(Cache, Db), authenticate.TenantId);

                statusCode =
                designationMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = designationMasters;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get designation list for the Designation dropdown
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReporteeDesignation")]
        public ResponseModel GetReporteeDesignation(int DesignationID, int HierarchyFor=1)
        {
            List<DesignationMaster> designationMasters = new List<DesignationMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(_token));
                DesignationCaller designationCaller = new DesignationCaller();
                designationMasters = designationCaller.GetReporteeDesignation(new DesignationService(Cache, Db), DesignationID, HierarchyFor,authenticate.TenantId);

                statusCode =
                designationMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = designationMasters;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
        /// <summary>
        /// GetReportTo user based reportee Designation
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReportTo")]
        public ResponseModel GetReportTo(int DesignationID, int IsStoreUser=1)
        {
            List<CustomSearchTicketAgent> designationMasters = new List<CustomSearchTicketAgent>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                DesignationCaller designationCaller = new DesignationCaller();
                designationMasters = designationCaller.GetReportToUser(new DesignationService(Cache, Db), DesignationID , IsStoreUser, authenticate.TenantId);

                statusCode =
                designationMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = designationMasters;
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