using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class StoreCampaignService : IStoreCampaign
    {

        /// <summary>
        /// Get Campaign Setting List
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public async Task<StoreCampaignModel3> GetStoreCampignSetting(int TenantId, int UserId, string ProgramCode)
        {
            List<StoreCampaignSettingModel> lstCampaignSetting = new List<StoreCampaignSettingModel>();
            StoreCampaignSettingTimer CampaignSettingTimer = new StoreCampaignSettingTimer();
            StoreCampaignModel3 campaignObj = new StoreCampaignModel3();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetCampaignSettingList", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_tenantID", TenantId);
                    cmd.Parameters.AddWithValue("@UserID", UserId);
                    cmd.Parameters.AddWithValue("@_prgramCode", ProgramCode);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            CampaignSettingTimer.ID = Convert.ToInt32(dr["ID"]);
                            CampaignSettingTimer.MaxClickAllowed = dr["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxClickAllowed"]);
                            CampaignSettingTimer.EnableClickAfterValue = dr["EnableClickAfterValue"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EnableClickAfterValue"]);
                            CampaignSettingTimer.EnableClickAfterDuration = dr["EnableClickAfterDuration"] == DBNull.Value ? string.Empty : Convert.ToString(dr["EnableClickAfterDuration"]);
                            CampaignSettingTimer.Programcode = dr["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Programcode"]);
                            CampaignSettingTimer.SmsFlag = dr["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["SmsFlag"]);
                            CampaignSettingTimer.EmailFlag = dr["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["EmailFlag"]);
                            CampaignSettingTimer.MessengerFlag = dr["MessengerFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["MessengerFlag"]);
                            CampaignSettingTimer.BotFlag = dr["BotFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["BotFlag"]);
                            CampaignSettingTimer.ProviderName = dr["ProviderName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProviderName"]);
                            CampaignSettingTimer.CampaignAutoAssigned = dr["CampaignAutoAssigned"] == DBNull.Value ? false : Convert.ToBoolean(dr["CampaignAutoAssigned"]);
                            CampaignSettingTimer.RaiseTicketFlag = dr["RaiseTicketFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["RaiseTicketFlag"]);
                            CampaignSettingTimer.AddCommentFlag = dr["AddCommentFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["AddCommentFlag"]);
                            CampaignSettingTimer.ZoneFlag = dr["ZoneFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["ZoneFlag"]);
                            CampaignSettingTimer.StoreFlag = dr["StoreFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["StoreFlag"]);
                            CampaignSettingTimer.UserProductivityReport = dr["UserProductivityReportFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["UserProductivityReportFlag"]);
                            CampaignSettingTimer.StoreProductivityReport = dr["StoreProductivityReportFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["StoreProductivityReportFlag"]);

                        }
                        campaignObj.CampaignSettingTimer = CampaignSettingTimer;
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
            }
            return campaignObj;
        }

        /// <summary>
        /// Update Campaign Setting
        /// </summary>
        /// <param name="CampaignModel"></param>
        /// <returns></returns>
        public async Task<int> UpdateStoreCampaignSetting(StoreCampaignSettingModel CampaignModel)
        {
            int UpdateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSUpdateHSCampaignSetting", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@_ID", CampaignModel.ID);
                    cmd.Parameters.AddWithValue("@_CampaignName", string.IsNullOrEmpty(CampaignModel.CampaignName) ? "" : CampaignModel.CampaignName);
                    cmd.Parameters.AddWithValue("@_SmsFlag", Convert.ToInt16(CampaignModel.SmsFlag));
                    cmd.Parameters.AddWithValue("@_EmailFlag", Convert.ToInt16(CampaignModel.EmailFlag));
                    cmd.Parameters.AddWithValue("@_MessengerFlag", Convert.ToInt16(CampaignModel.MessengerFlag));
                    cmd.Parameters.AddWithValue("@_BotFlag", Convert.ToInt16(CampaignModel.BotFlag));

                    cmd.CommandType = CommandType.StoredProcedure;
                    UpdateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
            }

            return UpdateCount;
        }


        /// <summary>
        ///  Update Campaign Max Click Timer
        /// </summary>
        /// <param name="storeCampaignSettingTimer"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public async Task<int> UpdateCampaignMaxClickTimer(StoreCampaignSettingTimer storeCampaignSettingTimer, int ModifiedBy)
        {
            int UpdateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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
                    cmd.Parameters.AddWithValue("@_CampaignAutoAssigned", Convert.ToBoolean(storeCampaignSettingTimer.CampaignAutoAssigned));
                    cmd.Parameters.AddWithValue("@_RaiseTicketFlag", Convert.ToBoolean(storeCampaignSettingTimer.RaiseTicketFlag));
                    cmd.Parameters.AddWithValue("@_AddCommentFlag", Convert.ToBoolean(storeCampaignSettingTimer.AddCommentFlag));
                    cmd.Parameters.AddWithValue("@_StoreFlag", Convert.ToBoolean(storeCampaignSettingTimer.StoreFlag));
                    cmd.Parameters.AddWithValue("@_ZoneFlag", Convert.ToBoolean(storeCampaignSettingTimer.ZoneFlag));
                    cmd.Parameters.AddWithValue("@_UserProductivityReport", Convert.ToBoolean(storeCampaignSettingTimer.UserProductivityReport));
                    cmd.Parameters.AddWithValue("@_StoreProductivityReport", Convert.ToBoolean(storeCampaignSettingTimer.StoreProductivityReport));

                    cmd.CommandType = CommandType.StoredProcedure;
                    UpdateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
        public async Task<StoreBroadcastConfiguration> GetBroadcastConfiguration(int tenantId, int userId, string programCode)
        {
            StoreBroadcastConfiguration storeBroadcastConfiguration = new StoreBroadcastConfiguration();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetBroadcastConfigurationList", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@_prgramCode", programCode);
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            storeBroadcastConfiguration.ID = Convert.ToInt32(dr["ID"]);
                            storeBroadcastConfiguration.MaxClickAllowed = dr["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxClickAllowed"]);
                            storeBroadcastConfiguration.EnableClickAfterValue = dr["EnableClickAfterValue"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EnableClickAfterValue"]);
                            storeBroadcastConfiguration.EnableClickAfterDuration = dr["EnableClickAfterDuration"] == DBNull.Value ? string.Empty : Convert.ToString(dr["EnableClickAfterDuration"]);
                            storeBroadcastConfiguration.Programcode = dr["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Programcode"]);
                            storeBroadcastConfiguration.SmsFlag = dr["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["SmsFlag"]);
                            storeBroadcastConfiguration.EmailFlag = dr["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["EmailFlag"]);
                            storeBroadcastConfiguration.WhatsappFlag = dr["WhatsappFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["WhatsappFlag"]);
                            storeBroadcastConfiguration.ProviderName = dr["ProviderName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProviderName"]);
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
            }
            return storeBroadcastConfiguration;
        }

        /// <summary>
        /// Get Appointment Configuration
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserId"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public async Task<StoreAppointmentConfiguration> GetAppointmentConfiguration(int tenantId, int userId, string programCode)
        {
            StoreAppointmentConfiguration storeAppointmentConfiguration = new StoreAppointmentConfiguration();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetAppointmentConfigurationList", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_tenantID", tenantId);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@_prgramCode", programCode);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            storeAppointmentConfiguration.ID = Convert.ToInt32(dr["ID"]);
                            storeAppointmentConfiguration.GenerateOTP = dr["GenerateOTP"] == DBNull.Value ? false : Convert.ToBoolean(dr["GenerateOTP"]);
                            storeAppointmentConfiguration.CardQRcode = dr["CardQRcode"] == DBNull.Value ? false : Convert.ToBoolean(dr["CardQRcode"]);
                            storeAppointmentConfiguration.CardBarcode = dr["CardBarcode"] == DBNull.Value ? false : Convert.ToBoolean(dr["CardBarcode"]);
                            storeAppointmentConfiguration.OnlyCard = dr["OnlyCard"] == DBNull.Value ? false : Convert.ToBoolean(dr["OnlyCard"]);
                            storeAppointmentConfiguration.ViaWhatsApp = dr["CommViaWhatsApp"] == DBNull.Value ? false : Convert.ToBoolean(dr["CommViaWhatsApp"]);
                            storeAppointmentConfiguration.IsMsgWithin24Hrs = dr["MsgWithin24Hrs"] == DBNull.Value ? false : Convert.ToBoolean(dr["MsgWithin24Hrs"]);
                            storeAppointmentConfiguration.MessageViaWhatsApp = dr["MessageViaWhatsApp"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MessageViaWhatsApp"]);
                            storeAppointmentConfiguration.ViaSMS = dr["CommViaSMS"] == DBNull.Value ? false : Convert.ToBoolean(dr["CommViaSMS"]);
                            storeAppointmentConfiguration.MessageViaSMS = dr["MessageViaSMS"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MessageViaSMS"]);
                            storeAppointmentConfiguration.Programcode = dr["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Programcode"]);
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
            }
            return storeAppointmentConfiguration;
        }

        /// <summary>
        /// Update Broadcast Configuration
        /// </summary>
        /// <param name="storeBroadcastConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        public async Task<int> UpdateBroadcastConfiguration(StoreBroadcastConfiguration storeBroadcastConfiguration, int modifiedBy)
        {
            int UpdateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
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
                    UpdateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
            }

            return UpdateCount;
        }

        /// <summary>
        /// Update Appointment Configuration
        /// </summary>
        /// <param name="storeAppointmentConfiguration"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        public async Task<int> UpdateAppointmentConfiguration(StoreAppointmentConfiguration storeAppointmentConfiguration, int modifiedBy)
        {
            int UpdateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSUpdateAppointmentConfiguration", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_ID", storeAppointmentConfiguration.ID);
                    cmd.Parameters.AddWithValue("@_GenerateOTP", Convert.ToInt16(storeAppointmentConfiguration.GenerateOTP));
                    cmd.Parameters.AddWithValue("@_CardQRcode", Convert.ToInt16(storeAppointmentConfiguration.CardQRcode));
                    cmd.Parameters.AddWithValue("@_CardBarcode", Convert.ToInt16(storeAppointmentConfiguration.CardBarcode));
                    cmd.Parameters.AddWithValue("@_OnlyCard", Convert.ToInt16(storeAppointmentConfiguration.OnlyCard));
                    cmd.Parameters.AddWithValue("@_ViaWhatsApp", Convert.ToInt16(storeAppointmentConfiguration.ViaWhatsApp));
                    cmd.Parameters.AddWithValue("@_ViaSMS", Convert.ToInt16(storeAppointmentConfiguration.ViaSMS));
                    cmd.Parameters.AddWithValue("@_MsgWithin24Hrs", Convert.ToInt16(storeAppointmentConfiguration.IsMsgWithin24Hrs));
                    cmd.Parameters.AddWithValue("@_MessageViaWhatsApp", storeAppointmentConfiguration.MessageViaWhatsApp);
                    cmd.Parameters.AddWithValue("@_MessageViaSMS", storeAppointmentConfiguration.MessageViaSMS);

                    cmd.Parameters.AddWithValue("@_ModifiedBy", modifiedBy);

                    cmd.CommandType = CommandType.StoredProcedure;
                    UpdateCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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
            }

            return UpdateCount;
        }

        /// <summary>
        /// Get Language Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public async Task<List<Languages>> GetLanguageDetails(int tenantId, int userId, string programCode)
        {
            List<Languages> languageslist = new List<Languages>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetLanguageDetails", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_Programcode", programCode);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            Languages languages = new Languages()
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                Language = dr["Language"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Language"]),
                                IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"]),
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
            }
            return languageslist;
        }

        /// <summary>
        /// Insert Language Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public async Task<int> InsertLanguageDetails(int tenantId, int userId, string programCode, int languageID)
        {
            int UpdateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSInsertLanguageDetails", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_Programcode", programCode);
                    cmd.Parameters.AddWithValue("@_LanguageID", languageID);

                    cmd.CommandType = CommandType.StoredProcedure;
                    UpdateCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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
            }

            return UpdateCount;
        }

        /// <summary>
        /// Get Selected Language Details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public async Task<List<SelectedLanguages>> GetSelectedLanguageDetails(int tenantId, int userId, string programCode)
        {
            List<SelectedLanguages> languageslist = new List<SelectedLanguages>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSGetListLanguageSelected", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_Programcode", programCode);

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            SelectedLanguages languages = new SelectedLanguages()
                            {
                                ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                LanguageID = dr["LanguageID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["LanguageID"]),
                                CreatedOn = dr["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedOn"]),
                                CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                Language = dr["Language"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Language"]),
                                IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"]),
                                CreaterName = dr["CreaterName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreaterName"]),
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
            }
            return languageslist;
        }

        /// <summary>
        /// Delete Selected Language
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="programCode"></param>
        /// <param name="SelectedLanguageID"></param>
        /// <returns></returns>
        public async Task<int> DeleteSelectedLanguage(int tenantId, int userId, string programCode, int selectedLanguageID, bool isActive)
        {
            int UpdateCount = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSDeleteSelectedLanguage", conn)
                    {
                        Connection = conn
                    };
                    cmd.Parameters.AddWithValue("@_TenantID", tenantId);
                    cmd.Parameters.AddWithValue("@_UserID", userId);
                    cmd.Parameters.AddWithValue("@_Programcode", programCode);
                    cmd.Parameters.AddWithValue("@_SelectedLanguageID", selectedLanguageID);
                    cmd.Parameters.AddWithValue("@_IsActive", isActive);

                    cmd.CommandType = CommandType.StoredProcedure;
                    UpdateCount = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
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
            }

            return UpdateCount;
        }
    }
}
