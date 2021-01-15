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
    public class SubCategoryController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion 

        #region Custom Methods
        public SubCategoryController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion 

        #region Custom Methods
        /// <summary>
        /// Get SubCategoryBy CategoryID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSubCategoryByCategoryID")]
        public ResponseModel GetSubCategoryByCategoryID(int CategoryID,int TypeId=0)
        {
            List<SubCategory> objSubCategory = new List<SubCategory>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller newMasterSubCat = new MasterCaller();

                objSubCategory = newMasterSubCat.GetSubCategoryByCategoryID(new SubCategoryService(_connectioSting), CategoryID,TypeId);

                StatusCode =
                objSubCategory.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objSubCategory;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Add Sub Category
        /// </summary>
        /// <param name="categoryID"></param>
        ///  <param name="SubcategoryName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddSubCategory")]
        public ResponseModel AddSubCategory(int categoryID,string SubcategoryName )
        {

            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                MasterCaller newMasterCategory = new MasterCaller();
                result = newMasterCategory.AddSubCategory(new SubCategoryService(_connectioSting), categoryID, SubcategoryName, authenticate.TenantId, authenticate.UserMasterID);
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
        /// Get SubCategory By Multi CategoryID
        /// </summary>
        /// <param name="CategoryIDs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSubCategoryByMultiCategoryID")]
        public ResponseModel GetSubCategoryByMultiCategoryID(string CategoryIDs)
        {
            List<SubCategory> objSubCategory = new List<SubCategory>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller newMasterSubCat = new MasterCaller();

                objSubCategory = newMasterSubCat.GetSubCategoryByMultiCategoryID(new SubCategoryService(_connectioSting), CategoryIDs);

                StatusCode =
                objSubCategory.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objSubCategory;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get SubCategory By Category On Search
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSubCategoryByCategoryOnSearch")]
        public ResponseModel GetSubCategoryByCategoryOnSearch(int CategoryID, string searchText)
        {
            List<SubCategory> objSubCategory = new List<SubCategory>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterSubCat = new MasterCaller();

                objSubCategory = newMasterSubCat.GetSubCategoryByCategoryOnSearch(new SubCategoryService(_connectioSting), authenticate.TenantId, CategoryID, searchText);

                StatusCode =
                objSubCategory.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objSubCategory;

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