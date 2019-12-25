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
        /// </summary>
        /// <param name="_template"></param>
        /// <returns></returns>
        public List<Template> GetOrderItemList(ITemplate _template)
        {
            _templateRepository = _template;
            return _templateRepository.getTemplateForNote();
        }
    }
}
