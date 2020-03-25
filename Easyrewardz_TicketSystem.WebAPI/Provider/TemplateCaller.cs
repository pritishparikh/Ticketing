using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class TemplateCaller
    {
        #region Variable declaration
        public ITemplate _templateRepository;
        #endregion

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="_template"></param>
        /// <returns></returns>
        public List<Template> GetTemplateForNote(ITemplate _template, int IssueTypeId, int TenantID)
        {
            _templateRepository = _template;
            return _templateRepository.getTemplateForNote(IssueTypeId, TenantID);
        }

        public Template GetTemplateContent(ITemplate _template, int TemplateId, int TenantId)
        {
            _templateRepository = _template;
            return _templateRepository.getTemplateContent(TemplateId, TenantId);
        }
    }
}
