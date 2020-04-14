using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreClaimController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _radisCacheServerAddress;
        private readonly string _connectionSting;
        private readonly string _ClaimProductImage;
        #endregion
        #region Constructor
        public StoreClaimController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _ClaimProductImage = configuration.GetValue<string>("RaiseClaimProductImage");
        }
        #endregion
        #region Custom Methods
        /// <summary>
        /// Raise Claim
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RaiseClaim")]
        public ResponseModel RaiseClaim(IFormFile File)
        {
            StoreClaimMaster storeClaimMaster = new StoreClaimMaster();

            var files = Request.Form.Files;
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            string fileName = "";
            string finalAttchment = "";

            if (files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    fileName += files[i].FileName.Replace(".", timeStamp + ".") + ",";
                }
                finalAttchment = fileName.TrimEnd(',');
            }
            var Keys = Request.Form;
            storeClaimMaster = JsonConvert.DeserializeObject<StoreClaimMaster>(Keys["storeClaimMaster"]);

            var exePath = Path.GetDirectoryName(System.Reflection
                    .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            string Folderpath = appRoot + "\\" + _ClaimProductImage;
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";

            try
            {
                StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                storeClaimMaster.TenantID = authenticate.TenantId;
                storeClaimMaster.CreatedBy = authenticate.UserMasterID;
                int result = storeClaimCaller.InsertRaiseClaim(new StoreClaimService(_connectionSting), storeClaimMaster, finalAttchment);
                if (result > 0)
                {
                    if (files.Count > 0)
                    {
                        string[] filesName = finalAttchment.Split(",");
                        for (int i = 0; i < files.Count; i++)
                        {
                            using (var ms = new MemoryStream())
                            {
                                files[i].CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                MemoryStream msfile = new MemoryStream(fileBytes);
                                FileStream docFile = new FileStream(Folderpath + "\\" + filesName[i], FileMode.Create, FileAccess.Write);
                                msfile.WriteTo(docFile);
                                docFile.Close();
                                ms.Close();
                                msfile.Close();
                                string s = Convert.ToBase64String(fileBytes);
                                byte[] a = Convert.FromBase64String(s);
                                // act on the Base64 data

                            }
                        }
                    }
                }
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
        /// Add Store Claim Comment
        /// </summary>
        /// <param name="CommentForId"></param>
        ///    <param name="ID"></param>
        ///   <param name="Comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddStoreClaimComment")]
        public ResponseModel AddStoreClaimComment(int ClaimID, string Comment)
        {
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = storeClaimCaller.AddClaimComment(new StoreClaimService(_connectionSting), ClaimID, Comment, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Claim Comment By ClaimID
        /// </summary>
        /// <param name="TaskId"></param>
        [HttpPost]
        [Route("GetClaimCommentByClaimID")]
        public ResponseModel GetClaimCommentByClaimID(int ClaimID)
        {
            List<UserComment> objClaimComment= new List<UserComment>();
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                objClaimComment = storeClaimCaller.GetClaimComment(new StoreClaimService(_connectionSting), ClaimID);
                statusCode =
                   objClaimComment == null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objClaimComment;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Claim List
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClaimList")]
        public ResponseModel GetClaimList()
        {
            List<CustomTaskMasterDetails> objtaskMaster = new List<CustomTaskMasterDetails>();
            StoreClaimCaller storeClaimCaller = new StoreClaimCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                objtaskMaster = storeClaimCaller.GetClaimList(new StoreClaimService(_connectionSting));
                StatusCode =
                   objtaskMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objtaskMaster;
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