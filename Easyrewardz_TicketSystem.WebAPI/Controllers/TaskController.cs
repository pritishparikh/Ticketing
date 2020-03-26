using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Easyrewardz_TicketSystem.MySqlDBContext;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class TaskController : ControllerBase
    {
        #region variable declaration
        private IConfiguration Configuration;

        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }

        #endregion

        #region Cunstructor
        public TaskController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
        }
        #endregion

        #region Custom Methods

        /// <summary>
        /// Add Task
        /// </summary>
        /// <param name="TaskMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createTask")]
        public ResponseModel createTask([FromBody]TaskMaster taskMaster)
        {
            TaskCaller taskCaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = taskCaller.AddTask(new TaskServices(Cache, Db), taskMaster, authenticate.TenantId, authenticate.UserMasterID);
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
        /// Get Task Detail By ID
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettaskdetailsbyid")]
        public ResponseModel gettaskdetailsbyid(int taskId)
        {

            CustomTaskMasterDetails objTaskMaster = new CustomTaskMasterDetails();
            TaskCaller taskCaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                objTaskMaster = taskCaller.gettaskDetailsById(new TaskServices(Cache, Db), taskId);
                statusCode =
               objTaskMaster == null ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objTaskMaster;
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
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gettasklist")]
        public ResponseModel gettasklist(int TicketId)
        {
            List<CustomTaskMasterDetails> objTaskMaster = new List<CustomTaskMasterDetails>();
            TaskCaller taskCaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                objTaskMaster = taskCaller.gettaskList(new TaskServices(Cache, Db), TicketId);
                statusCode =
                   objTaskMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objTaskMaster;
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
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deleteask")]
        public ResponseModel deletetask(int task_Id)
        {
            TaskCaller taskCaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                int result = taskCaller.DeleteTask(new TaskServices(Cache, Db), task_Id);
                statusCode =
               result == 0 ?
                      (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;

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
        /// Get Task List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getassignedto")]
        public ResponseModel getassignedto(int Function_ID)
        {
            List<CustomUserAssigned> objUserAssign = new List<CustomUserAssigned>();
            TaskCaller taskCaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                objUserAssign = taskCaller.GetAssignedTo(new TaskServices(Cache, Db), Function_ID);
                statusCode =
                   objUserAssign.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUserAssign;
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
        /// <param name="ID"></param>
        ///    <param name="TaskID"></param>
        ///   <param name="ClaimID"></param>
        ///   <param name="TicketID"></param>
        ///   <param name="Comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddComment")]
        //public ResponseModel AddComment(int Id, int TaskID, int ClaimID, int TicketID, string Comment)
        public ResponseModel AddComment(int CommentForId, int ID, string Comment)
        {
            TaskCaller taskCaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                int result = taskCaller.AddComment(new TaskServices(Cache, Db), CommentForId, ID, Comment, authenticate.UserMasterID);
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
        /// getclaimlist
        /// </summary>
        /// <param name="TicketId"></param>
        [HttpPost]
        [Route("getclaimlist")]
        public ResponseModel getclaimlist(int TicketId)
        {
            List<CustomClaimMaster> obClaimMaster = new List<CustomClaimMaster>();
            TaskCaller taskCaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                obClaimMaster = taskCaller.GetClaimList(new TaskServices(Cache, Db), TicketId);
                statusCode =
                   obClaimMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// getclaimlist
        /// </summary>
        /// <param name="TaskId"></param>
        [HttpPost]
        [Route("getTaskComment")]
        public ResponseModel getTaskComment(int TaskId)
        {
            List<UserComment> obClaimMaster = new List<UserComment>();
            TaskCaller taskCaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                obClaimMaster = taskCaller.GetTaskComment(new TaskServices(Cache, Db), TaskId);
                statusCode =
                   obClaimMaster == null ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// <returns></returns>
        [HttpPost]
        [Route("AddCommentOnTask")]
        public ResponseModel AddCommentOnTask(TaskMaster taskMaster)
        {
            TaskCaller taskCaller = new TaskCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                taskMaster.CreatedBy = authenticate.UserMasterID;

                int result = taskCaller.AddCommentOnTask(new TaskServices(Cache, Db), taskMaster);
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

        #endregion

    }
}