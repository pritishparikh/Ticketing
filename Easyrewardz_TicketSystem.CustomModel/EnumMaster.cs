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

            #endregion

            #region HTTP Request/Response Code

            /// <summary>
            /// Success 
            /// </summary>
            [Description("Sucess")]
            Success = 200,

           
            [Description("We had an error! Sorry about that")]
            InternalServerError = 500

            #endregion

        }
    }
}
