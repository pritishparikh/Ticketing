﻿using Easyrewardz_TicketSystem.Model;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Easyrewardz_TicketSystem.DBContext
{
    public class ETSContext /*: DbContext*/
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DBContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        //public ETSContext(DbContextOptions<ETSContext> options)
        //    : base(options)
        //{
        //}

        //#region Model
        //public virtual DbSet<TicketingDetails> DB_Ticketing { get; set; }
        //#endregion


        MySqlConnection conn = new MySqlConnection();
        public ETSContext()
        {
            conn.ConnectionString = "server=192.168.11.19;userid=ticketing;password=Frv810FM#bBgI88;database=ticketing;";
        }

        private MySqlHelper _connection;
        //userDetails _user = new userDetails();
        public string SaveRecord(string ProgramCode, string Domainname, string applicationid, string sessionid, string userId, string password, string newToken)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                //MySqlCommand cmd1 = new MySqlCommand("sp_insertER_CurrentSession", conn);
                MySqlCommand cmd1 = new MySqlCommand("prc_insertCurrentSession", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@UserName", userId);
                cmd1.Parameters.AddWithValue("@SecurityToken", newToken);
                cmd1.Parameters.AddWithValue("@SessionID", sessionid);
                cmd1.Parameters.AddWithValue("@ProgramCode", ProgramCode);
                cmd1.Parameters.AddWithValue("@Password", password);
                cmd1.ExecuteNonQuery();
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

            return "";
        }

        public DataSet validateSecurityToken(string SecretToken,int ModuleID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("prc_validateToken", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@SecurityToken", SecretToken);
                cmd1.Parameters.AddWithValue("@ModuleID", ModuleID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null)
                {
                  var setoken =  ds.Tables["a"].Rows[0]["a"];
                  var Module  =  ds.Tables["a"].Rows[0]["a"];
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
            return ds;
        }

        //public bool updatePassword(string EmailId,string Password)
        //{
        //    bool isUpdated = false;

        //    DataSet ds = new DataSet();
        //    MySqlCommand cmd = new MySqlCommand();
        //    try
        //    {
        //        conn.Open();
        //        cmd.Connection = conn;
        //        MySqlCommand cmd1 = new MySqlCommand("SP_UpdatePassword", conn);
        //        cmd1.CommandType = CommandType.StoredProcedure;
        //        cmd1.Parameters.AddWithValue("@EmailId", EmailId);
        //        cmd1.Parameters.AddWithValue("@Password", Password);
        //        cmd1.ExecuteScalar();
        //        isUpdated = true;
        //    }
        //    catch (MySql.Data.MySqlClient.MySqlException ex)
        //    {
        //    }
        //    finally
        //    {
        //        if (conn != null)
        //        {
        //            conn.Close();
        //        }
        //    }
        //    return isUpdated;
        //}
    }
    public class UserDetails
    {
        public int IndexID { get; set; }
        public string UserName { get; set; }
        public string SecurityToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean Isactive { get; set; }
        public string SessionID { get; set; }
        public string ProgramCode { get; set; }
        public string Password { get; set; }
    }
}
