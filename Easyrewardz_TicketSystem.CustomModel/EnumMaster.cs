using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class EnumMaster
    {
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
    }
}
