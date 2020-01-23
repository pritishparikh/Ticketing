using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class MasterController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Cunstructor
        public MasterController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                MasterCaller _newMasterChannel = new MasterCaller();
                objChannelPuerchaseList = _newMasterChannel.GetChannelOfPurchaseList(new MasterServices(_connectioSting), authenticate.TenantId);
                StatusCode =
                objChannelPuerchaseList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objChannelPuerchaseList;
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

        #region Department

        /// <summary>
        /// Get Department List 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getDepartmentList")]
        public ResponseModel getDepartmentList()
        {

            List<DepartmentMaster> _objDepartmentList = new List<DepartmentMaster>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                MasterCaller _newMasterBrand = new MasterCaller();

                _objDepartmentList = _newMasterBrand.GetDepartmentListDetails(new MasterServices(_connectioSting), authenticate.TenantId);

                StatusCode =
                _objDepartmentList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objDepartmentList;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = "Message" + Convert.ToString(ex.Message) + "Inner Exception" + Convert.ToString(ex.InnerException);
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                MasterCaller _newMasterChannel = new MasterCaller();
                objFunctionList = _newMasterChannel.GetFunctionbyDepartment(new MasterServices(_connectioSting), DepartmentId, authenticate.TenantId);
                StatusCode =
                objFunctionList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objFunctionList;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = "Message" + Convert.ToString(ex.Message) + "Inner Exception" + Convert.ToString(ex.InnerException);
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller _newMasterChannel = new MasterCaller();
                paymentModes = _newMasterChannel.GetPaymentMode(new MasterServices(_connectioSting));
                StatusCode =
                paymentModes.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = paymentModes;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller _newMasterChannel = new MasterCaller();
                ticketSourceMasters = _newMasterChannel.GetTicketSource(new MasterServices(_connectioSting));
                StatusCode =
                ticketSourceMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = ticketSourceMasters;
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
            List<StateMaster> _objStateList = new List<StateMaster>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                MasterCaller _newMasterBrand = new MasterCaller();

                _objStateList = _newMasterBrand.GetStatelist(new MasterServices(_connectioSting));

                StatusCode =
                _objStateList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objStateList;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = "Message" + Convert.ToString(ex.Message) + "Inner Exception" + Convert.ToString(ex.InnerException);
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
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
            List<CityMaster> _objCityList = new List<CityMaster>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                MasterCaller _newMasterCity = new MasterCaller();

                _objCityList = _newMasterCity.GetCitylist(new MasterServices(_connectioSting), StateId);

                StatusCode =
                _objCityList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objCityList;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = "Message" + Convert.ToString(ex.Message) + "Inner Exception" + Convert.ToString(ex.InnerException);
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
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
            List<RegionMaster> _objRegionList = new List<RegionMaster>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                MasterCaller _newMasterRegion = new MasterCaller();

                _objRegionList = _newMasterRegion.GetRegionlist(new MasterServices(_connectioSting));

                StatusCode =
                _objRegionList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objRegionList;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = "Message" + Convert.ToString(ex.Message) + "Inner Exception" + Convert.ToString(ex.InnerException);
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }

        #endregion

        #endregion

    }
}