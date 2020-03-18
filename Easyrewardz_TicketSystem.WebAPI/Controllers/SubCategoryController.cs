using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class SubCategoryController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion 

        #region Custom Methods
        public SubCategoryController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;
        }
        #endregion 

        #region Custom Methods
        /// <summary>
        /// Get SubCategoryBy CategoryID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSubCategoryByCategoryID")]
        public ResponseModel GetSubCategoryByCategoryID(int CategoryID)
        {
            List<SubCategory> objSubCategory = new List<SubCategory>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller _newMasterSubCat = new MasterCaller();

                objSubCategory = _newMasterSubCat.GetSubCategoryByCategoryID(new SubCategoryService(_Cache, Db), CategoryID);

                StatusCode =
                objSubCategory.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objSubCategory;

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

        /// <summary>
        /// Add Sub Category
        /// </summary>
        /// <param name="CategoryID"></param>
        ///  <param name="SubcategoryName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddSubCategory")]
        public ResponseModel AddSubCategory(int categoryID,string SubcategoryName )
        {

            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            int result = 0;
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                MasterCaller _newMasterCategory = new MasterCaller();
                result = _newMasterCategory.AddSubCategory(new SubCategoryService(_Cache, Db), categoryID, SubcategoryName, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
               result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
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
                _objResponseModel.Message = ex.Message;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }

        /// <summary>
        /// Get SubCategoryBy CategoryID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSubCategoryByMultiCategoryID")]
        public ResponseModel GetSubCategoryByMultiCategoryID(string CategoryIDs)
        {
            List<SubCategory> objSubCategory = new List<SubCategory>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller _newMasterSubCat = new MasterCaller();

                objSubCategory = _newMasterSubCat.GetSubCategoryByMultiCategoryID(new SubCategoryService(_Cache, Db), CategoryIDs);

                StatusCode =
                objSubCategory.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objSubCategory;

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
    }
}