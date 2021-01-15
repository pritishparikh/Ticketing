using Easyrewardz_TicketSystem.CustomModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IHierarchy
    {
        /// <summary>
        /// Create Hierarchy
        /// </summary>
        /// <param name="customHierarchymodel"></param>
        /// <returns></returns>
        int CreateHierarchy(CustomHierarchymodel customHierarchymodel);

        /// <summary>
        /// List Hierarchy
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="HierarchyFor"></param>
        /// <returns></returns>
        List<CustomHierarchymodel> ListHierarchy(int TenantID,int HierarchyFor);

        /// <summary>
        /// Bulk Upload Hierarchy
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="HierarchyFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> BulkUploadHierarchy(int TenantID, int CreatedBy, int HierarchyFor,  DataSet DataSetCSV);
    }
}
