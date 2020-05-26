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

        public StoreDashboardService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        public LoggedInAgentModel GetLogginAccountInfo(int tenantID, int UserId, string ProfilePicPath)
        {
            DataSet ds = new DataSet();
            DateTime now = DateTime.Now; DateTime temp = new DateTime();
            TimeSpan diff = new TimeSpan();
            MySqlCommand cmd = new MySqlCommand();
            LoggedInAgentModel loggedInAcc = new LoggedInAgentModel();
            ChatStatus chatstat = new ChatStatus();
            string profileImage = string.Empty;
            int ShiftDuration = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;

                loggedInAcc.AgentId = UserId;
                //loggedInAcc.AgentName = AccountName;
                //loggedInAcc.AgentEmailId = EmailID;

                MySqlCommand cmd1 = new MySqlCommand("SP_StoreLoggedInAccountInformation", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("_tenantID", tenantID);
                cmd1.Parameters.AddWithValue("_userID", UserId);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd1
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
                    //if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    //{
                    //    loggedInAcc.SLAScore = ds.Tables[1].Rows[0]["SLAScore"] != System.DBNull.Value ? Convert.ToString(ds.Tables[1].Rows[0]["SLAScore"]) : string.Empty;
                    //    loggedInAcc.AvgResponseTime = ds.Tables[1].Rows[0]["AverageResponseTime"] != System.DBNull.Value ? Convert.ToString(ds.Tables[1].Rows[0]["AverageResponseTime"]) : string.Empty;
                    //    loggedInAcc.CSATScore = ds.Tables[1].Rows[0]["CSATScore"] != System.DBNull.Value ? Convert.ToString(ds.Tables[1].Rows[0]["CSATScore"]) : string.Empty;
                    //}

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
                if (ds != null) ds.Dispose(); conn.Close();
            }

            return loggedInAcc;
        }

        /// <summary>
        /// Get task Data For store dashboard
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<StoreDashboardResponseModel> GetTaskDataForStoreDashboard(StoreDashboardModel model)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreDashboardResponseModel> departmentMasters = new List<StoreDashboardResponseModel>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("sp_getStoreDashboardTaskData", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@objtaskID", model.taskid);
                cmd1.Parameters.AddWithValue("@objtaskTitle", model.tasktitle);
                cmd1.Parameters.AddWithValue("@objtaskStatus", model.taskstatus);
                cmd1.Parameters.AddWithValue("@objticketID", model.ticketID);
                cmd1.Parameters.AddWithValue("@objDepartment", model.Department);
                cmd1.Parameters.AddWithValue("@objfuncation", model.functionID);
                cmd1.Parameters.AddWithValue("@objcreatedFrom", model.CreatedOnFrom);
                cmd1.Parameters.AddWithValue("@objcreatedTo", model.CreatedOnTo);
                cmd1.Parameters.AddWithValue("@objassignTo", model.AssigntoId);
                cmd1.Parameters.AddWithValue("@objtaskCreatedBy", model.createdID);
                cmd1.Parameters.AddWithValue("@objtaskwithticket", model.taskwithTicket);
                cmd1.Parameters.AddWithValue("@objtaskwithclaim", model.taskwithClaim);
                cmd1.Parameters.AddWithValue("@objclaimID", model.claimID);
                cmd1.Parameters.AddWithValue("@objtaskPriority", model.Priority);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                  

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                       string TaskStatusName = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TaskStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"]));

                        StoreDashboardResponseModel storedashboard = new StoreDashboardResponseModel();
                        storedashboard.taskid = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        storedashboard.totalCount = ds.Tables[0].Rows.Count;
                        storedashboard.taskstatus = TaskStatusName;

                        storedashboard.tasktitle = Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]);

                        storedashboard.Department = Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]);

                        storedashboard.storeName = Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);

                        storedashboard.StoreAddress = Convert.ToString(ds.Tables[0].Rows[i]["StoreAddress"]);

                        storedashboard.Priority = Convert.ToString(ds.Tables[0].Rows[i]["Priorty"]);

                        storedashboard.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]);


                        storedashboard.AssigntoId = Convert.ToString(ds.Tables[0].Rows[i]["Assignto"]);

                        storedashboard.CreatedBy = Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]);
                        storedashboard.modifedOn = Convert.ToString(ds.Tables[0].Rows[i]["Modifiedon"]);


                        storedashboard.ModifiedBy = Convert.ToString(ds.Tables[0].Rows[i]["ModifiedBy"]);


                       

                        departmentMasters.Add(storedashboard);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return departmentMasters;

        }

        /// <summary>
        /// Get task Data For store dashboard for claim
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>

        public List<StoreDashboardClaimResponseModel> GetClaimDataForStoreDashboard(StoreDashboardClaimModel model)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreDashboardClaimResponseModel> ClaimSearchResponse = new List<StoreDashboardClaimResponseModel>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("sp_getStoreDashboardClaimData", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@objclaimID", model.claimID == null ? 0 : model.claimID);
                cmd1.Parameters.AddWithValue("@objticketID", model.ticketID == null ? 0 : model.ticketID);
                cmd1.Parameters.AddWithValue("@objclaimissueType", model.claimissueType == null ? 0 : model.claimissueType);
                cmd1.Parameters.AddWithValue("@objticketMapped", model.ticketMapped == null ? 0 : model.ticketMapped);
                cmd1.Parameters.AddWithValue("@objclaimsubcat", model.claimsubcat == null ? 0 : model.claimsubcat);
                cmd1.Parameters.AddWithValue("@objassignTo", model.assignTo == null ? 0 : model.assignTo);
                cmd1.Parameters.AddWithValue("@objclaimcat", model.claimcat == null ? 0 : model.claimcat);
                cmd1.Parameters.AddWithValue("@objclaimraise", string.IsNullOrEmpty(model.claimraiseddate) ? "" : model.claimraiseddate);
                cmd1.Parameters.AddWithValue("@objtaskID", model.taskID == null ? 0 : model.taskID);
                cmd1.Parameters.AddWithValue("@objclaimstatus", model.claimstatus == null ? 0 : model.claimstatus);
                cmd1.Parameters.AddWithValue("@objtaskmapped", model.taskmapped == null ? 0 : model.taskmapped);
                cmd1.Parameters.AddWithValue("@objraisedby", model.raisedby == null ? 0 : model.raisedby);
                cmd1.Parameters.AddWithValue("@objtenantID", model.tenantID);


                cmd1.Parameters.AddWithValue("@objbrandIDs", string.IsNullOrEmpty(model.BrandIDs) ? "" : model.BrandIDs.TrimEnd(','));
                cmd1.Parameters.AddWithValue("@objAgentIds", string.IsNullOrEmpty(model.AgentIds) ? "" : model.AgentIds.TrimEnd(','));
                cmd1.Parameters.AddWithValue("@objFromDate", string.IsNullOrEmpty(model.FromDate) ? "" : model.FromDate);
                cmd1.Parameters.AddWithValue("@objToDate", string.IsNullOrEmpty(model.ToDate) ? "" : model.ToDate); 



                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {


                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                       
                        StoreDashboardClaimResponseModel StoreDashboard = new StoreDashboardClaimResponseModel();

                        StoreDashboard.ClaimID = ds.Tables[0].Rows[i]["ClaimID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ClaimID"]);
                        StoreDashboard.ClaimStatusId = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"]);
                        StoreDashboard.ClaimStatus = StoreDashboard.ClaimStatusId.Equals(0) ? string.Empty : Convert.ToString((EnumMaster.ClaimStatus)StoreDashboard.ClaimStatusId);
                        StoreDashboard.BrandID = ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        StoreDashboard.BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        StoreDashboard.CategoryID = ds.Tables[0].Rows[i]["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        StoreDashboard.CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        StoreDashboard.SubCategoryID = ds.Tables[0].Rows[i]["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        StoreDashboard.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        StoreDashboard.IssueTypeID = ds.Tables[0].Rows[i]["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        StoreDashboard.IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);

                        StoreDashboard.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        StoreDashboard.CreatedByName = ds.Tables[0].Rows[i]["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedByName"]);
                        StoreDashboard.AssignedId = ds.Tables[0].Rows[i]["AssignedID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["AssignedID"]);
                        StoreDashboard.AssignTo = ds.Tables[0].Rows[i]["AssignedToName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AssignedToName"]);
                        StoreDashboard.ClaimRaisedByID = ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"]);
                        StoreDashboard.ClaimRaisedBy = ds.Tables[0].Rows[i]["Raisedby"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Raisedby"]);
                        StoreDashboard.CreationOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]);

                        ClaimSearchResponse.Add(StoreDashboard);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return ClaimSearchResponse;

        }


    }
}
