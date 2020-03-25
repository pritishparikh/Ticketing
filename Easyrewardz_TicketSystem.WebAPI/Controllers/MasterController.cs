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
    public class MasterController : ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        public MasterController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
        }
        #endregion

        #region Custom Methods

        #region Channel of Purchase

        /// <summary>
        /// Get Channel Of PurchaseList
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChannelOfPurchaseList")]
        public ResponseModel GetChannelOfPurchaseList()
        {
            List<ChannelOfPurchase> objChannelPuerchaseList = new List<ChannelOfPurchase>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterChannel = new MasterCaller();
                objChannelPuerchaseList = newMasterChannel.GetChannelOfPurchaseList(new MasterServices(Cache, Db), authenticate.TenantId);
                statusCode =
                objChannelPuerchaseList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objChannelPuerchaseList;
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        #endregion

        #region Department

        /// <summary>
        /// Add Department  
        /// </summary>
        /// <param name="DepartmentName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddDepartment")]
        public ResponseModel AddDepartment( string DepartmentName)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            int result = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterBrand = new MasterCaller();

                result = newMasterBrand.AddDepartment(new MasterServices(Cache, Db), DepartmentName, authenticate.TenantId, authenticate.UserMasterID);

                statusCode =
                result == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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

        /// <summary>
        /// Add Function   
        /// </summary>
        /// <param name="DepartmentID"></param>
        ///  <param name="FunctionName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddFunction")]
        public ResponseModel AddFunction(int DepartmentID,string FunctionName)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            int result = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterBrand = new MasterCaller();

                result = newMasterBrand.AddFunction(new MasterServices(Cache, Db), DepartmentID, FunctionName, authenticate.TenantId, authenticate.UserMasterID);

                statusCode =
                result == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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

        /// <summary>
        /// Get Department List 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDepartmentList")]
        public ResponseModel getDepartmentList()
        {

            List<DepartmentMaster> objDepartmentList = new List<DepartmentMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterBrand = new MasterCaller();

                objDepartmentList = newMasterBrand.GetDepartmentListDetails(new MasterServices(Cache, Db), authenticate.TenantId);

                statusCode =
                objDepartmentList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objDepartmentList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Function Name By Department ID
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getFunctionNameByDepartmentId")]
        public ResponseModel getFunctionNameByDepartmentId(int DepartmentId)
        {
            List<FuncationMaster> objFunctionList = new List<FuncationMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterChannel = new MasterCaller();
                objFunctionList = newMasterChannel.GetFunctionbyDepartment(new MasterServices(Cache, Db), DepartmentId, authenticate.TenantId);
                statusCode =
                objFunctionList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objFunctionList;
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        #endregion

        #region Payment Mode

        /// <summary>
        /// Get Payment mode
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getPaymentMode")]
        public ResponseModel getPaymentMode()
        {
            List<PaymentMode> paymentModes = new List<PaymentMode>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller newMasterChannel = new MasterCaller();
                paymentModes = newMasterChannel.GetPaymentMode(new MasterServices(Cache, Db));
                statusCode =
                paymentModes.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = paymentModes;
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;

        }

        #endregion

        #region Ticket Sources 

        /// <summary>
        /// Get Ticket Sources
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getTicketSources")]
        public ResponseModel getTicketSources()
        {
            List<TicketSourceMaster> ticketSourceMasters = new List<TicketSourceMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller newMasterChannel = new MasterCaller();
                ticketSourceMasters = newMasterChannel.GetTicketSource(new MasterServices(Cache, Db));
                statusCode =
                ticketSourceMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ticketSourceMasters;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        #endregion

        #region State List 

        /// <summary>
        /// get state list 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getstatelist")]
        public ResponseModel getstatelist()
        {
            List<StateMaster> objStateList = new List<StateMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterBrand = new MasterCaller();

                objStateList = newMasterBrand.GetStatelist(new MasterServices(Cache, Db));

                statusCode =
                objStateList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStateList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        #endregion

        #region City List 

        /// <summary>
        /// get state list 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getcitylist")]
        public ResponseModel getcitylist(int StateId)
        {
            List<CityMaster> objCityList = new List<CityMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller _newMasterCity = new MasterCaller();

                objCityList = _newMasterCity.GetCitylist(new MasterServices(Cache, Db), StateId);

                statusCode =
                objCityList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCityList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        #endregion

        #region Region List 

        /// <summary>
        /// get region list 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("getregionlist")]
        public ResponseModel getregionlist()
        {
            List<RegionMaster> objRegionList = new List<RegionMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterRegion = new MasterCaller();

                objRegionList = newMasterRegion.GetRegionlist(new MasterServices(Cache, Db));

                statusCode =
                objRegionList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objRegionList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        #endregion

        #region StoreType List 

        /// <summary>
        /// get StoreType list 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("getstoretypelist")]
        public ResponseModel getstoretypelist()
        {
            List<StoreTypeMaster> objStoreTypeList = new List<StoreTypeMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterRegion = new MasterCaller();

                objStoreTypeList = newMasterRegion.GetStoreTypelist(new MasterServices(Cache, Db));

                statusCode =
                objStoreTypeList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStoreTypeList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Store Code With Store Name
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("GetStoreCodeWithStoreName")]
        public ResponseModel GetStoreCodeWithStoreName()
        {
            List<StoreTypeMaster> objStoreTypeList = new List<StoreTypeMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterRegion = new MasterCaller();

                objStoreTypeList = newMasterRegion.GetStoreNameWithStoreCode(new MasterServices(Cache, Db), authenticate.TenantId);

                statusCode =
                objStoreTypeList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStoreTypeList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }
        #endregion

        #region Get Language List
        /// <summary>
        /// Get language List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetLanguageList")]
        public ResponseModel GetLanguageList()
        {
            List<LanguageModel> objlanguagemodels = new List<LanguageModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                
                MasterCaller newMasterCaller = new MasterCaller();

                objlanguagemodels = newMasterCaller.GetLanguageList(new MasterServices(Cache, Db), authenticate.TenantId);

                statusCode =
                objlanguagemodels.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objlanguagemodels;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion
      
        #region Get  Country State City List
        /// <summary>
        ///Get Country State City List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCountryStateCityList")]
        public ResponseModel GetCountryStateCityList(string Pincode)
        {
            List<CommonModel> objcommonModels = new List<CommonModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCaller = new MasterCaller();

                objcommonModels = newMasterCaller.GetCountryStateCityList(new MasterServices(Cache, Db),authenticate.TenantId, Pincode);

                statusCode =
                objcommonModels.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objcommonModels;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion

        #region Create Department
        /// <summary>
        /// create new department
        /// </summary>
        /// <param name="createDepartmentModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateDepartment")]
        public ResponseModel CreateDepartment([FromBody] CreateDepartmentModel createDepartmentModel)
        {
            MasterCaller newMasterCaller = new MasterCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            int tid = 1;
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                createDepartmentModel.CreatedBy = authenticate.UserMasterID;
                //createDepartmentModel.TenantID = authenticate.TenantId;
                createDepartmentModel.TenantID = tid;

                int result = newMasterCaller.CreateDepartment(new MasterServices(Cache, Db), createDepartmentModel);

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

        #endregion
    }
}