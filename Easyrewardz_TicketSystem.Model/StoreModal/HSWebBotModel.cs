using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
   public  class HSWebBotModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Options
        /// </summary>
        public string Option { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }
    }

    public class CountryCodeModel
    {
        /// <summary>
        /// ISO
        /// </summary>
        public string ISO { get; set; }

        /// <summary>
        /// PhoneCode
        /// </summary>
        public int PhoneCode { get; set; }

        /// <summary>
        /// CountryName
        /// </summary>
        public string CountryName { get; set; }

    }


    public class WebBotLinkRequest
    {
        
        public string programCode { get; set; }
        public string storeCode { get; set; }
        public string mobile { get; set; }
        public string wabaNumber { get; set; }
        public string shoppingBagNumber { get; set; } = "";
    }


    public class Data
    {
        public string shortUrl { get; set; }
    }

    public class WebBotLinkResponse
    {
        public Data data { get; set; }
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string appVersion { get; set; }
    }

    public class WebBotContentRequest
    {
        public int TenantID { get; set; }
        public string ProgramCode { get; set; }
        public int OptionID { get; set; }
        public int UserID { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public int ShopingBagNo { get; set; }
        public int? OrderID { get; set; }
        public string MakeBellActiveUrl { get; set; }
        public string ClientAPIUrl { get; set; }
        public string WeBBotGenerationLink { get; set; }
        public string MaxWebBotHSMURL { get; set; }
        public string WebBotLink { get; set; }
        public string WABANo { get; set; }
        
        public MaxWebBotHSMRequest MaxHSMRequest { get; set; }

        public WebBotHSMSetting webBotHSMSetting { get; set; }
    }


    public class WebContentDetails
    {
        public string MobileNo  { get; set; }
        public string TemplateName { get; set; }
        public string AdditionalInfo { get; set; }
        public string HSMText { get; set; }
        public string StoreCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ShoppingBagNo { get; set; }
        public string TemplateLanguage { get; set; }
        public bool MakebellActive { get; set; }
        public bool ChatActive { get; set; }
        public bool IsHSMSent { get; set; }
        public bool IsMaxClickCount { get; set; }
        public int ChatID  { get; set; }
    }


    #region MAX tenant webbot request/response

    public class MaxWebBotHSMRequest
    {
        public Body body { get; set; }
    }

    public class Language
    {
        public string policy { get; set; }
        public string code { get; set; }
    }

    public class LocalizableParam
    {
        public string @default { get; set; }
    }

    public class Hsm
    {
        public string @namespace { get; set; }
        public string element_name { get; set; }
        public Language language { get; set; }
        public List<LocalizableParam> localizable_params { get; set; }
    }

    public class Body
    {
        public string to { get; set; }
        public string from { get; set; }
        public int ttl { get; set; }
        public string type { get; set; }
        public Hsm hsm { get; set; }
    }

    public class MaxWebBotHSMResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

    #endregion


    public class WebBotFilterData
    {
        public int WABAId { get; set; }
        public string WABANo { get; set; }
    }

    public class WebBotFilterByOptionID
    {
        public List<WebBotFilterData> WebBotFilterDataList { get; set; }
        public List<object> WebBotFilter { get; set; }
    }

    public class WebBotHSMSetting
    {
        public string Programcode { get; set; }
        public string bot { get; set; }
        public string @namespace { get; set; }
    }

}
