using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using Easyrewardz_TicketSystem.Interface;
namespace Easyrewardz_TicketSystem.Services
{
    public class TaskServices:ITask
    {
        #region Cunstructor
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



        public TaskMaster GetTaskbyId(int taskID)
        {
           // MySqlCommand cmd  = new MySqlCommand();
            TaskMaster taskMaster = new TaskMaster();
            try        
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_GetTaskbyId", conn);
                cmd.Connection = conn;       
                cmd.Parameters.AddWithValue("@taskID", taskID);
                cmd.CommandType = CommandType.StoredProcedure;
                //conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    DepartmentMaster department = new DepartmentMaster();

                    department.DepartmentName = Convert.ToString(rdr["Department"]);
                    taskMaster.departments = department;
                    StatusMaster statusMaster = new StatusMaster();
                    statusMaster.TaskStatusName = Convert.ToString(rdr["Status"]);
                    taskMaster.statusMasters = statusMaster;
                    
                    taskMaster.TicketingTaskID = Convert.ToInt32(rdr["ID"].ToString());
                    taskMaster.TaskTitle = Convert.ToString(rdr["TaskTitle"].ToString());
                    //taskMaster.CreatedBy = Convert.ToInt32(rdr["CreatedBy"]);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
            //finally
            //{
            //    if (conn != null)
            //    {
            //        conn.Close();
            //    }
            //}
            return taskMaster;                
        }

        public List<TaskMaster> GetTaskList()
        {

            DataSet ds = new DataSet();
            List<TaskMaster> lsttask = new List<TaskMaster>();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("sp_GetTaskList", conn);
                //cmd1.Parameters.AddWithValue("@ID", taskID);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TaskMaster taskMaster = new TaskMaster();
                        DepartmentMaster department = new DepartmentMaster();

                        department.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["Department"]);
                        taskMaster.departments = department;
                        StatusMaster statusMaster = new StatusMaster();
                        statusMaster.TaskStatusName = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                        taskMaster.statusMasters = statusMaster;
                        taskMaster.TicketingTaskID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        taskMaster.TaskTitle = Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]);
                        
                        //taskMaster.departments.Departmentname = ds.Tables[0].Rows[i]["Department"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Department"]);
                        //TaskMaster.CreatedBy = Convert.ToString(ds.Tables[0].Rows[i]["CustomerPhoneNumber"]);
                        //TaskMaster.CreatedDate = Convert.ToString(ds.Tables[0].Rows[i]["CustomerEmailId"]);
                        //TaskMaster.AssignToID = Convert.ToInt32(ds.Tables[0].Rows[i]["GenderID"]);                     
                        lsttask.Add(taskMaster);
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
            return lsttask;
        }
    }
}
