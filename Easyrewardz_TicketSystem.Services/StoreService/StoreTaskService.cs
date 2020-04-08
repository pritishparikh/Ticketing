using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
                MySqlCommand cmd1 = new MySqlCommand("SP_createStoreTask", conn);
                cmd1.Connection = conn;
                cmd1.Parameters.AddWithValue("@Task_Title", taskMaster.TaskTitle);
                cmd1.Parameters.AddWithValue("@Task_Description", taskMaster.TaskDescription);
                cmd1.Parameters.AddWithValue("@Department_Id", taskMaster.DepartmentId);
                cmd1.Parameters.AddWithValue("@Function_ID", taskMaster.FunctionID);
                cmd1.Parameters.AddWithValue("@AssignTo_ID", taskMaster.AssignToID);
                cmd1.Parameters.AddWithValue("@Priority_ID", taskMaster.PriorityID);
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                cmd1.Parameters.AddWithValue("@Created_By", UserID);
                cmd1.CommandType = CommandType.StoredProcedure;
                taskId = Convert.ToInt32(cmd1.ExecuteNonQuery());

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
                
                MySqlDataAdapter da 
= new MySqlDataAdapter
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
                            CreatedBy = ds.Tables[0].Rows[i]["Createdby"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Createdby"]),
                            CreationOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            Assignto = ds.Tables[0].Rows[i]["Assignto"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignto"]),
                            PriorityName = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]),
                            FunctionName = ds.Tables[0].Rows[i]["FuncationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FuncationName"])
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
            StoreTaskMaster storetaskmaster = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetTaskByTaskID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@_TenantID", TenantID);
                cmd.Parameters.AddWithValue("@_TaskID", TaskID);

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
                                AssignToName = ds.Tables[0].Rows[0]["AssignToName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["AssignToName"]),
                                CreatedByName = ds.Tables[0].Rows[0]["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["CreatedByName"]),
                                StoreName = ds.Tables[0].Rows[0]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]),
                                Address = ds.Tables[0].Rows[0]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["Address"]),
                                StoreCode = ds.Tables[0].Rows[0]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["StoreCode"])
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
    }
}
