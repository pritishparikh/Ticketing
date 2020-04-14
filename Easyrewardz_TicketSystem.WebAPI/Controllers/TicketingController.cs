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

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   // [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
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
        public ResponseModel Gettitlesuggestions(string TikcketTitle)
        {
            List<TicketTitleDetails> objTicketList = new List<TicketTitleDetails>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                TicketingCaller newTicket = new TicketingCaller();

                objTicketList = newTicket.GetAutoSuggestTicketList(new TicketingService(_connectioSting), TikcketTitle, authenticate.TenantId);
                StatusCode =
                objTicketList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// <param name="File"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createTicket")]
        public ResponseModel CreateTicket(IFormFile File)
        {
            TicketingDetails ticketingDetails = new TicketingDetails();
            OrderMaster orderDetails = new OrderMaster();
            List<OrderItem> OrderItemDetails = new List<OrderItem>();
            List<StoreMaster> storeMaster = new List<StoreMaster>();
            List<string> ListStoreDetails = new List<string>();
            var files = Request.Form.Files;
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            string fileName = "";
            string finalAttchment = "";
            string ResponseMessage = "";
            int result = 0;
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

            // get order details from form
            orderDetails = JsonConvert.DeserializeObject<OrderMaster>(Keys["orderDetails"]);
            OrderItemDetails = JsonConvert.DeserializeObject<List<OrderItem>>(Keys["orderItemDetails"]);
            storeMaster = JsonConvert.DeserializeObject<List<StoreMaster>>(Keys["storeDetails"]);


            List<TicketingDetails> objTicketList = new List<TicketingDetails>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                TicketingCaller newTicket = new TicketingCaller();

                ticketingDetails.TenantID = authenticate.TenantId;
                ticketingDetails.CreatedBy = authenticate.UserMasterID; ///Created  By from the token
                ticketingDetails.AssignedID = authenticate.UserMasterID;

             

                var exePath = Path.GetDirectoryName(System.Reflection
                .Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                string Folderpath = appRoot + "\\" + _ticketAttachmentFolderName;


                #region check orderdetails and item details 

                if (orderDetails != null)
                {

                    if (orderDetails.OrderMasterID.Equals(0))
                    {

                        string OrderNumber = string.Empty;
                        string OrderItemsIds = string.Empty;
                        OrderMaster objorderMaster = null;

                        OrderCaller ordercaller = new OrderCaller();
                        //call insert order
                        orderDetails.CreatedBy = authenticate.UserMasterID;
                        OrderNumber = ordercaller.addOrder(new OrderService(_connectioSting), orderDetails, authenticate.TenantId);
                        if (!string.IsNullOrEmpty(OrderNumber))
                        {
                            objorderMaster = ordercaller.getOrderDetailsByNumber(new OrderService(_connectioSting), OrderNumber, authenticate.TenantId);


                            if (objorderMaster != null)
                            {
                                if(OrderItemDetails!=null)
                                {
                                    foreach (var item in OrderItemDetails)
                                    {
                                        item.OrderMasterID = objorderMaster.OrderMasterID;
                                        item.InvoiceDate = orderDetails.InvoiceDate;
                                    }

                                    OrderItemsIds = ordercaller.AddOrderItem(new OrderService(_connectioSting), OrderItemDetails, authenticate.TenantId, authenticate.UserMasterID);
                                   
                                }
                                else
                                {
                                    OrderItemsIds = Convert.ToString(objorderMaster.OrderMasterID) + "|0|1";
                                }

                            }

                            ticketingDetails.OrderMasterID = objorderMaster.OrderMasterID;
                            ticketingDetails.OrderItemID = OrderItemsIds;
                        }

                    }
                    

                }
                #endregion

                #region check Store details

                if (storeMaster != null)
                {
                  
                    if (storeMaster.Count > 0)
                    {
                        StoreCaller newStore = new StoreCaller();

                        foreach (var store in storeMaster)
                        {
                            if (store.StoreID.Equals(0))
                            {
                                int InsertedStoreID = 0;

                                store.BrandIDs = Convert.ToString(ticketingDetails.BrandID);

                                InsertedStoreID = newStore.AddStore(new StoreService(_connectioSting), store, authenticate.TenantId, authenticate.UserMasterID);
                                if (InsertedStoreID > 0)
                                {
                                    store.StoreVisitDate = string.IsNullOrEmpty(store.StoreVisitDate) ? "" : store.StoreVisitDate; 
                                    ListStoreDetails.Add(Convert.ToString(InsertedStoreID) + "|" + store.StoreVisitDate + "|" + store.Purpose);

                                }
                            }
                        }

                        ticketingDetails.StoreID = ListStoreDetails.Count > 0 ? string.Join(',', ListStoreDetails) : "";
                    }
                }


                #endregion

                    result = newTicket.addTicketDetails(new TicketingService(_connectioSting), ticketingDetails, authenticate.TenantId, Folderpath, finalAttchment);


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

               
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = ResponseMessage;
                objResponseModel.ResponseData = result; 
            }
            catch (Exception)
            {
                //if (ticketingDetails.StatusID == 100)
                //{
                //    ErrorResponseMessage = "Draft not created.";
                //}
                //else
                //{
                //    ErrorResponseMessage = "Ticket not created.";
                //}

                //objResponseModel.Status = false;
                //objResponseModel.StatusCode = StatusCode;
                //objResponseModel.Message = ErrorResponseMessage;
                //objResponseModel.ResponseData = null;

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
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int UserID = authenticate.UserMasterID;
                objDraftDetails = TicketCaller.GetDraft(new TicketingService(_connectioSting), UserID, authenticate.TenantId);
                StatusCode =
                objDraftDetails.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        public ResponseModel SearchAgent(string FirstName, string LastName, string Email, int DesignationID)
        {
            List<CustomSearchTicketAgent> objSearchagent = new List<CustomSearchTicketAgent>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                objSearchagent = TicketCaller.SearchAgent(new TicketingService(_connectioSting), FirstName, LastName, Email, DesignationID, authenticate.TenantId);
                StatusCode =
                objSearchagent.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// <returns></returns>
        [HttpPost]
        [Route("listSavedSearch")]
        public ResponseModel ListSavedSearch()
        {
            List<UserTicketSearchMaster> objSavedSearch = new List<UserTicketSearchMaster>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int UserID = authenticate.UserMasterID;

                objSavedSearch = TicketCaller.ListSavedSearch(new TicketingService(_connectioSting), UserID);
                StatusCode =
                objSavedSearch.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        public ResponseModel Getsavedsearchbyid(int SearchParamID)
        {
            UserTicketSearchMaster objSavedSearchbyID = new UserTicketSearchMaster();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objSavedSearchbyID = TicketCaller.SavedSearchByID(new TicketingService(_connectioSting), SearchParamID);
                StatusCode =
               objSavedSearchbyID == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// <param name="SearchParamID"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("deletesavedsearch")]
        public ResponseModel Deletesavedsearch(int SearchParamID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int UserID = authenticate.UserMasterID;

                int result = TicketCaller.DeleteSavedSearch(new TicketingService(_connectioSting), SearchParamID, UserID);
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
        /// save search
        /// </summary>
        /// <param name="SearchSaveName"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("savesearch")]
        public ResponseModel Savesearch(string SearchSaveName, string parameter)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = TicketCaller.SaveSearch(new TicketingService(_connectioSting), authenticate.UserMasterID, SearchSaveName, parameter, authenticate.TenantId);
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
        /// Assign Tickets to Agent(User)
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="AgentID"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AssignTickets")]
        public ResponseModel AssignTickets(string TicketID, int AgentID, string Remark)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                TicketingCaller TicketCaller = new TicketingCaller();

                int result = TicketCaller.AssignTicket(new TicketingService(_connectioSting), TicketID, authenticate.TenantId, authenticate.UserMasterID, AgentID, Remark);
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
        /// Schedule
        /// </summary>
        /// <param name="scheduleMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Schedule")]
        public ResponseModel Schedule([FromBody]ScheduleMaster scheduleMaster)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                TicketingCaller TicketCaller = new TicketingCaller();

                int result = TicketCaller.Schedule(new TicketingService(_connectioSting), scheduleMaster, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                result >= 0 ?
                       (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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
        /// Export ToCSV
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExportToCSV")]
        public IActionResult ExportToCSV([FromBody] SearchRequest searchparams)
        {
            List<SearchResponse> _searchResult = null;
            ResponseModel _objResponseModel = new ResponseModel();
            SearchCaller _newsearchMaster = new SearchCaller();
            try
            {
                _searchResult = _newsearchMaster.GetSearchResults(new SearchService(_connectioSting), searchparams);
                string csv = ExportSearch(_searchResult);
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "ABC.csv");
            }
            catch (Exception)
            {
                return RedirectToAction("", "");
            }
        }

        [NonAction]
        private string ExportSearch(IEnumerable<SearchResponse> objData)
        {
            return CommonService.ListToCSV(objData, "");
        }

        /// <summary>
        /// Ge tNotes By Ticket Id
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getNotesByTicketId")]
        public ResponseModel GetNotesByTicketId(int TicketId)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                TicketingCaller TicketCaller = new TicketingCaller();

                List<TicketNotes> result = TicketCaller.getNotesByTicketId(new TicketingService(_connectioSting), TicketId);
                StatusCode =
                result.Count == 0 ?
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
        /// Get Ticket Details By Ticket Id
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getTicketDetailsByTicketId")]
        public ResponseModel GetTicketDetailsByTicketId(int ticketID)
        {
            CustomTicketDetail objTicketDetail = new CustomTicketDetail();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                string url = configuration.GetValue<string>("APIURL") + _ticketAttachmentFolderName;
                objTicketDetail = TicketCaller.getTicketDetailsByTicketId(new TicketingService(_connectioSting), ticketID, authenticate.TenantId, url);
                StatusCode =
               objTicketDetail == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Update ticket status
        /// </summary>
        /// <param name="customTicketSolvedModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Updateticketstatus")]
        public ResponseModel Updateticketstatus([FromBody]CustomTicketSolvedModel customTicketSolvedModel)
        {
            TicketingCaller TicketCaller = new TicketingCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = TicketCaller.submitticket(new TicketingService(_connectioSting), customTicketSolvedModel, authenticate.UserMasterID, authenticate.TenantId);
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
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                SMTPDetails sMTPDetails = masterCaller.GetSMTPDetails(new MasterServices(_connectioSting), authenticate.TenantId);

                CommonService commonService = new CommonService();

                bool isUpdate = ticketingCaller.SendMail(new TicketingService(_connectioSting), sMTPDetails, EmailID, Mailcc, Mailbcc, Mailsubject, MailBody, informStore, storeID, authenticate.TenantId);

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
        /// Get ticket history
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettickethistory")]
        public ResponseModel Gettickethistory(int ticketID)
        {


            List<CustomTicketHistory> objTicketHistory = new List<CustomTicketHistory>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objTicketHistory = TicketCaller.getTickethistory(new TicketingService(_connectioSting), ticketID);
                StatusCode =
               objTicketHistory == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                objCountByTicket = TicketCaller.GetCounts(new TicketingService(_connectioSting), ticketID);
                StatusCode =
               objCountByTicket == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        public ResponseModel Getticketmessage(int ticketID)
        {
            List<TicketMessage> objTicketMessage = new List<TicketMessage>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                string url = configuration.GetValue<string>("APIURL") + _ticketAttachmentFolderName;
                objTicketMessage = TicketCaller.TicketMessage(new TicketingService(_connectioSting), ticketID, authenticate.TenantId, url);
                StatusCode =
               objTicketMessage == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// get agent list
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getagentlist")]
        public ResponseModel Getagentlist(int TicketID)
        {
            List<CustomSearchTicketAgent> objSearchagent = new List<CustomSearchTicketAgent>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objSearchagent = TicketCaller.AgentList(new TicketingService(_connectioSting), authenticate.TenantId, TicketID);
                StatusCode =
                objSearchagent.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// <param name="File"></param>
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
            int StatusCode = 0;
            string statusMessage = "";
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                TicketingCaller TicketCaller = new TicketingCaller();
                ticketingMailerQue.TenantID = authenticate.TenantId;
                ticketingMailerQue.CreatedBy = authenticate.UserMasterID;

                int result = TicketCaller.CommentticketDetail(new TicketingService(_connectioSting), ticketingMailerQue, finalAttchment);
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
        /// Get Ticket Detail progress bar
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getprogressbardetail")]
        public ResponseModel Getprogressbardetail(int TicketID)
        {
            ProgressBarDetail objProgressBarDetail = new ProgressBarDetail();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                TicketingCaller Ticket = new TicketingCaller();

                objProgressBarDetail = Ticket.GetProgressBarDetails(new TicketingService(_connectioSting), TicketID, authenticate.TenantId);
                StatusCode =
                objProgressBarDetail == null ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Ticke tassig for followup
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="FollowUPUserID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ticketassigforfollowup")]
        public ResponseModel Ticketassigforfollowup(int TicketID, string FollowUPUserID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                int UserID = authenticate.UserMasterID;
                TicketCaller.setticketassigforfollowup(new TicketingService(_connectioSting), TicketID, FollowUPUserID, authenticate.UserMasterID);
                //StatusCode =
                //objDraftDetails.Count == 0 ?
                //     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                //statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        public ResponseModel Getticketsforfollowup()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int UserID = authenticate.UserMasterID;
                string ticketIds = TicketCaller.getticketsforfollowup(new TicketingService(_connectioSting), authenticate.UserMasterID);
                StatusCode =
                string.IsNullOrEmpty(ticketIds) ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Ticket unassig from followup
        /// </summary>
        /// <param name="TicketIDs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ticketunassigfromfollowup")]
        public ResponseModel Ticketunassigfromfollowup(string TicketIDs)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller TicketCaller = new TicketingCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                int UserID = authenticate.UserMasterID;
                bool isUpdate = TicketCaller.ticketunassigfromfollowup(new TicketingService(_connectioSting), TicketIDs, authenticate.UserMasterID);
                StatusCode =
                isUpdate ?
                     (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Update Draft Ticket
        /// </summary>
        /// <returns></returns>
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
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                TicketingCaller newTicket = new TicketingCaller();

                ticketingDetails.TenantID = authenticate.TenantId;
                ticketingDetails.CreatedBy = authenticate.UserMasterID; ///Created  By from the token
                ticketingDetails.AssignedID = authenticate.UserMasterID;

                var exePath = Path.GetDirectoryName(System.Reflection
                     .Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                string Folderpath = appRoot + "\\" + _ticketAttachmentFolderName;

                int result = newTicket.UpdateDraftTicket(new TicketingService(_connectioSting), ticketingDetails, authenticate.TenantId, Folderpath, finalAttchment);

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
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
