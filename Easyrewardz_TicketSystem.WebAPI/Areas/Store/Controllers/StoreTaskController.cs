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

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class StoreTaskController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _radisCacheServerAddress;
        private readonly string _connectionSting;
        #endregion
        #region Constructor
        public StoreTaskController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion
        #region Custom Methods
        /// <summary>
        /// Add Task
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateStoreTask")]
        public ResponseModel CreateStoreTask([FromBody]TaskMaster taskMaster)
        {
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = taskcaller.AddTask(new StoreTaskService(_connectionSting), taskMaster, authenticate.TenantId, authenticate.UserMasterID);
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
        /// Get Store Task List
        /// </summary>
        /// <param name="tabFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreTaskList")]
        public ResponseModel GetStoreTaskList(int tabFor)
        {
            List<CustomStoreTaskDetails> objtaskMaster = new List<CustomStoreTaskDetails>();
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objtaskMaster = taskcaller.GettaskList(new StoreTaskService(_connectionSting), tabFor, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   objtaskMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objtaskMaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Store Task By ID
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreTaskByID")]
        public ResponseModel GetStoreTaskByID(int TaskID)
        {
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            StoreTaskMaster storetaskmaster = new StoreTaskMaster();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                storetaskmaster = taskcaller.GetStoreTaskByID(new StoreTaskService(_connectionSting), TaskID, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode =
                storetaskmaster.TaskID == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = storetaskmaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Add Store Task Comment
        /// </summary>
        /// <param name="TaskComment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddStoreTaskComment")]
        public ResponseModel AddStoreTaskComment([FromBody]StoreTaskComment TaskComment)
        {
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                result = taskcaller.AddStoreTaskComment(new StoreTaskService(_connectionSting), TaskComment, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode = result == 0 ?
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
        /// Get Comment On Task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCommentOnTask")]
        public ResponseModel GetCommentOnTask(int TaskID)
        {
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            List<TaskCommentModel> TaskCommentList = new List<TaskCommentModel>();

            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                TaskCommentList = taskcaller.GetCommentOnTask(new StoreTaskService(_connectionSting), TaskID, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode = TaskCommentList.Count == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = TaskCommentList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Task History
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTaskHistory")]
        public ResponseModel GetTaskHistory(int TaskID)
        {
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            List<CustomTaskHistory> CustomTaskHistory = new List<CustomTaskHistory>();

            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CustomTaskHistory = taskcaller.GetTaskHistory(new StoreTaskService(_connectionSting), TaskID, authenticate.TenantId, authenticate.UserMasterID);
                StatusCode = CustomTaskHistory.Count == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CustomTaskHistory;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Task Status
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateTaskStatus")]
        public ResponseModel UpdateTaskStatus([FromBody]StoreTaskMaster taskMaster)
        {
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = taskcaller.SubmitTask(new StoreTaskService(_connectionSting), taskMaster, authenticate.UserMasterID, authenticate.TenantId);
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
        /// UserListDropdown
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserDropdown")]
        public ResponseModel UserDropdown(int TaskID, int TaskFor = 1)
        {
            List<CustomStoreUserList> objUserList = new List<CustomStoreUserList>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                StoreTaskCaller taskcaller = new StoreTaskCaller();

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objUserList = taskcaller.UserList(new StoreTaskService(_connectionSting), authenticate.TenantId, TaskID, TaskFor);
                StatusCode =
                objUserList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUserList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Assign Task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="AgentID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AssignTask")]
        public ResponseModel AssignTask(string TaskID, int AgentID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreTaskCaller taskcaller = new StoreTaskCaller();

                int result = taskcaller.AssignTask(new StoreTaskService(_connectionSting), TaskID, authenticate.TenantId, authenticate.UserMasterID, AgentID);
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
        /// Get Store Task By Ticket
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreTaskByTicket")]
        public ResponseModel GetStoreTaskByTicket()
        {
            List<CustomStoreTaskDetails> objtaskMaster = new List<CustomStoreTaskDetails>();
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objtaskMaster = taskcaller.GetStoreTaskByTicket(new StoreTaskService(_connectionSting), authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   objtaskMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objtaskMaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Store Ticketing Task By TaskID
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreTicketingTaskByTaskID")]
        public ResponseModel GetStoreTicketingTaskByTaskID(int TaskID)
        {
            StoreTaskWithTicket objtaskMaster = new StoreTaskWithTicket();
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objtaskMaster = taskcaller.GetStoreTicketingTaskByTaskID(new StoreTaskService(_connectionSting), TaskID, authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   objtaskMaster.StoreTaskMasterDetails.TicketID == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objtaskMaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Assigned To
        /// </summary>
        /// <param name="Function_ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAssignedTo")]
        public ResponseModel GetAssignedTo(int Function_ID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            List<CustomUserAssigned> objresult = new List<CustomUserAssigned>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreTaskCaller taskcaller = new StoreTaskCaller();

                objresult = taskcaller.GetAssignedTo(new StoreTaskService(_connectionSting), Function_ID);
                StatusCode =
                objresult.Count == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objresult;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Store Task ProcressBar
        /// </summary>
        /// <param name="TaskId"></param>
        /// <param name="TaskBy"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreTaskProcressBar")]
        public ResponseModel GetStoreTaskProcressBar(int TaskId, int TaskBy)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            List<StoreTaskProcressBar> objresult = new List<StoreTaskProcressBar>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreTaskCaller taskcaller = new StoreTaskCaller();

                objresult = taskcaller.GetStoreTaskProcressBar(new StoreTaskService(_connectionSting), TaskId, TaskBy);
                StatusCode =
                objresult.Count == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objresult;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Submit Task By Ticket
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SubmitTaskByTicket")]
        public ResponseModel SubmitTaskByTicket([FromBody]StoreTaskMaster taskMaster)
        {
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = taskcaller.SubmitTaskByTicket(new StoreTaskService(_connectionSting), taskMaster, authenticate.UserMasterID, authenticate.TenantId);
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
        /// Assign Task By Ticket
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AssignTaskByTicket")]
        public ResponseModel AssignTaskByTicket(string TaskID, int AgentID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                StoreTaskCaller taskcaller = new StoreTaskCaller();

                int result = taskcaller.AssignTaskByTicket(new StoreTaskService(_connectionSting), TaskID, authenticate.TenantId, authenticate.UserMasterID, AgentID);
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

        #region Campaign

        /// <summary>
        /// Get Store Campaign Customer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreCampaignCustomer")]
        public ResponseModel GetStoreCampaignCustomer()
        {
            List<StoreCampaign> objList = new List<StoreCampaign>();
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                objList = taskcaller.GetStoreCampaignCustomer(new StoreTaskService(_connectionSting), authenticate.TenantId, authenticate.UserMasterID);
                statusCode = (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Campaign Status Response
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCampaignStatusResponse")]
        public ResponseModel GetCampaignStatusResponse()
        {
            CampaignStatusResponse obj = new CampaignStatusResponse();
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                obj = taskcaller.GetCampaignStatusResponse(new StoreTaskService(_connectionSting), authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   obj.CampaignResponseList.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = obj;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Campaign Status Response
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCampaignStatusResponse")]
        public ResponseModel UpdateCampaignStatusResponse([FromBody]StoreCampaignCustomerRequest objRequest)
        {
            int result = 0;
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                result = taskcaller.UpdateCampaignStatusResponse(new StoreTaskService(_connectionSting), objRequest, authenticate.TenantId, authenticate.UserMasterID);
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
        /// Close Campaign
        /// </summary>
        /// <param name="CampaignTypeID"></param>
        /// <param name="IsClosed"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CloseCampaign")]
        public ResponseModel CloseCampaign(int CampaignTypeID, int IsClosed)
        {
            int result = 0;
            StoreTaskCaller taskcaller = new StoreTaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                result = taskcaller.CloseCampaign(new StoreTaskService(_connectionSting), CampaignTypeID, IsClosed, authenticate.TenantId, authenticate.UserMasterID);
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

        #endregion

        #endregion
    }
}