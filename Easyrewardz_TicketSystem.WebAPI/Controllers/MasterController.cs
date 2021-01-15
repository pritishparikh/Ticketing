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
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class MasterController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string connectioSting;
        private readonly string radisCacheServerAddress;
        #endregion

        #region Cunstructor
        public MasterController(IConfiguration iConfig)
        {
            configuration = iConfig;
            connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods

        #region Channel of Purchase

        /// <summary>
        /// Get Channel Of PurchaseList
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChannelOfPurchaseList")]
        public ResponseModel GetChannelOfPurchaseList()
        {
            List<ChannelOfPurchase> objChannelPuerchaseList = new List<ChannelOfPurchase>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterChannel = new MasterCaller();
                objChannelPuerchaseList = newMasterChannel.GetChannelOfPurchaseList(new MasterServices(connectioSting), authenticate.TenantId);
                StatusCode =
                objChannelPuerchaseList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
               objResponseModel.Status = true;
               objResponseModel.StatusCode = StatusCode;
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
            int StatusCode = 0;
            int result = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterBrand = new MasterCaller();

                result = newMasterBrand.AddDepartment(new MasterServices(connectioSting), DepartmentName, authenticate.TenantId, authenticate.UserMasterID);

                StatusCode =
                result == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
            int StatusCode = 0;
            int result = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterBrand = new MasterCaller();

                result = newMasterBrand.AddFunction(new MasterServices(connectioSting), DepartmentID, FunctionName, authenticate.TenantId, authenticate.UserMasterID);

                StatusCode =
                result == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        /// Get Department List 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDepartmentList")]
        public async  Task<ResponseModel> getDepartmentList()
        {

            List<DepartmentMaster> objDepartmentList = new List<DepartmentMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                MasterCaller newMasterBrand = new MasterCaller();

                objDepartmentList = await newMasterBrand.GetDepartmentListDetails(new MasterServices(connectioSting), authenticate.TenantId, authenticate.UserMasterID);

                StatusCode =
                objDepartmentList.Count > 0 ?
                     (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

               objResponseModel.Status = true;
               objResponseModel.StatusCode = StatusCode;
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
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterChannel = new MasterCaller();
                objFunctionList = newMasterChannel.GetFunctionbyDepartment(new MasterServices(connectioSting), DepartmentId, authenticate.TenantId);
                StatusCode =
                objFunctionList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller _newMasterChannel = new MasterCaller();
                paymentModes = _newMasterChannel.GetPaymentMode(new MasterServices(connectioSting));
                StatusCode =
                paymentModes.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = paymentModes;
            }
            catch (Exception )
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
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller newMasterChannel = new MasterCaller();
                ticketSourceMasters = newMasterChannel.GetTicketSource(new MasterServices(connectioSting));
                StatusCode =
                ticketSourceMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
               objResponseModel.Status = true;
               objResponseModel.StatusCode = StatusCode;
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
        public ResponseModel getStateList()
        {
            List<StateMaster> objStateList = new List<StateMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller _newMasterBrand = new MasterCaller();

                objStateList = _newMasterBrand.GetStatelist(new MasterServices(connectioSting));

                StatusCode =
                objStateList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// <param name="StateId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getcitylist")]
        public ResponseModel getCityList(int StateId)
        {
            List<CityMaster> objCityList = new List<CityMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterCity = new MasterCaller();

                objCityList = newMasterCity.GetCitylist(new MasterServices(connectioSting), StateId);

                StatusCode =
                objCityList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCityList;
            }
            catch (Exception )
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
        public ResponseModel getRegionList()
        {
            List<RegionMaster> objRegionList = new List<RegionMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller _newMasterRegion = new MasterCaller();

                objRegionList = _newMasterRegion.GetRegionlist(new MasterServices(connectioSting));

                StatusCode =
                objRegionList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        public ResponseModel getStoreTypeList()
        {
            List<StoreTypeMaster> objStoreTypeList = new List<StoreTypeMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller _newMasterRegion = new MasterCaller();

                objStoreTypeList = _newMasterRegion.GetStoreTypelist(new MasterServices(connectioSting));

                StatusCode =
                objStoreTypeList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStoreTypeList;
            }
            catch (Exception )
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
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                MasterCaller _newMasterRegion = new MasterCaller();

                objStoreTypeList = _newMasterRegion.GetStoreNameWithStoreCode(new MasterServices(connectioSting), authenticate.TenantId);

                StatusCode =
                objStoreTypeList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                
                MasterCaller _newMasterCaller = new MasterCaller();

                objlanguagemodels = _newMasterCaller.GetLanguageList(new MasterServices(connectioSting), authenticate.TenantId);

                StatusCode =
                objlanguagemodels.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

               objResponseModel.Status = true;
               objResponseModel.StatusCode = StatusCode;
               objResponseModel.Message = statusMessage;
               objResponseModel.ResponseData = objlanguagemodels;

            }
            catch (Exception )
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion

        #region Get  Country State City List

        /// <summary>
        /// Get Country State City List
        /// </summary>
        /// <param name="Pincode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCountryStateCityList")]
        public ResponseModel GetCountryStateCityList(string Pincode)
        {
            List<CommonModel> objcommonModels = new List<CommonModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCaller = new MasterCaller();

                objcommonModels = newMasterCaller.GetCountryStateCityList(new MasterServices(connectioSting),authenticate.TenantId, Pincode);

                StatusCode =
                objcommonModels.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objcommonModels;

            }
            catch (Exception )
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion

        #region Create Department

        /// <summary>
        ///CreateDepartment
        /// </summary>
        /// <param name="CreateDepartmentModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateDepartment")]
        public ResponseModel CreateDepartment([FromBody] CreateDepartmentModel createDepartmentModel)
        {
            MasterCaller newMasterCaller = new MasterCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int tid = 1;
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                createDepartmentModel.CreatedBy = authenticate.UserMasterID;
                //createDepartmentModel.TenantID = authenticate.TenantId;
                createDepartmentModel.TenantID = tid;

                int result = newMasterCaller.CreateDepartment(new MasterServices(connectioSting), createDepartmentModel);

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
        #endregion

        #region Get LogedInEmail

        /// <summary>
        ///Get Logged in Email
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetLogedInEmail")]
        public ResponseModel GetLogedInEmail()
        {

            MasterCaller  masterCaller = new MasterCaller();
            CustomGetEmailID customGetEmailID = new CustomGetEmailID();
            ResponseModel  objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();

                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                customGetEmailID = masterCaller.GetLogedInEmail(new MasterServices(connectioSting), authenticate.UserMasterID, authenticate.TenantId);
                statusCode =
           customGetEmailID == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = customGetEmailID;

            }
            catch (Exception )
            {
                throw;
            }

            return objResponseModel;
        }
        #endregion

        #endregion
    }
}