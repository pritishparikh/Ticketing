using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreHierarchyCaller
    {
        #region Variable
        public IStoreHierarchy _HierarchyRepository;
        #endregion
        /// <summary>
        /// Create Store Hierarchy
        /// </summary>
        /// <param name="customHierarchymodel"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public int CreateStoreHierarchy(IStoreHierarchy Hierarchy, CustomHierarchymodel customHierarchymodel)
        {
            _HierarchyRepository = Hierarchy;
            return _HierarchyRepository.CreateStoreHierarchy(customHierarchymodel);
        }

        /// <summary>
        /// Update Store Hierarchy
        /// </summary>
        /// <param name="customHierarchymodel"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public int UpdateStoreHierarchy(IStoreHierarchy Hierarchy, CustomHierarchymodel customHierarchymodel)
        {
            _HierarchyRepository = Hierarchy;
            return _HierarchyRepository.UpdateStoreHierarchy(customHierarchymodel);
        }

        /// <summary>
        /// Delete Store Hierarchy
        /// </summary>
        /// <param name="designationID"></param>
        /// <param name="usermasterID"></param>
        /// <param name="tenantID"></param>
        /// /// <returns></returns>
        public int DeleteStoreHierarchy(IStoreHierarchy Hierarchy, int designationID, int usermasterID,int tenantID)
        {
            _HierarchyRepository = Hierarchy;
            return _HierarchyRepository.DeleteStoreHierarchy(designationID, usermasterID, tenantID);
        }

        /// <summary>
        /// List Store Hierarchy
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CustomHierarchymodel> ListStoreHierarchy(IStoreHierarchy Hierarchy, int tenantID)
        {
            _HierarchyRepository = Hierarchy;
            return _HierarchyRepository.ListStoreHierarchy(tenantID);
        }

        /// <summary>
        /// Store Hierarchy BulkUpload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        public List<string> StoreHierarchyBulkUpload(IStoreHierarchy Hierarchy, int tenantID, int createdBy, DataSet dataSetCSV)
        {
            _HierarchyRepository = Hierarchy;
            return _HierarchyRepository.BulkUploadStoreHierarchy(tenantID, createdBy, dataSetCSV);
        }

        /// <summary>
        /// Get designation list for the Designation dropdown
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<DesignationMaster> GetDesignations(IStoreHierarchy  hierarchy, int tenantId)
        {
            _HierarchyRepository = hierarchy;
            return _HierarchyRepository.GetDesignations(tenantId);
        }
    }
}
