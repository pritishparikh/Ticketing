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
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        public FeatureController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;
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
            ResponseModel _objResponseModel = new ResponseModel();
            FeaturePlanModel feature = new FeaturePlanModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                FeaturePlanCaller fcaller = new FeaturePlanCaller();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                feature = fcaller.GetFeaturePlan(new FeaturePlanService(_Cache, Db),authenticate.TenantId);

                StatusCode = feature == null ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = feature;
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
            ResponseModel _objResponseModel = new ResponseModel();
            string Result =string.Empty;
           // FeaturePlanModel feature = new FeaturePlanModel();
            int StatusCode = 0;
            //int Result =0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                FeaturePlanCaller fcaller = new FeaturePlanCaller();
               // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                objFeatures.UserID = 6;
                Result = fcaller.AddFeature(new FeaturePlanService(_Cache, Db),objFeatures);

                StatusCode = Result !=""  ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = Result;
                _objResponseModel.ResponseData = Result;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int Result = 0;
            // FeaturePlanModel feature = new FeaturePlanModel();
            int StatusCode = 0;
            //int Result =0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                FeaturePlanCaller fcaller = new FeaturePlanCaller();
                // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                int UserID = 6;
               
                Result = fcaller.DeleteFeature(new FeaturePlanService(_Cache, Db), UserID, FeatureID);

                StatusCode = Result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;
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
            FeaturePlanCaller _newfeaturePlanCaller = new FeaturePlanCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                planModel.CreatedBy = authenticate.UserMasterID;

                int result = _newfeaturePlanCaller.AddPlan(new FeaturePlanService(_Cache, Db), planModel);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                FeaturePlanCaller _newFeaturePlanCaller = new FeaturePlanCaller();

                objplanModels = _newFeaturePlanCaller.GetPlanOnEdit(new FeaturePlanService(_Cache, Db), authenticate.TenantId);

                StatusCode =
                objplanModels.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objplanModels;

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

        #endregion

    }
}