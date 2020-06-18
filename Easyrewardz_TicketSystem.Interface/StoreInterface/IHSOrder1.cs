using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IHSOrder
    {
        ModuleConfiguration GetModuleConfiguration(int tenantId, int userId, string programCode);

        int UpdateModuleConfiguration(ModuleConfiguration moduleConfiguration, int modifiedBy);

        OrderConfiguration GetOrderConfiguration(int tenantId, int userId, string programCode);

        int UpdateOrderConfiguration(OrderConfiguration orderConfiguration, int modifiedBy);

        OrderDeliveredDetails GetOrderDeliveredDetails(int tenantId, int userId, OrderDeliveredFilterRequest orderDeliveredFilter);

        List<OrderStatusFilter> GetOrderStatusFilter(int tenantId, int userId, int pageID);

        ShipmentAssignedDetails GetShipmentAssignedDetails(int tenantId, int userId, ShipmentAssignedFilterRequest shipmentAssignedFilter);

        int UpdateMarkAsDelivered(int tenantId, int userId, int orderID);

        int UpdateShipmentAssignedData(ShipmentAssignedRequest shipmentAssignedRequest, int tenantId, int userId, string programCode, string ClientAPIUrl);

        int UpdateShipmentBagCancelData(int shoppingID, string cancelComment, int userId);

        int UpdateShipmentPickupPendingData(int OrderID);

        int InsertOrderDetails(ConvertToOrder convertToOrder, int tenantId, int userId, string ProgramCode, string ClientAPIUrl);

        int UpdateAddressPending(AddressPendingRequest addressPendingRequest, int tenantId, int userId);

        OrderReturnsDetails GetOrderReturnDetails(int tenantId, int userId, OrderReturnsFilterRequest orderReturnsFilter);

        int UpdateShipmentAssignedDelivered(int orderID);

        int UpdateShipmentAssignedRTO(int orderID);

        PrintManifestResponse ShipmentAssignedPrintManifest(Int64 OrderIds, string ClientAPIURL);

        PrintLabelResponse ShipmentAssignedPrintLabel(Int64 OrderIds, string ClientAPIURL);
    }
}
