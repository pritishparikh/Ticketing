using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreHierarchy
    {
        /// <summary>
        /// Create Store Hierarchy
        /// </summary>
        /// <param name="customHierarchymodel"></param>
        /// <param name=""></param>
        /// <returns></returns>
        int CreateStoreHierarchy(CustomHierarchymodel customHierarchymodel);

        /// <summary>
        /// Update Store Hierarchy
        /// </summary>
        /// <param name="customHierarchymodel"></param>
        /// <param name=""></param>
        /// <returns></returns>
        int UpdateStoreHierarchy(CustomHierarchymodel customHierarchymodel);

        /// <summary>
        /// Delete Store Hierarchy
        /// </summary>
        /// <param name="designationID"></param>
        /// <param name="usermasterID"></param>
        /// <param name="tenantID"></param>
        /// /// <returns></returns>
        int DeleteStoreHierarchy(int designationID,int userMasterID,int tenantID);

        /// <summary>
        /// List Store Hierarchy
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        List<CustomHierarchymodel> ListStoreHierarchy(int tenantID);

        /// <summary>
        /// Get designation list for the Designation dropdown
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<DesignationMaster> GetDesignations(int tenantID);

        /// <summary>
        /// Store Hierarchy BulkUpload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> BulkUploadStoreHierarchy(int tenantID, int createdBy, DataSet dataSetCSV);
    }
}
