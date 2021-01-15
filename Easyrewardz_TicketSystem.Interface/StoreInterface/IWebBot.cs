using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IWebBot
    {
        /// <summary>
        /// Get WebBot Option
        /// </summary>
        /// <returns></returns>
        Task<List<HSWebBotModel>> GetWebBotOption();


        /// <summary>
        /// Get WebBot Filter Details By OptionID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="OptionID"></param>
        /// <returns></returns>
        Task<WebBotFilterByOptionID> GetWebBotFilterByOptionID(int TenantID, int UserID, int OptionID);




        /// <summary>
        ///Send WebBot HSM
        /// </summary>
        /// <param name="WebBotContentRequest"></param>
        /// <returns></returns>
        Task<WebContentDetails> SendWebBotHSM(WebBotContentRequest webBotcontentRequest);



        /// <summary>
        ///Update HSM Options
        /// </summary>
        /// <param name="ActiveOptions"></param>
        /// <param name="InActiveOptions"></param>
        /// <returns></returns>
        Task<int> UpdateHSMOptions(string ActiveOptions, string InActiveOptions);



    }
}
