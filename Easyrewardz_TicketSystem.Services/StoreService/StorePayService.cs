using ClientAPIServiceCall;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public class StorePayService : IStorePay
    {
        MySqlConnection conn = new MySqlConnection();
        private readonly GeneratePaymentLinkHttpClientService _generatePaymentLinkHttpClientService;
        private readonly GenerateToken _generateToken;
        private readonly ILogger<object> _logger;

        public StorePayService(string _connectionString, GeneratePaymentLinkHttpClientService generatePaymentLinkHttpClientService =null, GenerateToken generateToken=null,
            ILogger<object> logger = null)
        {
            conn.ConnectionString = _connectionString;
            _generatePaymentLinkHttpClientService = generatePaymentLinkHttpClientService;
            _generateToken = generateToken;
            _logger = logger;
        }


        /// <summary>
        /// Generate Store Pay Link
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        ///  <param name="clientAPIUrlForGenerateToken"></param>
        ///   <param name="clientAPIUrlForGeneratePaymentLink"></param>
        ///    <param name="hSRequestGenerateToken"></param>
        /// <returns></returns>
        public async Task<string> GenerateStorePayLink(int TenantID, string ProgramCode, int UserID, string clientAPIUrlForGenerateToken, 
            string clientAPIUrlForGeneratePaymentLink, HSRequestGenerateToken hSRequestGenerateToken)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            string PaymentLink = string.Empty;
            string ClientAPIResponse = string.Empty;
            HSResponseGenerateToken hSResponseGenerateToken = new HSResponseGenerateToken();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_GetTenantStorePayURL", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);

                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        PaymentLink = ds.Tables[0].Rows[0]["PaymentLink"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["PaymentLink"]);
                    }
                }

                if (!string.IsNullOrEmpty(PaymentLink))
                {
                    // generate token 
                    string apiReq = "Client_Id=" + hSRequestGenerateToken.Client_Id + "&Client_Secret=" + hSRequestGenerateToken.Client_Secret +
                        "&Grant_Type=" + hSRequestGenerateToken.Grant_Type + "&Scope=" + hSRequestGenerateToken.Scope;
                   // ClientAPIResponse = CommonService.SendApiRequestToken(clientAPIUrlForGenerateToken + "connect/token", apiReq);

                    ClientAPIResponse = await _generateToken.SendApiRequest(clientAPIUrlForGenerateToken + hSRequestGenerateToken.tokenGeneration, apiReq);

                    if (!string.IsNullOrEmpty(ClientAPIResponse))
                    {
                        hSResponseGenerateToken = JsonConvert.DeserializeObject<HSResponseGenerateToken>(ClientAPIResponse);

                        if (!string.IsNullOrEmpty(hSResponseGenerateToken.access_token))
                        {
                            //generate HASH 
                            var RequestHASH = new { password = "programCode=" + ProgramCode + "&userCode=" + UserID };

                            ClientAPIResponse = await _generatePaymentLinkHttpClientService.SendApiRequest(clientAPIUrlForGeneratePaymentLink + hSRequestGenerateToken.SHAHash,
                                JsonConvert.SerializeObject(RequestHASH), hSResponseGenerateToken.access_token);

                            //ClientAPIResponse = CommonService.SendApiRequestMerchantApi(clientAPIUrlForGeneratePaymentLink + "api/SHAHash",
                            //                     JsonConvert.SerializeObject(RequestHASH), hSResponseGenerateToken.access_token);

                            if (!string.IsNullOrEmpty(ClientAPIResponse))
                            {
                                var HASHResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(ClientAPIResponse);
                                if (HASHResponse.Count > 0)
                                {
                                    string HashRequest = HASHResponse["hashedPassword"];

                                    var RequestEpoch = new { currentDateTime = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") };

                                    //ClientAPIResponse = CommonService.SendApiRequestMerchantApi(clientAPIUrlForGeneratePaymentLink + "api/EpochTime",
                                    //                    JsonConvert.SerializeObject(RequestEpoch), hSResponseGenerateToken.access_token);

                                    ClientAPIResponse = CommonService.SendApiRequestMerchantApi(clientAPIUrlForGeneratePaymentLink + hSRequestGenerateToken.EpochTime,
                                                        JsonConvert.SerializeObject(RequestEpoch), hSResponseGenerateToken.access_token);

                                    if (!string.IsNullOrEmpty(ClientAPIResponse))
                                    {
                                        var EpochTimeResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(ClientAPIResponse);
                                        if (EpochTimeResponse.Count > 0)
                                        {
                                            string EpochTime = EpochTimeResponse["epochTime"];

                                            if (!string.IsNullOrEmpty(EpochTime))
                                            {
                                                var RequestEncrypt = new { text = "token=" + HashRequest + "&programCode=" + ProgramCode + "&userCode=" + UserID + "&epochTime=" + EpochTime };
                                                //ClientAPIResponse = CommonService.SendApiRequestMerchantApi(clientAPIUrlForGeneratePaymentLink + "api/AESEncrypt",
                                                //                    JsonConvert.SerializeObject(RequestEncrypt), hSResponseGenerateToken.access_token);

                                                ClientAPIResponse = CommonService.SendApiRequestMerchantApi(clientAPIUrlForGeneratePaymentLink + hSRequestGenerateToken.AESEncrypt,
                                                                  JsonConvert.SerializeObject(RequestEncrypt), hSResponseGenerateToken.access_token);

                                                if (!string.IsNullOrEmpty(ClientAPIResponse))
                                                {
                                                    if (!string.IsNullOrEmpty(ClientAPIResponse))
                                                    {
                                                        var EncryptedResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(ClientAPIResponse);

                                                        if (EncryptedResponse.Count > 0)
                                                        {
                                                            string EncryptedText = EncryptedResponse["encryptedText"];
                                                            PaymentLink = !string.IsNullOrEmpty(EncryptedText) ? PaymentLink + "?" + EncryptedText : string.Empty;

                                                        }

                                                    }
                                                }
                                            }

                                        }
                                    }




                                }
                            }

                        }
                    }






                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return PaymentLink;
        }
    }
}
