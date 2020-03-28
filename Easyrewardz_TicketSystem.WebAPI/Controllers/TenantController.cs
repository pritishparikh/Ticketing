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
    public class TenantController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;

        #endregion

        #region constructor
        public TenantController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");

        }
        #endregion

        #region method

        /// <summary>
        /// Insert Company
        /// </summary>
        /// <param name="companyModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertCompany")]
        public ResponseModel InsertCompany([FromBody] CompanyModel companyModel )
        {
            TenantCaller newTenantCaller = new TenantCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                companyModel.CreatedBy = authenticate.UserMasterID;
                companyModel.TenantID = authenticate.TenantId;

                int result = newTenantCaller.InsertCompany(new TenantService(_connectioSting), companyModel);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// BillingDetails_crud
        /// </summary>
        /// <param name="BillingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BillingDetails_crud")]
        public ResponseModel BillingDetails_crud([FromBody] BillingDetails BillingDetails)
        {
            TenantCaller newTenantCaller = new TenantCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                BillingDetails.Tennant_ID = authenticate.TenantId;
                BillingDetails.Created_By = authenticate.UserMasterID;
                BillingDetails.Modified_By = authenticate.UserMasterID;

                int result = newTenantCaller.BillingDetails_crud(new TenantService(_connectioSting), BillingDetails);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// OtherDetails
        /// </summary>
        /// <param name="OtherDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OtherDetails")]
        public ResponseModel OtherDetails([FromBody] OtherDetailsModel OtherDetails)
        {
            TenantCaller newTenantCaller = new TenantCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                OtherDetails._TenantID = authenticate.TenantId;
                OtherDetails._ModifiedBy = authenticate.UserMasterID;
                OtherDetails._Createdby = authenticate.UserMasterID;
                int result = newTenantCaller.OtherDetails(new TenantService(_connectioSting), OtherDetails);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch(Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// InsertPlanFeature
        /// </summary>
        /// <param name="PlanName"></param>
        /// <param name="FeatureID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertPlanFeature")]
        public ResponseModel InsertPlanFeature(string PlanName,string FeatureID)
        {
            TenantCaller newTenantCaller = new TenantCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                
                int result = newTenantCaller.InsertPlanFeature(new TenantService(_connectioSting), PlanName, FeatureID, authenticate.UserMasterID,authenticate.TenantId);

                StatusCode = result == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Plan Details
        /// </summary>
        /// <param name="CustomPlanID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPlanDetails")]
        public ResponseModel GetPlanDetails(int CustomPlanID)
        {
            List<GetPlanDetails> getPlanDetails = new List<GetPlanDetails>();
            TenantCaller newTenantCaller = new TenantCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                getPlanDetails = newTenantCaller.GetPlanDetails(new TenantService(_connectioSting), CustomPlanID, authenticate.TenantId);

                StatusCode =
                getPlanDetails.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = getPlanDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Company Type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCompany")]
        public ResponseModel GetCompanyType()
        {
            List<CompanyTypeModel> lstCompanyType = new List<CompanyTypeModel>();
            TenantCaller newTenantCaller = new TenantCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                lstCompanyType = newTenantCaller.GetCompanyType(new TenantService(_connectioSting));

                StatusCode =
                lstCompanyType.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = lstCompanyType;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        #region Get Registered Tenant

        /// <summary>
        /// Get Registered Tenant
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRegisteredTenant")]
        public ResponseModel GetRegisteredTenant()
        {
            List<CompanyModel> objcompanyModels = new List<CompanyModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                TenantCaller newtenantCaller = new TenantCaller();

                objcompanyModels = newtenantCaller.GetRegisteredTenant(new TenantService(_connectioSting), authenticate.TenantId);

                StatusCode =
                objcompanyModels.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objcompanyModels;

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