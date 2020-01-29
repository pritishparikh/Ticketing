using Easyrewardz_TicketSystem.CustomModel;
using System;
using System.Collections.Generic;
using System.Text;


namespace Easyrewardz_TicketSystem.Interface
{
    public interface IHierarchy
    {
        int CreateHierarchy(CustomHierarchymodel customHierarchymodel);
        List<CustomHierarchymodel> ListHierarchy(int TenantID,int HierarchyFor);
    }
}
