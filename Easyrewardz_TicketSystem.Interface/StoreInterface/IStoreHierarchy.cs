using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreHierarchy
    {
        int CreateStoreHierarchy(CustomHierarchymodel customHierarchymodel);
        int UpdateStoreHierarchy(CustomHierarchymodel customHierarchymodel);
        int DeleteStoreHierarchy(int designationID,int userMasterID,int tenantID);
        List<CustomHierarchymodel> ListStoreHierarchy(int TenantID);
        List<DesignationMaster> GetDesignations(int TenantID);
        List<string> BulkUploadStoreHierarchy(int TenantID, int CreatedBy, DataSet DataSetCSV);
    }
}
