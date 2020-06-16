using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ElasticErrorLogModel
    {
        public string applicationId { get; set; }
        public string actionName { get; set; }
        public string controllerName { get; set; }
        public string tenantID { get; set; }
        public string userID { get; set; }
        public string exceptions { get; set; }
        public string messageException { get; set; }
        public string ipAddress { get; set; }
    }
}
