using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class AppointmentServices : IAppointment
    {
        /// <summary>
        /// GetBroadcastConfigurationResponses
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programcode"></param>
        /// <param name="storeCode"></param>
        /// <returns></returns>
        public StoreDetails GetStoreDetailsByStoreCode(int tenantID, int userID, string programcode, string storeCode)
        {
            DataSet ds = new DataSet();
            StoreDetails storeDetails = new StoreDetails();
            List<CampaignExecutionDetailsResponse> objList = new List<CampaignExecutionDetailsResponse>();
            BroadcastConfigurationResponse BroadcastConfigurationResponse = new BroadcastConfigurationResponse();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetStoreDetailsByStoreCode", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", tenantID);
                cmd.Parameters.AddWithValue("@_UserID", userID);
                cmd.Parameters.AddWithValue("@_Programcode", programcode);
                cmd.Parameters.AddWithValue("@_StoreCode", storeCode);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        storeDetails = new StoreDetails()
                        {
                            StoreName = ds.Tables[0].Rows[0]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]),
                            Address = ds.Tables[0].Rows[0]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Address"]),
                            StorePhoneNo = ds.Tables[0].Rows[0]["StorePhoneNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StorePhoneNo"]),
                        };
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
            return storeDetails;

        }

        /// <summary>
        /// Get Store Operational Days
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<StoreOperationalDays> GetStoreOperationalDays(int TenantID, string ProgramCode, int UserID)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<StoreOperationalDays> Operationaldays = new List<StoreOperationalDays>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetStoreOperationalDays", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_UserID", UserID);
                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            StoreOperationalDays Obj = new StoreOperationalDays()
                            {
                                DayID = dr["DayID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DayID"]),
                                DayName = dr["DayName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DayName"])
                            };
                            Operationaldays.Add(Obj);
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

            return Operationaldays;
        }

        /// <summary>
        /// Get Slot Templates
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public List<SlotTemplateModel> GetSlotTemplates(int TenantID, string ProgramCode)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<SlotTemplateModel> TemplateList = new List<SlotTemplateModel>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetSlotTemplates", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SlotTemplateModel Obj = new SlotTemplateModel()
                            {
                                SlotTemplateID = dr["SlotTemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotTemplateID"]),
                                SlotTemplateName = dr["SlotTemplateName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SlotTemplateName"])
                            };
                            TemplateList.Add(Obj);
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

            return TemplateList;
        }

        /// <summary>
        ///Get Generated Slots
        /// </summary>
        /// <param name="CreateStoreSlotTemplate"></param>
        /// <returns></returns>
        public List<TemplateBasedSlots> GetGeneratedSlots(CreateStoreSlotTemplate Template)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<TemplateBasedSlots> SlotsList = new List<TemplateBasedSlots>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGenerateTemplateSlots", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantId", Template.TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", Template.ProgramCode);
                cmd.Parameters.AddWithValue("@_SlotTemplateID", 0);
                cmd.Parameters.AddWithValue("@_StoreOpenAt", Template.StoreOpenAt);
                cmd.Parameters.AddWithValue("@_StoreCloseAt", Template.StoreCloseAt);
                cmd.Parameters.AddWithValue("@_Slotduration", Template.Slotduration);
                cmd.Parameters.AddWithValue("@_SlotGaps", Template.SlotGaps);
                cmd.Parameters.AddWithValue("@_StoreNonOpFromAt", Template.StoreNonOpFromAt);
                cmd.Parameters.AddWithValue("@_StoreNonOpToAt", Template.StoreNonOpToAt);
                cmd.Parameters.AddWithValue("@_UserID", Template.UserID);
                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TemplateBasedSlots Obj = new TemplateBasedSlots()
                            {
                                SlotID = dr["SlotID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotID"]),
                                SlotTemplateID = dr["SlotTemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotTemplateID"]),
                                SlotStartTime = dr["SlotStartTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SlotStartTime"]),
                                SlotEndTime = dr["SlotEndTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SlotEndTime"]),
                                SlotOccupancy = dr["SlotOccupancy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotOccupancy"]),
                                IsSlotEnabled = dr["SlotStatus"] == DBNull.Value ? false : Convert.ToBoolean(dr["SlotStatus"]),

                            };
                            SlotsList.Add(Obj);
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

            return SlotsList;
        }

        /// <summary>
        ///Create Slot Template
        /// </summary>
        /// <param name="CreateStoreSlotTemplate"></param>
        /// <returns></returns>
        public int CreateSlotTemplate(CreateStoreSlotTemplate Template)
        {
            MySqlCommand cmd = new MySqlCommand();
            string XmlText = string.Empty;
            int Result = 0;
            XDocument SlotDetails = null;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if(Template.SlotTemplateType.Equals("M") && Template.TemplateSlots.Count > 0)
                {
                    SlotDetails= new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
                    new XElement("ManualSlots",
                    from Slots in Template.TemplateSlots
                    select new XElement("SlotDetails",
                    new XElement("SlotStartTime", Slots.SlotStartTime),
                    new XElement("SlotEndTime", Slots.SlotEndTime))));

                    XmlText = SlotDetails.ToString();
                }

                cmd = new MySqlCommand("SP_HSCreateSlotTemplates", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantId", Template.TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", Template.ProgramCode);
                cmd.Parameters.AddWithValue("@_SlotTemplateName", Template.SlotTemplateName);
                cmd.Parameters.AddWithValue("@_SlotTemplateType", Template.SlotTemplateType);
                cmd.Parameters.AddWithValue("@_StoreOpenAt", Template.StoreOpenAt);
                cmd.Parameters.AddWithValue("@_StoreCloseAt", Template.StoreCloseAt);
                cmd.Parameters.AddWithValue("@_Slotduration", Template.Slotduration);
                cmd.Parameters.AddWithValue("@_SlotGaps", Template.SlotGaps);
                cmd.Parameters.AddWithValue("@_StoreNonOpFromAt", Template.StoreNonOpFromAt);
                cmd.Parameters.AddWithValue("@_StoreNonOpToAt", Template.StoreNonOpToAt);
                cmd.Parameters.AddWithValue("@_XmlManualSlots",string.IsNullOrEmpty(XmlText) ? "" : XmlText);
                cmd.Parameters.AddWithValue("@_UserID", Template.UserID);
                cmd.CommandType = CommandType.StoredProcedure;

                Result = Convert.ToInt32(cmd.ExecuteScalar());

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

            return Result;
        }

        /// <summary>
        ///GetS lots By TemplateID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <param name="SlotTemplateID"></param>
        /// <returns></returns>
        public List<TemplateBasedSlots> GetSlotsByTemplateID(int TenantID, string ProgramCode, int UserID, int SlotTemplateID)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            List<TemplateBasedSlots> SlotsList = new List<TemplateBasedSlots>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd = new MySqlCommand("SP_HSGetSlotsByTemplateID", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_UserID", UserID);
                cmd.Parameters.AddWithValue("@_SlotTemplateID", SlotTemplateID);
                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TemplateBasedSlots Obj = new TemplateBasedSlots()
                            {
                                SlotID = dr["SlotID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotID"]),
                                SlotTemplateID = dr["SlotTemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotTemplateID"]),
                                SlotStartTime = dr["SlotStartTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SlotStartTime"]),
                                SlotEndTime = dr["SlotEndTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SlotEndTime"]),
                                SlotOccupancy = dr["SlotOccupancy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotOccupancy"]),
                                IsSlotEnabled = dr["SlotStatus"] == DBNull.Value ? false : Convert.ToBoolean(dr["SlotStatus"]),

                            };
                            SlotsList.Add(Obj);
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

            return SlotsList;
        }
    }
}
