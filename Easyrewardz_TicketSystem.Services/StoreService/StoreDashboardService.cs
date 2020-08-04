using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreDashboardService : IStoreDashboard
    {
        public CultureInfo culture = CultureInfo.InvariantCulture;
        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion

        /// <summary>
        /// Get Brand list for drop down 
        /// </summary>
        /// <param name="EncptToken"></param>
        /// <returns></returns>
        MySqlConnection conn = new MySqlConnection();

        #region Constructor
        public StoreDashboardService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        /// <summary>
        /// Get Loggin Account Info
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserId"></param>
        /// <param name="ProfilePicPath"></param>
        /// <returns></returns>
        public LoggedInAgentModel GetLogginAccountInfo(int tenantID, int UserId, string ProfilePicPath)
        {
            DataSet ds = new DataSet();
            DateTime now = DateTime.Now; DateTime temp = new DateTime();
            TimeSpan diff = new TimeSpan();
            LoggedInAgentModel loggedInAcc = new LoggedInAgentModel();
            ChatStatus chatstat = new ChatStatus();
            string profileImage = string.Empty;
            int ShiftDuration = 0;
            try
            {
                conn.Open();
                loggedInAcc.AgentId = UserId;
                MySqlCommand cmd = new MySqlCommand("SP_StoreLoggedInAccountInformation", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_tenantID", tenantID);
                cmd.Parameters.AddWithValue("_userID", UserId);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        loggedInAcc.LoginTime = ds.Tables[0].Rows[0]["logintime"] != System.DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["logintime"]).ToString("h:mm tt", culture) : "";
                        loggedInAcc.LogoutTime = ds.Tables[0].Rows[0]["logouttime"] != System.DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["logouttime"]).ToString("h:mm tt", culture) : "";
                        loggedInAcc.AgentName = ds.Tables[0].Rows[0]["AccountName"] != System.DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["AccountName"]) : string.Empty;
                        loggedInAcc.AgentEmailId = ds.Tables[0].Rows[0]["EmailID"] != System.DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]) : string.Empty;
                        ShiftDuration = ds.Tables[0].Rows[0]["ShiftDuration"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["ShiftDuration"]) : 0;
                        profileImage = ds.Tables[0].Rows[0]["ProfilePicture"] != System.DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["ProfilePicture"]) : string.Empty;

                        loggedInAcc.ProfilePicture = !string.IsNullOrEmpty(profileImage) ? Path.Combine(ProfilePicPath, profileImage) : string.Empty;
                        if (ShiftDuration > 0)
                        {
                            temp = temp.AddHours(ShiftDuration);
                            loggedInAcc.ShiftDurationInHour = temp.Hour;
                            loggedInAcc.ShiftDurationInMinutes = temp.Minute;
                        }
                        if (!string.IsNullOrEmpty(loggedInAcc.LoginTime))
                        {
                            diff = now - Convert.ToDateTime(ds.Tables[0].Rows[0]["logintime"]);
                            loggedInAcc.LoggedInDuration = Math.Abs(diff.Hours) + "H " + Math.Abs(diff.Minutes) + "M";
                            loggedInAcc.LoggedInDurationInHours = Math.Abs(diff.Hours);
                            loggedInAcc.LoggedInDurationInMinutes = Math.Abs(diff.Minutes);

                            chatstat.isOnline = true;
                        }
                        else
                        {
                            loggedInAcc.LoggedInDuration = "0 H 0 M";
                            chatstat.isOffline = true;
                        }
                        loggedInAcc.Chatstatus = chatstat;
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        loggedInAcc.WorkTimeInPercentage = Convert.ToString(ds.Tables[1].Rows[0]["WorkTimeInPercentage"]);
                        loggedInAcc.TotalWorkingTime = Convert.ToString(ds.Tables[1].Rows[0]["TotalWorkingTime"]);
                        loggedInAcc.workingMinute = Convert.ToString(ds.Tables[1].Rows[0]["workingMinute"]);
                    }
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        loggedInAcc.ProgramCode = ds.Tables[2].Rows[0]["ProgramCode"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["ProgramCode"]);
                        loggedInAcc.StoreID = ds.Tables[2].Rows[0]["StoreID"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[2].Rows[0]["StoreID"]);
                        loggedInAcc.StoreCode = ds.Tables[2].Rows[0]["StoreCode"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[0]["StoreCode"]);
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
            return loggedInAcc;
        }

        /// <summary>
        /// Get task Data For store dashboard
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<StoreDashboardResponseModel> GetTaskDataForStoreDashboard(StoreDashboardModel model)
        {
            DataSet ds = new DataSet();
            List<StoreDashboardResponseModel> departmentMasters = new List<StoreDashboardResponseModel>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getStoreDashboardTaskData", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@objtaskID", model.taskid);
                cmd.Parameters.AddWithValue("@objtaskTitle", model.tasktitle);
                cmd.Parameters.AddWithValue("@objtaskStatus", model.taskstatus);
                cmd.Parameters.AddWithValue("@objticketID", model.ticketID);
                cmd.Parameters.AddWithValue("@objDepartment", model.Department);
                cmd.Parameters.AddWithValue("@objfuncation", model.functionID);
                cmd.Parameters.AddWithValue("@objcreatedFrom", model.CreatedOnFrom);
                cmd.Parameters.AddWithValue("@objcreatedTo", model.CreatedOnTo);
                cmd.Parameters.AddWithValue("@objassignTo", model.AssigntoId);
                cmd.Parameters.AddWithValue("@objtaskCreatedBy", model.createdID);
                cmd.Parameters.AddWithValue("@objtaskwithticket", model.taskwithTicket);
                cmd.Parameters.AddWithValue("@objtaskwithclaim", model.taskwithClaim);
                cmd.Parameters.AddWithValue("@objclaimID", model.claimID);
                cmd.Parameters.AddWithValue("@objtaskPriority", model.Priority);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                       string TaskStatusName = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TaskStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"]));

                        StoreDashboardResponseModel storedashboard = new StoreDashboardResponseModel
                        {
                            taskid = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            totalCount = ds.Tables[0].Rows.Count,
                            taskstatus = TaskStatusName,
                            tasktitle = ds.Tables[0].Rows[i]["TaskTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]),
                            Department = ds.Tables[0].Rows[i]["DepartmentName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]),
                            storeName = ds.Tables[0].Rows[i]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]),
                            StoreAddress = ds.Tables[0].Rows[i]["StoreAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreAddress"]),
                            Priority = ds.Tables[0].Rows[i]["Priorty"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Priorty"]),
                            CreatedOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            AssigntoId = ds.Tables[0].Rows[i]["Assignto"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignto"]),
                            CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]),
                            modifedOn = ds.Tables[0].Rows[i]["Modifiedon"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Modifiedon"]),
                            ModifiedBy = ds.Tables[0].Rows[i]["ModifiedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedBy"]),
                            FunctionName = ds.Tables[0].Rows[i]["FuncationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FuncationName"]),
                            Createdago = ds.Tables[0].Rows[i]["CreatedDate"] == System.DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]), "CreatedSpan"),
                            Assignedago = ds.Tables[0].Rows[i]["AssignedDate"] == System.DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(ds.Tables[0].Rows[i]["AssignedDate"]), "AssignedSpan"),
                            Updatedago = ds.Tables[0].Rows[i]["ModifiedDate"] == System.DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDate"]), "ModifiedSpan"),
                            TaskCloureDate = ds.Tables[0].Rows[i]["ClosureTaskDate"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ClosureTaskDate"]),
                            ResolutionTimeRemaining = ds.Tables[0].Rows[i]["RemainingTime"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["RemainingTime"]),
                            ResolutionOverdueBy = ds.Tables[0].Rows[i]["taskoverDue"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["taskoverDue"]),
                            ColorName = ds.Tables[0].Rows[i]["ColorName"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]),
                            ColorCode = ds.Tables[0].Rows[i]["ColorCode"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ColorCode"])
                        };
                        departmentMasters.Add(storedashboard);
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
            return departmentMasters;

        }

        /// <summary>
        /// set Creation details
        /// </summary>
        /// <param name="time"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        public string SetCreationdetails(string time, string ColName)
        {
            string timespan = string.Empty;
            DateTime now = DateTime.Now;
            TimeSpan diff = new TimeSpan();
            string[] PriorityArr = null;
            string spantext = "{0}D {1}H {2}M Ago";
            try
            {
                if (ColName == "CreatedSpan" || ColName == "ModifiedSpan" || ColName == "AssignedSpan")
                {
                    diff = DateTime.Now - Convert.ToDateTime(time);
                    timespan = string.Format(spantext, Math.Abs(diff.Days), Math.Abs(diff.Hours), Math.Abs(diff.Minutes));

                }
                else if (ColName == "RespondTimeRemainingSpan")
                {
                    PriorityArr = time.Split(new char[] { '|' })[0].Split(new char[] { '-' });
                    DateTime assigneddate = Convert.ToDateTime(time.Split(new char[] { '|' })[1]);

                    switch (PriorityArr[1])
                    {
                        case "D":
                            if (assigneddate.AddDays(Convert.ToDouble(PriorityArr[0])) > DateTime.Now)
                            {
                                diff = (assigneddate.AddDays(Convert.ToDouble(PriorityArr[0]))) - DateTime.Now;
                            }
                            break;
                        case "H":
                            if (assigneddate.AddHours(Convert.ToDouble(PriorityArr[0])) > DateTime.Now)
                            {
                                diff = (assigneddate.AddHours(Convert.ToDouble(PriorityArr[0]))) - DateTime.Now;
                            }
                            break;
                        case "M":
                            if (assigneddate.AddMinutes(Convert.ToDouble(PriorityArr[0])) > DateTime.Now)
                            {
                                diff = (assigneddate.AddMinutes(Convert.ToDouble(PriorityArr[0]))) - DateTime.Now;
                            }
                            break;
                    }
                    timespan = string.Format(spantext, Math.Abs(diff.Days), Math.Abs(diff.Hours), Math.Abs(diff.Minutes));
                }
                else if (ColName == "ResponseOverDueSpan" || ColName == "ResolutionOverDueSpan")
                {
                    PriorityArr = time.Split(new char[] { '|' })[0].Split(new char[] { '-' });
                    DateTime assigneddate = Convert.ToDateTime(time.Split(new char[] { '|' })[1]);

                    switch (PriorityArr[1])
                    {
                        case "D":
                            if (assigneddate.AddDays(Convert.ToDouble(PriorityArr[0])) < DateTime.Now)
                            {
                                diff = DateTime.Now - (assigneddate.AddDays(Convert.ToDouble(PriorityArr[0])));
                            }
                            break;
                        case "H":
                            if (assigneddate.AddHours(Convert.ToDouble(PriorityArr[0])) < DateTime.Now)
                            {
                                diff = DateTime.Now - (assigneddate.AddHours(Convert.ToDouble(PriorityArr[0])));
                            }
                            break;
                        case "M":
                            if (assigneddate.AddMinutes(Convert.ToDouble(PriorityArr[0])) < DateTime.Now)
                            {
                                diff = DateTime.Now - (assigneddate.AddMinutes(Convert.ToDouble(PriorityArr[0])));
                            }
                            break;
                    }
                    timespan = string.Format(spantext, Math.Abs(diff.Days), Math.Abs(diff.Hours), Math.Abs(diff.Minutes));
                }
            }
            catch (Exception ex)
            {
               // throw;
            }
            finally
            {
                if (PriorityArr != null && PriorityArr.Length > 0)
                    Array.Clear(PriorityArr, 0, PriorityArr.Length);
            }
            return timespan;

        }

        /// <summary>
        /// Get task Data For store dashboard for claim
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>

        public List<CustomClaimList> GetClaimDataForStoreDashboard(StoreDashboardClaimModel model)
        {
            DataSet ds = new DataSet();
            List<CustomClaimList> ClaimSearchResponse = new List<CustomClaimList>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getStoreDashboardClaimData", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@objclaimID", model.claimID == null ? 0 : model.claimID);
                cmd.Parameters.AddWithValue("@objticketID", model.ticketID == null ? 0 : model.ticketID);
                cmd.Parameters.AddWithValue("@objclaimissueType", model.claimissueType == null ? 0 : model.claimissueType);
                cmd.Parameters.AddWithValue("@objticketMapped", model.ticketMapped == null ? 0 : model.ticketMapped);
                cmd.Parameters.AddWithValue("@objclaimsubcat", model.claimsubcat == null ? 0 : model.claimsubcat);
                cmd.Parameters.AddWithValue("@objassignTo", model.assignTo == null ? 0 : model.assignTo);
                cmd.Parameters.AddWithValue("@objclaimcat", model.claimcat == null ? 0 : model.claimcat);
                cmd.Parameters.AddWithValue("@objclaimraise", string.IsNullOrEmpty(model.claimraiseddate) ? "" : model.claimraiseddate);
                cmd.Parameters.AddWithValue("@objtaskID", model.taskID == null ? 0 : model.taskID);
                cmd.Parameters.AddWithValue("@objclaimstatus", model.claimstatus == null ? 0 : model.claimstatus);
                cmd.Parameters.AddWithValue("@objtaskmapped", model.taskmapped == null ? 0 : model.taskmapped);
                cmd.Parameters.AddWithValue("@objraisedby", model.raisedby == null ? 0 : model.raisedby);
                cmd.Parameters.AddWithValue("@objtenantID", model.tenantID);
                cmd.Parameters.AddWithValue("@objbrandIDs", string.IsNullOrEmpty(model.BrandIDs) ? "" : model.BrandIDs.TrimEnd(','));
                cmd.Parameters.AddWithValue("@objAgentIds", string.IsNullOrEmpty(model.AgentIds) ? "" : model.AgentIds.TrimEnd(','));
                cmd.Parameters.AddWithValue("@objFromDate", string.IsNullOrEmpty(model.FromDate) ? "" : model.FromDate);
                cmd.Parameters.AddWithValue("@objToDate", string.IsNullOrEmpty(model.ToDate) ? "" : model.ToDate);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomClaimList StoreDashboard = new CustomClaimList();
                        StoreDashboard.ClaimID = ds.Tables[0].Rows[i]["ClaimID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ClaimID"]);
                        //StoreDashboard.ClaimStatusId = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"]);
                        StoreDashboard.Status = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.ClaimStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"]));
                        StoreDashboard.BrandID = ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        StoreDashboard.BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        StoreDashboard.CategoryID = ds.Tables[0].Rows[i]["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        StoreDashboard.CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        StoreDashboard.SubCategoryID = ds.Tables[0].Rows[i]["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        StoreDashboard.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        StoreDashboard.IssueTypeID = ds.Tables[0].Rows[i]["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        StoreDashboard.IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        StoreDashboard.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        //StoreDashboard.CreatedByName = ds.Tables[0].Rows[i]["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedByName"]);
                        //StoreDashboard.AssignedId = ds.Tables[0].Rows[i]["AssignedID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["AssignedID"]);
                        StoreDashboard.AssignTo = ds.Tables[0].Rows[i]["AssignedToName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AssignedToName"]);
                        //StoreDashboard.ClaimRaisedByID = ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"]);
                        StoreDashboard.RaiseBy = ds.Tables[0].Rows[i]["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedByName"]);
                        StoreDashboard.CreationOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]);
                        StoreDashboard.CreationAgo = ds.Tables[0].Rows[i]["CreationAgo"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(ds.Tables[0].Rows[i]["CreationAgo"]), "CreatedSpan");
                        StoreDashboard.AssignOn = ds.Tables[0].Rows[i]["AssignedOn"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(ds.Tables[0].Rows[i]["AssignedOn"]), "AssignedSpan");
                        StoreDashboard.ModifyOn = ds.Tables[0].Rows[i]["ModifyOn"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(ds.Tables[0].Rows[i]["ModifyOn"]), "ModifiedSpan");
                        ClaimSearchResponse.Add(StoreDashboard);
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
            return ClaimSearchResponse;
        }
    }
}
