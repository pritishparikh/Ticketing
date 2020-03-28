using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TestAPIController : ControllerBase
    {
        /// <summary>
        /// Testing API controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("values")]
        public ResponseModel Values()
        {
            int StatusCode = (int)EnumMaster.StatusCode.Success;
            string statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
            ResponseModel _objResponseModel = new ResponseModel();
            _objResponseModel.Status = true;
            _objResponseModel.StatusCode = StatusCode;
            _objResponseModel.Message = statusMessage;
            _objResponseModel.ResponseData = null;

            return _objResponseModel;
        }
    }
}