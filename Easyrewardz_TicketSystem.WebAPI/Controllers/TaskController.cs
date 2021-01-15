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
    public class TaskController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _radisCacheServerAddress;
        private readonly string _connectionSting;
        #endregion

        #region Cunstructor
        public TaskController(IConfiguration _iConfig)
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
        [Route("createTask")]
        public ResponseModel CreateTask([FromBody]TaskMaster taskMaster)
        {
            TaskCaller taskcaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = taskcaller.AddTask(new TaskServices(_connectionSting), taskMaster, authenticate.TenantId, authenticate.UserMasterID);
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
        /// Get Task Detail By ID
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettaskdetailsbyid")]
        public ResponseModel Gettaskdetailsbyid(int taskId)
        {

            CustomTaskMasterDetails objtaskMaster = new CustomTaskMasterDetails();
            TaskCaller taskcaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                objtaskMaster = taskcaller.gettaskDetailsById(new TaskServices(_connectionSting), taskId);
                StatusCode =
               objtaskMaster == null ?
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

        /// <summary>
        /// Get Task List
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettasklist")]
        public ResponseModel Gettasklist(int TicketId)
        {
            List<CustomTaskMasterDetails> objtaskMaster = new List<CustomTaskMasterDetails>();
            TaskCaller taskcaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                objtaskMaster = taskcaller.gettaskList(new TaskServices(_connectionSting), TicketId);
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


        /// <summary>
        ///Soft Delete Task
        /// </summary>
        /// <param name="task_Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deleteask")]
        public ResponseModel Deletetask(int task_Id)
        {
            TaskCaller taskcaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                int result = taskcaller.DeleteTask(new TaskServices(_connectionSting), task_Id);
                StatusCode =
               result == 0 ?
                      (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

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
        /// Get Task List
        /// </summary>
        /// <param name="Function_ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getassignedto")]
        public ResponseModel Getassignedto(int Function_ID)
        {
            List<CustomUserAssigned> objuserassign = new List<CustomUserAssigned>();
            TaskCaller taskcaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                objuserassign = taskcaller.GetAssignedTo(new TaskServices(_connectionSting), Function_ID);
                StatusCode =
                   objuserassign.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objuserassign;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Add Comment
        /// </summary>
        /// <param name="CommentForId"></param>
        ///    <param name="ID"></param>
        ///   <param name="Comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddComment")]
        public ResponseModel AddComment(int CommentForId, int ID, string Comment)
        {
            TaskCaller taskcaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = taskcaller.AddComment(new TaskServices(_connectionSting), CommentForId, ID, Comment, authenticate.UserMasterID);
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
        /// Get Claim List By TicketID
        /// </summary>
        /// <param name="ticketId"></param>
        [HttpPost]
        [Route("GetClaimListByTicketID")]
        public ResponseModel GetClaimListByTicketID(int ticketId)
        {
            List<CustomClaimMaster> obClaimMaster = new List<CustomClaimMaster>();
            TaskCaller taskcaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                obClaimMaster = taskcaller.GetClaimList(new TaskServices(_connectionSting), ticketId);
                StatusCode =
                   obClaimMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = obClaimMaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Task Comment
        /// </summary>
        /// <param name="TaskId"></param>
        [HttpPost]
        [Route("getTaskComment")]
        public ResponseModel GetTaskComment(int TaskId)
        {
            List<UserComment> obClaimMaster = new List<UserComment>();
            TaskCaller taskcaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                obClaimMaster = taskcaller.GetTaskComment(new TaskServices(_connectionSting), TaskId);
                StatusCode =
                   obClaimMaster == null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = obClaimMaster;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Add Comment ON Task
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddCommentOnTask")]
        public ResponseModel AddCommentOnTask(TaskMaster taskMaster)
        {
            TaskCaller taskcaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                taskMaster.CreatedBy = authenticate.UserMasterID;

                int result = taskcaller.AddCommentOnTask(new TaskServices(_connectionSting), taskMaster);
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

        #endregion

    }
}