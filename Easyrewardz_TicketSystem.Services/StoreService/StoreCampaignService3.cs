using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class StoreCampaignService : IStoreCampaign
    {

        /// <summary>
        /// Get Campaign Setting List
        /// </summary>
        /// <returns></returns>
        public StoreCampaignModel3 GetStoreCampignSetting(int TenantId, int UserId, string ProgramCode)
        {
            DataSet ds = new DataSet();
            List<StoreCampaignSettingModel> lstCampaignSetting = new List<StoreCampaignSettingModel>();
            StoreCampaignSettingTimer CampaignSettingTimer = new StoreCampaignSettingTimer();
            StoreCampaignModel3 campaignObj = new StoreCampaignModel3();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignSettingList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", TenantId);
                cmd.Parameters.AddWithValue("@UserID", UserId);
                cmd.Parameters.AddWithValue("@_prgramCode", ProgramCode);
           

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                //if (ds != null && ds.Tables[0] != null)
                //{
                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //{
                    //    StoreCampaignSettingModel storecampaign = new StoreCampaignSettingModel()
                    //    {
                    //        ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                    //        CampaignName = ds.Tables[0].Rows[i]["CampaignName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignName"]),
                    //        CampaignCode = ds.Tables[0].Rows[i]["CampaignCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignCode"]),
                    //        Programcode = ds.Tables[0].Rows[i]["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Programcode"]),
                           
                    //        SmsFlag = ds.Tables[0].Rows[i]["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["SmsFlag"]),
                    //        EmailFlag = ds.Tables[0].Rows[i]["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["EmailFlag"]),
                    //        MessengerFlag = ds.Tables[0].Rows[i]["MessengerFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["MessengerFlag"]),
                    //        BotFlag = ds.Tables[0].Rows[i]["BotFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["BotFlag"]),  


                    //    };
                    //    lstCampaignSetting.Add(storecampaign);
                    //}

                
                    //campaignObj.CampaignSetting = lstCampaignSetting;
               // }

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        CampaignSettingTimer.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                        CampaignSettingTimer.MaxClickAllowed = ds.Tables[0].Rows[0]["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["MaxClickAllowed"]);
                        CampaignSettingTimer.EnableClickAfterValue = ds.Tables[0].Rows[0]["EnableClickAfterValue"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["EnableClickAfterValue"]);
                        CampaignSettingTimer.EnableClickAfterDuration = ds.Tables[0].Rows[0]["EnableClickAfterDuration"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["EnableClickAfterDuration"]);
                        CampaignSettingTimer.Programcode = ds.Tables[0].Rows[0]["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Programcode"]);
                        CampaignSettingTimer.SmsFlag = ds.Tables[0].Rows[0]["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["SmsFlag"]);
                        CampaignSettingTimer.EmailFlag = ds.Tables[0].Rows[0]["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["EmailFlag"]);
                        CampaignSettingTimer.MessengerFlag = ds.Tables[0].Rows[0]["MessengerFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["MessengerFlag"]);
                        CampaignSettingTimer.BotFlag = ds.Tables[0].Rows[0]["BotFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["BotFlag"]);
                        CampaignSettingTimer.ProviderName = ds.Tables[0].Rows[0]["ProviderName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ProviderName"]);
                    }

                    campaignObj.CampaignSettingTimer = CampaignSettingTimer;
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
            return campaignObj;
        }

        /// <summary>
        /// Update Campaign Setting
        /// </summary>
        /// <param name="CampaignModel"></param>
        /// <returns></returns>
        public int UpdateStoreCampaignSetting(StoreCampaignSettingModel CampaignModel)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSUpdateHSCampaignSetting", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_ID", CampaignModel.ID);
                cmd.Parameters.AddWithValue("@_CampaignName", string.IsNullOrEmpty(CampaignModel.CampaignName) ? "" : CampaignModel.CampaignName);
                cmd.Parameters.AddWithValue("@_SmsFlag", Convert.ToInt16(CampaignModel.SmsFlag));
                cmd.Parameters.AddWithValue("@_EmailFlag", Convert.ToInt16(CampaignModel.EmailFlag));
                cmd.Parameters.AddWithValue("@_MessengerFlag", Convert.ToInt16(CampaignModel.MessengerFlag));
                cmd.Parameters.AddWithValue("@_BotFlag", Convert.ToInt16(CampaignModel.BotFlag));
               
                cmd.CommandType = CommandType.StoredProcedure;
                UpdateCount = Convert.ToInt32(cmd.ExecuteNonQuery());

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
            }

            return UpdateCount;
        }


        /// <summary>
        /// Update Campaign Setting
        /// </summary>
        /// <param name="CampaignModel"></param>
        /// <returns></returns>
        public int UpdateCampaignMaxClickTimer(StoreCampaignSettingTimer storeCampaignSettingTimer, int ModifiedBy)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSUpdateCampaignMaxClickTimer", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@timerID", storeCampaignSettingTimer.ID);
                cmd.Parameters.AddWithValue("@_MaxClickAllowed", storeCampaignSettingTimer.MaxClickAllowed);
                cmd.Parameters.AddWithValue("@_EnableClickAfterValue", storeCampaignSettingTimer.EnableClickAfterValue);
                cmd.Parameters.AddWithValue("@_EnableClickAfterDuration", storeCampaignSettingTimer.EnableClickAfterDuration);
                cmd.Parameters.AddWithValue("@_ModifiedBy", ModifiedBy);
                cmd.Parameters.AddWithValue("@_SmsFlag", Convert.ToInt16(storeCampaignSettingTimer.SmsFlag));
                cmd.Parameters.AddWithValue("@_EmailFlag", Convert.ToInt16(storeCampaignSettingTimer.EmailFlag));
                cmd.Parameters.AddWithValue("@_MessengerFlag", Convert.ToInt16(storeCampaignSettingTimer.MessengerFlag));
                cmd.Parameters.AddWithValue("@_BotFlag", Convert.ToInt16(storeCampaignSettingTimer.BotFlag));
                cmd.Parameters.AddWithValue("@_ProviderName", Convert.ToString(storeCampaignSettingTimer.ProviderName));

                cmd.CommandType = CommandType.StoredProcedure;
                UpdateCount = Convert.ToInt32(cmd.ExecuteNonQuery());

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
            }

            return UpdateCount;
        }

        /// <summary>
        /// GetBroadcastConfiguration
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public StoreBroadcastConfiguration GetBroadcastConfiguration(int tenantId, int userId, string programCode)
        {
            DataSet ds = new DataSet();
            StoreBroadcastConfiguration storeBroadcastConfiguration = new StoreBroadcastConfiguration();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetBroadcastConfigurationList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@_prgramCode", programCode);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        storeBroadcastConfiguration.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                        storeBroadcastConfiguration.MaxClickAllowed = ds.Tables[0].Rows[0]["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["MaxClickAllowed"]);
                        storeBroadcastConfiguration.EnableClickAfterValue = ds.Tables[0].Rows[0]["EnableClickAfterValue"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["EnableClickAfterValue"]);
                        storeBroadcastConfiguration.EnableClickAfterDuration = ds.Tables[0].Rows[0]["EnableClickAfterDuration"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["EnableClickAfterDuration"]);
                        storeBroadcastConfiguration.Programcode = ds.Tables[0].Rows[0]["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Programcode"]);
                        storeBroadcastConfiguration.SmsFlag = ds.Tables[0].Rows[0]["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["SmsFlag"]);
                        storeBroadcastConfiguration.EmailFlag = ds.Tables[0].Rows[0]["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["EmailFlag"]);
                        storeBroadcastConfiguration.WhatsappFlag = ds.Tables[0].Rows[0]["WhatsappFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["WhatsappFlag"]);
                        storeBroadcastConfiguration.ProviderName = ds.Tables[0].Rows[0]["ProviderName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ProviderName"]);
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
            return storeBroadcastConfiguration;
        }

        /// <summary>
        /// GetAppointmentConfiguration
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public StoreAppointmentConfiguration GetAppointmentConfiguration(int tenantId, int userId, string programCode)
        {
            DataSet ds = new DataSet();
            StoreAppointmentConfiguration storeAppointmentConfiguration = new StoreAppointmentConfiguration();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetAppointmentConfigurationList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@_prgramCode", programCode);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        storeAppointmentConfiguration.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                        storeAppointmentConfiguration.GenerateOTP = ds.Tables[0].Rows[0]["GenerateOTP"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["GenerateOTP"]);
                        storeAppointmentConfiguration.CardQRcode = ds.Tables[0].Rows[0]["CardQRcode"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["CardQRcode"]);
                        storeAppointmentConfiguration.CardBarcode = ds.Tables[0].Rows[0]["CardBarcode"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["CardBarcode"]);
                        storeAppointmentConfiguration.OnlyCard = ds.Tables[0].Rows[0]["OnlyCard"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["OnlyCard"]);
                        storeAppointmentConfiguration.Programcode = ds.Tables[0].Rows[0]["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Programcode"]);
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
            return storeAppointmentConfiguration;
        }

        /// <summary>
        /// Update Broadcast Configuration
        /// </summary>
        /// <param name="storeBroadcastConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        public int UpdateBroadcastConfiguration(StoreBroadcastConfiguration storeBroadcastConfiguration, int modifiedBy)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSUpdateBroadcastConfiguration", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_ID", storeBroadcastConfiguration.ID);
                cmd.Parameters.AddWithValue("@_MaxClickAllowed", storeBroadcastConfiguration.MaxClickAllowed);
                cmd.Parameters.AddWithValue("@_EnableClickAfterValue", storeBroadcastConfiguration.EnableClickAfterValue);
                cmd.Parameters.AddWithValue("@_EnableClickAfterDuration", storeBroadcastConfiguration.EnableClickAfterDuration);
                cmd.Parameters.AddWithValue("@_ModifiedBy", modifiedBy);
                cmd.Parameters.AddWithValue("@_SmsFlag", Convert.ToInt16(storeBroadcastConfiguration.SmsFlag));
                cmd.Parameters.AddWithValue("@_EmailFlag", Convert.ToInt16(storeBroadcastConfiguration.EmailFlag));
                cmd.Parameters.AddWithValue("@_WhatsappFlag", Convert.ToInt16(storeBroadcastConfiguration.WhatsappFlag));
                cmd.Parameters.AddWithValue("@_ProviderName", Convert.ToString(storeBroadcastConfiguration.ProviderName));

                cmd.CommandType = CommandType.StoredProcedure;
                UpdateCount = Convert.ToInt32(cmd.ExecuteNonQuery());

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
            }

            return UpdateCount;
        }

        /// <summary>
        /// Update Appointment Configuration
        /// </summary>
        /// <param name="storeAppointmentConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        public int UpdateAppointmentConfiguration(StoreAppointmentConfiguration storeAppointmentConfiguration, int modifiedBy)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSUpdateAppointmentConfiguration", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_ID", storeAppointmentConfiguration.ID);
                cmd.Parameters.AddWithValue("@_GenerateOTP", Convert.ToInt16(storeAppointmentConfiguration.GenerateOTP));
                cmd.Parameters.AddWithValue("@_CardQRcode", Convert.ToInt16(storeAppointmentConfiguration.CardQRcode));
                cmd.Parameters.AddWithValue("@_CardBarcode", Convert.ToInt16(storeAppointmentConfiguration.CardBarcode));
                cmd.Parameters.AddWithValue("@_OnlyCard", Convert.ToInt16(storeAppointmentConfiguration.OnlyCard));
                cmd.Parameters.AddWithValue("@_ModifiedBy", modifiedBy);
              
                cmd.CommandType = CommandType.StoredProcedure;
                UpdateCount = Convert.ToInt32(cmd.ExecuteNonQuery());

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
            }

            return UpdateCount;
        }

        /// <summary>
        /// GetLanguageDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public List<Languages> GetLanguageDetails(int tenantId, int userId, string programCode)
        {
            DataSet ds = new DataSet();
            List<Languages> languageslist = new List<Languages>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetLanguageDetails", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
                cmd.Parameters.AddWithValue("@_Programcode", programCode);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Languages languages = new Languages()
                            {
                                ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                                Language = ds.Tables[0].Rows[i]["Language"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Language"]),
                                IsActive = ds.Tables[0].Rows[i]["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]),
                                
                            };
                            languageslist.Add(languages);
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
            return languageslist;
        }

        /// <summary>
        /// InsertLanguageDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public int InsertLanguageDetails(int tenantId, int userId, string programCode, int languageID)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSInsertLanguageDetails", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
                cmd.Parameters.AddWithValue("@_Programcode", programCode);
                cmd.Parameters.AddWithValue("@_LanguageID", languageID);
                
                cmd.CommandType = CommandType.StoredProcedure;
                UpdateCount = Convert.ToInt32(cmd.ExecuteNonQuery());

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
            }

            return UpdateCount;
        }

        /// <summary>
        /// GetSelectedLanguageDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public List<SelectedLanguages> GetSelectedLanguageDetails(int tenantId, int userId, string programCode)
        {
            DataSet ds = new DataSet();
            List<SelectedLanguages> languageslist = new List<SelectedLanguages>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetListLanguageSelected", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
                cmd.Parameters.AddWithValue("@_Programcode", programCode);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            SelectedLanguages languages = new SelectedLanguages()
                            {
                                ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                                LanguageID = ds.Tables[0].Rows[i]["LanguageID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["LanguageID"]),
                                CreatedOn = ds.Tables[0].Rows[i]["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]),
                                CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]),
                                Language = ds.Tables[0].Rows[i]["Language"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Language"]),
                                IsActive = ds.Tables[0].Rows[i]["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]),
                                CreaterName = ds.Tables[0].Rows[i]["CreaterName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreaterName"]),
                            };
                            languageslist.Add(languages);
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
            return languageslist;
        }

        /// <summary>
        /// DeleteSelectedLanguage
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="SelectedLanguageID"></param>
        /// <returns></returns>
        public int DeleteSelectedLanguage(int tenantId, int userId, string programCode, int selectedLanguageID)
        {
            int UpdateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSDeleteSelectedLanguage", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                cmd.Parameters.AddWithValue("@_UserID", userId);
                cmd.Parameters.AddWithValue("@_Programcode", programCode);
                cmd.Parameters.AddWithValue("@_SelectedLanguageID", selectedLanguageID);

                cmd.CommandType = CommandType.StoredProcedure;
                UpdateCount = Convert.ToInt32(cmd.ExecuteNonQuery());

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
            }

            return UpdateCount;
        }
    }
}
