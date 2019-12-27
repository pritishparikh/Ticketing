using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class DesignationMaster
    {
        /// <summary>
        /// Id of the designation
        /// </summary>
        public int DesignationID { get; set; }

        /// <summary>
        /// Id of the tenant
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Name of the designation
        /// </summary>
        public string DesignationName { get; set; }

        /// <summary>
        /// Report to designation
        /// </summary>
        public int ReportToDesignation { get; set; }

        /// <summary>
        /// Created BY
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
        public DateTime ModifiedDate { get; set; }

    }
}
