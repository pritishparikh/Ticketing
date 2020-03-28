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
    public class TaskServices : ITask
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public TaskServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion
        /// <summary>
        /// Add Task Detail
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
                MySqlCommand cmd1 = new MySqlCommand("SP_createTask", conn);
                cmd1.Connection = conn;
                cmd1.Parameters.AddWithValue("@Ticket_ID", taskMaster.TicketID);
                cmd1.Parameters.AddWithValue("@TaskTitle", taskMaster.TaskTitle);
                cmd1.Parameters.AddWithValue("@TaskDescription", taskMaster.TaskDescription);
                cmd1.Parameters.AddWithValue("@DepartmentId", taskMaster.DepartmentId);
                cmd1.Parameters.AddWithValue("@FunctionID", taskMaster.FunctionID);
                cmd1.Parameters.AddWithValue("@AssignTo_ID", taskMaster.AssignToID);
                cmd1.Parameters.AddWithValue("@PriorityID", taskMaster.PriorityID);
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
        /// GetTask By ID
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public CustomTaskMasterDetails GetTaskbyId(int taskID)
        {
            DataSet ds = new DataSet();
            CustomTaskMasterDetails taskMaster = new CustomTaskMasterDetails();
            
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetTaskById", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@task_ID", taskID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {                      
                        taskMaster.TicketingTaskID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        taskMaster.TaskStatus = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TaskStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"]));
                        taskMaster.TaskTitle = ds.Tables[0].Rows[i]["TaskTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]);
                        taskMaster.TaskDescription = ds.Tables[0].Rows[i]["TaskDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TaskDescription"]);           
                        taskMaster.Duedate = Convert.ToDateTime(ds.Tables[0].Rows[i]["TaskEndTime"]);
                        taskMaster.AssignName = ds.Tables[0].Rows[i]["AssignName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AssignName"]);
                        taskMaster.DateFormat = taskMaster.Duedate.ToString("dd/MMM/yyyy");
                        int MasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        taskMaster.Comments = ds.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("TicketingTaskID")).
                        Equals(MasterId)).Select(x => new UserComment()
                        {
                            Name = x.Field<object>("Name") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("Name")),
                            Comment= x.Field<object>("TaskComment") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("TaskComment")),
                            datetime= x.Field<object>("CommentAt") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("CommentAt"))
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return taskMaster;
        }
        /// <summary>
        /// Get Task List
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        public List<CustomTaskMasterDetails> GetTaskList(int TicketId)
        {

            DataSet ds = new DataSet();
            List<CustomTaskMasterDetails> lsttask = new List<CustomTaskMasterDetails>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetTaskList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Ticket_ID", TicketId);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomTaskMasterDetails taskMaster = new CustomTaskMasterDetails();
                        taskMaster.TicketingTaskID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        taskMaster.TaskStatus = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TaskStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"]));
                        taskMaster.TaskTitle = ds.Tables[0].Rows[i]["TaskTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]);
                        taskMaster.DepartmentName = ds.Tables[0].Rows[i]["Departmentname"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Departmentname"]);
                        taskMaster.StoreCode = Convert.ToInt32(ds.Tables[0].Rows[i]["Storecode"]);
                        taskMaster.CreatedBy = ds.Tables[0].Rows[i]["Createdby"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Createdby"]);
                        taskMaster.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreationOn"]);
                        taskMaster.DateFormat = taskMaster.CreatedDate.ToString("dd/MMM/yyyy");
                        taskMaster.AssignName = ds.Tables[0].Rows[i]["AssignName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AssignName"]);
                        lsttask.Add(taskMaster);
                    }
                }
            }
            catch(Exception)

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
        ///  Delete Task 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public int DeleteTask(int taskId)
        {
            MySqlCommand cmd = new MySqlCommand();
            int i = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_DeleteTask", conn);
                cmd1.Parameters.AddWithValue("@task_ID", taskId);
                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteScalar());
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
                MySqlCommand cmd = new MySqlCommand("SP_GetAssignedTo", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Function_ID", Function_ID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomUserAssigned assigned = new CustomUserAssigned();
                        assigned.UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        assigned.UserName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
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
        /// Add Comment 
        /// </summary>
        /// <param name="Id"></param> 1 for task comment, 2 for claim comment and 3 for ticket notes comment
        /// <param name="Comment"></param> 
        /// <param name="UserID"></param> 
        /// <returns></returns>
        public int AddComment(int CommentForId, int ID, string Comment, int UserID)
        {

            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SP_AddComment", conn);
                cmd1.Connection = conn;
                cmd1.Parameters.AddWithValue("@CommentForId", CommentForId);
                cmd1.Parameters.AddWithValue("@ID", ID);
                cmd1.Parameters.AddWithValue("@Comments", Comment);
                cmd1.Parameters.AddWithValue("@User_ID", UserID);
                cmd1.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd1.ExecuteNonQuery());

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

            return success;

        }

        /// <summary>
        /// Get list of claims
        /// <param name="TicketId"></param>
        /// </summary>
        public List<CustomClaimMaster> GetClaimList(int TicketId)
        {
            DataSet ds = new DataSet();
            List<CustomClaimMaster> lsttask = new List<CustomClaimMaster>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetClaimList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Ticket_ID", TicketId);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomClaimMaster taskMaster = new CustomClaimMaster();
                        taskMaster.TicketClaimID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        taskMaster.TaskStatus = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.ClaimStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"]));
                        taskMaster.ClaimIssueType = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        taskMaster.Category = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        taskMaster.Creation_on = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]);
                        taskMaster.Dateformat = taskMaster.Creation_on.ToString("dd/MMM/yyyy");
                        taskMaster.RaisedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]);
                        taskMaster.AssignName = ds.Tables[0].Rows[i]["AssignName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AssignName"]);         
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
        /// Get list of the task comment for the task
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        public List<UserComment> GetTaskComment(int TaskId)
        {
            DataSet ds = new DataSet();
            List<UserComment> lstClaimComment = new List<UserComment>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetTaskCommentByTaskId", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Task_ID", TaskId);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        UserComment userComment = new UserComment();
                        userComment.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                        userComment.Comment = Convert.ToString(ds.Tables[0].Rows[i]["Comment"]);
                        userComment.datetime = Convert.ToString(ds.Tables[0].Rows[i]["datetime"]);
                        lstClaimComment.Add(userComment);
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
            return lstClaimComment;
        }

        /// <summary>
        /// Add Comment ON task
        /// <param name="TaskMaster"></param>
        /// </summary>
        /// <returns></returns>
        public int AddCommentOnTask(TaskMaster taskMaster)
        {
            int i = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SP_AddCommentOnTask", conn);
                cmd1.Connection = conn;
                cmd1.Parameters.AddWithValue("@Task_title", taskMaster.TaskTitle);
                cmd1.Parameters.AddWithValue("@Department_ID", taskMaster.DepartmentId);
                cmd1.Parameters.AddWithValue("@Function_ID", taskMaster.FunctionID);
                cmd1.Parameters.AddWithValue("@Task_Description", taskMaster.TaskDescription);
                cmd1.Parameters.AddWithValue("@Task_Comments", taskMaster.TaskComments);
                cmd1.Parameters.AddWithValue("@CreatedBy", taskMaster.CreatedBy);

                cmd1.CommandType = CommandType.StoredProcedure;
                i = Convert.ToInt32(cmd1.ExecuteNonQuery());

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
    }
}
