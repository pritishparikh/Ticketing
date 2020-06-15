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
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSAppointmentDeatils", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd1.Parameters.AddWithValue("@User_Id", UserId);
                cmd1.Parameters.AddWithValue("@Apt_Date", AppDate);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AppointmentModel obj = new AppointmentModel
                        {
                            AppointmentDate = Convert.ToString(ds.Tables[0].Rows[i]["AppointmentDate"]),
                            SlotId = Convert.ToInt32(ds.Tables[0].Rows[i]["SlotId"]),
                            TimeSlot = Convert.ToString(ds.Tables[0].Rows[i]["TimeSlot"]),
                            NOofPeople = Convert.ToInt32(ds.Tables[0].Rows[i]["NOofPeople"]),
                            MaxCapacity = Convert.ToInt32(ds.Tables[0].Rows[i]["MaxCapacity"]),
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
            }

            return appointments;
        }

        /// <summary>
        /// Get Appointment Count
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<AppointmentCount> GetAppointmentCount(int TenantID, int UserId)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<AppointmentCount> appointmentsCount = new List<AppointmentCount>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSGetAppointmentCount", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd1.Parameters.AddWithValue("@User_Id", UserId);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                AppointmentCount obj = new AppointmentCount
                {
                    Today = ds.Tables[0].Rows.Count == 0 ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["Today"]),
                    Tomorrow = ds.Tables[1].Rows.Count == 0 ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["Tomorrow"]),
                    DayAfterTomorrow = ds.Tables[2].Rows.Count == 0 ? 0 : Convert.ToInt32(ds.Tables[2].Rows[0]["DayAfterTomorrow"]) 
                };

                appointmentsCount.Add(obj);
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
            }

            return appointmentsCount;

        }

        public int UpdateAppointmentStatus(AppointmentCustomer appointmentCustomer, int TenantId)
        {

            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSUpdateAppoinmentStatus", conn);
                cmd1.Parameters.AddWithValue("@Appointment_ID", appointmentCustomer.AppointmentID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd1.Parameters.AddWithValue("@_Status", appointmentCustomer.Status);

                cmd1.CommandType = CommandType.StoredProcedure;
                i = cmd1.ExecuteNonQuery();
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

            return i;
        }


        #region TimeSlotMaster CRUD

        /// <summary>
        /// Insert/ Update HSTimeSlotMaster
        /// </summary>
        /// <param name="StoreTimeSlotInsertUpdate"></param>
        /// <returns></returns>
        /// 
        public int InsertTimeSlotSetting(StoreTimeSlotInsertUpdate Slot)
        {

            int Result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                MySqlCommand cmd = new MySqlCommand("SP_HSInsertUpdateHSStoreTimeSlotMaster", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_TenantId", Slot.TenantId);
                cmd.Parameters.AddWithValue("@_ProgramCode", Slot.ProgramCode);
                cmd.Parameters.AddWithValue("@_StoreIds", string.IsNullOrEmpty(Slot.StoreIds) ? "" : Slot.StoreIds.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_StoreOpenValue", Slot.ProgramCode); 
                cmd.Parameters.AddWithValue("@_StoreOpenAt", Slot.StoreOpenAt);
                cmd.Parameters.AddWithValue("@_StoreCloseValue", Slot.StoreCloseAt);
                cmd.Parameters.AddWithValue("@_StoreCloseAt", Slot.StoreCloseAt); 
                cmd.Parameters.AddWithValue("@_Slotduration", Slot.Slotduration); 
                cmd.Parameters.AddWithValue("@_SlotMaxCapacity", Slot.SlotMaxCapacity);

                cmd.Parameters.AddWithValue("@_StoreNonOpFromValue", Slot.StoreNonOpFromValue);
                cmd.Parameters.AddWithValue("@_StoreNonOpFromAt", Slot.StoreNonOpFromAt);
                cmd.Parameters.AddWithValue("@_StoreNonOpToValue", Slot.StoreNonOpToValue);
                cmd.Parameters.AddWithValue("@_StoreNonOpToAt", Slot.StoreNonOpToAt);
                cmd.Parameters.AddWithValue("@_StoreTotalSlot", Slot.StoreTotalSlot);
                cmd.Parameters.AddWithValue("@_AppointmentDays", Slot.AppointmentDays);
                cmd.Parameters.AddWithValue("@_UserID", Slot.UserID);

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
        /// Get HSTimeSlotMaster List
        /// </summary>
        /// <returns></returns>
        public List<StoreTimeSlotSettingModel> GetStoreSettingTimeSlot(int TenantID, string ProgramCode,int SlotID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreTimeSlotSettingModel> TimeSlotList = new List<StoreTimeSlotSettingModel>();
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
                                NonOperationalTimimg = dr["NonOperationalTimimg"] == DBNull.Value ? string.Empty : Convert.ToString(dr["NonOperationalTimimg"]),
                                StoreSlotDuration = dr["StoreSlotDuration"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StoreSlotDuration"]),
                                MaxCapacity = dr["MaxCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxCapacity"]),
                                TotalSlot = dr["TotalSlot"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalSlot"]),
                                CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                ModifyBy = dr["ModifyBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifyBy"]),
                                ModifyByName = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                ModifyDate = dr["ModifyDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyDate"]),



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
