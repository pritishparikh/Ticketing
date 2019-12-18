using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTypeController : ControllerBase
    {
        [HttpPost]
        [Route("GetIssueTypeList")]
        [AllowAnonymous]

        public ResponseModel GetIssueTypeList(int TenantID)
        {
            List<IssueType> objIssueTypeList = new List<IssueType>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                MasterCaller _newMasterBrand = new MasterCaller();

                objIssueTypeList = _newMasterBrand.GetIssueTypeList(new IssueTypeServices(), TenantID);

                StatusCode =
                objIssueTypeList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objIssueTypeList;

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
    }
}