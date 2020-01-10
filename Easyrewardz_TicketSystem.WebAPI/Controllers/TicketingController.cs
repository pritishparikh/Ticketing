using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Web;
namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class TicketingController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string _ticketAttachmentFolderName;
        #endregion

        #region Cunstructor
        public TicketingController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                TicketingCaller _newTicket = new TicketingCaller();

                objTicketList = _newTicket.GetAutoSuggestTicketList(new TicketingService(_connectioSting), TikcketTitle, authenticate.TenantId);
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
        public ResponseModel createTicket()
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                TicketingCaller _newTicket = new TicketingCaller();

                ticketingDetails.TenantID = authenticate.TenantId;
                ticketingDetails.CreatedBy = authenticate.UserMasterID; ///Created  By from the token
                ticketingDetails.AssignedID = authenticate.UserMasterID;

                var exePath = Path.GetDirectoryName(System.Reflection
                     .Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                string Folderpath = appRoot + "\\" + _ticketAttachmentFolderName;

                 int result = _newTicket.addTicketDetails(new TicketingService(_connectioSting), ticketingDetails, authenticate.TenantId, Folderpath, finalAttchment);
               
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                int UserID = authenticate.UserMasterID;
                objDraftDetails = _TicketCaller.GetDraft(new TicketingService(_connectioSting), UserID, authenticate.TenantId);
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                objSearchagent = _TicketCaller.SearchAgent(new TicketingService(_connectioSting), FirstName, LastName, Email, DesignationID, authenticate.TenantId);
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                int UserID = authenticate.UserMasterID;

                objSavedSearch = _TicketCaller.ListSavedSearch(new TicketingService(_connectioSting), UserID);
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                objSavedSearchbyID = _TicketCaller.SavedSearchByID(new TicketingService(_connectioSting), SearchParamID);
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                int UserID = authenticate.UserMasterID;

                int result = _TicketCaller.DeleteSavedSearch(new TicketingService(_connectioSting), SearchParamID, UserID);
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                int result = _TicketCaller.SaveSearch(new TicketingService(_connectioSting), authenticate.UserMasterID, SearchSaveName, parameter, authenticate.TenantId);
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                TicketingCaller _TicketCaller = new TicketingCaller();

                int result = _TicketCaller.AssignTicket(new TicketingService(_connectioSting), TicketID, authenticate.TenantId, authenticate.UserMasterID, AgentID, Remark);
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                TicketingCaller _TicketCaller = new TicketingCaller();

                int result = _TicketCaller.Schedule(new TicketingService(_connectioSting), scheduleMaster, authenticate.TenantId, authenticate.UserMasterID);
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
                _searchResult = _newsearchMaster.GetSearchResults(new SearchService(_connectioSting), searchparams);
                string csv = ExportSearch(_searchResult);
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "ABC.csv");
            }
            catch (Exception ex)
            {
                return RedirectToAction("", "");
            }
        }

        [NonAction]
        private string ExportSearch(IEnumerable<SearchResponse> objData)
        {
            return CommonService.ListToCSV(objData, "");
        }

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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                TicketingCaller _TicketCaller = new TicketingCaller();

                List<TicketNotes> result = _TicketCaller.getNotesByTicketId(new TicketingService(_connectioSting), TicketId);
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
        /// getTicketDetailsByTicketId
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                objTicketDetail = _TicketCaller.getTicketDetailsByTicketId(new TicketingService(_connectioSting), ticketID, authenticate.TenantId);
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
        public ResponseModel Updateticketstatus(int TicketID,int status)
        { 
            TicketingCaller _TicketCaller = new TicketingCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                int result = _TicketCaller.submitticket(new TicketingService(_connectioSting), TicketID, status, authenticate.UserMasterID,authenticate.TenantId);
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
        #endregion
    }
}
