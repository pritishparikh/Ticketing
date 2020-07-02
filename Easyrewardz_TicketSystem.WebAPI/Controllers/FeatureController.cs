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

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class FeatureController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor
        public FeatureController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods

        #region GetFeatureList
        /// <summary>
        ///Get Feature List
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
                FeaturePlanCaller fcaller = new FeaturePlanCaller();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                feature = fcaller.GetFeaturePlan(new FeaturePlanService(_connectioSting),authenticate.TenantId);

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

        /// <summary>
        /// Add Feature
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
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                FeaturePlanCaller fcaller = new FeaturePlanCaller();
               // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                objFeatures.UserID = 6;
                result = fcaller.AddFeature(new FeaturePlanService(_connectioSting),objFeatures);

                statusCode = result != ""  ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                FeaturePlanCaller fcaller = new FeaturePlanCaller();
                // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                int UserID = 6;

                result = fcaller.DeleteFeature(new FeaturePlanService(_connectioSting), UserID, FeatureID);

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
            FeaturePlanCaller newfeaturePlanCaller = new FeaturePlanCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                planModel.CreatedBy = authenticate.UserMasterID;

                int result = newfeaturePlanCaller.AddPlan(new FeaturePlanService(_connectioSting), planModel);

                statusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;

            }
            catch (Exception )
            {
                throw;
            }

            return objResponseModel;
        }

        #endregion
        /// <summary>
        /// Get plan on edit
        /// </summary>
        /// <returns></returns>
        #region GetPlanOnEdit
        [HttpPost]
        [Route("GetPlanOnEdit")]
        public ResponseModel GetPlanOnEdit()
        {
            List<PlanModel> objplanModels = new List<PlanModel>();
            ResponseModel  objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                FeaturePlanCaller newFeaturePlanCaller = new FeaturePlanCaller();

                objplanModels = newFeaturePlanCaller.GetPlanOnEdit(new FeaturePlanService(_connectioSting), authenticate.TenantId);

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


    }
}