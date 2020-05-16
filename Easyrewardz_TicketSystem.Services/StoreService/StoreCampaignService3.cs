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
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreCampaignSettingModel storecampaign = new StoreCampaignSettingModel()
                        {
                            ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            CampaignName = ds.Tables[0].Rows[i]["CampaignName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignName"]),
                            CampaignCode = ds.Tables[0].Rows[i]["CampaignCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignCode"]),
                            Programcode = ds.Tables[0].Rows[i]["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Programcode"]),
                           
                            SmsFlag = ds.Tables[0].Rows[i]["SmsFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["SmsFlag"]),
                            EmailFlag = ds.Tables[0].Rows[i]["EmailFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["EmailFlag"]),
                            MessengerFlag = ds.Tables[0].Rows[i]["MessengerFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["MessengerFlag"]),
                            BotFlag = ds.Tables[0].Rows[i]["BotFlag"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["BotFlag"]),


                        };
                        lstCampaignSetting.Add(storecampaign);
                    }

                
                    campaignObj.CampaignSetting = lstCampaignSetting;
                }

                if (ds != null && ds.Tables[1] != null)
                {
                   if(ds.Tables[1].Rows.Count > 0)
                    {
                       CampaignSettingTimer.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                       CampaignSettingTimer.MaxClickAllowed = ds.Tables[1].Rows[0]["MaxClickAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["MaxClickAllowed"]);
                       CampaignSettingTimer.EnableClickAfterValue = ds.Tables[1].Rows[0]["EnableClickAfterValue"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["EnableClickAfterValue"]);
                        CampaignSettingTimer.Programcode = ds.Tables[1].Rows[0]["Programcode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["Programcode"]);

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



    }
}
