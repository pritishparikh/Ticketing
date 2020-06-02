using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class ModuleConfiguration
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Programcode
        /// </summary>
        public string ProgramCode { get; set; }
        /// <summary>
        /// ShoppingBag
        /// </summary>
        public bool ShoppingBag { get; set; }
        /// <summary>
        /// Payment
        /// </summary>
        public bool Payment { get; set; }
        /// <summary>
        /// Shipment
        /// </summary>
        public bool Shipment { get; set; }
    }

    public class OrderConfiguration
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Programcode
        /// </summary>
        public string ProgramCode { get; set; }
        /// <summary>
        /// ShoppingBag
        /// </summary>
        public bool IntegratedSystem { get; set; }
        /// <summary>
        /// Payment
        /// </summary>
        public bool Payment { get; set; }
        /// <summary>
        /// Shipment
        /// </summary>
        public bool Shipment { get; set; }
    }
}
