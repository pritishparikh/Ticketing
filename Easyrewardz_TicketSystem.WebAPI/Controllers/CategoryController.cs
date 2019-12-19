using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CategoryController : ControllerBase
    {
        [HttpPost]
        [Route("GetCategoryList")]
        [AllowAnonymous]

        public List<Category> GetCategoryList(int TenantID)
        {
            List<Category> objCategoryList = new List<Category>();
            try
            {
                MasterCaller _newMasterCategory = new MasterCaller();

                objCategoryList = _newMasterCategory.GetCategoryList(new CategoryServices(), TenantID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objCategoryList;

        }
    }
}