using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    /// <summary>
    /// Enum Master
    /// </summary>
    public class EnumMaster
    {
        /// <summary>
        /// Status Code 
        /// </summary>
        public enum StatusCode
        {

            #region API Response Code

            /// <summary>
            /// Record Not Found
            /// </summary>
            [Description("Record Not Found")]
            RecordNotFound = 1001,

            [Description("Property IsEmpty Or Null")]
            PropertyIsEmptyOrNull = 1002,

            [Description("IncorrectDatatype")]
            IncorrectDatatype = 1003,

            [Description("Database Server not responding")]
            DatabaseServerNotResponding = 1004,

            [Description("Server memory exception ")]
            ServerMemoryException = 1005,

            [Description("Service unavailable")]
            ServiceUnavailable = 1006,

            [Description("Subscribed Package is expired")]
            SubscribedPackageIsExpired = 1007,

            [Description("Email Server is down")]
            EmailServerIsDown = 1008,

            [Description("Internalservice not working")]
            InternalServiceNotWorking = 1009,

            //[Description("Success")]
            //Success = 1010,



            #endregion

            #region HTTP Request/Response Code

            /// <summary>
            /// Success 
            /// </summary>
            [Description("Success")]
            Success = 200,

            [Description("We had an error! Sorry about that")]
            InternalServerError = 500,

            [Description("Request fulfilled, but no body")]
            RequestFulfilled, ButNoBody = 204,

            [Description("The request was formatted improperly")]
            TheRequestWasFormattedImproperly = 400,

            [Description(" API Key missing or invalid")]
            APIKeyMissingOrInvalid = 401,

            [Description("Insufficient permissions")]
            InsufficientPermissions = 403,

            [Description("The resource requested does not exist")]
            TheResourceRequestedDoesNotExist = 404,

            [Description("We had an error! Sorry about that.")]
            WehadanerrorSorryaboutthat = 404,

            #endregion

        }

        /// <summary>
        /// Ticekt Status
        /// </summary>
        public enum TicketStatus
        {
            /// <summary>
            ///TicketStatus - Draft
            /// </summary>
            [Description("Draft")]
            Draft = 100,

            /// <summary>
            ///TicketStatus - New
            /// </summary>
            [Description("New")]
            New = 101,

            /// <summary>
            ///TicketStatus - Open
            /// </summary>
            [Description("Open")]
            Open = 102,

            /// <summary>
            ///TicketStatus - Resolved
            /// </summary>
            [Description("Resolved")]
            Resolved = 103,

            /// <summary>
            ///TicketStatus - Closed
            /// </summary>
            [Description("Closed")]
            Closed = 104,

            /// <summary>
            ///TicketStatus - Re-Opened
            /// </summary>
            [Description("Re-Opened")]
            ReOpened = 105,

        }

        /// <summary>
        /// Ticekt Action
        /// </summary>
        public enum TicketAction
        {
            /// <summary>
            ///TicketStatus - Draft
            /// </summary>
            [Description("QC")]
            QC = 200,

            /// <summary>
            ///TicketStatus - New
            /// </summary>
            [Description("ETB")]
            ETB = 201

        }

        /// <summary>
        /// Claim Status
        /// </summary>
        public enum ClaimStatus
        {
            /// <summary>
            ///ClaimStatus - Draft
            /// </summary>
            [Description("New")]
            New = 210,

            /// <summary>
            ///ClaimStatus - Open
            /// </summary>
            [Description("Open/Pending")]
            Open = 211,

            /// <summary>
            ///ClaimStatus - Pending 
            /// </summary>
            [Description("Resolved")]
            Resolved = 212
        }



        /// <summary>
        /// Task Status
        /// </summary>
        public enum TaskStatus
        {
            /// <summary>
            ///TaskStatus - Draft
            /// </summary>
            [Description("New")]
            New = 220,

            /// <summary>
            ///TaskStatus - Open
            /// </summary>
            [Description("Open/Pending")]
            Open = 221,

            /// <summary>
            ///TaskStatus - Pending 
            /// </summary>
            [Description("Resolved")]
            Resolved = 222
        }

        /// <summary>
        /// Schedule
        /// </summary>
        public enum Schedule
        {
            /// <summary>
            ///Schedule - Daily
            /// </summary>
            [Description("Daily")]
            Daily = 230,

            /// <summary>
            ///Schedule - Weekly
            /// </summary>
            [Description("Weekly")]
            Weekly = 231,

            /// <summary>
            ///Schedule -  Same day each month 
            /// </summary>
            [Description("Samedayeachmonth")]
            Samedayeachmonth = 232,

            /// <summary>
            ///Schedule - Same Week Each Month 
            /// </summary>
            [Description("SameWeekEachMonth")]
            Sameweekeachmonth = 233,

            /// <summary>
            ///Schedule -  Same Day Each Year 
            /// </summary>
            [Description("SameDayEachYear")]
            Samedayeachyear = 234,

            /// <summary>
            ///Schedule -  Same Week Each Year 
            /// </summary>
            [Description("SameWeekEachYear")]
            Sameweekeachyear = 235,
        }

    }
}
