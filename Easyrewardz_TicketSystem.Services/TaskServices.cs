using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// Add Customer Detail
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        public int AddTaskDetails(TaskMaster taskMaster)
        {

            // MySqlCommand cmd = new MySqlCommand();
            int taskId = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SP_createTask", conn);
                cmd1.Connection = conn;
                cmd1.Parameters.AddWithValue("@TicketID", taskMaster.TicketID);
                cmd1.Parameters.AddWithValue("@TaskTitle", taskMaster.TaskTitle);
                cmd1.Parameters.AddWithValue("@TaskDescription", taskMaster.TaskDescription);
                cmd1.Parameters.AddWithValue("@DepartmentId", taskMaster.DepartmentId);
                cmd1.Parameters.AddWithValue("@FunctionID", taskMaster.FunctionID);
                cmd1.Parameters.AddWithValue("@AssignToID", taskMaster.AssignToID);
                cmd1.Parameters.AddWithValue("@PriorityID", taskMaster.PriorityID);
                cmd1.CommandType = CommandType.StoredProcedure;
                taskId = Convert.ToInt32(cmd1.ExecuteScalar());
                conn.Close();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
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
        /// <param name="customerMaster"></param>
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
                        taskMaster.TaskStatus = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                        taskMaster.TaskTitle = Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]);
                        taskMaster.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]);
                        taskMaster.StoreCode = Convert.ToInt32(ds.Tables[0].Rows[i]["Storecode"]);
                        taskMaster.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        taskMaster.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]);
                        taskMaster.AssignName = Convert.ToString(ds.Tables[0].Rows[i]["AssignName"]);
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return taskMaster;
        }
        /// <summary>
        /// Get Task List
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        public List<CustomTaskMasterDetails> GetTaskList()
        {

            DataSet ds = new DataSet();
            List<CustomTaskMasterDetails> lsttask = new List<CustomTaskMasterDetails>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetTaskList", conn);
                cmd.Connection = conn;
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
                        taskMaster.TaskStatus = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                        taskMaster.TaskTitle = Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]);
                        taskMaster.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["Departmentname"]);
                        taskMaster.StoreCode = Convert.ToInt32(ds.Tables[0].Rows[i]["Storecode"]);
                        taskMaster.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["Createdby"]);
                        taskMaster.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreationOn"]);
                        taskMaster.AssignName = Convert.ToString(ds.Tables[0].Rows[i]["AssignName"]);
                        lsttask.Add(taskMaster);
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return lsttask;
        }
        /// <summary>
        /// Soft Delete Task 
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
                i = cmd1.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

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
