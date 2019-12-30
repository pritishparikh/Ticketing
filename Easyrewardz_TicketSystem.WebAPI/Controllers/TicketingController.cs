using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class TicketingController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        #endregion

        #region Cunstructor
        public TicketingController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
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
        [AllowAnonymous]

        public ResponseModel gettitlesuggestions(string TikcketTitle)
        {
            List<TicketingDetails> objTicketList = new List<TicketingDetails>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _newTicket = new TicketingCaller();

                objTicketList = _newTicket.GetAutoSuggestTicketList(new TicketingService(_connectioSting), TikcketTitle);
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

        public ResponseModel createTicket([FromBody] TicketingDetails ticketingDetails)
        {
            List<TicketingDetails> objTicketList = new List<TicketingDetails>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _newTicket = new TicketingCaller();

                int result = _newTicket.addTicketDetails(new TicketingService(_connectioSting), ticketingDetails);
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
        /// Get Draft Details
        /// </summary>
        /// <param name="ticketingDetails"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("GetDraftDetails")]
        public ResponseModel GetDraftDetails(int UserID)
        {
            List<CustomDraftDetails> objDraftDetails = new List<CustomDraftDetails>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                objDraftDetails = _TicketCaller.GetDraft(new TicketingService(_connectioSting), UserID);
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

                objSearchagent = _TicketCaller.SearchAgent(new TicketingService(_connectioSting), FirstName, LastName, Email, DesignationID);
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
        public ResponseModel listSavedSearch(int UserID)
        {
            List<UserTicketSearchMaster> objSavedSearch = new List<UserTicketSearchMaster>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

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

        public ResponseModel deletesavedsearch(int SearchParamID, int UserID)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

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

        public ResponseModel savesearch(int UserID, string SearchSaveName, string parameter)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                TicketingCaller _TicketCaller = new TicketingCaller();

                int result = _TicketCaller.SaveSearch(new TicketingService(_connectioSting), UserID, SearchSaveName, parameter);
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
        #endregion
    }
}
