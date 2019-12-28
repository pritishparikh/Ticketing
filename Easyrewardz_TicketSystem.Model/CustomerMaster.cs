using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Csutomer master
    /// </summary>
    public class CustomerMaster
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Phone Number
        /// </summary>
        public string CustomerPhoneNumber { get; set; }

        /// <summary>
        /// Customer Email Id
        /// </summary>
        public string CustomerEmailId { get; set; }

        /// <summary>
        /// Gender Id
        /// </summary>
        public int GenderID { get; set; }

        /// <summary>
        /// Alternate Number
        /// </summary>
        public string AltNumber { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Alternate Email Id
        /// </summary>
        public string AltEmailID { get; set; }

        /// <summary>
        /// Active / In active
        /// </summary>
        public int IsActive { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public int ModifyBy { get; set; }

        /// <summary>
        /// Modfied Date
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// DOB format
        /// </summary>
        public string DOB { get; set; }

    }
}
