using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Easyrewardz_TicketSystem.MySqlDBContext;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class TicketingController : ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string _ticketAttachmentFolderName;
        #endregion

        #region Cunstructor
        public TicketingController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
            _ticketAttachmentFolderName = Configuration.GetValue<string>("TicketAttachment");
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Get Ticket Title Suggestion
        /// </summary>
        /// <param name="TikcketTitle"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettitlesuggestions")]
        public ResponseModel gettitlesuggestions(string TikcketTitle)
        {
            List<TicketTitleDetails> objTicketList = new List<TicketTitleDetails>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                TicketingCaller newTicket = new TicketingCaller();

                objTicketList = newTicket.GetAutoSuggestTicketList(new TicketingService(Cache, Db), TikcketTitle, authenticate.TenantId);
                statusCode =
                objTicketList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objTicketList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Create Ticket
        /// </summary>
        /// <param name="ticketingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createTicket")]
        public ResponseModel createTicket(IFormFile File)
        {
            TicketingDetails ticketingDetails = new TicketingDetails();
            var files = Request.Form.Files;
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            string fileName = "";
            string finalAttchment = "";
            string ResponseMessage = "";
            string ErrorResponseMessage = "";

            if (files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    fileName += files[i].FileName.Replace(".", timeStamp + ".") + ",";
                }
                finalAttchment = fileName.TrimEnd(',');
            }
            var Keys = Request.Form;
            ticketingDetails = JsonConvert.DeserializeObject<TicketingDetails>(Keys["ticketingDetails"]);

            List<TicketingDetails> objTicketList = new List<TicketingDetails>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                TicketingCaller newTicket = new TicketingCaller();

                ticketingDetails.TenantID = authenticate.TenantId;
                ticketingDetails.CreatedBy = authenticate.UserMasterID; ///Created  By from the token
                ticketingDetails.AssignedID = authenticate.UserMasterID;

                var exePath = Path.GetDirectoryName(System.Reflection
                     .Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                string Folderpath = appRoot + "\\" + _ticketAttachmentFolderName;

                int result = newTicket.addTicketDetails(new TicketingService(Cache, Db), ticketingDetails, authenticate.TenantId, Folderpath, finalAttchment);

                if (ticketingDetails.StatusID == 100)
                {
                    ResponseMessage = "Draft created successfully.";
                }
                else
                {
                    ResponseMessage = "Ticket created successfully.";
                }
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
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = ResponseMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Draft Details
        /// </summary>
        /// <param name="ticketingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDraftDetails")]
        public ResponseModel GetDraftDetails()
        {
            List<CustomDraftDetails> objDraftDetails = new List<CustomDraftDetails>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                int UserID = authenticate.UserMasterID;
                objDraftDetails = ticketCaller.GetDraft(new TicketingService(Cache, Db), UserID, authenticate.TenantId);
                statusCode =
                objDraftDetails.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objDraftDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// search Ticket Agent
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        /// <param name="DesignationID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchAgent")]
        public ResponseModel searchAgent(string FirstName, string LastName, string Email, int DesignationID)
        {
            List<CustomSearchTicketAgent> objSearchagent = new List<CustomSearchTicketAgent>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objSearchagent = ticketCaller.SearchAgent(new TicketingService(Cache, Db), FirstName, LastName, Email, DesignationID, authenticate.TenantId);
                statusCode =
                objSearchagent.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objSearchagent;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// List of Saved Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("listSavedSearch")]
        public ResponseModel listSavedSearch()
        {
            List<UserTicketSearchMaster> objSavedSearch = new List<UserTicketSearchMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                int userID = authenticate.UserMasterID;

                objSavedSearch = ticketCaller.ListSavedSearch(new TicketingService(Cache, Db), userID);
                statusCode =
                objSavedSearch.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objSavedSearch;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Saved Search By ID 
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getsavedsearchbyid")]
        public ResponseModel getsavedsearchbyid(int SearchParamID)
        {
            UserTicketSearchMaster objSavedSearchbyID = new UserTicketSearchMaster();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objSavedSearchbyID = ticketCaller.SavedSearchByID(new TicketingService(Cache, Db), SearchParamID);
                statusCode =
               objSavedSearchbyID == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objSavedSearchbyID;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Delete Saved Searcht
        /// </summary>
        /// <param name="ticketingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deletesavedsearch")]
        public ResponseModel deletesavedsearch(int SearchParamID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                int UserID = authenticate.UserMasterID;

                int result = ticketCaller.DeleteSavedSearch(new TicketingService(Cache, Db), SearchParamID, UserID);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// save search
        /// </summary>
        /// <param name="UserID"></param>
        /// /// <param name="SearchSaveName"></param>
        /// /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("savesearch")]
        public ResponseModel savesearch(string SearchSaveName, string parameter)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = ticketCaller.SaveSearch(new TicketingService(Cache, Db), authenticate.UserMasterID, SearchSaveName, parameter, authenticate.TenantId);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// Assign Tickets to Agent(User)
        /// </summary>
        /// <param name="UserID"></param>
        /// /// <param name="SearchSaveName"></param>
        /// /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AssignTickets")]
        public ResponseModel AssignTickets(string TicketID, int AgentID, string Remark)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                TicketingCaller ticketCaller = new TicketingCaller();

                int result = ticketCaller.AssignTicket(new TicketingService(Cache, Db), TicketID, authenticate.TenantId, authenticate.UserMasterID, AgentID, Remark);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// Schedule
        /// </summary>
        /// <param name="UserID"></param>
        /// /// <param name="SearchSaveName"></param>
        /// /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Schedule")]
        public ResponseModel Schedule([FromBody]ScheduleMaster scheduleMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                TicketingCaller ticketCaller = new TicketingCaller();

                int result = ticketCaller.Schedule(new TicketingService(Cache, Db), scheduleMaster, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                result >= 0 ?
                       (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// Export ToCSV
        /// </summary>
        /// <param name="UserID"></param>
        /// /// <param name="SearchSaveName"></param>
        /// /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExportToCSV")]
        public IActionResult ExportToCSV([FromBody] SearchRequest searchparams)
        {
            List<SearchResponse> searchResult = null;
            // string[] searchResult = null;
            ResponseModel objResponseModel = new ResponseModel();
            SearchCaller newsearchMaster = new SearchCaller();
            try
            {
                searchResult = newsearchMaster.GetSearchResults(new SearchService(Cache, Db), searchparams);
                string csv = ExportSearch(searchResult);
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "ABC.csv");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Export search
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        [NonAction]
        private string ExportSearch(IEnumerable<SearchResponse> objData)
        {
            return CommonService.ListToCSV(objData, "");
        }

        /// <summary>
        /// Get notes by ticket id
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getNotesByTicketId")]
        public ResponseModel getNotesByTicketId(int TicketId)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                TicketingCaller ticketCaller = new TicketingCaller();

                List<TicketNotes> result = ticketCaller.getNotesByTicketId(new TicketingService(Cache, Db), TicketId);
                statusCode =
                result.Count == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// Get ticket details by ticket id
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getTicketDetailsByTicketId")]
        public ResponseModel getTicketDetailsByTicketId(int ticketID)
        {
            CustomTicketDetail objTicketDetail = new CustomTicketDetail();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                string url = Configuration.GetValue<string>("APIURL") + _ticketAttachmentFolderName;
                objTicketDetail = ticketCaller.getTicketDetailsByTicketId(new TicketingService(Cache, Db), ticketID, authenticate.TenantId, url);
                statusCode =
               objTicketDetail == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objTicketDetail;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// submitticket
        /// </summary>
        /// <param name="status"></param>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Updateticketstatus")]
        public ResponseModel Updateticketstatus([FromBody]CustomTicketSolvedModel customTicketSolvedModel)
        {
            TicketingCaller ticketCaller = new TicketingCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = ticketCaller.submitticket(new TicketingService(Cache, Db), customTicketSolvedModel, authenticate.UserMasterID, authenticate.TenantId);
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Send Mail
        /// </summary>
        /// <param name="EmailID"></param>
        /// <param name="Mailcc"></param>
        /// <param name="Mailbcc"></param>
        /// <param name="Mailsubject"></param>
        /// <param name="MailBody"></param>
        /// <param name="informStore"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendMail")]
        public ResponseModel SendMail(string EmailID, string Mailcc, string Mailbcc, string Mailsubject, string MailBody, bool informStore, string storeID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            TicketingCaller ticketingCaller = new TicketingCaller();
            MasterCaller masterCaller = new MasterCaller();

            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                SMTPDetails sMTPDetails = masterCaller.GetSMTPDetails(new MasterServices(Cache, Db), authenticate.TenantId);

                CommonService commonService = new CommonService();

                bool isUpdate = ticketingCaller.SendMail(new TicketingService(Cache, Db), sMTPDetails, EmailID, Mailcc, Mailbcc, Mailsubject, MailBody, informStore, storeID, authenticate.TenantId);

                if (isUpdate)
                {
                    objResponseModel.Status = true;
                    objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                    objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                    objResponseModel.ResponseData = "Mail sent successfully.";
                }
                else
                {
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                    objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                    objResponseModel.ResponseData = "Mail sent failure.";
                }


            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// gettickethistory
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettickethistory")]
        public ResponseModel gettickethistory(int ticketID)
        {


            List<CustomTicketHistory> objTicketHistory = new List<CustomTicketHistory>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objTicketHistory = ticketCaller.getTickethistory(new TicketingService(Cache, Db), ticketID);
                statusCode =
               objTicketHistory == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objTicketHistory;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Count By ticketID
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCountByticketID")]
        public ResponseModel GetCountByticketID(int ticketID)
        {
            CustomCountByTicket objCountByTicket = new CustomCountByTicket();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                objCountByTicket = ticketCaller.GetCounts(new TicketingService(Cache, Db), ticketID);
                statusCode =
               objCountByTicket == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objCountByTicket;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// get ticket message
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getticketmessage")]
        public ResponseModel getticketmessage(int ticketID)
        {
            List<TicketMessage> objTicketMessage = new List<TicketMessage>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                string url = Configuration.GetValue<string>("APIURL") + _ticketAttachmentFolderName;
                objTicketMessage = ticketCaller.TicketMessage(new TicketingService(Cache, Db), ticketID, authenticate.TenantId, url);
                statusCode =
               objTicketMessage == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objTicketMessage;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get agent list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getagentlist")]
        public ResponseModel getagentlist(int TicketID)
        {
            List<CustomSearchTicketAgent> objSearchagent = new List<CustomSearchTicketAgent>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                objSearchagent = ticketCaller.AgentList(new TicketingService(Cache, Db), authenticate.TenantId, TicketID);
                statusCode =
                objSearchagent.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objSearchagent;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Message Comment
        /// </summary>
        /// <param name="TicketingMailerQue"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MessageComment")]
        public ResponseModel MessageComment(IFormFile File)
        {
            TicketingMailerQue ticketingMailerQue = new TicketingMailerQue();

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
            ticketingMailerQue = JsonConvert.DeserializeObject<TicketingMailerQue>(Keys["ticketingMailerQue"]);

            var exePath = Path.GetDirectoryName(System.Reflection
                    .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            string Folderpath = appRoot + "\\" + _ticketAttachmentFolderName;

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                TicketingCaller ticketCaller = new TicketingCaller();
                ticketingMailerQue.TenantID = authenticate.TenantId;
                ticketingMailerQue.CreatedBy = authenticate.UserMasterID;

                int result = ticketCaller.CommentticketDetail(new TicketingService(Cache, Db), ticketingMailerQue, finalAttchment);
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
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// Get Ticket Detail progress bar
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getprogressbardetail")]
        public ResponseModel getprogressbardetail(int TicketID)
        {
            ProgressBarDetail objProgressBarDetail = new ProgressBarDetail();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                TicketingCaller _Ticket = new TicketingCaller();

                objProgressBarDetail = _Ticket.GetProgressBarDetails(new TicketingService(Cache, Db), TicketID, authenticate.TenantId);
                statusCode =
                objProgressBarDetail == null ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objProgressBarDetail;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Ticket assign for followup
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="FollowUPUserID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ticketassigforfollowup")]
        public ResponseModel ticketassigforfollowup(int TicketID, string FollowUPUserID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                int UserID = authenticate.UserMasterID;
                ticketCaller.setticketassigforfollowup(new TicketingService(Cache, Db), TicketID, FollowUPUserID, authenticate.UserMasterID);
                //StatusCode =
                //objDraftDetails.Count == 0 ?
                //     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                //statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get tickets for followup
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getticketsforfollowup")]
        public ResponseModel getticketsforfollowup()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                int UserID = authenticate.UserMasterID;
                string ticketIds = ticketCaller.getticketsforfollowup(new TicketingService(Cache, Db), authenticate.UserMasterID);
                statusCode =
                string.IsNullOrEmpty(ticketIds) ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ticketIds;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Ticket unassign from followup
        /// </summary>
        /// <param name="TicketIDs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ticketunassigfromfollowup")]
        public ResponseModel ticketunassigfromfollowup(string TicketIDs)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller ticketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                int userID = authenticate.UserMasterID;
                bool isUpdate = ticketCaller.ticketunassigfromfollowup(new TicketingService(Cache, Db), TicketIDs, authenticate.UserMasterID);
                statusCode =
                isUpdate ?
                     (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("UpdateDraftTicket")]
        public ResponseModel UpdateDraftTicket()
        {
            TicketingDetails ticketingDetails = new TicketingDetails();

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
            ticketingDetails = JsonConvert.DeserializeObject<TicketingDetails>(Keys["ticketingDetails"]);


            List<TicketingDetails> objTicketList = new List<TicketingDetails>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                TicketingCaller newTicket = new TicketingCaller();

                ticketingDetails.TenantID = authenticate.TenantId;
                ticketingDetails.CreatedBy = authenticate.UserMasterID; ///Created  By from the token
                ticketingDetails.AssignedID = authenticate.UserMasterID;

                var exePath = Path.GetDirectoryName(System.Reflection
                     .Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                string Folderpath = appRoot + "\\" + _ticketAttachmentFolderName;

                int result = newTicket.UpdateDraftTicket(new TicketingService(Cache, Db), ticketingDetails, authenticate.TenantId, Folderpath, finalAttchment);

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

                                string path = Folderpath + "\\" + filesName[i];

                                bool fileExist = System.IO.File.Exists(path);
                                if (fileExist)
                                {
                                    System.IO.File.Delete(path);
                                }

                                FileStream docFile = new FileStream(path, FileMode.Create, FileAccess.Write);

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
                statusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = "Ticket updated successfully.";
                objResponseModel.ResponseData = result;
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
