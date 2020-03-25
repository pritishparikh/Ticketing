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
    public class FeatureController : ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        public FeatureController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
        }
        #endregion

        #region Custom Methods

        #region GetFeatureList
        /// <summary>
        ///GetFeatureList
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetFeaturePlanList")]
        public ResponseModel GetFeaturePlanList()
        {
            ResponseModel objResponseModel = new ResponseModel();
            FeaturePlanModel feature = new FeaturePlanModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                FeaturePlanCaller fCaller = new FeaturePlanCaller();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                feature = fCaller.GetFeaturePlan(new FeaturePlanService(Cache, Db),authenticate.TenantId);

                statusCode = feature == null ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = feature;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
        #endregion

        #region Add Feature 
        /// <summary>
        /// Add new feature
        /// </summary>
        /// <param name="objFeatures"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddFeature")]
        [AllowAnonymous]
        public ResponseModel AddFeature([FromBody] FeaturesModel objFeatures)
        {
            ResponseModel objResponseModel = new ResponseModel();
            string result =string.Empty;
           // FeaturePlanModel feature = new FeaturePlanModel();
            int statusCode = 0;
            //int Result =0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                FeaturePlanCaller fCaller = new FeaturePlanCaller();

                objFeatures.UserID = authenticate.UserMasterID;
                result = fCaller.AddFeature(new FeaturePlanService(Cache, Db),objFeatures);

                statusCode = result !=""  ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = result;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
        #endregion

        #region Delete Feature 
        /// <summary>
        /// Delete Future
        /// </summary>
        /// <param name="FeatureID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteFeature")]
        [AllowAnonymous]
        public ResponseModel DeleteFeature(int FeatureID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            // FeaturePlanModel feature = new FeaturePlanModel();
            int statusCode = 0;
            //int Result =0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                FeaturePlanCaller fCaller = new FeaturePlanCaller();
                // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                int userID = 6;
               
                result = fCaller.DeleteFeature(new FeaturePlanService(Cache, Db), userID, FeatureID);

                statusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        #endregion

        #region Add Plan
        /// <summary>
        /// Add new plan
        /// </summary>
        /// <param name="planModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPlan")]
        public ResponseModel AddPlan([FromBody] PlanModel planModel)
        {
            FeaturePlanCaller newFeaturePlanCaller = new FeaturePlanCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                planModel.CreatedBy = authenticate.UserMasterID;

                int result = newFeaturePlanCaller.AddPlan(new FeaturePlanService(Cache, Db), planModel);

                statusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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

        #endregion

        #region GetPlanOnEdit
        /// <summary>
        /// Get plan on edit
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPlanOnEdit")]
        public ResponseModel GetPlanOnEdit()
        {
            List<PlanModel> objplanModels = new List<PlanModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                FeaturePlanCaller newFeaturePlanCaller = new FeaturePlanCaller();

                objplanModels = newFeaturePlanCaller.GetPlanOnEdit(new FeaturePlanService(Cache, Db), authenticate.TenantId);

                statusCode =
                objplanModels.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objplanModels;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        #endregion

        #endregion

    }
}