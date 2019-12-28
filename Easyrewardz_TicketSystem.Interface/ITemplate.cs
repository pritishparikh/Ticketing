using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ITemplate
    {
        List<Template> getTemplateForNote(int IssueTypeId, int TenantID);
    }
}
