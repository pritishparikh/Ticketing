using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ITemplate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IssueTypeId"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<Template> getTemplateForNote(int IssueTypeId, int TenantID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        Template getTemplateContent(int TemplateId, int TenantId);

        #region Settings Template

        /// <summary>
        /// Insert Template
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="TemplateName"></param>
        /// <param name="TemplatSubject"></param>
        /// <param name="TemplatBody"></param>
        /// <param name="issueTypes"></param>
        /// <param name="isTemplateActive"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        int InsertTemplate(int tenantId, string TemplateName, string TemplatSubject, string TemplatBody, string issueTypes, bool isTemplateActive, int createdBy);

        /// <summary>
        /// Delete Template
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        int DeleteTemplate(int tenantID, int TemplateID);

        /// <summary>
        /// Update Template
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="TemplateID"></param>
        /// <param name="TemplateName"></param>
        /// <param name="issueType"></param>
        /// <param name="isTemplateActive"></param>
        /// <param name="ModifiedBy"></param>
        /// <param name="templateSubject"></param>
        /// <param name="templateContent"></param>
        /// <returns></returns>
        int UpdateTemplate(int tenantId, int TemplateID, string TemplateName, string issueType, bool isTemplateActive, int ModifiedBy, string templateSubject, string templateContent);

        /// <summary>
        /// Get Templates
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        List<TemplateModel> GetTemplates(int tenantId );

        /// <summary>
        /// Get Mail Parameter
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="AlertID"></param>
        /// <returns></returns>
        List<MailParameterModel> GetMailParameter(int tenantId, int AlertID);


        #endregion
    }
}
