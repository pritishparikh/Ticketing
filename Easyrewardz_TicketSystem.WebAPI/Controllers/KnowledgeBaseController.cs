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
    public class KnowledgeBaseController : ControllerBase
    {

        #region variable declaration
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Cunstructor
        public KnowledgeBaseController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;
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

            List<KnowlegeBaseMaster> _objKnowlegeBaseMaster = new List<KnowlegeBaseMaster>();
            KnowledgeCaller _KnowledgeCaller = new KnowledgeCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                _objKnowlegeBaseMaster = _KnowledgeCaller.SearchByCategory(new KnowlegeBaseService(_Cache, Db), Type_ID, Category_ID, SubCategor_ID, authenticate.TenantId);
                StatusCode =
               _objKnowlegeBaseMaster == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objKnowlegeBaseMaster;
            }
            catch (Exception _ex)
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
        /// Add new KB
        /// </summary>
        /// <param name="knowlegeBaseMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddKB")]
        public ResponseModel AddKB([FromBody]KnowlegeBaseMaster knowlegeBaseMaster)
        {

            KnowledgeCaller _KnowledgeCaller = new KnowledgeCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                knowlegeBaseMaster.TenantID = authenticate.TenantId;
                knowlegeBaseMaster.CreatedBy = authenticate.UserMasterID;
                int result = _KnowledgeCaller.AddKB(new KnowlegeBaseService(_Cache, Db), knowlegeBaseMaster);
                StatusCode =
               result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
            }
            catch (Exception _ex)
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
        /// Update KB
        /// </summary>
        /// <param name="knowlegeBaseMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateKB")]
        public ResponseModel UpdateKB([FromBody]KnowlegeBaseMaster knowlegeBaseMaster)
        {

            KnowledgeCaller _KnowledgeCaller = new KnowledgeCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                knowlegeBaseMaster.TenantID = authenticate.TenantId;
                knowlegeBaseMaster.ModifyBy = authenticate.UserMasterID;
                int result = _KnowledgeCaller.UpdateKB(new KnowlegeBaseService(_Cache, Db), knowlegeBaseMaster);
                StatusCode =
               result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
            }
            catch (Exception _ex)
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
        /// Delete KB 
        /// </summary>
        /// <param name="KBID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteKB")]
        public ResponseModel DeleteKB(int KBID)
        {

            KnowledgeCaller _KnowledgeCaller = new KnowledgeCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                int result = _KnowledgeCaller.DeleteKB(new KnowlegeBaseService(_Cache, Db), KBID, authenticate.TenantId);
                StatusCode =
               result == 0 ?
                       (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
            }
            catch (Exception _ex)
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
        /// Get the list of KB
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("KBList")]
        public ResponseModel KBList()
        {

            CustomKBList _objKnowlegeBaseMaster = new CustomKBList();

            KnowledgeCaller _KnowledgeCaller = new KnowledgeCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                _objKnowlegeBaseMaster = _KnowledgeCaller.KBList(new KnowlegeBaseService(_Cache, Db), authenticate.TenantId);

                StatusCode =
               _objKnowlegeBaseMaster == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objKnowlegeBaseMaster;
            }
            catch (Exception _ex)
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

            CustomKBList _objKnowlegeBaseMaster = new CustomKBList();

            KnowledgeCaller _KnowledgeCaller = new KnowledgeCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                _objKnowlegeBaseMaster = _KnowledgeCaller.SearchKB(new KnowlegeBaseService(_Cache, Db), Category_ID, SubCategory_ID, type_ID, authenticate.TenantId);

                StatusCode =
               _objKnowlegeBaseMaster == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objKnowlegeBaseMaster;
            }
            catch (Exception _ex)
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
        /// Get reject approve KB
        /// </summary>
        /// <param name="knowlegeBaseMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RejectApproveKB")]
        public ResponseModel RejectApproveKB([FromBody]KnowlegeBaseMaster knowlegeBaseMaster)
        {


            KnowledgeCaller _KnowledgeCaller = new KnowledgeCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                knowlegeBaseMaster.TenantID = authenticate.TenantId;
                int result = _KnowledgeCaller.RejectApproveKB(new KnowlegeBaseService(_Cache, Db), knowlegeBaseMaster);
                StatusCode =
               result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
            }
            catch (Exception _ex)
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
