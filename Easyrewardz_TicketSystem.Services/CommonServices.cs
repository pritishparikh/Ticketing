using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public  class CommonServices  : IDisposable
    {
        private readonly ETSContext _etsContext;
        MySqlConnection conn = new MySqlConnection();
        
        //public CommonServices(ETSContext context, IConfiguration configuration)
        //{
        //    _etsContext = context;
        //     conn.ConnectionString = "Data Source = 13.67.69.216; port = 3306; Initial Catalog = Ticketing; User Id = brainvire; password = Logitech@123";

        //}
        public string Authenticateuser(string programCode, string email, string password)
        {
            //tenant id chekwith  
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("prc_authenticateUser", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@programCode", programCode);
                cmd1.Parameters.AddWithValue("@email", email);
                cmd1.Parameters.AddWithValue("@password", password);
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (db != null)
                //{
                //    db.Dispose();
                //    db = null;
                //}
            }
        }

    }
    
    
}
