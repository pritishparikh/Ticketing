using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Cache service
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// To check Key exist or not
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
    

        /// <summary>
        /// To set the key and value in cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        bool Set(IDistributedCache _cache, string key, string value);

        /// <summary>
        /// To get the value from the cache 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(IDistributedCache _cache,string key);

        /// <summary>
        /// Remove the key
        /// </summary>
        /// <param name="key"></param>
      

    }
}
