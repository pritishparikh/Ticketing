﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectionSting;
        #endregion

        #region Cunstructor
        public TaskController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectionSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
        }
        #endregion

        /// <summary>
        /// Add Task
        /// </summary>
        /// <param name="TaskMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createTask")]
        public ResponseModel createTask([FromBody]TaskMaster taskMaster)
        {
            TaskCaller _taskcaller = new TaskCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                int result = _taskcaller.AddTask(new TaskServices(_connectionSting), taskMaster);
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

        [HttpGet]
        [Route("gettaskdetailsbyid")]
        [AllowAnonymous]
        public ResponseModel gettaskdetailsbyid(int taskId)
        {

            TaskMaster _objtaskMaster = new TaskMaster();
            TaskCaller _taskcaller = new TaskCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                if (taskId > 0)
                {
                    _objtaskMaster = _taskcaller.gettaskDetailsById(new TaskServices(_connectionSting), taskId);
                    //StatusCode =
                    //_objtaskMaster.Count == 0 ?
                    // (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                    //statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = StatusCode;
                    _objResponseModel.Message = statusMessage;
                    _objResponseModel.ResponseData = _objtaskMaster;
                }
                else
                {
                    StatusCode = (int)EnumMaster.StatusCode.RecordNotFound;
                    statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = StatusCode;
                    _objResponseModel.Message = statusMessage;
                    _objResponseModel.ResponseData = null;
                }
            }
            catch (Exception _ex)
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
        [HttpGet]
        [Route("gettasklist")]
        public ResponseModel gettasklist()
        {
            List<TaskMaster> _objtaskMaster = new List<TaskMaster>();
            TaskCaller _taskcaller = new TaskCaller();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {        
                _objtaskMaster = _taskcaller.gettaskList(new TaskServices(_connectionSting));
                StatusCode =
                   _objtaskMaster.Count == 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);


                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = _objtaskMaster;
            }          
            catch (Exception _ex)
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


    }
}

