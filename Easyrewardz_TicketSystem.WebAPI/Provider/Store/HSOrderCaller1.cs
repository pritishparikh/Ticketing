using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class HSOrderCaller
    {
        public IHSOrder _OrderRepository;

        public ModuleConfiguration GetModuleConfiguration(IHSOrder order, int tenantId, int userId, string programCode)
        {
            _OrderRepository = order;
            return _OrderRepository.GetModuleConfiguration(tenantId, userId, programCode);
        }

        public int UpdateModuleConfiguration(IHSOrder order, ModuleConfiguration moduleConfiguration, int modifiedBy)
        {
            _OrderRepository = order;
            return _OrderRepository.UpdateModuleConfiguration(moduleConfiguration, modifiedBy);
        }

        public OrderConfiguration GetOrderConfiguration(IHSOrder order, int tenantId, int userId, string programCode)
        {
            _OrderRepository = order;
            return _OrderRepository.GetOrderConfiguration(tenantId, userId, programCode);
        }

        public int UpdateOrderConfiguration(IHSOrder order, OrderConfiguration orderConfiguration, int modifiedBy)
        {
            _OrderRepository = order;
            return _OrderRepository.UpdateOrderConfiguration(orderConfiguration, modifiedBy);
        }

        public OrderDeliveredDetails GetOrderDeliveredDetails(IHSOrder order, int tenantId, int userId, OrderDeliveredFilterRequest orderDeliveredFilter)
        {
            _OrderRepository = order;
            return _OrderRepository.GetOrderDeliveredDetails(tenantId, userId, orderDeliveredFilter);
        }

        public List<OrderStatusFilter> GetOrderStatusFilter(IHSOrder order, int tenantId, int userId, int pageID)
        {
            _OrderRepository = order;
            return _OrderRepository.GetOrderStatusFilter(tenantId, userId, pageID);
        }

        public ShipmentAssignedDetails GetShipmentAssignedDetails(IHSOrder order, int tenantId, int userId, ShipmentAssignedFilterRequest shipmentAssignedFilter)
        {
            _OrderRepository = order;
            return _OrderRepository.GetShipmentAssignedDetails(tenantId, userId, shipmentAssignedFilter);
        }
    }
}
