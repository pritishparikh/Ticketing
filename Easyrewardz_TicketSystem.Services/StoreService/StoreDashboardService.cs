using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreDashboardService : IStoreDashboard
    {

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
                cmd1.Parameters.AddWithValue("@objcreatedOn", model.CreatedOn);
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
                        StoreDashboardResponseModel storedashboard = new StoreDashboardResponseModel();
                        storedashboard.taskid = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);

                        storedashboard.taskstatus = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);

                        storedashboard.tasktitle = Convert.ToString(ds.Tables[0].Rows[i]["TaskTitle"]);

                        storedashboard.Department = Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]);

                        storedashboard.storeName = Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);

                        storedashboard.StoreAddress = Convert.ToString(ds.Tables[0].Rows[i]["StoreAddress"]);

                        storedashboard.Priority = Convert.ToString(ds.Tables[0].Rows[i]["Priorty"]);

                        storedashboard.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]);


                        storedashboard.AssigntoId = Convert.ToString(ds.Tables[0].Rows[i]["Assignto"]);

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
    }
}
