using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreTaskService : IStoreTask
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public StoreTaskService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion
        /// <summary>
        /// Add Task Details
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int AddTaskDetails(TaskMaster taskMaster, int TenantID, int UserID)
        {
            int taskId = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_createStoreTask", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Task_Title", taskMaster.TaskTitle);
                cmd.Parameters.AddWithValue("@Task_Description", taskMaster.TaskDescription);
                cmd.Parameters.AddWithValue("@Department_Id", taskMaster.DepartmentId);
                cmd.Parameters.AddWithValue("@Function_ID", taskMaster.FunctionID);
                cmd.Parameters.AddWithValue("@AssignTo_ID", taskMaster.AssignToID);
                cmd.Parameters.AddWithValue("@Priority_ID", taskMaster.PriorityID);
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd.Parameters.AddWithValue("@Created_By", UserID);
                
                taskId = Convert.ToInt32(cmd.ExecuteNonQuery());

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
            return taskId;
        }

        /// <summary>
        /// Get Task List
        /// </summary>
        /// <param name="tabFor"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<CustomStoreTaskDetails> GetTaskList(int tabFor, int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            List<CustomStoreTaskDetails> lsttask = new List<CustomStoreTaskDetails>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreTaskList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@tab_For", tabFor);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@user_ID", userID);
                
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomStoreTaskDetails taskMaster = new CustomStoreTaskDetails
                        {
                            StoreTaskID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            TaskStatus = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TaskStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"])),
                            TaskTitle = ds.Tables[0].Rows[i]["TaskTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]),
                            TaskDescription = ds.Tables[0].Rows[i]["TaskDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TaskDescription"]),
                            DepartmentName = ds.Tables[0].Rows[i]["Departmentname"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Departmentname"]),
                            StoreName = ds.Tables[0].Rows[i]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]),
                            StoreAddress = ds.Tables[0].Rows[i]["StoreAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreAddress"]),
                            CreatedBy = ds.Tables[0].Rows[i]["Createdby"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Createdby"]),
                            UpdatedBy = ds.Tables[0].Rows[i]["ModifiedBy"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedBy"]),
                            CreationOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            Assignto = ds.Tables[0].Rows[i]["Assignto"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignto"]),
                            PriorityName = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]),
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
                        lsttask.Add(taskMaster);
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
            return lsttask;
        }

        /// <summary>
        /// Get Store Task By ID
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public StoreTaskMaster GetStoreTaskByID(int TaskID, int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            StoreTaskMaster storetaskmaster = new StoreTaskMaster();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetTaskByTaskID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_TaskID", TaskID);
                cmd.Parameters.AddWithValue("@_UserID", UserID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            storetaskmaster = new StoreTaskMaster()
                            {
                                TaskID = Convert.ToInt32(ds.Tables[0].Rows[0]["TaskID"]),
                                TaskTitle = ds.Tables[0].Rows[0]["TaskTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["TaskTitle"]),
                                TaskDescription = ds.Tables[0].Rows[0]["TaskDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["TaskDescription"]),
                                DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[0]["DepartmentId"]),
                                FunctionID = Convert.ToInt32(ds.Tables[0].Rows[0]["FunctionID"]),
                                PriorityID = Convert.ToInt32(ds.Tables[0].Rows[0]["PriorityID"]),
                                TaskStatusId = Convert.ToInt32(ds.Tables[0].Rows[0]["TaskStatusId"]),
                                TaskStatusName = ds.Tables[0].Rows[0]["TaskStatusId"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TaskStatus)Convert.ToInt32(ds.Tables[0].Rows[0]["TaskStatusId"])),
                                AssignToName = ds.Tables[0].Rows[0]["AssignToName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["AssignToName"]),
                                CreatedByName = ds.Tables[0].Rows[0]["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CreatedByName"]),
                                StoreName = ds.Tables[0].Rows[0]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]),
                                Address = ds.Tables[0].Rows[0]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Address"]),
                                StoreCode = ds.Tables[0].Rows[0]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StoreCode"]),
                                CanEdit = Convert.ToInt32(ds.Tables[0].Rows[0]["CanEdit"]),
                                CanSubmit = Convert.ToInt32(ds.Tables[0].Rows[0]["CanSubmit"]),
                                IsAssignTo = Convert.ToInt32(ds.Tables[0].Rows[0]["IsAssignTo"]),
                            };
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
            return storetaskmaster;
        }

        /// <summary>
        /// Add Store Task Comment
        /// </summary>
        /// <param name="TaskComment"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int AddStoreTaskComment (StoreTaskComment TaskComment, int TenantID, int UserID)
        {
            int taskId = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddStoreTaskComment", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TaskID", TaskComment.TaskID);
                cmd.Parameters.AddWithValue("@_Comment", TaskComment.Comment);
                cmd.Parameters.AddWithValue("@_CommentBy", UserID);
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);

                
                taskId = Convert.ToInt32(cmd.ExecuteNonQuery());

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
            return taskId;
        }

        /// <summary>
        /// Get Comment On Task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<TaskCommentModel> GetCommentOnTask(int TaskID, int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            List<TaskCommentModel> TaskCommentList = new List<TaskCommentModel>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetCommentOnTask", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TaskID", TaskID);
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        TaskCommentList = ds.Tables[0].AsEnumerable().Select(r => new TaskCommentModel()
                        {
                            TaskCommentID = Convert.ToInt32(r.Field<object>("TaskCommentID")),
                            TaskID = Convert.ToInt32(r.Field<object>("TaskID")),
                            Comment = r.Field<object>("Comment") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("Comment")),
                            CommentedDate = r.Field<object>("CommentedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CommentedDate")),
                            CommentByName = r.Field<object>("CommentByName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CommentByName")),
                            CommentedDiff = r.Field<object>("CommentedDiff") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CommentedDiff"))

                        }).ToList();
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
            return TaskCommentList;
        }

        /// <summary>
        /// Get Task History
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<CustomTaskHistory> GetTaskHistory(int TaskID, int TenantID, int UserID)
        {

            DataSet ds = new DataSet();
            List<CustomTaskHistory> ListTaskHistory = new List<CustomTaskHistory>();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SP_GetHistoryOfTask", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TaskID", TaskID);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomTaskHistory TaskHistory = new CustomTaskHistory
                        {
                            Name = ds.Tables[0].Rows[i]["Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Name"]),
                            Action = ds.Tables[0].Rows[i]["Action"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Action"]),
                            DateandTime = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"])
                        };
                        ListTaskHistory.Add(TaskHistory);
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
            return ListTaskHistory;
        }
        
        /// <summary>
        /// Update Task Status
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <param name="taskMaster"></param>
        /// <returns></returns>
        public int SubmitTask(StoreTaskMaster taskMaster, int UserID, int TenantId)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateTaskStatus", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Task_ID", taskMaster.TaskID);
                cmd.Parameters.AddWithValue("@Department_Id", taskMaster.DepartmentId);
                cmd.Parameters.AddWithValue("@Function_ID", taskMaster.FunctionID);
                cmd.Parameters.AddWithValue("@Priority_ID", taskMaster.PriorityID);
                cmd.Parameters.AddWithValue("@TaskStatus_ID", taskMaster.TaskStatusId);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantId);
                
                i = cmd.ExecuteNonQuery();
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

        /// <summary>
        /// GetUserList
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public List<CustomStoreUserList> GetUserList(int TenantID, int TaskID, int TaskFor)
        {
            DataSet ds = new DataSet();
            List<CustomStoreUserList> listUser = new List<CustomStoreUserList>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Task_ID", TaskID);
                cmd.Parameters.AddWithValue("@_TaskFor", TaskFor);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomStoreUserList customUserList = new CustomStoreUserList
                        {
                            User_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]),
                            UserName = ds.Tables[0].Rows[i]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UserName"])
                        };
                        listUser.Add(customUserList);
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
            return listUser;
        }

        /// <summary>
        /// AssignTask
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        public int AssignTask(string TaskID, int TenantID, int UserID, int AgentID)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_StoreTaskAssign", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@_TaskID", TaskID);
                cmd.Parameters.AddWithValue("@AssignTo_ID", AgentID);
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd.Parameters.AddWithValue("@Created_By", UserID);
                
                cmd.CommandType = CommandType.StoredProcedure;
                i = cmd.ExecuteNonQuery();
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

        /// <summary>
        /// Get Store Task By Ticket
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<CustomStoreTaskDetails> GetStoreTaskByTicket(int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            List<CustomStoreTaskDetails> lsttask = new List<CustomStoreTaskDetails>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreTaskByTicket", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@User_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomStoreTaskDetails taskMaster = new CustomStoreTaskDetails
                        {
                            StoreTaskID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]),
                            TicketID = Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]),
                            TaskStatus = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TaskStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"])),
                            TaskTitle = ds.Tables[0].Rows[i]["TaskTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]),
                            TaskDescription = ds.Tables[0].Rows[i]["TaskDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TaskDescription"]),
                            DepartmentName = ds.Tables[0].Rows[i]["Departmentname"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Departmentname"]),
                            StoreName = ds.Tables[0].Rows[i]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]),
                            StoreAddress = ds.Tables[0].Rows[i]["StoreAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreAddress"]),
                            CreatedBy = ds.Tables[0].Rows[i]["Createdby"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Createdby"]),
                            UpdatedBy = ds.Tables[0].Rows[i]["ModifiedBy"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedBy"]),
                            CreationOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            Assignto = ds.Tables[0].Rows[i]["Assignto"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignto"]),
                            PriorityName = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]),
                            FunctionName = ds.Tables[0].Rows[i]["FuncationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FuncationName"]),
                            Createdago = ds.Tables[0].Rows[i]["Createdago"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Createdago"]),
                            Assignedago = ds.Tables[0].Rows[i]["Assignedago"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignedago"]),
                            Updatedago = ds.Tables[0].Rows[i]["ModifiedDate"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDate"]),
                            TaskCloureDate = ds.Tables[0].Rows[i]["ClosureTaskDate"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ClosureTaskDate"]),
                            ResolutionTimeRemaining = ds.Tables[0].Rows[i]["RemainingTime"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["RemainingTime"]),
                            ResolutionOverdueBy = ds.Tables[0].Rows[i]["taskoverDue"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["taskoverDue"]),
                            ColorName = ds.Tables[0].Rows[i]["ColorName"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]),
                            ColorCode = ds.Tables[0].Rows[i]["ColorCode"] == System.DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ColorCode"])
                        };
                        lsttask.Add(taskMaster);
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
            return lsttask;
        }

        /// <summary>
        /// Get Store Ticketing TaskByTaskID
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public StoreTaskWithTicket GetStoreTicketingTaskByTaskID(int TaskID, int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            StoreTaskWithTicket StoreTaskWithTicketDetials = new StoreTaskWithTicket();
            StoreTaskMaster storetaskmaster = new StoreTaskMaster();
            TaskTicketDetails ticketDetails = new TaskTicketDetails();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreTicketingTaskByTaskID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_TaskID", TaskID);
                cmd.Parameters.AddWithValue("@_UserID", UserID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            storetaskmaster = new StoreTaskMaster()
                            {
                                TaskID = Convert.ToInt32(ds.Tables[0].Rows[0]["TicketingTaskID"]),
                                TicketID = Convert.ToInt32(ds.Tables[0].Rows[0]["TicketID"]),
                                TaskTitle = ds.Tables[0].Rows[0]["TaskTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["TaskTitle"]),
                                TaskDescription = ds.Tables[0].Rows[0]["TaskDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["TaskDescription"]),
                                DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[0]["DepartmentId"]),
                                FunctionID = Convert.ToInt32(ds.Tables[0].Rows[0]["FunctionID"]),
                                PriorityID = Convert.ToInt32(ds.Tables[0].Rows[0]["PriorityID"]),
                                TaskStatusId = Convert.ToInt32(ds.Tables[0].Rows[0]["TaskStatusId"]),
                                TaskStatusName = ds.Tables[0].Rows[0]["TaskStatusId"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TaskStatus)Convert.ToInt32(ds.Tables[0].Rows[0]["TaskStatusId"])),
                                AssignToName = ds.Tables[0].Rows[0]["AssignToName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["AssignToName"]),
                                CreatedByName = ds.Tables[0].Rows[0]["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CreatedByName"]),
                                StoreName = ds.Tables[0].Rows[0]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]),
                                Address = ds.Tables[0].Rows[0]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Address"]),
                                StoreCode = ds.Tables[0].Rows[0]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StoreCode"]),
                                CanEdit = Convert.ToInt32(ds.Tables[0].Rows[0]["CanEdit"]),
                                CanSubmit = Convert.ToInt32(ds.Tables[0].Rows[0]["CanSubmit"]),
                                IsAssignTo = Convert.ToInt32(ds.Tables[0].Rows[0]["IsAssignTo"]),
                            };
                        }
                    }
                }
                
                if (ds != null && ds.Tables[1] != null)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        ticketDetails = new TaskTicketDetails
                        {
                            TicketID = ds.Tables[1].Rows[0]["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["TicketID"]),
                            TicketTitle = ds.Tables[1].Rows[0]["TikcketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["TikcketTitle"]),
                            Ticketdescription = ds.Tables[1].Rows[0]["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["TicketDescription"]),
                            CustomerName = ds.Tables[1].Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["CustomerName"]),
                            Gender = ds.Tables[1].Rows[0]["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["Gender"]),
                            CustomerPhoneNumber = ds.Tables[1].Rows[0]["CustomerPhoneNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["CustomerPhoneNumber"]),
                            CustomerEmailId = ds.Tables[1].Rows[0]["CustomerEmailId"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["CustomerEmailId"]),
                            Status = ds.Tables[1].Rows[0]["StatusID"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(ds.Tables[1].Rows[0]["StatusID"])),
                            PriortyName = ds.Tables[1].Rows[0]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["PriortyName"]),
                            CategoryName = ds.Tables[1].Rows[0]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["CategoryName"]),
                            SubCategoryName = ds.Tables[1].Rows[0]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["SubCategoryName"]),
                            IssueTypeName = ds.Tables[1].Rows[0]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[0]["IssueTypeName"]),
                        };

                    }
                }
                StoreTaskWithTicketDetials.StoreTaskMasterDetails = storetaskmaster;
                StoreTaskWithTicketDetials.TaskTicketDetails = ticketDetails;
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
            return StoreTaskWithTicketDetials;
        }

        /// <summary>
        /// Get Assigned To 
        /// </summary>
        /// <param name="Function_ID"></param>
        /// <returns></returns>
        public List<CustomUserAssigned> GetAssignedTo(int Function_ID)
        {
            DataSet ds = new DataSet();
            List<CustomUserAssigned> Assignedto = new List<CustomUserAssigned>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreAssignedToByFunctionID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Function_ID", Function_ID);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomUserAssigned assigned = new CustomUserAssigned
                        {
                            UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]),
                            UserName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"])
                        };
                        Assignedto.Add(assigned);
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
            return Assignedto;
        }

        /// <summary>
        /// Get Store Task ProcressBar
        /// </summary>
        /// <param name="TaskId"></param>
        /// <param name="TaskBy"></param>
        /// <returns></returns>
        public List<StoreTaskProcressBar> GetStoreTaskProcressBar(int TaskId, int TaskBy)
        {
            DataSet ds = new DataSet();
            List<StoreTaskProcressBar> objresult = new List<StoreTaskProcressBar>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_StoreTaskProcressBar", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@_TaskId", TaskId);
                cmd.Parameters.AddWithValue("@_TaskBy", TaskBy);

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        StoreTaskProcressBar result = new StoreTaskProcressBar
                        {
                            Progress = Convert.ToInt32(ds.Tables[0].Rows[0]["Progress"]),
                            ProgressIn = ds.Tables[0].Rows[0]["ProgressIn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ProgressIn"]),
                            RemainingTime = ds.Tables[0].Rows[0]["RemainingTime"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["RemainingTime"]),
                            ClosureTaskDate = ds.Tables[0].Rows[0]["ClosureTaskDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ClosureTaskDate"]),
                            ColorName = ds.Tables[0].Rows[0]["ColorName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ColorName"]),
                            ColorCode = ds.Tables[0].Rows[0]["ColorCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["ColorCode"])
                        };
                        objresult.Add(result);
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
            return objresult;
        }

        /// <summary>
        /// Submit Task By Ticket
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <param name="UserID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int SubmitTaskByTicket(StoreTaskMaster taskMaster, int UserID, int TenantId)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateTaskByTicketStatus", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Task_ID", taskMaster.TaskID);
                cmd.Parameters.AddWithValue("@Department_Id", taskMaster.DepartmentId);
                cmd.Parameters.AddWithValue("@Function_ID", taskMaster.FunctionID);
                cmd.Parameters.AddWithValue("@Priority_ID", taskMaster.PriorityID);
                cmd.Parameters.AddWithValue("@TaskStatus_ID", taskMaster.TaskStatusId);
                cmd.Parameters.AddWithValue("@User_ID", UserID);

                i = cmd.ExecuteNonQuery();
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

        /// <summary>
        /// Assign Task By Ticket
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        public int AssignTaskByTicket(string TaskID, int TenantID, int UserID, int AgentID)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_TaskByTicketAssign", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@Task_ID", TaskID);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@AssignedUser_ID", AgentID);
                cmd.CommandType = CommandType.StoredProcedure;
                i = cmd.ExecuteNonQuery();
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

        #region Campaign

        /// <summary>
        /// Get Store Campaign Customer
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<StoreCampaign> GetStoreCampaignCustomer(int TenantID, int UserID)
        {
            DataSet ds = new DataSet();
            List<StoreCampaign> objList = new List<StoreCampaign>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreCampaignCustomer", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);


                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);


                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreCampaign obj = new StoreCampaign
                        {
                            CampaignTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["CampaignTypeID"]),
                            CampaignName = ds.Tables[0].Rows[i]["CampaignName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignName"]),
                            CampaignScript = ds.Tables[0].Rows[i]["CampaignScript"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignScript"]),
                            CampaignScriptLess = ds.Tables[0].Rows[i]["CampaignScript"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignScript"]).Length < 15 ? Convert.ToString(ds.Tables[0].Rows[i]["CampaignScript"]) : Convert.ToString(ds.Tables[0].Rows[i]["CampaignScript"]).Substring(0, 15),
                            ContactCount = Convert.ToInt32(ds.Tables[0].Rows[i]["ContactCount"]),
                            CampaignEndDate = ds.Tables[0].Rows[i]["CampaignEndDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CampaignEndDate"]),
                            StoreCampaignCustomerList = new List<StoreCampaignCustomer>()
                        };

                        obj.StoreCampaignCustomerList = ds.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("CampaignTypeID")).
                        Equals(obj.CampaignTypeID)).Select(x => new StoreCampaignCustomer()
                        {
                            CampaignCustomerID = Convert.ToInt32(x.Field<int>("CampaignCustomerID")),
                            CustomerID = Convert.ToInt32(x.Field<int>("CustomerID")),
                            CampaignTypeDate = x.Field<object>("CampaignTypeDate") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("CampaignTypeDate")),
                            CampaignTypeID = Convert.ToInt32(x.Field<int>("CampaignTypeID")),
                            CampaignStatus = x.Field<object>("CampaignStatus") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("CampaignStatus")),
                            Response = x.Field<object>("Response") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("Response")),
                            CallReScheduledTo = x.Field<object>("CallReScheduledTo") == DBNull.Value ? string.Empty : ConvertDatetimeToString(Convert.ToString(x.Field<object>("CallReScheduledTo"))),
                            CustomerName = x.Field<object>("CustomerName") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("CustomerName")),
                            CustomerPhoneNumber = x.Field<object>("CustomerPhoneNumber") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("CustomerPhoneNumber")),
                            CustomerEmailId = x.Field<object>("CustomerEmailId") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("CustomerEmailId")),
                            DOB = x.Field<object>("DOB") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("DOB")),
                            NoOfTimesNotContacted = x.Field<object>("NoOfTimesNotContacted") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("NoOfTimesNotContacted")),
                            CampaignResponseList = ds.Tables[2].AsEnumerable().Select(r => new CampaignResponse()
                            {
                                ResponseID = Convert.ToInt32(r.Field<object>("ResponseID")),
                                Response = r.Field<object>("Response") == DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("Response")),
                                StatusNameID = Convert.ToInt32(r.Field<object>("Status"))

                            }).ToList()

                    }).ToList();

                        objList.Add(obj);
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
            return objList;
        }

        public string ConvertDatetimeToString(string DateInString)
        {
            string result = "";
            string GMT = " GMT+05:30 (" + TimeZoneInfo.Local.StandardName + ")";
            try
            {
                if(!String.IsNullOrEmpty(DateInString))
                {
                    result = DateInString + GMT;
                }
               
            }
            catch(Exception)
            {

            }

            return result;
        }

        /// <summary>
        /// Get Campaign Status Response
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CampaignStatusResponse GetCampaignStatusResponse(int TenantID, int UserID)
        {

            DataSet ds = new DataSet();
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SP_GetCampaignStatusResponse", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    List<CampaignStatus> objStatusList = new List<CampaignStatus>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CampaignStatus objStatus = new CampaignStatus
                        {
                            StatusID = Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"]),
                            StatusName = ds.Tables[0].Rows[i]["StatusName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StatusName"]),
                            StatusNameID = Convert.ToInt32(ds.Tables[0].Rows[i]["StatusNameID"])
                        };
                        objStatusList.Add(objStatus);
                    }
                    obj.CampaignStatusList = objStatusList;
                }

                if (ds != null && ds.Tables[1] != null)
                {
                    List<CampaignResponse> objResponseList = new List<CampaignResponse>();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        CampaignResponse objResponse = new CampaignResponse
                        {
                            ResponseID = Convert.ToInt32(ds.Tables[1].Rows[i]["ResponseID"]),
                            Response = ds.Tables[1].Rows[i]["Response"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["Response"]),
                            StatusNameID = Convert.ToInt32(ds.Tables[1].Rows[i]["Status"])
                        };
                        objResponseList.Add(objResponse);
                    }
                    obj.CampaignResponseList = objResponseList;
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
            return obj;
        }

        /// <summary>
        /// Update Campaign Status Response
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int UpdateCampaignStatusResponse(StoreCampaignCustomerRequest objRequest, int TenantID, int UserID)
        {

            int result = 0;
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SP_UpdateStoreCampaignCustomer", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_CampaignCustomerID", objRequest.CampaignCustomerID);
                cmd.Parameters.AddWithValue("@_StatusNameID", objRequest.StatusNameID);
                cmd.Parameters.AddWithValue("@_ResponseID", objRequest.ResponseID);

                if (!string.IsNullOrEmpty(objRequest.CallReScheduledTo))
                {
                    objRequest.CallReScheduledToDate = Convert.ToDateTime(objRequest.CallReScheduledTo);
                }

                cmd.Parameters.AddWithValue("@_CallReScheduledTo", objRequest.CallReScheduledToDate);

                result = Convert.ToInt32(cmd.ExecuteNonQuery());
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
            return result;
        }

        /// <summary>
        /// Close Campaign
        /// </summary>
        /// <param name="CampaignTypeID"></param>
        /// <param name="IsClosed"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int CloseCampaign(int CampaignTypeID, int IsClosed, int TenantID, int UserID)
        {

            int result = 0;
            CampaignStatusResponse obj = new CampaignStatusResponse();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SP_CloseCampaign", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_CampaignTypeID", CampaignTypeID);
                cmd.Parameters.AddWithValue("@_IsClosed", IsClosed);

                result = Convert.ToInt32(cmd.ExecuteNonQuery());
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
            return result;
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
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                if (PriorityArr != null && PriorityArr.Length > 0)
                    Array.Clear(PriorityArr, 0, PriorityArr.Length);
            }
            return timespan;

        }
        #endregion

    }
}
