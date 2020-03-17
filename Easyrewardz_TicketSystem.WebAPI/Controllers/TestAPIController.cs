using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

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
        /// 
        private readonly IDistributedCache _cache;
        public TestAPIController(IDistributedCache cache)
        {
            _cache = cache;
        }
        [HttpGet]
        [Route("values")]
        public ResponseModel values()
        {

            int StatusCode = (int)EnumMaster.StatusCode.Success;
            string statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
            ResponseModel _objResponseModel = new ResponseModel();
            _objResponseModel.Status = true;
            _objResponseModel.StatusCode = StatusCode;
            _objResponseModel.Message = statusMessage;
            
            _objResponseModel.ResponseData = null;
            SetProgramDetails(_cache,"abcd","mangsh");
            GetProgramDetails(_cache, "abcd");

            return _objResponseModel;
        }
        public static bool SetProgramDetails(IDistributedCache _cache, string programkey,string data)
        {
            if (string.IsNullOrEmpty(programkey))
            {
                return false;
            }

            byte[] bytes = Encoding.ASCII.GetBytes(data);
            _cache.Set("prgdtl:" + programkey, bytes);
            return true;
        }
        public static string GetProgramDetails(IDistributedCache _cache, string programkey)
        {
            if (string.IsNullOrEmpty(programkey))
            {
                return null;
            }
            byte[] bytes =_cache.Get("prgdtl:" + programkey);
            if (bytes == null)
            {
                return null;
            }
            string name= Encoding.ASCII.GetString(bytes);
            return name;
        }
    }
}