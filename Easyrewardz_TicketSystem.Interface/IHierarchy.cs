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
        int CreateHierarchy(CustomHierarchymodel customHierarchymodel);
        List<CustomHierarchymodel> ListHierarchy(int TenantID,int HierarchyFor);

        List<string> BulkUploadHierarchy(int TenantID, int CreatedBy, int HierarchyFor, string FileName, DataSet DataSetCSV);
    }
}
