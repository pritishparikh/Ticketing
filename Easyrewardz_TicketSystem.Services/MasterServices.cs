using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public class MasterServices : IMasterInterface
    {
        #region
        MySqlConnection conn = new MySqlConnection();
        public MasterServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion


        /// <summary>
        /// Get Channel Of Purchase List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<ChannelOfPurchase> GetChannelOfPurchaseList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<ChannelOfPurchase> objChannel = new List<ChannelOfPurchase>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetChannelOfPurchaseList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ChannelOfPurchase channel = new ChannelOfPurchase();
                        channel.ChannelOfPurchaseID = Convert.ToInt32(ds.Tables[0].Rows[i]["ChannelOfPurchaseID"]);
                        channel.TenantID = Convert.ToInt32(ds.Tables[0].Rows[i]["TenantID"]);
                        channel.NameOfChannel = Convert.ToString(ds.Tables[0].Rows[i]["NameOfChannel"]);
                        channel.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        //brand.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["dd"]);

                        objChannel.Add(channel);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objChannel;
        }

        /// <summary>
        /// Get DepartMent List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<DepartmentMaster> GetDepartmentList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<DepartmentMaster> departmentMasters = new List<DepartmentMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetDepartmentList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DepartmentMaster department = new DepartmentMaster();
                        department.DepartmentID = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentID"]);
                        department.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]);
                        department.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        departmentMasters.Add(department);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return departmentMasters;
        }
        /// <summary>
        /// Get Function By DepartmentID
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <returns></returns>
        public List<FuncationMaster> GetFunctionByDepartment(int DepartmentID,int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<FuncationMaster> funcationMasters = new List<FuncationMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getFunctionByDepartmentId", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Department_ID", DepartmentID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FuncationMaster function = new FuncationMaster();
                        function.FunctionID = Convert.ToInt32(ds.Tables[0].Rows[i]["FunctionID"]);
                        function.FuncationName = Convert.ToString(ds.Tables[0].Rows[i]["FuncationName"]);
                        funcationMasters.Add(function);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return funcationMasters;
        }

        /// <summary>
        /// Get Payment List
        /// </summary>
        /// <returns></returns>
        public List<PaymentMode> GetPaymentMode()
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<PaymentMode> paymentModes = new List<PaymentMode>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getPaymentModeMaster", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        PaymentMode paymentMode = new PaymentMode();
                        paymentMode.PaymentModeID = Convert.ToInt32(ds.Tables[0].Rows[i]["PaymentModeID"]);
                        paymentMode.PaymentModename = Convert.ToString(ds.Tables[0].Rows[i]["PaymentModename"]);
                        paymentModes.Add(paymentMode);
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

            return paymentModes;
        }


    }
}
