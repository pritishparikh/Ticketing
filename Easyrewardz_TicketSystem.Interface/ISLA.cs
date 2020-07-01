using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Brand
    /// </summary>
    public interface ISLA
    {
        /// <summary>
        /// Get SLA Status List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<SLAStatus> GetSLAStatusList(int TenantID);

        /// <summary>
        /// Insert SLA
        /// </summary>
        /// <param name="SLA"></param>
        /// <returns></returns>
        int InsertSLA(SLAModel SLA);

        /// <summary>
        /// Update SLA
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="SLAID"></param>
        /// <param name="IssuetypeID"></param>
        /// <param name="isActive"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        int UpdateSLA(int tenantID, int SLAID, int IssuetypeID, bool isActive, int modifiedBy);

        /// <summary>
        /// Delete SLA
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="SLAID"></param>
        /// <returns></returns>
        int DeleteSLA(int tenantID, int SLAID);

        /// <summary>
        /// SLA List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="SLAFor"></param>
        /// <returns></returns>
        //List<SLAResponseModel> SLAList(int tenantID, int pageNo, int PageSize);
        List<SLAResponseModel> SLAList(int tenantID, int SLAFor);

        /// <summary>
        /// Bind Issue Type List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        List<IssueTypeList> BindIssueTypeList(int tenantID, string SearchText);

        /// <summary>
        /// Search Issue Type
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        List<IssueTypeList> SearchIssueType(int tenantID, string SearchText);

        /// <summary>
        /// Bulk Upload SLA
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="SLAFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> BulkUploadSLA(int TenantID, int CreatedBy, int SLAFor,DataSet DataSetCSV);

        /// <summary>
        /// Get SLA Detail
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="SLAFor"></param>
        /// <returns></returns>
        SLADetail GetSLADetail(int tenantID, int SLAFor);

        /// <summary>
        /// Update SLA Details
        /// </summary>
        /// <param name="sLADetail"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        bool UpdateSLADetails(SLADetail sLADetail, int TenantID, int UserID);

    }
}
