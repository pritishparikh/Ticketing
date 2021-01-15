using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider.Store
{
    public class WebBotCaller
    {
        #region Variable Declaration
        private IWebBot _WebBot;
        #endregion

        #region Custom Methods

        /// <summary>
        /// Get WebBot Option
        /// </summary>
        /// <returns></returns>
        public async Task<List<HSWebBotModel>> GetWebBotOption(IWebBot webbot)
        {
            _WebBot = webbot;
            return await _WebBot.GetWebBotOption();
        }

        /// <summary>
        /// Get WebBot Filter Details By OptionID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="OptionID"></param>
        /// <returns></returns>
        public async Task<WebBotFilterByOptionID> GetWebBotFilterByOptionID(IWebBot webbot, int TenantID, int UserID, int OptionID)
        {
            _WebBot = webbot;
            return await _WebBot.GetWebBotFilterByOptionID( TenantID,  UserID,  OptionID);
        }

        /// <summary>
        /// Store WebBot Link
        /// </summary>
        /// <param name="WebBotContentRequest"></param>
        /// <returns></returns>
        public async Task<WebContentDetails> SendWebBotHSM(IWebBot webbot, WebBotContentRequest webBotcontentRequest)
        {
            _WebBot = webbot;
            return await _WebBot.SendWebBotHSM(webBotcontentRequest);
        }

        /// <summary>
        ///Update HSM Options
        /// </summary>
        /// <param name="ActiveOptions"></param>
        /// <param name="InActiveOptions"></param>
        /// <returns></returns>
        public async Task<int> UpdateHSMOptions(IWebBot webbot, string ActiveOptions, string InActiveOptions)
        {
            _WebBot = webbot;
            return await _WebBot.UpdateHSMOptions( ActiveOptions,  InActiveOptions);
        }
        #endregion
    }
}
