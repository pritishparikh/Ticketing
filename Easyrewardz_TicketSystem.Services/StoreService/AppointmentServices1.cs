using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Xml.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class AppointmentServices : IAppointment
    {
        MySqlConnection conn = new MySqlConnection();

        public AppointmentServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// Get AppointmentList List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<AppointmentModel> GetAppointmentList(int TenantID, int UserId, string AppDate)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<AppointmentModel> appointments = new List<AppointmentModel>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                cmd = new MySqlCommand("SP_HSAppointmentDeatils", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd.Parameters.AddWithValue("@User_Id", UserId);
                cmd.Parameters.AddWithValue("@Apt_Date", AppDate);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AppointmentModel obj = new AppointmentModel
                        {
                            AppointmentDate = ds.Tables[0].Rows[i]["AppointmentDate"] == DBNull.Value ? "": Convert.ToString(ds.Tables[0].Rows[i]["AppointmentDate"]),
                            SlotId = ds.Tables[0].Rows[i]["SlotId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SlotId"]),
                            TimeSlot = ds.Tables[0].Rows[i]["TimeSlot"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["TimeSlot"]),
                            NOofPeople = ds.Tables[0].Rows[i]["NOofPeople"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NOofPeople"]),
                            MaxCapacity = ds.Tables[0].Rows[i]["MaxCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["MaxCapacity"]),
                            AppointmentCustomerList = new List<AppointmentCustomer>()
                        };


                        obj.AppointmentCustomerList = ds.Tables[1].AsEnumerable().Where(x => (x.Field<string>("AppointmentDate")).
                        Equals(obj.AppointmentDate) && (x.Field<int>("SlotId")).Equals(obj.SlotId)).Select(x => new AppointmentCustomer()
                        {
                            AppointmentID = Convert.ToInt32(x.Field<int>("AppointmentID")),
                            CustomerName = Convert.ToString(x.Field<string>("CustomerName")),
                            CustomerNumber = Convert.ToString(x.Field<string>("CustomerNumber")),
                            NOofPeople = Convert.ToInt32(x.Field<int>("NOofPeople")),
                            Status = x.Field<int?>("Status").ToString() == "" ? "" :
                        Convert.ToInt32(x.Field<int?>("Status")) == 1 ? "Visited" :
                        Convert.ToInt32(x.Field<int?>("Status")) == 2 ? "Not Visited" : "Cancel",

                        }).ToList();

                        appointments.Add(obj);
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

            return appointments;
        }

        /// <summary>
        /// Get Appointment Count
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<AppointmentCount> GetAppointmentCount(int TenantID,string ProgramCode, int UserId)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<AppointmentCount> appointmentsCount = new List<AppointmentCount>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                cmd = new MySqlCommand("SP_HSGetAppointmentCount", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_UserID", UserId);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            AppointmentCount Apt = new AppointmentCount()
                            {

                                AppointmentDate = dr["AppointmentDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AppointmentDate"]),
                                DayName = dr["DayName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DayName"]),
                                AptCount = dr["AptCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AptCount"])

                            };

                            appointmentsCount.Add(Apt);
                        }
                    }
                }
               
            }
            catch (Exception )
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

            return appointmentsCount;

        }

        /// <summary>
        /// Update Appointment Status
        /// </summary>
        /// <param name="appointmentCustomer"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int UpdateAppointmentStatus(AppointmentCustomer appointmentCustomer, int TenantID, string ProgramCode, int UserID)
        {

            MySqlCommand cmd = new MySqlCommand();
            int Result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                cmd = new MySqlCommand("SP_HSUpdateAppoinmentStatus", conn);

                cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd.Parameters.AddWithValue("@_AptID", appointmentCustomer.AppointmentID);
                cmd.Parameters.AddWithValue("@_AptStatus", appointmentCustomer.AppointmentID);
                cmd.Parameters.AddWithValue("@_UserID", UserID);

                cmd.CommandType = CommandType.StoredProcedure;
                Result = Convert.ToInt32(cmd.ExecuteNonQuery());
                conn.Close();
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


        #region TimeSlotMaster CRUD

        /// <summary>
        /// Insert/ Update Time Slot Setting
        /// </summary>
        /// <param name="StoreTimeSlotInsertUpdate"></param>
        /// <returns></returns>
        /// 
        public int InsertUpdateTimeSlotSetting(StoreTimeSlotInsertUpdate Slot)
        {

            int Result = 0;
            string XmlSlots = string.Empty;
            XDocument SlotDetails = null;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                if(Slot.TemplateSlots.Count > 0)
                {
                    SlotDetails = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
                  new XElement("Slots",
                  from Slots in Slot.TemplateSlots
                  select new XElement("SlotDetails",
                  new XElement("SlotID", Slots.SlotID),
                  new XElement("SlotStartTime", Slots.SlotStartTime),
                  new XElement("SlotEndTime", Slots.SlotEndTime),
                  new XElement("SlotOccupancy", Slots.SlotOccupancy),
                  new XElement("SlotStatus", Convert.ToInt16(Slots.IsSlotEnabled))
                  )));

                    XmlSlots = SlotDetails.ToString();
                }

                MySqlCommand cmd = new MySqlCommand(Slot.SlotId > 0 ? "SP_HSUpdateStoreTimeSlotSetting" : "SP_HSInsertStoreTimeSlotSetting", conn);
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@_TenantId", Slot.TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", Slot.ProgramCode);

                if (Slot.SlotId > 0) //insert
                {
                    cmd.Parameters.AddWithValue("@_SlotSettingID", Slot.SlotId);
                }
                else //update
                {
                    cmd.Parameters.AddWithValue("@_StoreIds", string.IsNullOrEmpty(Slot.StoreIds) ? "" : Slot.StoreIds.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@_OpDays", string.IsNullOrEmpty(Slot.StoreOpdays) ? "" : Slot.StoreOpdays.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@_SlotTemplateID", Slot.SlotTemplateID);
                    cmd.Parameters.AddWithValue("@_SlotMaxCapacity", Slot.SlotMaxCapacity);
                    cmd.Parameters.AddWithValue("@_ApplicableFromDate", Slot.ApplicableFromDate);
                }

              
                cmd.Parameters.AddWithValue("@_AppointmentDays", Slot.AppointmentDays); 
                cmd.Parameters.AddWithValue("@_IsActive",Convert.ToInt16(Slot.IsActive));
                cmd.Parameters.AddWithValue("@_SlotDisplayCode", Slot.SlotDisplayCode);
                cmd.Parameters.AddWithValue("@_XmlSlots", string.IsNullOrEmpty(XmlSlots) ? "" : XmlSlots);
                cmd.Parameters.AddWithValue("@_UserID", Slot.UserID);
                

                cmd.CommandType = CommandType.StoredProcedure;
                Result = Convert.ToInt32(cmd.ExecuteScalar()) ;
                conn.Close();
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
        /// Delete HSTimeSlotMaster
        /// </summary>
        /// <param name="SlotID"></param>
        /// <returns></returns>
        public int DeleteTimeSlotMaster(int SlotID, int TenantID, string ProgramCode)
        {

            int Result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                MySqlCommand cmd = new MySqlCommand("SP_DeleteStoreTimeSlotMaster", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_SlotSettingID", SlotID);
                cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);

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
        /// Get HSTimeSlotMaster List
        /// </summary>
        /// <returns></returns>
        public List<StoreTimeSlotSettingModel> GetStoreSettingTimeSlot(int TenantID, string ProgramCode,int SlotID, int StoreID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreTimeSlotSettingModel> TimeSlotList = new List<StoreTimeSlotSettingModel>();
            List<TemplateBasedSlots> TemplateSlotsList = new List<TemplateBasedSlots>();
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreTimeSlotSettingList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_TenantId", TenantID);
                cmd1.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                cmd1.Parameters.AddWithValue("@_SlotID", SlotID);
                cmd1.Parameters.AddWithValue("@_StoreID", StoreID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);


                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {                        
                            TimeSlotList.Add(new StoreTimeSlotSettingModel()
                            {

                                SlotSettingID = dr["SlotSettingID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotSettingID"]),
                                TenantId = dr["TenantId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TenantId"]),
                                ProgramCode = dr["ProgramCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProgramCode"]),
                                StoreId = dr["StoreId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreId"]),
                                StoreCode = dr["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreCode"]),
                                StoreTimimg = dr["StoreTimimg"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreTimimg"]),
                                OperationalDaysCount = dr["OperationalDays"] == DBNull.Value ? 0 : Convert.ToString(dr["OperationalDays"]).Split(',').Length,
                                OperationalDays = dr["OperationalDays"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OperationalDays"]),
                                SlotTemplateID = dr["SlotTemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotTemplateID"]),
                                SlotTemplateName = dr["SlotTemplateName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SlotTemplateName"]),
                                StoreSlotDuration = dr["StoreSlotDuration"] == DBNull.Value ? string.Empty : Convert.ToString(dr["StoreSlotDuration"]),
                                TotalSlot = dr["TotalSlot"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalSlot"]),
                                AppointmentDays = dr["AppointmentDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AppointmentDays"]),
                                CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                ModifyBy = dr["ModifyBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifyBy"]),
                                ModifyByName = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                ModifyDate = dr["ModifyDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyDate"]),
                                Status = dr["Status"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Status"]),
                                SlotDisplayCode = dr["SlotDisplayCode"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotDisplayCode"]),
                                TemplateSlots = ds.Tables[1].AsEnumerable().Where(x => (x.Field<int>("SlotSettingID")).Equals(dr["SlotSettingID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotSettingID"]))).Select(r => new TemplateBasedSlots()
                                {
                                    SlotID = r.Field<object>("SlotID") == DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("SlotID")),
                                    SlotStartTime = r.Field<object>("TimeSlot") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TimeSlot")).Split('-')[0],
                                    SlotEndTime = r.Field<object>("TimeSlot") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TimeSlot")).Split('-')[1],
                                    SlotOccupancy = r.Field<object>("MaxCapacity") == DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("MaxCapacity")),
                                    IsSlotEnabled = r.Field<object>("SlotStatus") == DBNull.Value ? false : Convert.ToBoolean(r.Field<object>("SlotStatus"))
                                }).ToList()
                        });

                            
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

            return TimeSlotList;

        }



        #endregion
    }



}
