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
    public class KnowledgeBaseController : ControllerBase
    {

        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        public KnowledgeBaseController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
        }
        #endregion

        #region Methods
        /// <summary>
        /// search by Issue type ,category and subcategory
        /// </summary>
        /// <param name="Type_ID"></param>
        /// <param name="Category_ID"></param>
        /// <param name="SubCategor_ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchbycategory")]
        public ResponseModel searchbycategory(int Type_ID, int Category_ID, int SubCategor_ID)
        {

            List<KnowlegeBaseMaster> objKnowlegeBaseMaster = new List<KnowlegeBaseMaster>();
            KnowledgeCaller knowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objKnowlegeBaseMaster = knowledgeCaller.SearchByCategory(new KnowlegeBaseService(Cache, Db), Type_ID, Category_ID, SubCategor_ID, authenticate.TenantId);
                statusCode =
               objKnowlegeBaseMaster == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objKnowlegeBaseMaster;
            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Add new KB
        /// </summary>
        /// <param name="knowlegeBaseMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddKB")]
        public ResponseModel AddKB([FromBody]KnowlegeBaseMaster knowlegeBaseMaster)
        {

            KnowledgeCaller knowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                knowlegeBaseMaster.TenantID = authenticate.TenantId;
                knowlegeBaseMaster.CreatedBy = authenticate.UserMasterID;
                int result = knowledgeCaller.AddKB(new KnowlegeBaseService(Cache, Db), knowlegeBaseMaster);
                statusCode =
               result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update KB
        /// </summary>
        /// <param name="knowlegeBaseMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateKB")]
        public ResponseModel UpdateKB([FromBody]KnowlegeBaseMaster knowlegeBaseMaster)
        {

            KnowledgeCaller knowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                knowlegeBaseMaster.TenantID = authenticate.TenantId;
                knowlegeBaseMaster.ModifyBy = authenticate.UserMasterID;
                int result = knowledgeCaller.UpdateKB(new KnowlegeBaseService(Cache, Db), knowlegeBaseMaster);
                statusCode =
               result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Delete KB 
        /// </summary>
        /// <param name="KBID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteKB")]
        public ResponseModel DeleteKB(int KBID)
        {

            KnowledgeCaller knowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = knowledgeCaller.DeleteKB(new KnowlegeBaseService(Cache, Db), KBID, authenticate.TenantId);
                statusCode =
               result == 0 ?
                       (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get the list of KB
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("KBList")]
        public ResponseModel KBList()
        {

            CustomKBList objKnowlegeBaseMaster = new CustomKBList();

            KnowledgeCaller knowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objKnowlegeBaseMaster = knowledgeCaller.KBList(new KnowlegeBaseService(Cache, Db), authenticate.TenantId);

                statusCode =
               objKnowlegeBaseMaster == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objKnowlegeBaseMaster;
            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Search KB using categoryId, SubcategoryId, typeId
        /// </summary>
        /// <param name="Category_ID"></param>
        /// <param name="SubCategory_ID"></param>
        /// <param name="type_ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SearchKB")]
        public ResponseModel SearchKB(int Category_ID, int SubCategory_ID, int type_ID)
        {

            CustomKBList objKnowlegeBaseMaster = new CustomKBList();

            KnowledgeCaller knowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objKnowlegeBaseMaster = knowledgeCaller.SearchKB(new KnowlegeBaseService(Cache, Db), Category_ID, SubCategory_ID, type_ID, authenticate.TenantId);

                statusCode =
               objKnowlegeBaseMaster == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objKnowlegeBaseMaster;
            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get reject approve KB
        /// </summary>
        /// <param name="knowlegeBaseMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RejectApproveKB")]
        public ResponseModel RejectApproveKB([FromBody]KnowlegeBaseMaster knowlegeBaseMaster)
        {


            KnowledgeCaller knowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                knowlegeBaseMaster.TenantID = authenticate.TenantId;
                int result = knowledgeCaller.RejectApproveKB(new KnowlegeBaseService(Cache, Db), knowlegeBaseMaster);
                statusCode =
               result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception _ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }


        #endregion
    }
}
