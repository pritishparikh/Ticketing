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
using System.Xml;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public partial class AppointmentServices : IAppointment
    {
       
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        MySqlConnection conn = new MySqlConnection();
        #endregion

        public AppointmentServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// Get AppointmentList List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public async Task<List<AppointmentModel>> GetAppointmentList(int TenantID, int UserId, string AppDate)
        {
            DataTable appointmentcount = new DataTable();
            DataTable appointmentList = new DataTable();
            MySqlCommand cmd = new MySqlCommand();
            List<AppointmentModel> appointments = new List<AppointmentModel>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                cmd = new MySqlCommand("SP_HSAppointmentDeatils", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd.Parameters.AddWithValue("@User_Id", UserId);
                cmd.Parameters.AddWithValue("@Apt_Date", AppDate);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        appointmentcount.Load(reader);
                        appointmentList.Load(reader);
                    }
                }
                if (appointmentcount != null)
                {
                    for (int i = 0; i < appointmentcount.Rows.Count; i++)
                    {
                        AppointmentModel obj = new AppointmentModel
                        {
                            AppointmentDate = appointmentcount.Rows[i]["AppointmentDate"] == DBNull.Value ? "": Convert.ToString(appointmentcount.Rows[i]["AppointmentDate"]),
                            SlotId = appointmentcount.Rows[i]["SlotId"] == DBNull.Value ? 0 : Convert.ToInt32(appointmentcount.Rows[i]["SlotId"]),
                            TimeSlot = appointmentcount.Rows[i]["TimeSlot"] == DBNull.Value ? "" : Convert.ToString(appointmentcount.Rows[i]["TimeSlot"]),
                            NOofPeople = appointmentcount.Rows[i]["NOofPeople"] == DBNull.Value ? 0 : Convert.ToInt32(appointmentcount.Rows[i]["NOofPeople"]),
                            MaxCapacity = appointmentcount.Rows[i]["MaxCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(appointmentcount.Rows[i]["MaxCapacity"]),
                            AppointmentCustomerList = new List<AppointmentCustomer>()
                        };


                        obj.AppointmentCustomerList = appointmentList.AsEnumerable().Where(x => (x.Field<string>("AppointmentDate")).
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
                if (appointmentcount != null)
                {
                    appointmentcount.Dispose();
                }
                if (appointmentList != null)
                {
                    appointmentList.Dispose();
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
                cmd.Parameters.AddWithValue("@_AptStatus", Convert.ToInt32(appointmentCustomer.Status));
                cmd.Parameters.AddWithValue("@_UserID", UserID);

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
        /// Get Appointment Message Tags
        /// </summary>
        /// <returns></returns>
        public List<AppointmentMessageTag> AppointmentMessageTags()
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<AppointmentMessageTag> appointmentsTags= new List<AppointmentMessageTag>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                cmd = new MySqlCommand("SP_HSGetAptMessageTags", conn);
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
                            AppointmentMessageTag Apt = new AppointmentMessageTag()
                            {

                                ID = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                                TagName = dr["TagName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TagName"]),
                                PlaceHolder = dr["PlacHolder"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PlacHolder"])

                            };

                            appointmentsTags.Add(Apt);
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

            return appointmentsTags;

        }

        /// <summary>
        /// Get Appointment Search List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserId"></param>
        /// <param name="appointmentSearchRequest"></param>
        /// <returns></returns>
        public async Task<List<AppointmentModel>> GetAppointmentSearchList(int TenantID, int UserId, AppointmentSearchRequest appointmentSearchRequest)
        {

            DataTable appointmentcount = new DataTable();
            DataTable appointmentList = new DataTable();
            MySqlCommand cmd = new MySqlCommand();
            List<AppointmentModel> appointments = new List<AppointmentModel>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                cmd = new MySqlCommand("SP_HSGetAppointmentSearchList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                cmd.Parameters.AddWithValue("@_UserId", UserId);
                cmd.Parameters.AddWithValue("@_AppointmentID", appointmentSearchRequest.AppointmentID);
                cmd.Parameters.AddWithValue("@_CustomerNumber", appointmentSearchRequest.CustomerNumber);
                cmd.Parameters.AddWithValue("@_AptDate", appointmentSearchRequest.Date);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        appointmentcount.Load(reader);
                        appointmentList.Load(reader);
                    }
                }
                if (appointmentcount != null )
                {
                    for (int i = 0; i < appointmentcount.Rows.Count; i++)
                    {
                        AppointmentModel obj = new AppointmentModel
                        {
                            AppointmentDate = appointmentcount.Rows[i]["AppointmentDate"] == DBNull.Value ? "" : Convert.ToString(appointmentcount.Rows[i]["AppointmentDate"]),
                            SlotId = appointmentcount.Rows[i]["SlotId"] == DBNull.Value ? 0 : Convert.ToInt32(appointmentcount.Rows[i]["SlotId"]),
                            TimeSlot = appointmentcount.Rows[i]["TimeSlot"] == DBNull.Value ? "" : Convert.ToString(appointmentcount.Rows[i]["TimeSlot"]),
                            NOofPeople = appointmentcount.Rows[i]["NOofPeople"] == DBNull.Value ? 0 : Convert.ToInt32(appointmentcount.Rows[i]["NOofPeople"]),
                            MaxCapacity = appointmentcount.Rows[i]["MaxCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(appointmentcount.Rows[i]["MaxCapacity"]),
                            AppointmentCustomerList = new List<AppointmentCustomer>()
                        };

                        obj.AppointmentCustomerList = appointmentList.AsEnumerable().Where(x => (x.Field<string>("AppointmentDate")).
                        Equals(obj.AppointmentDate) && (x.Field<int>("SlotId")).Equals(obj.SlotId)).Select(x => new AppointmentCustomer()
                        {
                            AppointmentID = Convert.ToInt32(x.Field<int>("AppointmentID")),
                            CustomerName = Convert.ToString(x.Field<string>("CustomerName")),
                            CustomerNumber = Convert.ToString(x.Field<string>("CustomerNumber")),
                            NOofPeople = Convert.ToInt32(x.Field<int>("NOofPeople")),
                            Status = Convert.ToString(x.Field<string>("Status").ToString())
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
                if (appointmentcount != null)
                {
                    appointmentcount.Dispose();
                }
                if (appointmentList != null)
                {
                    appointmentList.Dispose();
                }
            }

            return appointments;
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
                if (Slot.SlotId > 0) //update
                {
                    cmd.Parameters.AddWithValue("@_SlotSettingID", Slot.SlotId);
                }
                else //insert
                {
                    cmd.Parameters.AddWithValue("@_StoreIds", string.IsNullOrEmpty(Slot.StoreIds) ? "" : Slot.StoreIds.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@_OpDays", string.IsNullOrEmpty(Slot.StoreOpdays) ? "" : Slot.StoreOpdays.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@_SlotTemplateID", Slot.SlotTemplateID);
                    cmd.Parameters.AddWithValue("@_ApplicableFromDate", Slot.ApplicableFromDate);
                    cmd.Parameters.AddWithValue("@_Source", "insert");
                }


                cmd.Parameters.AddWithValue("@_AppointmentDays", Slot.AppointmentDays);
                cmd.Parameters.AddWithValue("@_IsActive", Convert.ToInt16(Slot.IsActive));
                cmd.Parameters.AddWithValue("@_SlotDisplayCode", Slot.SlotDisplayCode);
                cmd.Parameters.AddWithValue("@_SlotMaxCapacity", Slot.SlotMaxCapacity);
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
        /// Insert/ Update Time Slot Setting
        /// </summary>
        /// <param name="StoreTimeSlotInsertUpdate"></param>
        /// <returns></returns>
        /// 
        public async Task<int> UpdateTimeSlotSettingNew(StoreTimeSlotInsertUpdate Slot)
        {

            int Result = 0;
            string XmlSlots = string.Empty;
            XDocument SlotDetails = null;
            try
            {
               

                    if (Slot.TemplateSlots.Count > 0)
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

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSUpdateStoreTimeSlotSetting_New", conn);
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@_TenantId", Slot.TenantId);
                    cmd.Parameters.AddWithValue("@_ProgramCode", Slot.ProgramCode);

                   
                    cmd.Parameters.AddWithValue("@_SlotSettingID", Slot.SlotId);
                    cmd.Parameters.AddWithValue("@_OpDays", string.IsNullOrEmpty(Slot.StoreOpdays) ? "" : Slot.StoreOpdays.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@_SlotTemplateID", Slot.SlotTemplateID);
                    cmd.Parameters.AddWithValue("@_SlotMaxCapacity", Slot.SlotMaxCapacity);
                    cmd.Parameters.AddWithValue("@_AppointmentDays", Slot.AppointmentDays);
                    cmd.Parameters.AddWithValue("@_IsActive", Convert.ToInt16(Slot.IsActive));
                    cmd.Parameters.AddWithValue("@_XmlSlots", string.IsNullOrEmpty(XmlSlots) ? "" : XmlSlots);
                    cmd.Parameters.AddWithValue("@_SlotDisplayCode", Slot.SlotDisplayCode);
                    cmd.Parameters.AddWithValue("@_UserID", Slot.UserID);


                    cmd.CommandType = CommandType.StoredProcedure;
                    Result = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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
                cmd.Parameters.AddWithValue("@_Source", "delete");

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
        /// Delete HSTimeSlotMaster
        /// </summary>
        /// <param name="SlotIDs"></param>
        /// <returns></returns>
        public async Task<int> BulkDeleteTimeSlotMaster(string SlotIDs, int TenantID, string ProgramCode)
        {

            int Result = 0;
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSBulkDeleteStoreTimeSlotMaster", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@_SlotSettingIDs", string.IsNullOrEmpty(SlotIDs) ? "" : SlotIDs.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                    cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                    cmd.CommandType = CommandType.StoredProcedure;
                    Result = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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

            return Result;
        }



        /// <summary>
        /// Get HSTimeSlotMaster List
        /// </summary>
        /// <returns></returns>
        public List<StoreTimeSlotSettingModel> GetStoreSettingTimeSlot(int TenantID, string ProgramCode, int SlotID, int StoreID)
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
                                MaxCapacity = dr["MaxCapacityPerSlot"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxCapacityPerSlot"]),
                                TemplateSlots = ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].AsEnumerable().Where(x => (x.Field<int>("SlotSettingID")).Equals(dr["SlotSettingID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotSettingID"]))).Select(r => new TemplateBasedSlots()
                                {
                                    SlotID = r.Field<object>("SlotID") == DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("SlotID")),
                                    SlotStartTime = r.Field<object>("TimeSlot") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TimeSlot")).Split('-')[0],
                                    SlotEndTime = r.Field<object>("TimeSlot") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TimeSlot")).Split('-')[1],
                                    SlotOccupancy = r.Field<object>("MaxCapacity") == DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("MaxCapacity")),
                                    IsSlotEnabled = r.Field<object>("SlotStatus") == DBNull.Value ? false : Convert.ToBoolean(r.Field<object>("SlotStatus"))
                                }).ToList() : null
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

        /// <summary>
        /// Get HSTimeSlotMaster List
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="SlotID"></param>
        /// <param name="StoreID"></param>
        /// <param name="Opdays"></param>
        /// <param name="SlotTemplateID"></param>
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoreTimeSlotSettingModel>> GetStoreSettingTimeSlotNew(int TenantID, string ProgramCode,int SlotID, string StoreID,string Opdays, int SlotTemplateID)
        {
           
            List<StoreTimeSlotSettingModel> TimeSlotList = new List<StoreTimeSlotSettingModel>();
            List<TemplateBasedSlots> TemplateSlotsList = new List<TemplateBasedSlots>();
            List<OperationalDaysFilter> OpdayListModel = new List<OperationalDaysFilter>();
            DataTable SlotTable1 = new DataTable();
            DataTable SlotTable2 = new DataTable();
            List<string> OpDaysIDList =  new List<string>();
            List<string> OpDaysList = new List<string>();
            try
            {


                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_GetStoreTimeSlotSettingList_New", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                    cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                    cmd.Parameters.AddWithValue("@_SlotID", SlotID);
                    cmd.Parameters.AddWithValue("@_StoreID", string.IsNullOrEmpty(StoreID) ? "" : StoreID.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@_Opdays", string.IsNullOrEmpty(Opdays) ? "" : Opdays);
                    cmd.Parameters.AddWithValue("@_SlotTemplateID", SlotTemplateID);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            SlotTable1.Load(reader);
                            SlotTable2.Load(reader);
                        }

                        if (SlotTable1 != null && SlotTable1.Rows.Count > 0)
                        {
                            foreach (DataRow dr in SlotTable1.Rows)
                            {
                                OpDaysIDList.Clear(); OpDaysList.Clear(); OpdayListModel.Clear();

                                StoreTimeSlotSettingModel slot=new StoreTimeSlotSettingModel()

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
                                    MaxCapacity = dr["MaxCapacityPerSlot"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxCapacityPerSlot"]),

                                    TemplateSlots = SlotTable2 != null && SlotTable2.Rows.Count > 0 ? SlotTable2.AsEnumerable()
                                    .Where(x => (x.Field<int>("SlotSettingID")).Equals(dr["SlotSettingID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SlotSettingID"]))).Select(r => new TemplateBasedSlots()
                                    {
                                        SlotID = r.Field<object>("SlotID") == DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("SlotID")),
                                        SlotStartTime = r.Field<object>("TimeSlot") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TimeSlot")).Split('-')[0],
                                        SlotEndTime = r.Field<object>("TimeSlot") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TimeSlot")).Split('-')[1],
                                        SlotOccupancy = r.Field<object>("MaxCapacity") == DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("MaxCapacity")),
                                        IsSlotEnabled = r.Field<object>("SlotStatus") == DBNull.Value ? false : Convert.ToBoolean(r.Field<object>("SlotStatus"))
                                    }).ToList() : null
                                };

                                OpDaysIDList = dr["OperationalDaysID"] == DBNull.Value ? new List<string>() : Convert.ToString(dr["OperationalDaysID"]).Split(',').ToList();
                                OpDaysList = dr["OperationalDays"] == DBNull.Value ? new List<string>() : Convert.ToString(dr["OperationalDays"]).Split(',').ToList();
                                if (OpDaysIDList.Count > 0)
                                {
                                    for(int i=0; i< OpDaysIDList.Count; i++)
                                    {
                                        OperationalDaysFilter days = new OperationalDaysFilter()
                                        {
                                            DayIDs = OpDaysIDList[i],
                                            DayNames = OpDaysList[i],

                                        };
                                        OpdayListModel.Add(days);


                                    }
                                    slot.OperationalDaysList = OpdayListModel;
                                    
                                }

                                TimeSlotList.Add(slot);
                            }
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
                if (SlotTable1 != null)
                {
                    SlotTable1.Dispose();
                }
                if (SlotTable2 != null)
                {
                    SlotTable2.Dispose();
                }
            }

            return TimeSlotList;

        }

        /// <summary>
        /// Slot bulk upload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="RoleFor"></param>
        /// <param name="DataSetCSV"></param>
        public List<string> BulkUploadSlot( int TenantID, string ProgramCode, int CreatedBy,  DataSet DataSetCSV)
        {
            XmlDocument xmlDoc = null;//new XmlDocument();
            List<string> csvLst = new List<string>();
            string SuccessFile = string.Empty; string ErrorFile = string.Empty;
            DataTable SuccessDt=new DataTable();
            DataTable ErrorDt= new DataTable(); 
            List<string> StoreList = new List<string>();

            try
            {

                if (DataSetCSV != null && DataSetCSV.Tables.Count > 0)
                {

                    if (DataSetCSV.Tables[0] != null && DataSetCSV.Tables[0].Rows.Count > 0)
                    {

                        StoreList = DataSetCSV.Tables[0].AsEnumerable().Select(x => Convert.ToString(x.Field<object>("StoreCode"))).Distinct().ToList();

                        if(StoreList.Count > 0)
                        {
                            if (conn != null && conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            foreach (string store in StoreList)
                            {
                                DataSet Dstemp = new DataSet();
                                DataSet Bulkds = new DataSet();

                            try
                            { 
                                xmlDoc = new XmlDocument();
                                Dstemp.Tables.Add(DataSetCSV.Tables[0].AsEnumerable().
                                                            Where(x => Convert.ToString(x.Field<object>("StoreCode")).Equals(store)).CopyToDataTable());
                                xmlDoc.LoadXml(Dstemp.GetXml());

                                 

                                    MySqlCommand cmd = new MySqlCommand("SP_HSBulkUploadSlot", conn);
                                cmd.Connection = conn;
                                cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                                cmd.Parameters.AddWithValue("@_node", Xpath);
                                cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                                cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                                cmd.Parameters.AddWithValue("@_createdBy", CreatedBy);

                                cmd.CommandType = CommandType.StoredProcedure;
                                MySqlDataAdapter da = new MySqlDataAdapter();
                                da.SelectCommand = cmd;
                                da.Fill(Bulkds);

                                if (Bulkds != null && Bulkds.Tables[0] != null && Bulkds.Tables[1] != null)
                                {
                                        // for success file
                                        if (Bulkds.Tables[0].Rows.Count > 0)
                                           SuccessDt.Merge(Bulkds.Tables[0]);

                                        // for Error file
                                        if (Bulkds.Tables[1].Rows.Count > 0)
                                            ErrorDt.Merge(Bulkds.Tables[1]);

                                }

                            }
                            catch (Exception ) {
                                    
                                }
                            finally
                            {

                                Dstemp.Dispose();

                            }

                            }

                            //for success file
                            SuccessFile = SuccessDt.Rows.Count > 0 ? CommonService.DataTableToCsv(SuccessDt) : string.Empty;
                            csvLst.Add(SuccessFile);

                            //for error file
                            ErrorFile = ErrorDt.Rows.Count > 0 ? CommonService.DataTableToCsv(ErrorDt) : string.Empty;
                            csvLst.Add(ErrorFile);



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
                if (DataSetCSV != null)
                {
                    DataSetCSV.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return csvLst;

        }

        /// <summary>
        /// Slot bulk upload New
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="RoleFor"></param>
        /// <param name="DataSetCSV"></param>
        public async Task<List<string>> BulkUploadSlotNew(int TenantID, string ProgramCode, int CreatedBy, DataSet DataSetCSV)
        {
            XmlDocument xmlDoc = null;//new XmlDocument();
            List<string> csvLst = new List<string>();
            string SuccessFile = string.Empty;
            string ErrorFile = string.Empty;
            DataTable SuccessDt = new DataTable();
            DataTable ErrorDt = new DataTable();

           
            List<string> StoreList = new List<string>();

            try
            {

                if (DataSetCSV != null && DataSetCSV.Tables.Count > 0)
                {

                    if (DataSetCSV.Tables[0] != null && DataSetCSV.Tables[0].Rows.Count > 0)
                    {

                        StoreList = DataSetCSV.Tables[0].AsEnumerable().Select(x => Convert.ToString(x.Field<object>("StoreCode"))).Distinct().ToList();

                        if (StoreList.Count > 0)
                        {
                            if (conn != null && conn.State == ConnectionState.Closed)
                            {
                                await conn.OpenAsync();
                            }

                            using (conn)
                            {

                                foreach (string store in StoreList)
                                {
                                    DataSet Dstemp = new DataSet();
                                    DataTable TempDtSuccess = new DataTable();
                                    DataTable TempDtError = new DataTable();

                                    try
                                    {
                                        xmlDoc = new XmlDocument();
                                        Dstemp.Tables.Add(DataSetCSV.Tables[0].AsEnumerable().
                                                                    Where(x => Convert.ToString(x.Field<object>("StoreCode")).Equals(store)).CopyToDataTable());
                                        xmlDoc.LoadXml(Dstemp.GetXml());

                                        using (MySqlCommand cmd = new MySqlCommand("SP_HSBulkUploadSlot_New", conn))
                                        {
                                            cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                                            cmd.Parameters.AddWithValue("@_node", Xpath);
                                            cmd.Parameters.AddWithValue("@_TenantId", TenantID);
                                            cmd.Parameters.AddWithValue("@_ProgramCode", ProgramCode);
                                            cmd.Parameters.AddWithValue("@_createdBy", CreatedBy);
                                            cmd.CommandType = CommandType.StoredProcedure;

                                            using (var reader = await cmd.ExecuteReaderAsync())
                                            {
                                                if (reader.HasRows)
                                                {
                                                    TempDtSuccess.Load(reader);
                                                    TempDtError.Load(reader);
                                                }
                                                // for success file
                                                if (TempDtSuccess!=null && TempDtSuccess.Rows.Count > 0)
                                                { 
                                                    SuccessDt.Merge(TempDtSuccess);
                                                }
                                                // for error file
                                                if (TempDtError != null && TempDtError.Rows.Count > 0)
                                                {
                                                    ErrorDt.Merge(TempDtError);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception) { }
                                    finally
                                    {

                                        Dstemp.Dispose();
                                        TempDtSuccess.Dispose();
                                        TempDtError.Dispose();
                                    }

                                }
                            }


                            //for success file
                            SuccessFile = SuccessDt.Rows.Count > 0 ? CommonService.DataTableToCsv(SuccessDt) : string.Empty;
                            csvLst.Add(SuccessFile);

                            //for error file
                            ErrorFile = ErrorDt.Rows.Count > 0 ? CommonService.DataTableToCsv(ErrorDt) : string.Empty;
                            csvLst.Add(ErrorFile);



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
                if (DataSetCSV != null)
                {
                    DataSetCSV.Dispose();
                }
                if (SuccessDt != null)
                {
                    SuccessDt.Dispose();
                }
                if (ErrorDt != null)
                {
                    ErrorDt.Dispose();
                }
                
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return csvLst;

        }


        /// <summary>
        /// Insert/ Update Time Slot Setting
        /// </summary>
        /// <param name="SlotsBulkUpdate"></param>
        /// <returns></returns>
        /// 
        public async Task<int> BulkUpdateSlots(SlotsBulkUpdate Slot)
        {

            int Result = 0;
            string XmlSlots = string.Empty;
            XDocument SlotDetails = null;
            try
            {


                if (Slot.TemplateSlots.Count > 0)
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

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                using (conn)
                {
                    MySqlCommand cmd = new MySqlCommand("SP_HSBulkUpdateSlots", conn);
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@_TenantId", Slot.TenantId);
                    cmd.Parameters.AddWithValue("@_ProgramCode", Slot.ProgramCode);
                    cmd.Parameters.AddWithValue("@_SlotSettingIDs", string.IsNullOrEmpty(Slot.SlotSettingIds) ? "" : Slot.SlotSettingIds.TrimEnd(',') );
                    cmd.Parameters.AddWithValue("@_OpDays", string.IsNullOrEmpty(Slot.StoreOpdays) ? "" : Slot.StoreOpdays.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@_SlotTemplateID", Slot.SlotTemplateID);
                    cmd.Parameters.AddWithValue("@_XmlSlots", string.IsNullOrEmpty(XmlSlots) ? "" : XmlSlots);
                    cmd.Parameters.AddWithValue("@_UserID", Slot.UserID);


                    cmd.CommandType = CommandType.StoredProcedure;
                    Result = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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

            return Result;
        }

        #endregion
    }



}
