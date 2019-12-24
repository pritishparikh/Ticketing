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

        //public IEnumerable<TaskMaster> GetTaskMasters()
        //{
        //    //List<TaskMaster> lsttask = new List<Employee>();

        //    //using (SqlConnection con = new SqlConnection(connectionString))
        //    //{
        //    //    SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
        //    //    cmd.CommandType = CommandType.StoredProcedure;

        //    //    con.Open();
        //    //    SqlDataReader rdr = cmd.ExecuteReader();

        //    //    while (rdr.Read())
        //    //    {
        //    //        Employee employee = new Employee();

        //    //        employee.ID = Convert.ToInt32(rdr["EmployeeID"]);
        //    //        employee.Name = rdr["Name"].ToString();
        //    //        employee.Gender = rdr["Gender"].ToString();
        //    //        employee.Department = rdr["Department"].ToString();
        //    //        employee.City = rdr["City"].ToString();

        //    //        lstemployee.Add(employee);
        //    //    }
        //    //    con.Close();
        //    //}
        //    //return lstemployee;
        //}
    }
}
