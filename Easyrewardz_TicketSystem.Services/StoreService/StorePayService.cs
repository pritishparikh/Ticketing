using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class StorePayService : IStorePay
    {
        MySqlConnection conn = new MySqlConnection();

        public StorePayService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
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
        public string GenerateStorePayLink(int TenantID, string ProgramCode, int UserID, string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, HSRequestGenerateToken hSRequestGenerateToken)
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
                    ClientAPIResponse = CommonService.SendApiRequestToken(clientAPIUrlForGenerateToken + "connect/token", apiReq);

                    if (!string.IsNullOrEmpty(ClientAPIResponse))
                    {
                        hSResponseGenerateToken = JsonConvert.DeserializeObject<HSResponseGenerateToken>(ClientAPIResponse);

                        if (!string.IsNullOrEmpty(hSResponseGenerateToken.access_token))
                        {
                            //generate HASH 
                            var RequestHASH = new { password = "programCode=" + ProgramCode + "&userCode=" + UserID };
                            ClientAPIResponse = CommonService.SendApiRequestMerchantApi(clientAPIUrlForGeneratePaymentLink + "api/SHAHash",
                                                 JsonConvert.SerializeObject(RequestHASH), hSResponseGenerateToken.access_token);

                            if (!string.IsNullOrEmpty(ClientAPIResponse))
                            {
                                var HASHResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(ClientAPIResponse);
                                if (HASHResponse.Count > 0)
                                {
                                    string HashRequest = HASHResponse["hashedPassword"];
                                    string EpochTime = Convert.ToString(CommonService.ConvertToUnixTimestamp(DateTime.Now));
                                    var RequestEncrypt = new { text = "token=" + HashRequest + "&programCode=" + ProgramCode + "&userCode=" + UserID + "&epochTime=" + EpochTime };
                                    ClientAPIResponse = CommonService.SendApiRequestMerchantApi(clientAPIUrlForGeneratePaymentLink + "api/AESEncrypt",
                                                        JsonConvert.SerializeObject(RequestEncrypt), hSResponseGenerateToken.access_token);

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
