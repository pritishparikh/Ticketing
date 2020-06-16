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

        public int UpdateMarkAsDelivered(IHSOrder order, int tenantId, int userId, int orderID)
        {
            _OrderRepository = order;
            return _OrderRepository.UpdateMarkAsDelivered(tenantId, userId, orderID);
        }

        public int UpdateShipmentAssignedData(IHSOrder order, ShipmentAssignedRequest shipmentAssignedRequest)
        {
            _OrderRepository = order;
            return _OrderRepository.UpdateShipmentAssignedData(shipmentAssignedRequest);
        }

        public int UpdateShipmentBagCancelData(IHSOrder order, int shoppingID, string cancelComment, int userId)
        {
            _OrderRepository = order;
            return _OrderRepository.UpdateShipmentBagCancelData(shoppingID, cancelComment, userId);
        }

        public int UpdateShipmentPickupPendingData(IHSOrder order, int OrderID)
        {
            _OrderRepository = order;
            return _OrderRepository.UpdateShipmentPickupPendingData(OrderID);
        }

        public int InsertOrderDetails(IHSOrder order, ConvertToOrder convertToOrder, int tenantId, int userId, string ProgramCode, string ClientAPIUrl)
        {
            _OrderRepository = order;
            return _OrderRepository.InsertOrderDetails(convertToOrder, tenantId, userId, ProgramCode, ClientAPIUrl);
        }

        public int UpdateAddressPending(IHSOrder order, AddressPendingRequest addressPendingRequest, int tenantId, int userId)
        {
            _OrderRepository = order;
            return _OrderRepository.UpdateAddressPending(addressPendingRequest, tenantId, userId);
        }

        public OrderReturnsDetails GetOrderReturnDetails(IHSOrder order, int tenantId, int userId, OrderReturnsFilterRequest orderReturnsFilter)
        {
            _OrderRepository = order;
            return _OrderRepository.GetOrderReturnDetails(tenantId, userId, orderReturnsFilter);
        }

        public int UpdateShipmentAssignedDelivered(IHSOrder order, int orderID)
        {
            _OrderRepository = order;
            return _OrderRepository.UpdateShipmentAssignedDelivered(orderID);
        }

        public int UpdateShipmentAssignedRTO(IHSOrder order, int orderID)
        {
            _OrderRepository = order;
            return _OrderRepository.UpdateShipmentAssignedRTO(orderID);
        }
    }
}
