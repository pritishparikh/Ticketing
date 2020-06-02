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
    }
}
