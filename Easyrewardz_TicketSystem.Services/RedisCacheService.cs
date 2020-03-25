using Easyrewardz_TicketSystem.Interface;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class RedisCacheService: ICacheService
    {
        //private readonly ISettings _settings;
        private readonly IDistributedCache _Cache;
        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(string _radisCacheServerAddress)
        {
            var connection = _radisCacheServerAddress;
            _connectionMultiplexer = ConnectionMultiplexer.Connect(connection);
            //_Cache = _connectionMultiplexer.GetDatabase();
        }
        public RedisCacheService(IDistributedCache cache)
        {
            _Cache = cache;
        }

        public bool Set(IDistributedCache _Cache, string key, string data)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return false;
                }

                byte[] bytes = Encoding.ASCII.GetBytes(data);
                _Cache.Set(key, bytes);
                return true;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
            
        }
        public string Get(IDistributedCache _Cache, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            byte[] bytes = _Cache.Get(key);
            if (bytes == null)
            {
                return null;
            }
            string name = Encoding.ASCII.GetString(bytes);
            return name;
        }

       
    }
}
