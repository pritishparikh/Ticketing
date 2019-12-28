using Easyrewardz_TicketSystem.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class RedisCacheService: ICacheService
    {
        //private readonly ISettings _settings;
        private readonly IDatabase _cache;
        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(string _radisCacheServerAddress)
        {
            var connection = _radisCacheServerAddress;
            _connectionMultiplexer = ConnectionMultiplexer.Connect(connection);
            _cache = _connectionMultiplexer.GetDatabase();
        }

        public bool Exists(string key)
        {
            return _cache.KeyExists(key);
        }

        public void Set(string key, string value)
        {
            _cache.StringSet(key, value);
        }

        public string Get(string key)
        {
            return _cache.StringGet(key);
        }

        public void Remove(string key)
        {
            _cache.KeyDelete(key);
        }
    }
}
