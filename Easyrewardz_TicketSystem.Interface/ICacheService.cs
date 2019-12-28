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
        bool Exists(string key);

        /// <summary>
        /// To set the key and value in cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, string value);

        /// <summary>
        /// To get the value from the cache 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// Remove the key
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

    }
}
