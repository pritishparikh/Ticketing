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
    public class IssueTypeController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string connectioSting;
        private readonly string radisCacheServerAddress;
        #endregion

        #region Cunstructor
        public IssueTypeController(IConfiguration iConfig)
        {
            configuration = iConfig;
            connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get IssueTypeList
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetIssueTypeList")]
        public ResponseModel GetIssueTypeList(int SubCategoryID)
        {
            List<IssueType> objIssueTypeList = new List<IssueType>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                MasterCaller newMasterBrand = new MasterCaller();

                objIssueTypeList = newMasterBrand.GetIssueTypeList(new IssueTypeServices(connectioSting), authenticate.TenantId , SubCategoryID);

                StatusCode =
                objIssueTypeList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objIssueTypeList;

            }
            catch (Exception )
            {
                throw;
            }

            return objResponseModel;
        }
        /// <summary>
        /// Add Issue Type
        /// </summary>
        /// <param name="SubcategoryID"></param>
        /// <param name="IssuetypeName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddIssueType")]
        public ResponseModel AddIssueType(int SubcategoryID, string IssuetypeName)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                result = newMasterCategory.AddIssueType(new IssueTypeServices(connectioSting), SubcategoryID, IssuetypeName, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
               result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;

            }
            catch (Exception )
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetIssueTypeListByMultiSubCategoryID
        /// </summary>
        /// <param name="SubCategoryIDs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetIssueTypeListByMultiSubCategoryID")]
        public ResponseModel GetIssueTypeListByMultiSubCategoryID(string SubCategoryIDs)
        {
            List<IssueType> objIssueTypeList = new List<IssueType>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                MasterCaller newMasterBrand = new MasterCaller();

                objIssueTypeList = newMasterBrand.IssueTypeListByMultiSubCategoryID(new IssueTypeServices(connectioSting), authenticate.TenantId, SubCategoryIDs);

                StatusCode =
                objIssueTypeList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objIssueTypeList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get IssueType On Seach
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetIssueTypeOnSeach")]
        public ResponseModel GetIssueTypeOnSeach(int SubCategoryID,string searchText)
        {
            List<IssueType> objIssueTypeList = new List<IssueType>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                MasterCaller newMasterBrand = new MasterCaller();

                objIssueTypeList = newMasterBrand.GetIssueTypeOnSearch(new IssueTypeServices(connectioSting), authenticate.TenantId, SubCategoryID, searchText);

                StatusCode =
                objIssueTypeList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objIssueTypeList;

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