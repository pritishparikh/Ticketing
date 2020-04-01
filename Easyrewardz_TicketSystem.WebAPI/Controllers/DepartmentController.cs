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
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class DepartmentController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        private readonly string _UploadedBulkFile;
        #endregion

        #region Constructor
        public DepartmentController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _UploadedBulkFile = configuration.GetValue<string>("FileUploadLocation");
            rootPath = configuration.GetValue<string>("APIURL");
        }
        #endregion

        #region Custom Methods
        
        /// <summary>
        /// Get Store Details By BrandID
        /// </summary>
        /// <param name="BrandIDs"></param>
        ///  /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreCodeByBrandID")]
        public ResponseModel GetStoreCodeByBrandID(string BrandIDs)
        {
            List<StoreCodeModel> objStoreCodeModel = new List<StoreCodeModel>();
            ResponseModel objResponseModel = new ResponseModel();
            StoreCaller newStore = new StoreCaller();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));



                objStoreCodeModel = newStore.getStoreByBrandID(new StoreService(_connectioSting), BrandIDs, authenticate.TenantId);
                statusCode =
               objStoreCodeModel.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objStoreCodeModel;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

       

        /// <summary>
        /// Delete department Brand Mapping 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="DepartmentBrandMappingID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteBrandDepartmentMapping")]
        public ResponseModel DeleteBrandDepartmentMapping(int DepartmentBrandMappingID)
        {

            ResponseModel objResponseModel = new ResponseModel();
            DepartmentCaller newDept = new DepartmentCaller();
            int statusCode = 0;
            string statusMessage = "";
            int DeleteCount = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));



                DeleteCount = newDept.DeleteDepartmentMapping(new DepartmentService(_connectioSting), authenticate.TenantId, DepartmentBrandMappingID);
                statusCode =
                DeleteCount == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = DeleteCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Update department Brand Mapping 
        /// </summary>
        /// <param name="DepartmentBrandID"></param>
        /// <param name="BrandID"></param>
        /// <param name="StoreID"></param>
        /// <param name="DepartmentID"></param>
        /// <param name="FunctionID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateBrandDepartmentMapping")]
        public ResponseModel UpdateBrandDepartmentMapping(int DepartmentBrandID, int BrandID, int StoreID, int DepartmentID, int FunctionID, bool Status)
        {

            ResponseModel objResponseModel = new ResponseModel();
            DepartmentCaller newDept = new DepartmentCaller();
            int statusCode = 0;
            string statusMessage = "";
            int UpdateCount = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));



                UpdateCount = newDept.UpdateDepartmentMapping(new DepartmentService(_connectioSting), authenticate.TenantId,
                    DepartmentBrandID, BrandID, StoreID, DepartmentID, FunctionID, Status,authenticate.UserMasterID);
                statusCode =
                UpdateCount == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
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
