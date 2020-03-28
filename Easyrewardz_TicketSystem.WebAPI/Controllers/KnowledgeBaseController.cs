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
    public class KnowledgeBaseController : ControllerBase
    {

        #region variable declaration
        private IConfiguration configuration;
        private readonly string connectionSting;
        private readonly string radisCacheServerAddress;
        #endregion

        #region Cunstructor
        public KnowledgeBaseController(IConfiguration iConfig)
        {
            configuration = iConfig;
            connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            radisCacheServerAddress = configuration.GetValue<string>("radishCache");
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
        public ResponseModel SearchByCategory(int Type_ID, int Category_ID, int SubCategor_ID)
        {

            List<KnowlegeBaseMaster> objKnowlegeBaseMaster = new List<KnowlegeBaseMaster>();
            KnowledgeCaller KnowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objKnowlegeBaseMaster = KnowledgeCaller.SearchByCategory(new KnowlegeBaseService(connectionSting), Type_ID, Category_ID, SubCategor_ID, authenticate.TenantId);
                StatusCode =
               objKnowlegeBaseMaster == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

               objResponseModel.Status = true;
               objResponseModel.StatusCode = StatusCode;
               objResponseModel.Message = statusMessage;
               objResponseModel.ResponseData = objKnowlegeBaseMaster;
            }
            catch (Exception )
            {
                throw;
            }
            return objResponseModel;
        }

      
        [HttpPost]
        [Route("AddKB")]
        
        public ResponseModel AddKB([FromBody]KnowlegeBaseMaster knowlegeBaseMaster)
        {

            KnowledgeCaller KnowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                knowlegeBaseMaster.TenantID = authenticate.TenantId;
                knowlegeBaseMaster.CreatedBy = authenticate.UserMasterID;
                int result = KnowledgeCaller.AddKB(new KnowlegeBaseService(connectionSting), knowlegeBaseMaster);
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

        [HttpPost]
        [Route("UpdateKB")]
        public ResponseModel UpdateKB([FromBody]KnowlegeBaseMaster knowlegeBaseMaster)
        {

            KnowledgeCaller KnowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                knowlegeBaseMaster.TenantID = authenticate.TenantId;
                knowlegeBaseMaster.ModifyBy = authenticate.UserMasterID;
                int result = KnowledgeCaller.UpdateKB(new KnowlegeBaseService(connectionSting), knowlegeBaseMaster);
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

        [HttpPost]
        [Route("DeleteKB")]
        public ResponseModel DeleteKB(int KBID)
        {

            KnowledgeCaller KnowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                
                int result = KnowledgeCaller.DeleteKB(new KnowlegeBaseService(connectionSting), KBID, authenticate.TenantId);
                StatusCode =
               result == 0 ?
                       (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

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

        [HttpPost]
        [Route("KBList")]
        public ResponseModel KBList()
        {

            CustomKBList objKnowlegeBaseMaster = new CustomKBList();
           
            KnowledgeCaller KnowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objKnowlegeBaseMaster = KnowledgeCaller.KBList(new KnowlegeBaseService(connectionSting), authenticate.TenantId);
                
                StatusCode =
               objKnowlegeBaseMaster == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objKnowlegeBaseMaster;
            }
            catch (Exception )
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("SearchKB")]
        public ResponseModel SearchKB(int Category_ID, int SubCategory_ID, int type_ID)
        {

            CustomKBList objKnowlegeBaseMaster = new CustomKBList();

            KnowledgeCaller KnowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objKnowlegeBaseMaster = KnowledgeCaller.SearchKB(new KnowlegeBaseService(connectionSting), Category_ID, SubCategory_ID, type_ID, authenticate.TenantId);

                StatusCode =
               objKnowlegeBaseMaster == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objKnowlegeBaseMaster;
            }
            catch (Exception )
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("RejectApproveKB")]
        public ResponseModel RejectApproveKB([FromBody]KnowlegeBaseMaster knowlegeBaseMaster)
        {


            KnowledgeCaller KnowledgeCaller = new KnowledgeCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                knowlegeBaseMaster.TenantID = authenticate.TenantId;
                int result = KnowledgeCaller.RejectApproveKB(new KnowlegeBaseService(connectionSting), knowlegeBaseMaster);
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


        #endregion
    }
}
