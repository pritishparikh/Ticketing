using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IConfiguration configuration;
        private readonly string _connectioSting;

        public CategoryController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
        }

        [HttpPost]
        [Route("GetCategoryList")]
        [AllowAnonymous]
        public List<Category> GetCategoryList(int TenantID)
        {
            List<Category> objCategoryList = new List<Category>();
            try
            {
                MasterCaller _newMasterCategory = new MasterCaller();

                objCategoryList = _newMasterCategory.GetCategoryList(new CategoryServices(_connectioSting), TenantID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objCategoryList;
        }
    }
}