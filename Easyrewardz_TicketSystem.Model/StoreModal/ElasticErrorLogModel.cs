using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ElasticErrorLogModel
    {
        /// <summary>
        /// application Id
        /// </summary>
        public string applicationId { get; set; }

        /// <summary>
        /// action Name
        /// </summary>
        public string actionName { get; set; }

        /// <summary>
        /// controller Name
        /// </summary>
        public string controllerName { get; set; }

        /// <summary>
        /// tenant ID
        /// </summary>
        public string tenantID { get; set; }

        /// <summary>
        /// user ID
        /// </summary>
        public string userID { get; set; }

        /// <summary>
        /// exceptions
        /// </summary>
        public string exceptions { get; set; }

        /// <summary>
        /// message Exception
        /// </summary>
        public string messageException { get; set; }

        /// <summary>
        /// ip Address
        /// </summary>
        public string ipAddress { get; set; }
    }
}
