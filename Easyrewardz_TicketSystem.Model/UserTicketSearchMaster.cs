using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class UserTicketSearchMaster
    {
        /// <summary>
        /// SearchParamID
        /// </summary>
        public int SearchParamID { get; set; }

        /// <summary>
        /// Id of the tenant
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// SearchParameters
        /// </summary>
        public string SearchParameters { get; set; }

        /// <summary>
        /// SearchName
        /// </summary>
        public string SearchName { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created date 
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Modified by 
        /// </summary>
        public int ModifyBy { get; set; }

        /// <summary>
        /// Modified ON
        /// </summary>
        public DateTime ModifyDate { get; set; }
    }
}
