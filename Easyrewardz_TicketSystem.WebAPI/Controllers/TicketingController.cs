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
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string _ticketAttachmentFolderName;
        #endregion

        #region Cunstructor
        public TicketingController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;
            _ticketAttachmentFolderName = configuration.GetValue<string>("TicketAttachment");
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                TicketingCaller _newTicket = new TicketingCaller();

                objTicketList = _newTicket.GetAutoSuggestTicketList(new TicketingService(_Cache, Db), TikcketTitle, authenticate.TenantId);
                StatusCode =
                objTicketList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objTicketList;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                TicketingCaller _newTicket = new TicketingCaller();

                ticketingDetails.TenantID = authenticate.TenantId;
                ticketingDetails.CreatedBy = authenticate.UserMasterID; ///Created  By from the token
                ticketingDetails.AssignedID = authenticate.UserMasterID;

                var exePath = Path.GetDirectoryName(System.Reflection
                     .Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                string Folderpath = appRoot + "\\" + _ticketAttachmentFolderName;

                int result = _newTicket.addTicketDetails(new TicketingService(_Cache, Db), ticketingDetails, authenticate.TenantId, Folderpath, finalAttchment);

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

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = "Ticket created successfully.";
                _objResponseModel.ResponseData = result;
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = false;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = "Ticket not created.";
                _objResponseModel.ResponseData = null;

            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                int UserID = authenticate.UserMasterID;
                objDraftDetails = _TicketCaller.GetDraft(new TicketingService(_Cache, Db), UserID, authenticate.TenantId);
                StatusCode =
                objDraftDetails.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objDraftDetails;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                objSearchagent = _TicketCaller.SearchAgent(new TicketingService(_Cache, Db), FirstName, LastName, Email, DesignationID, authenticate.TenantId);
                StatusCode =
                objSearchagent.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objSearchagent;
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
        /// List of Saved Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("listSavedSearch")]
        public ResponseModel listSavedSearch()
        {
            List<UserTicketSearchMaster> objSavedSearch = new List<UserTicketSearchMaster>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                int UserID = authenticate.UserMasterID;

                objSavedSearch = _TicketCaller.ListSavedSearch(new TicketingService(_Cache, Db), UserID);
                StatusCode =
                objSavedSearch.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objSavedSearch;
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
        /// Get Saved Search By ID 
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getsavedsearchbyid")]
        public ResponseModel getsavedsearchbyid(int SearchParamID)
        {
            UserTicketSearchMaster objSavedSearchbyID = new UserTicketSearchMaster();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                objSavedSearchbyID = _TicketCaller.SavedSearchByID(new TicketingService(_Cache, Db), SearchParamID);
                StatusCode =
               objSavedSearchbyID == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objSavedSearchbyID;
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
        /// Delete Saved Searcht
        /// </summary>
        /// <param name="ticketingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deletesavedsearch")]
        public ResponseModel deletesavedsearch(int SearchParamID)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                int UserID = authenticate.UserMasterID;

                int result = _TicketCaller.DeleteSavedSearch(new TicketingService(_Cache, Db), SearchParamID, UserID);
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
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;

            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                int result = _TicketCaller.SaveSearch(new TicketingService(_Cache, Db), authenticate.UserMasterID, SearchSaveName, parameter, authenticate.TenantId);
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
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;

            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                TicketingCaller _TicketCaller = new TicketingCaller();

                int result = _TicketCaller.AssignTicket(new TicketingService(_Cache, Db), TicketID, authenticate.TenantId, authenticate.UserMasterID, AgentID, Remark);
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
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;

            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                TicketingCaller _TicketCaller = new TicketingCaller();

                int result = _TicketCaller.Schedule(new TicketingService(_Cache, Db), scheduleMaster, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result >= 0 ?
                       (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;

            }
            return _objResponseModel;
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
            List<SearchResponse> _searchResult = null;
            // string[] _searchResult = null;
            ResponseModel _objResponseModel = new ResponseModel();
            SearchCaller _newsearchMaster = new SearchCaller();
            try
            {
                _searchResult = _newsearchMaster.GetSearchResults(new SearchService(_Cache, Db), searchparams);
                string csv = ExportSearch(_searchResult);
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "ABC.csv");
            }
            catch (Exception ex)
            {
                return RedirectToAction("", "");
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                TicketingCaller _TicketCaller = new TicketingCaller();

                List<TicketNotes> result = _TicketCaller.getNotesByTicketId(new TicketingService(_Cache, Db), TicketId);
                StatusCode =
                result.Count == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;

            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                string url = configuration.GetValue<string>("APIURL") + _ticketAttachmentFolderName;
                objTicketDetail = _TicketCaller.getTicketDetailsByTicketId(new TicketingService(_Cache, Db), ticketID, authenticate.TenantId, url);
                StatusCode =
               objTicketDetail == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objTicketDetail;

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
        /// submitticket
        /// </summary>
        /// <param name="status"></param>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Updateticketstatus")]
        public ResponseModel Updateticketstatus([FromBody]CustomTicketSolvedModel customTicketSolvedModel)
        {
            TicketingCaller _TicketCaller = new TicketingCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                int result = _TicketCaller.submitticket(new TicketingService(_Cache, Db), customTicketSolvedModel, authenticate.UserMasterID, authenticate.TenantId);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;

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
            ResponseModel _objResponseModel = new ResponseModel();
            TicketingCaller _ticketingCaller = new TicketingCaller();
            MasterCaller masterCaller = new MasterCaller();

            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                SMTPDetails sMTPDetails = masterCaller.GetSMTPDetails(new MasterServices(_Cache, Db), authenticate.TenantId);

                CommonService commonService = new CommonService();

                bool isUpdate = _ticketingCaller.SendMail(new TicketingService(_Cache, Db), sMTPDetails, EmailID, Mailcc, Mailbcc, Mailsubject, MailBody, informStore, storeID, authenticate.TenantId);

                if (isUpdate)
                {
                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                    _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                    _objResponseModel.ResponseData = "Mail sent successfully.";
                }
                else
                {
                    _objResponseModel.Status = false;
                    _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                    _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                    _objResponseModel.ResponseData = "Mail sent failure.";
                }


            }
            catch (Exception ex)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                _objResponseModel.ResponseData = "We had an error! Sorry about that.";
            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                objTicketHistory = _TicketCaller.getTickethistory(new TicketingService(_Cache, Db), ticketID);
                StatusCode =
               objTicketHistory == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objTicketHistory;

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
        /// Get Count By ticketID
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCountByticketID")]
        public ResponseModel GetCountByticketID(int ticketID)
        {
            CustomCountByTicket objCountByTicket = new CustomCountByTicket();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                objCountByTicket = _TicketCaller.GetCounts(new TicketingService(_Cache, Db), ticketID);
                StatusCode =
               objCountByTicket == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objCountByTicket;
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
        /// get ticket message
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getticketmessage")]
        public ResponseModel getticketmessage(int ticketID)
        {
            List<TicketMessage> objTicketMessage = new List<TicketMessage>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                string url = configuration.GetValue<string>("APIURL") + _ticketAttachmentFolderName;
                objTicketMessage = _TicketCaller.TicketMessage(new TicketingService(_Cache, Db), ticketID, authenticate.TenantId, url);
                StatusCode =
               objTicketMessage == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objTicketMessage;

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
        /// Get agent list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getagentlist")]
        public ResponseModel getagentlist()
        {
            List<CustomSearchTicketAgent> objSearchagent = new List<CustomSearchTicketAgent>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                objSearchagent = _TicketCaller.AgentList(new TicketingService(_Cache, Db), authenticate.TenantId);
                StatusCode =
                objSearchagent.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objSearchagent;
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

            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                TicketingCaller _TicketCaller = new TicketingCaller();
                ticketingMailerQue.TenantID = authenticate.TenantId;
                ticketingMailerQue.CreatedBy = authenticate.UserMasterID;

                int result = _TicketCaller.CommentticketDetail(new TicketingService(_Cache, Db), ticketingMailerQue, finalAttchment);
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

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;

            }
            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));

                TicketingCaller _Ticket = new TicketingCaller();

                objProgressBarDetail = _Ticket.GetProgressBarDetails(new TicketingService(_Cache, Db), TicketID, authenticate.TenantId);
                StatusCode =
                objProgressBarDetail == null ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objProgressBarDetail;
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
        /// Ticket assign for followup
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="FollowUPUserID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ticketassigforfollowup")]
        public ResponseModel ticketassigforfollowup(int TicketID, string FollowUPUserID)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                int UserID = authenticate.UserMasterID;
                _TicketCaller.setticketassigforfollowup(new TicketingService(_Cache, Db), TicketID, FollowUPUserID, authenticate.UserMasterID);
                //StatusCode =
                //objDraftDetails.Count == 0 ?
                //     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                //statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
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
        /// Get tickets for followup
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getticketsforfollowup")]
        public ResponseModel getticketsforfollowup()
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                int UserID = authenticate.UserMasterID;
                string ticketIds = _TicketCaller.getticketsforfollowup(new TicketingService(_Cache, Db), authenticate.UserMasterID);
                StatusCode =
                string.IsNullOrEmpty(ticketIds) ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = ticketIds;
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
        /// Ticket unassign from followup
        /// </summary>
        /// <param name="TicketIDs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ticketunassigfromfollowup")]
        public ResponseModel ticketunassigfromfollowup(string TicketIDs)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(_Cache, SecurityService.DecryptStringAES(_token));
                int UserID = authenticate.UserMasterID;
                bool isUpdate = _TicketCaller.ticketunassigfromfollowup(new TicketingService(_Cache, Db), TicketIDs, authenticate.UserMasterID);
                StatusCode =
                isUpdate ?
                     (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
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
