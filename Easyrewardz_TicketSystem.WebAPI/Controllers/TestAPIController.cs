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
        private readonly IDistributedCache Cache;
        public TestAPIController(IDistributedCache cache)
        {
            Cache = cache;
        }
        [HttpGet]
        [Route("values")]
        public ResponseModel values()
        {
          

            int statusCode = (int)EnumMaster.StatusCode.Success;
            string statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
            ResponseModel objResponseModel = new ResponseModel();
            objResponseModel.Status = true;
            objResponseModel.StatusCode = statusCode;
            objResponseModel.Message = statusMessage;
            
            objResponseModel.ResponseData = null;
            SetProgramDetails(Cache,"abcd","mangsh");
            GetProgramDetails(Cache, "abcd");

            return objResponseModel;
        }
        public static bool SetProgramDetails(IDistributedCache Cache, string programkey,string data)
        {
            if (string.IsNullOrEmpty(programkey))
            {
                return false;
            }

            byte[] bytes = Encoding.ASCII.GetBytes(data);
            Cache.Set("prgdtl:" + programkey, bytes);
            return true;
        }
        public static string GetProgramDetails(IDistributedCache Cache, string programkey)
        {
            if (string.IsNullOrEmpty(programkey))
            {
                return null;
            }
            byte[] bytes =Cache.Get("prgdtl:" + programkey);
            if (bytes == null)
            {
                return null;
            }
            string name= Encoding.ASCII.GetString(bytes);
            return name;
        }
    }
}