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
    public class KnowledgeBaseController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectionSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Cunstructor
        public KnowledgeBaseController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion
        /// <summary>
        /// search by Issue type ,category and subcategory
        /// </summary>
        /// <param name="Type_ID"></param>
        /// <param name="Category_ID"></param>
        /// <param name="SubCategor_ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchbycategory")]
        [AllowAnonymous]
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                _objKnowlegeBaseMaster = _KnowledgeCaller.SearchByCategory(new KnowlegeBaseService(_connectionSting), Type_ID, Category_ID, SubCategor_ID, authenticate.TenantId);
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
    }
}