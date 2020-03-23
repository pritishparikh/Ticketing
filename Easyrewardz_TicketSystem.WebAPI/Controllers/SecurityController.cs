using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SecurityController : ControllerBase
    {
        private IConfiguration Configuration;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }

        public SecurityController(IConfiguration _iConfig, TicketDBContext db, IDistributedCache cache)
        {
            Configuration = _iConfig;
            Db = db;
            Cache = cache;
        }
    }
}