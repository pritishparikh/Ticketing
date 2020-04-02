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
        /// <param name=""></param>
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
        /// <param name=""></param>
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
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
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
        public List<CustomHierarchymodel> ListStoreHierarchy(IStoreHierarchy Hierarchy, int TenantID)
        {
            _HierarchyRepository = Hierarchy;
            return _HierarchyRepository.ListStoreHierarchy(TenantID);
        }
        /// <summary>
        /// Store Hierarchy BulkUpload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        //public List<string> StoreHierarchyBulkUpload(IStoreHierarchy Hierarchy, int TenantID, int CreatedBy, DataSet DataSetCSV)
        //{
        //    _HierarchyRepository = Hierarchy;
        //    return _HierarchyRepository.BulkUploadStoreHierarchy(TenantID, CreatedBy, DataSetCSV);
        //}
        public List<DesignationMaster> GetDesignations(IStoreHierarchy  hierarchy, int TenantId)
        {
            _HierarchyRepository = hierarchy;
            return _HierarchyRepository.GetDesignations(TenantId);
        }
    }
}
