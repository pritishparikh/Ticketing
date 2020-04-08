﻿using Easyrewardz_TicketSystem.CustomModel;
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
        /// Get Task List
        /// </summary>
        /// <param name="TicketId"></param>
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

                objtaskMaster = taskcaller.gettaskList(new StoreTaskService(_connectionSting), tabFor, authenticate.TenantId, authenticate.UserMasterID);
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
        #endregion
    }
}