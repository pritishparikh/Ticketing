using Easyrewardz_TicketSystem.Interface;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class RedisCacheService: ICacheService
    {
        //private readonly ISettings _settings;
        private readonly IDistributedCache Cache;
        private ConnectionMultiplexer connectionMultiplexer;

        public RedisCacheService(string radisCacheServerAddress)
        {
            var connection = radisCacheServerAddress;
            connectionMultiplexer = ConnectionMultiplexer.Connect(connection);
            //Cache = connectionMultiplexer.GetDatabase();
        }
        public RedisCacheService(IDistributedCache cache)
        {
            Cache = cache;
        }

        public bool Set(IDistributedCache Cache, string key, string data)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return false;
                }

                byte[] bytes = Encoding.ASCII.GetBytes(data);
                Cache.Set(key, bytes);
                return true;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
            
        }
        public string Get(IDistributedCache Cache, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            byte[] bytes = Cache.Get(key);
            if (bytes == null)
            {
                return null;
            }
            string name = Encoding.ASCII.GetString(bytes);
            return name;
        }

       
    }
}
