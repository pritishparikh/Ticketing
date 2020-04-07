using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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

        public List<CustomStoreTaskDetails> GetTaskList(int tabFor, int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            List<CustomStoreTaskDetails> lsttask = new List<CustomStoreTaskDetails>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreTaskList", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@tab_For", tabFor);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@user_ID", userID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomStoreTaskDetails taskMaster = new CustomStoreTaskDetails();
                        taskMaster.StoreTaskID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        taskMaster.TaskStatus = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TaskStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"]));
                        taskMaster.TaskTitle = ds.Tables[0].Rows[i]["TaskTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]);
                        taskMaster.TaskDescription = ds.Tables[0].Rows[i]["TaskDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TaskDescription"]);
                        taskMaster.DepartmentName = ds.Tables[0].Rows[i]["Departmentname"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Departmentname"]);
                        taskMaster.StoreName = ds.Tables[0].Rows[i]["StoreName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);
                        taskMaster.CreatedBy = ds.Tables[0].Rows[i]["Createdby"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Createdby"]);
                        taskMaster.CreationOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]);
                        taskMaster.Assignto = ds.Tables[0].Rows[i]["Assignto"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignto"]);
                        taskMaster.PriorityName = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]);
                        taskMaster.FunctionName = ds.Tables[0].Rows[i]["FuncationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FuncationName"]);
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
    }
}
