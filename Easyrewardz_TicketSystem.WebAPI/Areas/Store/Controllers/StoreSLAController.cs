using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.WebAPI.Provider.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreSLAController : ControllerBase
    {

        #region Variable Declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Constructor


        public StoreSLAController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }


        #endregion

        #region Custom Methods 


        /// <summary>
        ///Bind Issuetype for SLA Creation DropDown
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BindFunctions")]
        public ResponseModel BindFunctions(string SearchText)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<FunctionList> _objresponseModel = new List<FunctionList>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreSLACaller _newCRM = new StoreSLACaller();

                _objresponseModel = _newCRM.BindFunctionList(new StoreSLAService(_connectioSting), authenticate.TenantId, SearchText);
                StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Create SLA
        /// </summary>
        /// <param name="insertSLA"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateStoreSLA")]
        public ResponseModel CreateStoreSLA([FromBody]StoreSLAModel insertSLA)
        {
            int insertcount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreSLACaller _newSLA = new StoreSLACaller();

                insertSLA.TenantID = authenticate.TenantId;
                insertSLA.CreatedBy = authenticate.UserMasterID;
                insertcount = _newSLA.InsertStoreSLA(new StoreSLAService(_connectioSting), insertSLA);

                StatusCode =
                insertcount == 0 ?
                     (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = insertcount;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Create SLA
        /// </summary>
        /// <param name="insertSLA"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateStoreSLA")]
        public ResponseModel UpdateStoreSLA([FromBody]StoreSLAModel insertSLA)
        {
            int updatecount = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreSLACaller _newSLA = new StoreSLACaller();

                insertSLA.TenantID = authenticate.TenantId;
                insertSLA.CreatedBy = authenticate.UserMasterID;
                updatecount = _newSLA.UpdateStoreSLA(new StoreSLAService(_connectioSting), insertSLA);

                StatusCode =
                updatecount == 0 ?
                     (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = updatecount;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }


        /// <summary>
        /// Delete SLA
        /// </summary>
        /// <param name="SLAID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteStoreSLA")]
        public ResponseModel DeleteSLA(int SLAID)
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

                StoreSLACaller _newSLA = new StoreSLACaller();

                Deletecount = _newSLA.DeleteStoreSLA(new StoreSLAService(_connectioSting), authenticate.TenantId, SLAID);

                StatusCode =
                Deletecount == 0 ?
                     (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Deletecount;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }


        /// <summary>
        /// Get SLA
        /// </summary>
        /// <param name="SLAFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreSLA")]
        public ResponseModel GetStoreSLA()
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<StoreSLAResponseModel> _objresponseModel = new List<StoreSLAResponseModel>();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                StoreSLACaller _newCRM = new StoreSLACaller();
               
                _objresponseModel = _newCRM.StoreSLAList(new StoreSLAService(_connectioSting), authenticate.TenantId);
                StatusCode = _objresponseModel.Count == 0 ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objresponseModel;

            }
            catch (Exception)
            {
                throw;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Get Store SLA Detail
        /// </summary>
        /// <param name="SLAId"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("GetStoreSLADetail")]
        //public ResponseModel GetSLADetail(int SLAId)
        //{
            //ResponseModel _objResponseModel = new ResponseModel();
            //int StatusCode = 0;
            //string statusMessage = "";
            //StoreSLAResponseModel _objresponseModel = new StoreSLAResponseModel();
            //try
            //{
            //    string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
            //    Authenticate authenticate = new Authenticate();
            //    authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
            //    StoreSLACaller _newSLA = new StoreSLACaller();
            //    _objresponseModel = _newSLA.GetStoreSLADetail(new StoreSLAService(_connectioSting), authenticate.TenantId, SLAId);
            //    StatusCode = (int)EnumMaster.StatusCode.Success;
            //    statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
            //    _objResponseModel.Status = true;
            //    _objResponseModel.StatusCode = StatusCode;
            //    _objResponseModel.Message = statusMessage;
            //    _objResponseModel.ResponseData = _objresponseModel;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //return _objResponseModel;
        //}

        #endregion
    }
}
