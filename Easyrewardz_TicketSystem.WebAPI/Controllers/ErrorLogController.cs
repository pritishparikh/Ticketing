using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogController : ControllerBase
    {
        /// <summary>
        /// Return Exception
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ReturnException")]
        public ResponseModel ReturnException()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            statusCode = (int)EnumMaster.StatusCode.InternalServerError;
            statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
            objResponseModel.Status = true;
            objResponseModel.StatusCode = statusCode;
            objResponseModel.Message = statusMessage;
            objResponseModel.ResponseData = null;
            return objResponseModel;
        }
    }
}