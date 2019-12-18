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

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        [HttpPost]
        [Route("GetBrandList")]
        [AllowAnonymous]

        public List<Brand> GetBrandList(int TenantID)
        {
            List<Brand> objBrandList = new List<Brand>();
            try
            {
                MasterCaller _newMasterBrand = new MasterCaller();

                objBrandList = _newMasterBrand.GetBrandList(new BrandServices(),TenantID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objBrandList;

        }
    }
}